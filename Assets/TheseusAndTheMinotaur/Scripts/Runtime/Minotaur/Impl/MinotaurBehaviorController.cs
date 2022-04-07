using System;
using TheseusAndTheMinotaur.Common;

namespace TheseusAndTheMinotaur.Minotaur
{
    internal class MinotaurBehaviorController : IMinotaurBehavior
    {
        public event Action TurnEnded;
        public event Action ArrivedAtTheseus;

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

            if (isInSameColumnAsTheseus && isInSameRowAsTheseus)
            {
                EndTurn();
            }

            Direction? horizontalMoveDirection = GetHorizontalMoveDirection();
            Direction? verticalMoveDirection = GetVerticalMoveDirection();

            MoveResult? moveResult = ChooseMovement(
                horizontalMoveDirection, verticalMoveDirection);

            if (Minotaur.CurrentTile == Theseus.CurrentTile)
            {
                ArrivedAtTheseus?.Invoke();
            }

            if (moveResult != null && CheckShouldEndTurn(moveResult.Value))
            {
                EndTurn();
            }

            return moveResult;
        }

        private MoveResult? ChooseMovement(Direction? horizontalMoveDirection, Direction? verticalMoveDirection)
        {
            MoveResult? moveResult = null;
            if (horizontalMoveDirection != null &&
                Movement.CheckCanMoveToDirection(horizontalMoveDirection.Value))
            {
                return Movement.Move(horizontalMoveDirection.Value);
            }

            if (verticalMoveDirection != null &&
                Movement.CheckCanMoveToDirection(verticalMoveDirection.Value))
            {
                return Movement.Move(verticalMoveDirection.Value);
            }

            return new MoveResult(
                Minotaur.CurrentTile,
                Minotaur.CurrentTile,
                MoveResultType.Failure);
        }

        private Direction? GetHorizontalMoveDirection()
        {
            Direction? direction = CoordinateUtility.GetHorizontalDirectionOffset(
                Minotaur.CurrentTile.Y, Theseus.CurrentTile.Y);

            return direction;
        }

        private Direction? GetVerticalMoveDirection()
        {
            Direction? direction = CoordinateUtility.GetVerticalDirectionOffset(
                Minotaur.CurrentTile.X, Theseus.CurrentTile.X);

            return direction;
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