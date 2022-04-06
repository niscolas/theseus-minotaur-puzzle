using UnityEngine;

namespace TheseusAndTheMinotaur.Common
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public void PauseTime()
        {
            Time.timeScale = 0;
        }

        public void ResumeTime()
        {
            Time.timeScale = 1;
        }
    }
}