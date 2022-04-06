using System;

namespace TheseusAndTheMinotaur
{
    public interface ITileBasedMovement : ITileBasedMovementData
    {
        event Action GotExhausted;
        void ResetMoveCount();
        MoveResult Move(Direction direction);
    }
}