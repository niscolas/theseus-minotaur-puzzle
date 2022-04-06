using System;
using TheseusAndTheMinotaur.Puzzle.Simple;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    [Serializable]
    internal class PuzzleLevelData
    {
        [SerializeField]
        private PuzzleLevelRowData[] _rows;

        public PuzzleLevelRowData[] Rows => _rows;
    }
}