namespace TheseusAndTheMinotaur
{
    public interface ITile : ITileData
    {
        void Disable();
        void AddEntity(IGameEntity entity);
        void UnlinkEntity(IGameEntity entity);
        void ActivateObstacle(Direction direction);
        bool CheckIsObstacleOfDirectionActive(Direction direction);
        void SetIsLevelEnd(bool value);
    }
}