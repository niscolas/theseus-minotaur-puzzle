using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TheseusAndTheMinotaur.Puzzle.Simple;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    public class MapMB : MonoBehaviour, IMap, IMapHumbleObject
    {
        [SerializeField]
        private IntReference _width;

        [SerializeField]
        private IntReference _height;

        [Required, SerializeField]
        private PuzzleLevelSO _puzzleLevelAsset;

        [Required, SerializeField]
        private TileFactoryMB _tileFactory;

        public int Width => _width.Value;
        public int Height => _height.Value;

        public ITileFactory TileFactory => _tileFactory;
        public ITile[,] Tiles { get; set; }

        private MapController _controller;

        private void Awake()
        {
            _controller = new MapController(this);
        }

        private void Start()
        {
            if (!_puzzleLevelAsset)
            {
                return;
            }

            _height.Value = _puzzleLevelAsset.Rows.Length;
            _width.Value = _puzzleLevelAsset.Rows[0].Tiles.Length;

            Create();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    SetupTile(i, j);
                }
            }
        }

        public void Create()
        {
            _controller.Create();
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

        private void SetupTile(int i, int j)
        {
            ITile tile = Tiles[i, j];
            PuzzleLevelTileData tileData = _puzzleLevelAsset.Rows[i].Tiles[j];

            if (!tileData.Exists)
            {
                tile.Disable();
                return;
            }

            List<Direction> obstacleDirections = new List<Direction>();

            if (tileData.HasLeftObstacle)
            {
                obstacleDirections.Add(Direction.Left);
            }

            if (tileData.HasRightObstacle)
            {
                obstacleDirections.Add(Direction.Right);
            }

            if (tileData.HasUpObstacle)
            {
                obstacleDirections.Add(Direction.Up);
            }

            if (tileData.HasDownObstacle)
            {
                obstacleDirections.Add(Direction.Down);
            }

            foreach (Direction obstacleDirection in obstacleDirections)
            {
                tile.ActivateObstacle(obstacleDirection);
            }
        }
    }
}