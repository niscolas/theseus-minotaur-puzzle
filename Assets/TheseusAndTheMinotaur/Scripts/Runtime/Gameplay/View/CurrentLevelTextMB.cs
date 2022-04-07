using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Gameplay
{
    public class CurrentLevelTextMB : MonoBehaviour
    {
        [SerializeField]
        private IntReference _currentLevel;

        [SerializeField]
        private IntReference _levelCount;

        [SerializeField]
        private TMP_Text _text;

        private void Start()
        {
            _text.SetText($"[{_currentLevel.Value + 1} / {_levelCount.Value}]");
        }
    }
}