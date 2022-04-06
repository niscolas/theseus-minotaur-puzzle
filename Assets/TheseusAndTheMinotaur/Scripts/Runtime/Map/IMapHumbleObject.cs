namespace TheseusAndTheMinotaur.Map
{
    internal interface IMapHumbleObject : IMapData
    {
        ITileFactory TileFactory { get; }
        ITile[,] Tiles { get; set; }
        PuzzleLevelData PuzzleLevelData { get; }
    }
}