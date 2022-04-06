using System;
using NaughtyAttributes;
using UnityEngine;

namespace TheseusAndTheMinotaur.Puzzle.Simple
{
    [Serializable]
    internal class PuzzleLevelTileData
    {
        [SerializeField]
        private bool _exists = true;

        [ShowIf(nameof(_exists))]
        [SerializeField]
        private bool _isTheseusInitialTile;

        [SerializeField]
        private bool _isMinotaurInitialTile;

        [ShowIf(nameof(_exists))]
        [SerializeField]
        private bool _hasLeftObstacle;

        [ShowIf(nameof(_exists))]
        [SerializeField]
        private bool _hasRightObstacle;

        [ShowIf(nameof(_exists))]
        [SerializeField]
        private bool _hasUpObstacle;

        [ShowIf(nameof(_exists))]
        [SerializeField]
        private bool _hasDownObstacle;

        public bool Exists => _exists;

        public bool IsTheseusInitialTile => _isTheseusInitialTile;

        public bool IsMinotaurInitialTile => _isMinotaurInitialTile;

        public bool HasLeftObstacle => _hasLeftObstacle;

        public bool HasRightObstacle => _hasRightObstacle;

        public bool HasUpObstacle => _hasUpObstacle;

        public bool HasDownObstacle => _hasDownObstacle;
    }
}