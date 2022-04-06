using TheseusAndTheMinotaur.Puzzle.Simple;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    internal class PuzzleLevelAssetsDatabaseMB : MonoBehaviour, IPuzzleLevelAssetDatabase
    {
        [SerializeField]
        private IntReference _currentLevelIndex;

        [SerializeField]
        private PuzzleLevelSO[] _levels;

        public PuzzleLevelSO GetCurrent()
        {
            int levelIndex = Mathf.Clamp(_currentLevelIndex.Value, 0, _levels.Length);
            PuzzleLevelSO puzzleLevelAsset = _levels[levelIndex];
            return puzzleLevelAsset;
        }

        public bool CheckHasNextLevel()
        {
            bool result = _currentLevelIndex.Value < _levels.Length - 1;
            return result;
        }

        public bool CheckHasPreviousLevel()
        {
            bool result = _currentLevelIndex.Value >= 1;
            return result;
        }
    }
}