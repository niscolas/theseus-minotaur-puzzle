namespace TheseusAndTheMinotaur
{
    public interface ITile : ITileData
    {
        void AddEntity(IGameEntity entity);
        void UnlinkEntity(IGameEntity entity);
        void AddObstacle(IObstacle obstacle, Direction direction);
        bool CheckIsDirectionFree(Direction direction);
    }
}