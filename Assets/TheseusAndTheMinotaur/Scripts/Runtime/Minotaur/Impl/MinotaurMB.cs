using UnityEngine;

namespace TheseusAndTheMinotaur.Minotaur
{
    public class MinotaurMB : MonoBehaviour, IMinotaur
    {
        public ITile CurrentTile { get; private set; }

        public void SetCurrentTile(ITile tile)
        {
            CurrentTile = tile;
        }
    }
}