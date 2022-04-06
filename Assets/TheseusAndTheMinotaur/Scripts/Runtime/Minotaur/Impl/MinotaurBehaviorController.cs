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

        public void StartTurn()
        {
            Movement.ResetMoveCount();
        }

        public MoveResult? ChaseTheseus()
        {
            bool isInSameColumnAsTheseus =
                Minotaur.CurrentTile.Y == Theseus.CurrentTile.Y;

            bool isInSameRowAsTheseus =
                Minotaur.CurrentTile.X == Theseus.CurrentTile.X;

            MoveResult? moveResult = null;
            if (isInSameColumnAsTheseus && isInSameRowAsTheseus)
            {
                EndTurn();
            }
            else if (!isInSameColumnAsTheseus)
            {
                moveResult = OnNotSameColumnAsTheseus();
            }
            else
            {
                moveResult = OnSameColumnAsTheseus();
            }

            if (moveResult != null && CheckShouldEndTurn(moveResult.Value))
            {
                EndTurn();
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