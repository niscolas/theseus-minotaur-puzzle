using System.Collections.Generic;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    internal class TileMB : MonoBehaviour, ITile, ITileHumbleObject
    {
        [SerializeField]
        private int _x;

        [SerializeField]
        private int _y;

        public int X => Humble_X;
        public int Y => Humble_X;

        public IMap ParentMap => Humble_ParentMap;

        public IList<IGameEntity> PlacedEntitiesList { get; } = new List<IGameEntity>();
        public IEnumerable<IGameEntity> PlacedEntities => PlacedEntitiesList;

        public int Humble_X
        {
            get => _x;
            set => _x = value;
        }

        public int Humble_Y
        {
            get => _y;
            set => _y = value;
        }

        public IMap Humble_ParentMap { get; set; }

        public IDictionary<Direction, IObstacle> Obstacles { get; } = new Dictionary<Direction, IObstacle>();

        private TileController _controller;

        private void Awake()
        {
            _controller = new TileController(this);
        }

        public void Setup(int x, int y, IMap parentMap)
        {
            _controller.Setup(x, y, parentMap);
        }

        public void AddEntity(IGameEntity entity)
        {
            _controller.AddEntity(entity);
        }

        public void UnlinkEntity(IGameEntity entity)
        {
            _controller.UnlinkEntity(entity);
        }

        public void AddObstacle(IObstacle obstacle, Direction direction)
        {
            _controller.AddObstacle(obstacle, direction);
        }

        public bool CheckIsDirectionFree(Direction direction)
        {
            return _controller.CheckIsDirectionFree(direction);
        }
    }
}