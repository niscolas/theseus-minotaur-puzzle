using NaughtyAttributes;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    public class MapMB : MonoBehaviour, IMap, IMapHumbleObject
    {
        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;

        [Required, SerializeField]
        private TileFactoryMB _tileFactory;

        public int Width => _width;
        public int Height => _height;

        public ITileFactory TileFactory => _tileFactory;
        public ITile[,] Tiles { get; set; }

        private MapController _controller;

        private void Awake()
        {
            _controller = new MapController(this);
        }

        public bool CheckIsValidTile(int x, int y)
        {
            return _controller.CheckIsValidTile(x, y);
        }

        public bool CheckHasValidNeighbourTile(ITile tile, Direction direction)
        {
            return _controller.CheckHasValidNeighbourTile(tile, direction);
        }

        public ITile GetNeighbourTile(ITile tile, Direction direction)
        {
            return _controller.GetNeighbourTile(tile, direction);
        }

        public bool TryGetNeighbourTile(ITile tile, Direction direction, out ITile neighbourTile)
        {
            return _controller.TryGetNeighbourTile(tile, direction, out neighbourTile);
        }

        public ITile GetTile(int x, int y)
        {
            return _controller.GetTile(x, y);
        }

        public bool TryGetTile(int x, int y, out ITile tile)
        {
            return _controller.TryGetTile(x, y, out tile);
        }
    }
}