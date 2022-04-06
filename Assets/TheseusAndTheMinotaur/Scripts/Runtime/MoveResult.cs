namespace TheseusAndTheMinotaur
{
    public struct MoveResult
    {
        public readonly ITile PreviousTile;
        public readonly ITile NewTile;
        public readonly MoveResultType Type;

        public MoveResult(ITile previousTile, ITile newTile, MoveResultType type)
        {
            PreviousTile = previousTile;
            NewTile = newTile;
            Type = type;
        }
    }
}