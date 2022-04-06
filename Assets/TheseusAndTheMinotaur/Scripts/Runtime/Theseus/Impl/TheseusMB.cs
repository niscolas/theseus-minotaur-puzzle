using UnityEngine;

namespace TheseusAndTheMinotaur.Theseus
{
    public class TheseusMB : MonoBehaviour, ITheseus
    {
        public ITile CurrentTile { get; private set; }

        public void SetCurrentTile(ITile tile)
        {
            CurrentTile = tile;
        }
    }
}