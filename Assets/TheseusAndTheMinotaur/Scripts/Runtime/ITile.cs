using System.Collections.Generic;

namespace TheseusAndTheMinotaur
{
    public interface ITile
    {
        int X { get; }
        int Y { get; }
        IMap ParentMap { get; }
        IEnumerable<IGameEntity> PlacedEntities { get; }

        void AddEntity(IGameEntity entity);
        void UnlinkEntity(IGameEntity entity);
        void AddObstacle(IObstacle obstacle, Direction direction);
        bool CheckIsDirectionFree(Direction direction);
    }
}