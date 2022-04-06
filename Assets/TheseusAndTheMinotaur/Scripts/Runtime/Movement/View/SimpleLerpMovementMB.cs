using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace TheseusAndTheMinotaur.Movement.View
{
    internal class SimpleLerpMovementMB : MonoBehaviour, IMovementView
    {
        [SerializeField]
        private FloatReference _speed;

        [SerializeField]
        private FloatReference _reachedDistance;

        private Transform _cachedTransform;

        private void Awake()
        {
            _cachedTransform = transform;
        }

        public void UpdatePosition(Vector3 targetPosition, float deltaTime)
        {
            Vector3 newPosition = Vector3.Lerp(_cachedTransform.position, targetPosition, deltaTime * _speed.Value);
            _cachedTransform.position = newPosition;
        }

        public bool CheckReachedDestination(Vector3 targetPosition)
        {
            bool result = Vector3.Distance(_cachedTransform.position, targetPosition) < _reachedDistance.Value;
            return result;
        }
    }
}