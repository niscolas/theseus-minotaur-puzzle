using System;
using TheseusAndTheMinotaur.Common;

namespace TheseusAndTheMinotaur.Minotaur
{
    internal class MinotaurBehaviorController : IMinotaurBehavior
    {
        public event Action TurnEnded;

        private ITileBasedMovement Movement => _humbleObject.Movement;
        private ITheseus Theseus => _humbleObject.Theseus;
        public IMinotaur Minotaur => _humbleObject.Minotaur;

        private readonly IMinotaurBehaviorHumbleObject _humbleObject;

        public MinotaurBehaviorController(IMinotaurBehaviorHumbleObject humbleObject)
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

        public void StartTurn() { }


        public MoveResult? Move()
        {
            bool isInSameColumnAsTheseus =
                Minotaur.CurrentTile.Y == Theseus.CurrentTile.Y;

            MoveResult? moveResult;
            if (!isInSameColumnAsTheseus)
            {
                moveResult = OnNotSameColumnAsTheseus();
            }
            else
            {
                moveResult = OnSameColumnAsTheseus();
            }

            return moveResult;
        }

        private MoveResult? OnNotSameColumnAsTheseus()
        {
            Direction? direction = CoordinateUtility.GetHorizontalDirectionOffset(
                Minotaur.CurrentTile.Y, Theseus.CurrentTile.Y);

            return Move(direction);
        }

        private MoveResult? OnSameColumnAsTheseus()
        {
            Direction? direction = CoordinateUtility.GetVerticalDirectionOffset(
                Minotaur.CurrentTile.X, Theseus.CurrentTile.X);

            return Move(direction);
        }

        private MoveResult? Move(Direction? direction)
        {
            if (direction == null)
            {
                return null;
            }

            MoveResult moveResult = Movement.Move(direction.Value);

            if (CheckShouldEndTurn(moveResult))
            {
                EndTurn();
            }

            return moveResult;
        }

        private static bool CheckShouldEndTurn(MoveResult moveResult)
        {
            return moveResult.Type == MoveResultType.Exhausted ||
                   moveResult.Type == MoveResultType.Failure;
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