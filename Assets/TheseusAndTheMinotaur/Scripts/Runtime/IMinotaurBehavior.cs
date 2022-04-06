using System;

namespace TheseusAndTheMinotaur
{
    public interface IMinotaurBehavior : IMinotaurBehaviorData
    {
        event Action TurnEnded;
        event Action ArrivedAtTheseus;
        void StartTurn();
        MoveResult? ChaseTheseus();
    }
}