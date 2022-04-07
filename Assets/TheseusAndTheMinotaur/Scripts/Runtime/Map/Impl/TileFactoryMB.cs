using TheseusAndTheMinotaur.Common;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    public class TileFactoryMB : MonoBehaviour, ITileFactory
    {
        [SerializeField]
        private TileMB _tilePrefab;

        public ITile Create(int x, int y, IMap parentMap, float tileOffset)
        {
            float positionX = y * tileOffset;
            float positionY = x * -tileOffset;
            Vector3 position = new Vector3(positionX, positionY, 0);

            TileMB tile = Services.SpawnService.Spawn(_tilePrefab, position, _tilePrefab.transform.rotation, transform);
            tile.Setup(x, y, parentMap);

            return tile;
        }
    }
}