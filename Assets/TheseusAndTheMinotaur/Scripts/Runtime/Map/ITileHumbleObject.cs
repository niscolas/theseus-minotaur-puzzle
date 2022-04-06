using System.Collections.Generic;

namespace TheseusAndTheMinotaur.Map
{
    public interface ITileHumbleObject : ITileData
    {
        int Humble_X { get; set; }
        int Humble_Y { get; set; }
        IMap Humble_ParentMap { get; set; }
        bool Humble_IsLevelEnd { get; set; }
        IDictionary<Direction, IObstacle> Obstacles { get; }
        IList<IGameEntity> PlacedEntitiesList { get; }
    }
}