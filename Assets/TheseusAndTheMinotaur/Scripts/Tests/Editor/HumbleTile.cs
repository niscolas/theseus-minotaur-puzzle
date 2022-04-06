using System.Collections.Generic;
using TheseusAndTheMinotaur.Map;
using UnityEngine;

namespace TheseusAndTheMinotaur.Tests.Editor
{
    public class HumbleTile : ITileHumbleObject
    {
        public Vector3 Position { get; }
        public int X => Humble_X;
        public int Y => Humble_Y;
        public bool IsDisabled { get; }
        public IMap ParentMap { get; }
        public IEnumerable<IGameEntity> PlacedEntities => PlacedEntitiesList;
        public bool IsLevelEnd => Humble_IsLevelEnd;
        public int Humble_X { get; set; }
        public int Humble_Y { get; set; }
        public IMap Humble_ParentMap { get; set; }
        public bool Humble_IsLevelEnd { get; set; }

        public IDictionary<Direction, IObstacle> Obstacles { get; } =
            new Dictionary<Direction, IObstacle>();

        public IList<IGameEntity> PlacedEntitiesList { get; } =
            new List<IGameEntity>();
    }
}