using System;
using NaughtyAttributes;
using TheseusAndTheMinotaur.Common;
using UnityEngine;

namespace TheseusAndTheMinotaur.Theseus
{
    public class TheseusManagerMB : MonoBehaviour
    {
        [Required, SerializeField]
        private TheseusMB _theseusPrefab;

        private IMap _map;

        private void Awake()
        {
            TryGetComponent(out _map);
        }

        private void Start()
        {
            ITile initialTheseusTile = _map.GetTheseusInitialTile();
            TheseusMB theseus = Services.SpawnService.Spawn(
                _theseusPrefab,
                initialTheseusTile.Position,
                _theseusPrefab.transform.rotation,
                transform);

            theseus.SetCurrentTile(initialTheseusTile);
            initialTheseusTile.AddEntity(theseus);
        }
    }
}