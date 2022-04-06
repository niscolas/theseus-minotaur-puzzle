using System;

namespace TheseusAndTheMinotaur
{
    public interface ITheseusBehavior : ITheseusBehaviorData
    {
        event Action TurnStarted;
        event Action TurnEnded;
        void StartTurn();
    }
}