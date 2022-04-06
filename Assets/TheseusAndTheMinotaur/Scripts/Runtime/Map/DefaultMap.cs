using TheseusAndTheMinotaur.Common;

namespace TheseusAndTheMinotaur.Map
{
    internal class DefaultMap : IMap
    {
        public int Width { get; }
        public int Height { get; }

        private readonly ITile[,] Tiles;

        public DefaultMap(int width, int height)
        {
            Width = width;
            Height = height;

            Tiles = new ITile[width, height];
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
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tiles[i, j] = new FixedTile(i, j, this);
                }
            }
        }
    }
}