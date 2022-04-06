namespace TheseusAndTheMinotaur
{
    public interface ITileBasedMovementData
    {
        int MaxMoveCount { get; }
        int CurrentMoveCountLeft { get; }
        IGameEntity Entity { get; }
    }
}