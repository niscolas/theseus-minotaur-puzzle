using System;
using UnityEngine;

namespace TheseusAndTheMinotaur.Movement
{
    public class BasicTileBasedMovementMB : MonoBehaviour, ITileBasedMovement, ITileBasedMovementHumbleObject
    {
        [field: SerializeField]
        public int Humble_MaxMoveCountLeft { get; set; }

        [field: SerializeField]
        public int Humble_CurrentMoveCountLeft { get; set; }

        public event Action GotExhausted;

        public int MaxMoveCount => Humble_MaxMoveCountLeft;
        public int CurrentMoveCountLeft => Humble_CurrentMoveCountLeft;

        public IGameEntity Entity { get; private set; }

        private BasicTileBasedMovementController _controller;

        private void Awake()
        {
            _controller = new BasicTileBasedMovementController(this);
            _controller.GotExhausted += Controller_OnExhausted;
            Entity = GetComponent<IGameEntity>();
        }

        public void ResetMoveCount()
        {
            _controller.ResetMoveCount();
        }

        public MoveResult Move(Direction direction)
        {
            return _controller.Move(direction);
        }

        public bool CheckCanMoveToDirection(Direction direction)
        {
            return _controller.CheckCanMoveToDirection(direction);
        }

        private void Controller_OnExhausted()
        {
            GotExhausted?.Invoke();
        }
    }
}