using System;
using UnityEngine;

namespace TheseusAndTheMinotaur.Puzzle.Simple
{
    [Serializable]
    internal class PuzzleLevelRowData
    {
        [SerializeField]
        private PuzzleLevelTileData[] _tiles;

        public PuzzleLevelTileData[] Tiles => _tiles;
    }
}