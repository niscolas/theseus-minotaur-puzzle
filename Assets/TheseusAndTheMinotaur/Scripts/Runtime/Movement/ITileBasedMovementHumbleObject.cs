namespace TheseusAndTheMinotaur.Movement
{
    internal interface ITileBasedMovementHumbleObject : ITileBasedMovementData
    {
        int Humble_MaxMoveCountLeft { get; set; }
        int Humble_CurrentMoveCountLeft { get; set; }
    }
}