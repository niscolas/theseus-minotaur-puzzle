using UnityEngine;

namespace TheseusAndTheMinotaur
{
    public interface IMovementView
    {
        void UpdatePosition(Vector3 targetPosition, float deltaTime);
        bool CheckReachedDestination(Vector3 targetPosition);
    }
}