namespace TheseusAndTheMinotaur
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }

        bool CheckIsValidTile(int x, int y);
        bool CheckIsValidNeighbourTile(ITile tile, Direction direction);
        ITile GetNeighbourTile(ITile tile, Direction direction);
        bool TryGetNeighbourTile(ITile tile, Direction direction, out ITile neighbourTile);
        ITile GetTile(int x, int y);
        bool TryGetTile(int x, int y, out ITile tile);
    }
}