using System.Collections.Generic;
using System.Linq;

namespace TheseusAndTheMinotaur.Map
{
    internal class TileController : ITile
    {
        public int X
        {
            get => _humbleObject.Humble_X;
            set => _humbleObject.Humble_X = value;
        }

        public int Y
        {
            get => _humbleObject.Humble_Y;
            set => _humbleObject.Humble_Y = value;
        }

        public IMap ParentMap
        {
            get => _humbleObject.Humble_ParentMap;
            set => _humbleObject.Humble_ParentMap = value;
        }

        public IEnumerable<IGameEntity> PlacedEntities => _humbleObject.PlacedEntities;

        private IDictionary<Direction, IObstacle> Obstacles => _humbleObject.Obstacles;
        private IList<IGameEntity> PlacedEntitiesList => _humbleObject.PlacedEntitiesList;

        private readonly ITileHumbleObject _humbleObject;

        public TileController(ITileHumbleObject humbleObject)
        {
            _humbleObject = humbleObject;
        }

        public void Setup(int x, int y, IMap parentMap)
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