using System.Collections.Generic;

namespace TheseusAndTheMinotaur
{
    public interface ITileData
    {
        int X { get; }
        int Y { get; }
        IMap ParentMap { get; }
        IEnumerable<IGameEntity> PlacedEntities { get; }
    }
}