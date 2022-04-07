using NaughtyAttributes;
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
        [ResizableTextArea, SerializeField]
        private string _devComment;

        [SerializeField]
        private Vector2Int _size;

        [Button]
        private void Fill()
        {
            _data.Rows = new PuzzleLevelRowData[_size.x];
            for (int i = 0; i < _data.Rows.Length; i++)
            {
                _data.Rows[i] = new PuzzleLevelRowData
                {
                    Tiles = new PuzzleLevelTileData[_size.y]
                };
            }
        }

        [SerializeField]
        private PuzzleLevelData _data;

        public string DevComment => _devComment;

        public PuzzleLevelData Data => _data;
    }
}