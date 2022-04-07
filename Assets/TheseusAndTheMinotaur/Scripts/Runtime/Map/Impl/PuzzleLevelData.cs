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

        public PuzzleLevelRowData[] Rows
        {
            get => _rows;
            set => _rows = value;
        }
    }
}