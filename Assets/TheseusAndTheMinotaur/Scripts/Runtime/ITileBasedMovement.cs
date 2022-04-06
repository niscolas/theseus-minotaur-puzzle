namespace TheseusAndTheMinotaur
{
    public interface ITileBasedMovement : ITileBasedMovementData
    {
        void Move(Direction direction);
    }
}