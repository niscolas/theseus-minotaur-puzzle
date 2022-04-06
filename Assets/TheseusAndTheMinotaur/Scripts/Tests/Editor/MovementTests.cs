using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TheseusAndTheMinotaur.Map;
using TheseusAndTheMinotaur.Movement;

namespace TheseusAndTheMinotaur.Tests.Editor
{
    [TestFixture]
    public class MovementTests
    {
        private IMap _map;

        private ITileBasedMovement _movement;

        private ITileBasedMovementHumbleObject _movementHumbleObjectSub;

        private IGameEntity _movementEntitySub;

        [SetUp]
        public void CommonInstall()
        {
            _movementEntitySub = Substitute.For<IGameEntity>();

            _movementHumbleObjectSub =
                Substitute.For<ITileBasedMovementHumbleObject>();
            _movementHumbleObjectSub.Entity.Returns(_movementEntitySub);

            _movement = new BasicTileBasedMovementController(_movementHumbleObjectSub);

            IMapHumbleObject mapHumbleSub = Substitute.For<IMapHumbleObject>();
            mapHumbleSub.TileFactory.Returns(new TileFactory());
            mapHumbleSub.Height.Returns(4);
            mapHumbleSub.Width.Returns(4);

            _map = new MapController(mapHumbleSub);
        }

        [TestCase(1, 1, 1, 2, Direction.Right)]
        [TestCase(1, 1, 1, 0, Direction.Left)]
        [TestCase(1, 1, 0, 1, Direction.Up)]
        [TestCase(1, 1, 2, 1, Direction.Down)]
        public void SimpleMovements_ShouldBeOnTargetSlot(
            int initialX, int initialY,
            int targetX, int targetY,
            Direction direction)
        {
            ITile initialTile = _map.GetTile(initialX, initialY);
            ITile targetTile = _map.GetTile(targetX, targetY);
            SetCurrentMoveCountLeft(1);
            SetInitialMovementEntityTile(initialTile);

            _movement.Move(direction);

            _movementEntitySub.CurrentTile.Should().Be(targetTile);
            initialTile.PlacedEntities.Should().BeEmpty();
            targetTile.PlacedEntities.Should().Contain(_movementEntitySub);
        }

        [TestCase(0, 0, Direction.Left)]
        [TestCase(0, 0, Direction.Up)]
        [TestCase(3, 3, Direction.Right)]
        [TestCase(3, 3, Direction.Down)]
        public void MovementsToOutOfBounds_MoveResultShouldBeFailure(
            int initialX, int initialY, Direction moveDirection)
        {
            ITile initialTile = _map.GetTile(initialX, initialY);
            SetCurrentMoveCountLeft(1);
            SetInitialMovementEntityTile(initialTile);

            MoveResult moveResult = _movement.Move(moveDirection);
            moveResult.Type.Should().Be(MoveResultType.Failure);
        }

        [Test]
        public void Move2Times_WithOnly1MovementCountLeft_MoveResultShouldBeExhausted()
        {
            ITile initialTile = _map.GetTile(0, 0);
            SetCurrentMoveCountLeft(1);
            SetInitialMovementEntityTile(initialTile);

            _movement.Move(Direction.Right);
            MoveResult moveResult = _movement.Move(Direction.Right);

            moveResult.Type.Should().Be(MoveResultType.Exhausted);
        }

        private void SetCurrentMoveCountLeft(int value)
        {
            _movementHumbleObjectSub.Humble_MaxMoveCountLeft.Returns(value);
            _movementHumbleObjectSub.Humble_CurrentMoveCountLeft.Returns(value);
        }

        private void SetInitialMovementEntityTile(ITile tile)
        {
            _movementEntitySub.CurrentTile.Returns(tile);
            tile.AddEntity(_movementEntitySub);
        }
    }
}