using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    internal class TileMB : MonoBehaviour, ITile, ITileHumbleObject
    {
        [SerializeField]
        private int _x;

        [SerializeField]
        private int _y;

        [SerializeField]
        private bool _isDisabled = false;

        [Required, SerializeField]
        private GameObject _isLevelEndIndicator;

        [Required, SerializeField]
        private ObstacleMB _leftObstacle;

        [Required, SerializeField]
        private ObstacleMB _rightObstacle;

        [Required, SerializeField]
        private ObstacleMB _upObstacle;

        [Required, SerializeField]
        private ObstacleMB _downObstacle;

        public Vector3 Position => _cachedTransform.position;
        public int X => Humble_X;
        public int Y => Humble_Y;
        public bool IsDisabled => _isDisabled;

        public IMap ParentMap => Humble_ParentMap;

        public IList<IGameEntity> PlacedEntitiesList { get; } = new List<IGameEntity>();
        public IEnumerable<IGameEntity> PlacedEntities => PlacedEntitiesList;

        public bool IsLevelEnd => Humble_IsLevelEnd;

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
        public bool Humble_IsLevelEnd { get; set; }

        public IDictionary<Direction, IObstacle> Obstacles { get; } = new Dictionary<Direction, IObstacle>();

        private TileController _controller;
        private Transform _cachedTransform;

        private void Awake()
        {
            _cachedTransform = transform;
            _controller = new TileController(this);
            Obstacles[Direction.Left] = _leftObstacle;
            Obstacles[Direction.Right] = _rightObstacle;
            Obstacles[Direction.Down] = _downObstacle;
            Obstacles[Direction.Up] = _upObstacle;
        }

        public void Setup(int x, int y, IMap parentMap)
        {
            _controller.Setup(x, y, parentMap);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            _controller.Disable();
        }

        public void AddEntity(IGameEntity entity)
        {
            _controller.AddEntity(entity);
        }

        public void UnlinkEntity(IGameEntity entity)
        {
            _controller.UnlinkEntity(entity);
        }

        public void ActivateObstacle(Direction direction)
        {
            _controller.ActivateObstacle(direction);
        }

        public bool CheckIsObstacleOfDirectionActive(Direction direction)
        {
            return _controller.CheckIsObstacleOfDirectionActive(direction);
        }

        public void SetIsLevelEnd(bool value)
        {
            _controller.SetIsLevelEnd(value);
            _isLevelEndIndicator.SetActive(value);
        }
    }
}