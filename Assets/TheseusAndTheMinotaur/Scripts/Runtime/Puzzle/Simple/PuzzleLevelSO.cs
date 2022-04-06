using UnityEngine;

namespace TheseusAndTheMinotaur.Puzzle.Simple
{
    [CreateAssetMenu(
        fileName = "PuzzleLevel",
        menuName = Constants.CreateAssetMenuPrefix + "Puzzle Level",
        order = Constants.CreateAssetMenuOrder)]
    internal class PuzzleLevelSO : ScriptableObject
    {
        [SerializeField]
        private PuzzleLevelRowData[] _rows;

        public PuzzleLevelRowData[] Rows => _rows;
    }
}