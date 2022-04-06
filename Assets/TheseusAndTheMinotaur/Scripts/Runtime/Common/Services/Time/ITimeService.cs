namespace TheseusAndTheMinotaur.Common
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        void PauseTime();
        void ResumeTime();
    }
}