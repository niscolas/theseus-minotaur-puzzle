using UnityEngine;

namespace TheseusAndTheMinotaur.Common
{
    public interface ISpawnService
    {
        Object Spawn(
            Object unityObj,
            Vector3 position,
            Quaternion rotation,
            Transform parent);

        T Spawn<T>(
            T component,
            Vector3 position,
            Quaternion rotation,
            Transform parent) where T : Component;
    }
}