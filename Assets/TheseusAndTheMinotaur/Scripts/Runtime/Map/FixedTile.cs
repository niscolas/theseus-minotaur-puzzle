using System.Collections.Generic;
using System.Linq;

namespace TheseusAndTheMinotaur.Map
{
    internal class FixedTile : ITile
    {
        public int X { get; }
        public int Y { get; }
        public IMap ParentMap { get; }

        public IEnumerable<IGameEntity> PlacedEntities => PlacedEntitiesList;

        private readonly IDictionary<Direction, IObstacle> Obstacles =
            new Dictionary<Direction, IObstacle>();

        private readonly IList<IGameEntity> PlacedEntitiesList = new List<IGameEntity>();

        public FixedTile(int x, int y, IMap parentMap)
        {
            X = x;
            Y = y;
            ParentMap = parentMap;
        }

        public void AddEntity(IGameEntity entity)
        {
            if (PlacedEntitiesList.Contains(entity))
            {
                return;
            }

            PlacedEntitiesList.Add(entity);
        }

        public void UnlinkEntity(IGameEntity entity)
        {
            if (!PlacedEntities.Contains(entity))
            {
                return;
            }

            PlacedEntitiesList.Remove(entity);
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