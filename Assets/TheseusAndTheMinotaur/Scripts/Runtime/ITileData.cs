using System.Collections.Generic;
using UnityEngine;

namespace TheseusAndTheMinotaur
{
    public interface ITileData
    {
        Vector3 Position { get; }
        int X { get; }
        int Y { get; }
        bool IsDisabled { get; }
        IMap ParentMap { get; }
        IEnumerable<IGameEntity> PlacedEntities { get; }
        bool IsLevelEnd { get; }
    }
}