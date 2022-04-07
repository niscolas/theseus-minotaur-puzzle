using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace TheseusAndTheMinotaur.Theseus
{
    public class TheseusInputMB : MonoBehaviour
    {
        [Required, SerializeField]
        private TheseusBehaviorMB _theseusBehavior;

        [SerializeField]
        private UnityEvent _onLoadNextLevelInput;

        [SerializeField]
        private UnityEvent _onLoadPreviousLevelInput;

        [SerializeField]
        private UnityEvent _onPassTurnInput;

        [FormerlySerializedAs("_onRestarLevelInput")]
        [SerializeField]
        private UnityEvent _onRestartLevelInput;

        public void HandleLoadNextLevelInput(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
            {
                return;
            }

            _onLoadNextLevelInput?.Invoke();
        }

        public void HandleLoadPreviousLevelInput(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
            {
                return;
            }

            _onLoadPreviousLevelInput?.Invoke();
        }

        public void HandlePassTurnInput(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
            {
                return;
            }

            _onPassTurnInput?.Invoke();
        }

        public void HandleRestartLevelInput(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
            {
                return;
            }

            _onRestartLevelInput?.Invoke();
        }

        public void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
            {
                return;
            }

            Vector2 value = ctx.ReadValue<Vector2>();
            if (value == Vector2.left)
            {
                _theseusBehavior.Move(Direction.Left);
            }
            else if (value == Vector2.right)
            {
                _theseusBehavior.Move(Direction.Right);
            }
            else if (value == Vector2.up)
            {
                _theseusBehavior.Move(Direction.Up);
            }
            else if (value == Vector2.down)
            {
                _theseusBehavior.Move(Direction.Down);
            }
        }
    }
}