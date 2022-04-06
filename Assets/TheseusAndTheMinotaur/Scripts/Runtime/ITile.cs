namespace TheseusAndTheMinotaur
{
    public interface ITile
    {
        int X { get; }
        int Y { get; }
        IMap ParentMap { get; }
        IGameEntity Entity { get; }

        void LinkEntity(IGameEntity entity);
        void UnlinkCurrentEntity();
        void AddObstacle(IObstacle obstacle, Direction direction);
        bool CheckIsDirectionFree(Direction direction);
    }
}