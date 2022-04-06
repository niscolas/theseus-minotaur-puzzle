using UnityEngine;

namespace TheseusAndTheMinotaur.Turn
{
    public class TurnManagerMB : MonoBehaviour, ITurnManager, ITurnManagerHumbleObject
    {
        public IMinotaurBehavior MinotaurBehavior { get; private set; }
        public ITheseusBehavior TheseusBehavior { get; private set; }

        private TurnManagerController _controller;

        private void Start()
        {
            MinotaurBehavior = GetComponentInChildren<IMinotaurBehavior>();
            TheseusBehavior = GetComponentInChildren<ITheseusBehavior>();

            _controller = new TurnManagerController(this);
            _controller.Init();
        }

        private void OnDestroy()
        {
            _controller.Dispose();
        }
    }
}