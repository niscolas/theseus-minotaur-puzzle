using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TheseusAndTheMinotaur.Puzzle.Simple;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    internal class MapMB : MonoBehaviour, IMap, IMapHumbleObject
    {
        [SerializeField]
        private IntReference _width;

        [SerializeField]
        private IntReference _height;

        [Required, SerializeField]
        private PuzzleLevelAssetsDatabaseMB _levelAssetsDatabase;

        [Required, SerializeField]
        private TileFactoryMB _tileFactory;

        public int Width => _width.Value;
        public int Height => _height.Value;

        public ITileFactory TileFactory => _tileFactory;
        public ITile[,] Tiles { get; set; }
        public PuzzleLevelData PuzzleLevelData => _levelAssetsDatabase.GetCurrent().Data;

        private MapController _controller;

        private void Awake()
        {
            _controller = new MapController(this);

            if (!_levelAssetsDatabase.GetCurrent())
            {
                return;
            }

            _height.Value = PuzzleLevelData.Rows.Length;
            _width.Value = PuzzleLevelData.Rows[0].Tiles.Length;

            Create();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    SetupTile(i, j);
                }
            }
        }

        public ITile GetTheseusInitialTile()
        {
            return _controller.GetTheseusInitialTile();
        }

        public ITile GetMinotaurInitialTile()
        {
            return _controller.GetMinotaurInitialTile();
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
            PuzzleLevelTileData tileData = PuzzleLevelData.Rows[i].Tiles[j];

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

            if (tileData.IsLevelEnd)
            {
                tile.SetIsLevelEnd(true);
            }

            foreach (Direction obstacleDirection in obstacleDirections)
            {
                ActivateOppositeObstacle(tile, obstacleDirection);
                tile.ActivateObstacle(obstacleDirection);
            }
        }

        private void ActivateOppositeObstacle(ITile tile, Direction obstacleDirection)
        {
            if (TryGetNeighbourTile(tile, obstacleDirection, out ITile neighbourTile))
            {
                neighbourTile.ActivateObstacle(GetOppositeDirection(obstacleDirection));
            }
        }

        private Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Direction.Right;
                    break;
                case Direction.Right:
                    return Direction.Left;
                    break;
                case Direction.Up:
                    return Direction.Down;
                    break;
                case Direction.Down:
                    return Direction.Up;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}