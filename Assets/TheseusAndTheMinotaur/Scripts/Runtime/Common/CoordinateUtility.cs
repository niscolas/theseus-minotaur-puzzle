namespace TheseusAndTheMinotaur.Common
{
    public class CoordinateUtility
    {
        public static Direction? GetHorizontalDirectionOffset(int fromY, int toY)
        {
            if (fromY > toY)
            {
                return Direction.Left;
            }

            if (fromY < toY)
            {
                return Direction.Right;
            }

            return null;
        }

        public static Direction? GetVerticalDirectionOffset(int fromX, int toX)
        {
            if (fromX > toX)
            {
                return Direction.Up;
            }

            if (fromX < toX)
            {
                return Direction.Down;
            }

            return null;
        }

        public static void GetOffsetForDirection(
            Direction direction,
            out int xOffset,
            out int yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            switch (direction)
            {
                case Direction.Left:
                    xOffset = -1;
                    break;

                case Direction.Right:
                    xOffset = +1;
                    break;

                case Direction.Up:
                    yOffset = -1;
                    break;

                case Direction.Down:
                    yOffset = +1;
                    break;
            }
        }

        public static void GetWithOffsetForDirection(
            int x,
            int y,
            Direction direction,
            out int xWithDirectionOffset,
            out int yWithDirectionOffset)
        {
            GetOffsetForDirection(direction, out int xOffset, out int yOffset);

            GetWithOffset(
                x,
                y,
                xOffset,
                yOffset,
                out xWithDirectionOffset,
                out yWithDirectionOffset);
        }

        public static void GetWithOffset(
            int x,
            int y,
            int xOffset,
            int yOffset,
            out int xWithOffset,
            out int yWithOffset)
        {
            xWithOffset = x + xOffset;
            yWithOffset = y + yOffset;
        }
    }
}