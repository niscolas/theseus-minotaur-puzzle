using System;

namespace TheseusAndTheMinotaur
{
    public interface ITheseusBehavior
    {
        event Action TurnEnded;
        void StartTurn();
    }
}