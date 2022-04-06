using System;

namespace TheseusAndTheMinotaur
{
    public interface ITheseusBehavior : ITheseusBehaviorData
    {
        event Action TurnStarted;
        event Action TurnEnded;
        event Action ReachedLevelEnd;

        void StartTurn();
        MoveResult Move(Direction direction);
    }
}