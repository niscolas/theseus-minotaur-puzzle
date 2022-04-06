using TheseusAndTheMinotaur.Common;

namespace TheseusAndTheMinotaur.Map
{
    internal class MapController : IMap
    {
        public int Width => _humbleObject.Width;
        public int Height => _humbleObject.Height;

        public ITileFactory TileFactory => _humbleObject.TileFactory;

        private ITile[,] Tiles
        {
            get => _humbleObject.Tiles;
            set => _humbleObject.Tiles = value;
        }

        private readonly IMapHumbleObject _humbleObject;

        public MapController(IMapHumbleObject humbleObject)
        {
            _humbleObject = humbleObject;
            CreateTiles();
        }

        public bool TryGetTile(int x, int y, out ITile tile)
        {
            tile = default;
            bool isAValidTile = CheckIsValidTile(x, y);

            if (!isAValidTile)
            {
                return false;
            }

            tile = GetTile(x, y);
            return true;
        }

        public bool CheckIsValidTile(int x, int y)
        {
            bool result = x >= 0 && x < Height &&
                          y >= 0 && y < Width;
            return result;
        }

        public bool CheckHasValidNeighbourTile(ITile tile, Direction direction)
        {
            GetNeighbourCoordinates(
                tile, direction, out int neighbourX, out int neighbourY);

            bool result = CheckIsValidTile(neighbourX, neighbourY);
            return result;
        }

        public bool TryGetNeighbourTile(ITile tile, Direction direction, out ITile neighbourTile)
        {
            neighbourTile = default;

            bool isNeighbourValid = CheckHasValidNeighbourTile(tile, direction);

            if (!isNeighbourValid)
            {
                return false;
            }

            neighbourTile = GetNeighbourTile(tile, direction);
            return true;
        }

        public ITile GetNeighbourTile(ITile tile, Direction direction)
        {
            GetNeighbourCoordinates(
                tile, direction, out int neighbourX, out int neighbourY);
            ITile neighbourTile = Tiles[neighbourX, neighbourY];

            return neighbourTile;
        }

        public ITile GetTile(int x, int y)
        {
            ITile result = Tiles[x, y];
            return result;
        }

        private static void GetNeighbourCoordinates(
            ITile tile, Direction direction, out int neighbourX, out int neighbourY)
        {
            CoordinateUtility.GetWithOffsetForDirection(
                tile.X,
                tile.Y,
                direction,
                out neighbourX,
                out neighbourY);
        }

        private void CreateTiles()
        {
            Tiles = new ITile[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tiles[i, j] = TileFactory.Create(i, j, this);
                }
            }
        }
    }
}