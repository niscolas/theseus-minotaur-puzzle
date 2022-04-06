using UnityEngine;

namespace TheseusAndTheMinotaur
{
    public interface IObstacle
    {
        bool IsActive { get; }
        void Enable();
        void Disable();
        void SetPosition(Vector3 position, Transform parent = null);
    }
}