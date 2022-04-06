namespace TheseusAndTheMinotaur.Movement
{
    internal class BasicTileBasedMovement : ITileBasedMovement
    {
        public IGameEntity Entity => _humbleObject.Entity;

        private readonly ITileBasedMovementHumbleObject _humbleObject;

        public BasicTileBasedMovement(ITileBasedMovementHumbleObject humbleObject)
        {
            _humbleObject = humbleObject;
        }

        public void Move(Direction direction)
        {
            bool canMoveToDirection = CheckCanMoveToDirection(direction);
            if (!canMoveToDirection)
            {
                return;
            }

            ITile currentTile = Entity.CurrentTile;
            if (!currentTile.ParentMap.TryGetNeighbourTile(
                    currentTile, direction, out ITile neighbourTile))
            {
                return;
            }

            neighbourTile.LinkEntity(Entity);
        }

        private bool CheckCanMoveToDirection(Direction direction)
        {
            bool result = Entity.CurrentTile.CheckIsDirectionFree(direction);
            return result;
        }
    }
}