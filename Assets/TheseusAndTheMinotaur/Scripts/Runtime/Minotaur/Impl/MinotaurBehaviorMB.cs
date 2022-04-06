using System;
using TheseusAndTheMinotaur.Common;
using UnityEngine;

namespace TheseusAndTheMinotaur.Minotaur
{
    public class MinotaurBehaviorMB : MonoBehaviour, IMinotaurBehavior, IMinotaurBehaviorHumbleObject
    {
        public event Action TurnEnded;
        public event Action ArrivedAtTheseus;
        public IMinotaur Minotaur { get; private set; }
        public ITheseus Theseus { get; private set; }
        public ITileBasedMovement Movement { get; private set; }

        private MinotaurBehaviorController _controller;
        private IMovementView _movementView;

        private Vector3 _targetPosition;
        private bool _reachedTargetPosition;
        private bool _isControllerTurnActive;
        private bool _isTurnActive;

        private void Awake()
        {
            Minotaur = GetComponent<IMinotaur>();
            Movement = GetComponent<ITileBasedMovement>();
            TryGetComponent(out _movementView);

            _controller = new MinotaurBehaviorController(this);
            _controller.TurnEnded += Controller_OnTurnEnded;
            _controller.Init();
        }

        public void Setup(ITheseus theseus)
        {
            Theseus = theseus;
        }

        private void Update()
        {
            if (!_isTurnActive)
            {
                return;
            }

            if (_reachedTargetPosition)
            {
                OnReachedTargetPosition();
                return;
            }

            _movementView.UpdatePosition(_targetPosition, Services.TimeService.DeltaTime);
            _reachedTargetPosition = _movementView.CheckReachedDestination(_targetPosition);
        }

        private void OnDestroy()
        {
            _controller.TurnEnded -= Controller_OnTurnEnded;
            _controller.Dispose();
        }

        public void StartTurn()
        {
            _controller.StartTurn();
            ChaseTheseus();
            _isControllerTurnActive = true;
            _isTurnActive = true;
        }

        public MoveResult? ChaseTheseus()
        {
            MoveResult? result = _controller.ChaseTheseus();

            if (result != null)
            {
                if (result.Value.Type == MoveResultType.Success)
                {
                    _targetPosition = result.Value.NewTile.Position;
                    _reachedTargetPosition = false;
                }
            }

            return result;
        }

        private void Controller_OnTurnEnded()
        {
            _isControllerTurnActive = false;
        }

        private void OnTurnEnded()
        {
            TurnEnded?.Invoke();
        }

        private void OnReachedTargetPosition()
        {
            if (Minotaur.CurrentTile == Theseus.CurrentTile)
            {
                OnArrivedAtTheseus();
                return;
            }

            if (!_isControllerTurnActive)
            {
                _isTurnActive = false;
                OnTurnEnded();
                return;
            }

            ChaseTheseus();
        }

        private void OnArrivedAtTheseus()
        {
            ArrivedAtTheseus?.Invoke();
        }
    }
}