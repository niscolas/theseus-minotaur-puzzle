namespace TheseusAndTheMinotaur.Turn
{
    internal class TurnManagerController : ITurnManager
    {
        public IMinotaurBehavior MinotaurBehavior => _humbleObject.MinotaurBehavior;
        public ITheseusBehavior TheseusBehavior => _humbleObject.TheseusBehavior;

        private readonly ITurnManagerHumbleObject _humbleObject;

        public TurnManagerController(ITurnManagerHumbleObject humbleObject)
        {
            _humbleObject = humbleObject;
        }

        public void Init()
        {
            MinotaurBehavior.TurnEnded += StartTheseusTurn;
            TheseusBehavior.TurnEnded += StartMinotaurTurn;

            StartTheseusTurn();
        }

        public void Dispose()
        {
            MinotaurBehavior.TurnEnded -= StartTheseusTurn;
            TheseusBehavior.TurnEnded -= StartMinotaurTurn;
        }

        private void StartMinotaurTurn()
        {
            MinotaurBehavior.StartTurn();
        }

        private void StartTheseusTurn()
        {
            TheseusBehavior.StartTurn();
        }
    }
}