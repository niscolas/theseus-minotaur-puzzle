using TheseusAndTheMinotaur.Map;

namespace TheseusAndTheMinotaur.Tests.Editor
{
    public class TileFactory : ITileFactory
    {
        public ITile Create(int x, int y, IMap parentMap)
        {
            TileController tile = new TileController(new HumbleTile());
            tile.Setup(x, y, parentMap);

            return tile;
        }
    }
}