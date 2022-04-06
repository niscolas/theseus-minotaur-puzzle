using System;

namespace TheseusAndTheMinotaur.Theseus
{
    internal class TheseusBehaviorController : ITheseusBehavior
    {
        public event Action TurnStarted;
        public event Action TurnEnded;
        public event Action ReachedLevelEnd;

        public IGameEntity Entity => _humbleObject.Entity;

        private ITileBasedMovement Movement => _humbleObject.Movement;

        private readonly ITheseusBehaviorHumbleObject _humbleObject;

        public TheseusBehaviorController(ITheseusBehaviorHumbleObject humbleObject)
        {
            _humbleObject = humbleObject;
        }

        public void Init()
        {
            Movement.GotExhausted += OnExhausted;
        }

        public void Dispose()
        {
            Movement.GotExhausted -= OnExhausted;
        }

        public void StartTurn()
        {
            TurnStarted?.Invoke();
            Movement.ResetMoveCount();
        }

        public MoveResult Move(Direction direction)
        {
            MoveResult result = Movement.Move(direction);
            return result;
        }

        private void OnExhausted()
        {
            EndTurn();
        }

        private void EndTurn()
        {
            TurnEnded?.Invoke();
        }
    }
}