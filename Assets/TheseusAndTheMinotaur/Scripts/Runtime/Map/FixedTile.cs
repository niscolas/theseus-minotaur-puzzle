using System.Collections.Generic;

namespace TheseusAndTheMinotaur.Map
{
    internal class FixedTile : ITile
    {
        public int X { get; }
        public int Y { get; }
        public IMap ParentMap { get; }

        public IGameEntity Entity { get; private set; }

        public IDictionary<Direction, IObstacle> Obstacles =
            new Dictionary<Direction, IObstacle>();

        public FixedTile(int x, int y, IMap parentMap)
        {
            X = x;
            Y = y;
            ParentMap = parentMap;
        }

        public void LinkEntity(IGameEntity entity)
        {
            Entity = entity;
        }

        public void UnlinkCurrentEntity()
        {
            Entity = default;
        }

        public void AddObstacle(IObstacle obstacle, Direction direction)
        {
            bool hasObstacleInDirection = !CheckIsDirectionFree(direction);

            if (!hasObstacleInDirection)
            {
                Obstacles.Add(direction, obstacle);
            }
        }

        public bool CheckIsDirectionFree(Direction direction)
        {
            bool result = !Obstacles.ContainsKey(direction);
            return result;
        }
    }
}