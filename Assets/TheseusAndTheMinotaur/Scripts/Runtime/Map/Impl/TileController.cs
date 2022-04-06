using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    internal class TileController : ITile
    {
        public Vector3 Position => _humbleObject.Position;

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

        public bool IsDisabled => _humbleObject.IsDisabled;

        public IMap ParentMap
        {
            get => _humbleObject.Humble_ParentMap;
            set => _humbleObject.Humble_ParentMap = value;
        }

        public IEnumerable<IGameEntity> PlacedEntities => _humbleObject.PlacedEntities;

        public bool IsLevelEnd => _humbleObject.IsLevelEnd;

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

        public void Disable() { }

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

        public void ActivateObstacle(Direction direction)
        {
            bool isGivenObstacleActive = CheckIsObstacleOfDirectionActive(direction);
            if (!isGivenObstacleActive)
            {
                GetObstacleForDirection(direction).Enable();
            }
        }

        public bool CheckIsObstacleOfDirectionActive(Direction direction)
        {
            bool result = GetObstacleForDirection(direction).IsActive;
            return result;
        }

        public void SetIsLevelEnd(bool value)
        {
            _humbleObject.Humble_IsLevelEnd = value;
        }

        private IObstacle GetObstacleForDirection(Direction direction)
        {
            return Obstacles[direction];
        }
    }
}