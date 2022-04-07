using UnityEngine;

namespace TheseusAndTheMinotaur.Common
{
    public class UnityInstantiateService : ISpawnService
    {
        Object ISpawnService.Spawn(Object unityObj, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Object.Instantiate(unityObj, position, rotation, parent);
        }

        public T Spawn<T>(T component, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
           return Object.Instantiate(component, position, rotation, parent);
        }
    }
}