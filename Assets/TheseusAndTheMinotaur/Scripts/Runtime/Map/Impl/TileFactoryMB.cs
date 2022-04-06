using TheseusAndTheMinotaur.Common;
using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    public class TileFactoryMB : MonoBehaviour, ITileFactory
    {
        [SerializeField]
        private TileMB _tilePrefab;

        [SerializeField]
        private float _offset;

        public ITile Create(int x, int y, IMap parentMap)
        {
            float positionX = y * _offset;
            float positionY = x * -_offset;
            Vector3 position = new Vector3(positionX, positionY, 0);

            TileMB tile = Services.SpawnService.Spawn(_tilePrefab, position, _tilePrefab.transform.rotation, transform);
            tile.Setup(x, y, parentMap);

            return tile;
        }
    }
}