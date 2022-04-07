using UnityAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Common
{
    [CreateAssetMenu(
        menuName = Constants.CreateAssetMenuPrefix + nameof(ToggleGameObjectAtomAction),
        order = Constants.CreateAssetMenuOrder)]
    public class ToggleGameObjectAtomAction : AtomAction<GameObject>
    {
        public override void Do(GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}