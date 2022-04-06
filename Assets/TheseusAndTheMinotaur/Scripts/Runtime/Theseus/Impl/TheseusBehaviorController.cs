using System;

namespace TheseusAndTheMinotaur.Theseus
{
    internal class TheseusBehaviorController : ITheseusBehavior
    {
        public event Action TurnEnded;
        
        private readonly ITheseusBehaviorHumbleObject _humbleObject;

        public TheseusBehaviorController(ITheseusBehaviorHumbleObject humbleObject)
        {
            _humbleObject = humbleObject;
        }

        public void StartTurn() { }
    }
}