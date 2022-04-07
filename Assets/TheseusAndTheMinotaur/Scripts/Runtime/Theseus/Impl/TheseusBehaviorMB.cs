using System;
using TheseusAndTheMinotaur.Common;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Theseus
{
    internal class TheseusBehaviorMB : MonoBehaviour, ITheseusBehavior, ITheseusBehaviorHumbleObject
    {
        [SerializeField]
        private BoolReference _isInputEnabled;

        public event Action TurnStarted;
        public event Action TurnEnded;
        public event Action ReachedLevelEnd;

        public IGameEntity Entity { get; private set; }
        public ITileBasedMovement Movement { get; private set; }

        private IMovementView _movementView;
        private TheseusBehaviorController _controller;
        private Vector3 _targetPosition;
        private bool _isTurnActive;
        private bool _isControllerTurnActive;
        private bool _reachedDestination;
        private bool _isFirstTurn = true;

        private void Awake()
        {
            Entity = GetComponent<ITheseus>();
            Movement = GetComponent<ITileBasedMovement>();
            TryGetComponent(out _movementView);

            _controller = new TheseusBehaviorController(this);
            _controller.Init();
            _controller.TurnStarted += OnTurnStarted;
            _controller.TurnEnded += Controller_OnTurnEnded;
        }

        private void Update()
        {
            if (!_isTurnActive)
            {
                return;
            }

            _reachedDestination = _movementView.CheckReachedDestination(_targetPosition);
            if (_reachedDestination && !_isControllerTurnActive)
            {
                if (Entity.CurrentTile.IsLevelEnd)
                {
                    OnReachedLevelEnd();
                }

                OnTurnEnded();
                return;
            }

            _movementView.UpdatePosition(_targetPosition, Services.TimeService.DeltaTime);
        }

        private void OnDestroy()
        {
            _controller.TurnStarted -= OnTurnStarted;
            _controller.TurnEnded -= Controller_OnTurnEnded;
            _controller.Dispose();
        }

        public void StartTurn()
        {
            _controller.StartTurn();
            if (_isFirstTurn)
            {
                _targetPosition = Entity.CurrentTile.Position;
            }
        }

        public MoveResult? Move(Direction direction)
        {
            if (!_isTurnActive || !_reachedDestination)
            {
                return null;
            }

            MoveResult? result = _controller.Move(direction);
            if (result.Value.Type == MoveResultType.Success)
            {
                _targetPosition = result.Value.NewTile.Position;
                _reachedDestination = false;
            }

            return result;
        }

        public void ForceEndTurn()
        {
            if (!_reachedDestination || !_isControllerTurnActive || !_isTurnActive)
            {
                return;
            }

            OnTurnEnded();
        }

        private void OnTurnStarted()
        {
            _isInputEnabled.Value = true;
            _isControllerTurnActive = true;
            _isTurnActive = true;
            TurnStarted?.Invoke();
        }

        private void Controller_OnTurnEnded()
        {
            _isInputEnabled.Value = false;
            _isControllerTurnActive = false;
        }

        private void OnTurnEnded()
        {
            _isControllerTurnActive = false;
            _reachedDestination = true;
            _isTurnActive = false;
            _isFirstTurn = false;
            TurnEnded?.Invoke();
        }

        private void OnReachedLevelEnd()
        {
            ReachedLevelEnd?.Invoke();
        }
    }
}