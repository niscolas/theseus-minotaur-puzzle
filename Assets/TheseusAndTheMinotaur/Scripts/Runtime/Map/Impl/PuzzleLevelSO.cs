using TheseusAndTheMinotaur.Map;
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
        private PuzzleLevelData _data;

        public PuzzleLevelData Data => _data;
    }
}