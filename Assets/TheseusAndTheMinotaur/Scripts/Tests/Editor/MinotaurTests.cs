using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TheseusAndTheMinotaur.Map;
using TheseusAndTheMinotaur.Minotaur;
using TheseusAndTheMinotaur.Movement;
using TheseusAndTheMinotaur.Theseus;
using Zenject;

namespace TheseusAndTheMinotaur.Tests.Editor
{
    [TestFixture]
    public class MinotaurTests : ZenjectUnitTestFixture
    {
        [Inject]
        private ITheseusBehavior _theseusBehavior;

        [Inject]
        private IMinotaurBehavior _minotaurBehavior;

        [Inject]
        private ITileBasedMovementHumbleObject _minotaurMovementHumbleSub;

        [Inject]
        private IMap _map;

        [SetUp]
        public void CommonInstall()
        {
            ITheseus theseus = SetupTheseus();
            SetupMinotaur(theseus);
            SetupMap();

            Container.Inject(this);
        }

        private ITheseus SetupTheseus()
        {
            ITheseus theseus = Substitute.For<ITheseus>();

            ITileBasedMovementHumbleObject theseusMovementHumbleSub =
                Substitute.For<ITileBasedMovementHumbleObject>();
            theseusMovementHumbleSub.Entity.Returns(theseus);

            ITileBasedMovement theseusMovementController =
                new BasicTileBasedMovementController(theseusMovementHumbleSub);

            ITheseusBehaviorHumbleObject theseusBehaviorHumbleSub =
                Substitute.For<ITheseusBehaviorHumbleObject>();
            theseusBehaviorHumbleSub.Movement.Returns(theseusMovementController);
            theseusBehaviorHumbleSub.Entity.Returns(theseus);

            ITheseusBehavior theseusBehavior =
                new TheseusBehaviorController(theseusBehaviorHumbleSub);

            Container.Bind<ITheseusBehavior>().FromInstance(theseusBehavior).AsSingle();
            return theseus;
        }

        private void SetupMinotaur(ITheseus theseus)
        {
            IMinotaur minotaur = Substitute.For<IMinotaur>();

            ITileBasedMovementHumbleObject minotaurMovementHumbleSub =
                Substitute.For<ITileBasedMovementHumbleObject>();
            minotaurMovementHumbleSub.Entity.Returns(minotaur);

            ITileBasedMovement minotaurMovementController =
                new BasicTileBasedMovementController(minotaurMovementHumbleSub);

            IMinotaurBehaviorHumbleObject minotaurBehaviorHumbleSub =
                Substitute.For<IMinotaurBehaviorHumbleObject>();
            minotaurBehaviorHumbleSub.Movement.Returns(minotaurMovementController);
            minotaurBehaviorHumbleSub.Minotaur.Returns(minotaur);
            minotaurBehaviorHumbleSub.Theseus.Returns(theseus);

            IMinotaurBehavior minotaurBehavior =
                new MinotaurBehaviorController(minotaurBehaviorHumbleSub);

            Container.Bind<IMinotaurBehavior>().FromInstance(minotaurBehavior).AsSingle();
            Container
                .Bind<ITileBasedMovementHumbleObject>()
                .FromInstance(minotaurMovementHumbleSub)
                .AsSingle();
        }

        private void SetupMap()
        {
            IMap map = new DefaultMap(10, 10);
            Container.Bind<IMap>().FromInstance(map).AsSingle();
        }

        [TestCase(0, 0, 2, 2)]
        [TestCase(0, 0, 1, 2)]
        [TestCase(3, 4, 0, 2)]
        public void HorizontalChases_TheseusTwoTilesAwayHorizontally_ShouldEndOnSameColumnAsTheseus(
            int initialMinotaurX,
            int initialMinotaurY,
            int initialTheseusX,
            int initialTheseusY)
        {
            SetMinotaurInitialTile(initialMinotaurX, initialMinotaurY);
            SetTheseusInitialTile(initialTheseusX, initialTheseusY);
            SetMinotaurMoveCountLeft(2);

            _minotaurBehavior.ChaseTheseus();
            _minotaurBehavior.ChaseTheseus();

            int minotaurColumn = _minotaurBehavior.Minotaur.CurrentTile.Y;
            int theseusColumn = _theseusBehavior.Entity.CurrentTile.Y;

            minotaurColumn.Should().Be(theseusColumn);
        }

        [TestCase(0, 0, 1, 1)]
        [TestCase(0, 0, 2, 0)]
        [TestCase(0, 0, 0, 2)]
        [TestCase(0, 0, 0, 0)]
        public void Catch_TheseusIsReachableWithTwoMoves_ShouldBeSameSlotAsTheseus(
            int initialMinotaurX,
            int initialMinotaurY,
            int initialTheseusX,
            int initialTheseusY)
        {
            SetMinotaurInitialTile(initialMinotaurX, initialMinotaurY);
            SetTheseusInitialTile(initialTheseusX, initialTheseusY);
            SetMinotaurMoveCountLeft(2);

            _minotaurBehavior.ChaseTheseus();
            _minotaurBehavior.ChaseTheseus();

            ITile minotaurTile = _minotaurBehavior.Minotaur.CurrentTile;
            ITile theseusTile = _theseusBehavior.Entity.CurrentTile;
            minotaurTile.Should().Be(theseusTile);
        }

        private void SetMinotaurMoveCountLeft(int value)
        {
            _minotaurMovementHumbleSub.Humble_MaxMoveCountLeft = value;
            _minotaurMovementHumbleSub.Humble_CurrentMoveCountLeft = value;
        }

        private void SetMinotaurInitialTile(int x, int y)
        {
            ITile tile = _map.GetTile(x, y);
            _minotaurBehavior.Minotaur.CurrentTile.Returns(tile);
            tile.AddEntity(_minotaurBehavior.Minotaur);
        }

        private void SetTheseusInitialTile(int x, int y)
        {
            ITile tile = _map.GetTile(x, y);
            _theseusBehavior.Entity.CurrentTile.Returns(tile);
            tile.AddEntity(_theseusBehavior.Entity);
        }
    }
}