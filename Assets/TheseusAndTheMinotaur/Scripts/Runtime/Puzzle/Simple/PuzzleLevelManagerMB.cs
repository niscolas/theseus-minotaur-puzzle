using UnityEngine;

namespace TheseusAndTheMinotaur.Puzzle.Simple
{
    internal class PuzzleLevelManagerMB : MonoBehaviour
    {
        [SerializeField]
        private PuzzleLevelSO _levelAsset;

        private ITileFactory _tileFactory;

        private void Awake()
        {
            CreateLevel();
        }

        private void CreateLevel() { }
    }
}