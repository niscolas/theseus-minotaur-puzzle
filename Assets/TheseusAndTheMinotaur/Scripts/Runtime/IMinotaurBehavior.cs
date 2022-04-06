using System;

namespace TheseusAndTheMinotaur
{
    public interface IMinotaurBehavior : IMinotaurBehaviorData
    {
        event Action TurnEnded;
        void StartTurn();
        MoveResult? ChaseTheseus();
    }
}