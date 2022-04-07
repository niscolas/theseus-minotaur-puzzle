using System;
using UnityEngine;

namespace TheseusAndTheMinotaur.Puzzle.Simple
{
    [Serializable]
    internal class PuzzleLevelRowData
    {
        [SerializeField]
        private PuzzleLevelTileData[] _tiles;

        public PuzzleLevelTileData[] Tiles
        {
            get => _tiles;
            set => _tiles = value;
        }
    }
}