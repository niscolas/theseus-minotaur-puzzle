using System;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Gameplay
{
    public class LevelCommentMB : MonoBehaviour
    {
        [SerializeField]
        private StringReference _levelComment;

        [SerializeField]
        private TMP_Text _text;

        private void Start()
        {
            _text.SetText(_levelComment.Value);
        }
    }
}