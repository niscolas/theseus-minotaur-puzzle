using System;

namespace TheseusAndTheMinotaur
{
    public interface IMinotaurBehavior
    {
        event Action TurnEnded;
        void StartTurn();
    }
}