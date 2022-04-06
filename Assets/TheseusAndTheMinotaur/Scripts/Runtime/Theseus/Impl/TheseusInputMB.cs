using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheseusAndTheMinotaur.Theseus
{
    public class TheseusInputMB : MonoBehaviour
    {
        [Required, SerializeField]
        private TheseusBehaviorMB _theseusBehavior;

        public void HandleLoadNextLevelInput() { }

        public void HandleLoadPreviousLevelInput() { }

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