using UnityEngine;

namespace TheseusAndTheMinotaur.Map
{
    public class ObstacleMB : MonoBehaviour, IObstacle
    {
        public bool IsActive => gameObject.activeSelf;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(true);
        }

        public void SetPosition(Vector3 position, Transform parent = null)
        {
            transform.position = position;
            transform.SetParent(parent);
        }
    }
}