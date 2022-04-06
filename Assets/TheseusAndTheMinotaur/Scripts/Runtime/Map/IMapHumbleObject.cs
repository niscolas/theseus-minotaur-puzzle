namespace TheseusAndTheMinotaur.Map
{
    public interface IMapHumbleObject : IMapData
    {
        ITileFactory TileFactory { get; }
        ITile[,] Tiles { get; set; }
    }
}