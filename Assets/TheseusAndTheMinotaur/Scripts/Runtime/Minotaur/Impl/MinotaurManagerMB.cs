using NaughtyAttributes;
using TheseusAndTheMinotaur.Common;
using UnityEngine;

namespace TheseusAndTheMinotaur.Minotaur
{
    public class MinotaurManagerMB : MonoBehaviour
    {
        [Required, SerializeField]
        private MinotaurMB _minotaurPrefab;

        private IMap _map;

        private void Awake()
        {
            TryGetComponent(out _map);
        }

        private void Start()
        {
            ITile initialMinotaurTile = _map.GetMinotaurInitialTile();
            MinotaurMB minotaur = Services.SpawnService.Spawn(
                _minotaurPrefab,
                initialMinotaurTile.Position,
                _minotaurPrefab.transform.rotation, transform);

            ITheseus theseus = GetComponentInChildren<ITheseus>();
            minotaur.GetComponent<MinotaurBehaviorMB>().Setup(theseus);

            minotaur.SetCurrentTile(initialMinotaurTile);
            initialMinotaurTile.AddEntity(minotaur);
        }
    }
}