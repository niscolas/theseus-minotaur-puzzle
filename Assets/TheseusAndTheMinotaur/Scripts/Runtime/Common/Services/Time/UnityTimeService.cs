using UnityEngine;

namespace TheseusAndTheMinotaur.Common
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}