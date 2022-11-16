#region

using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using Global.Services.InputViews.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    public class InertialMovementInput : ISwitchCallbacks
    {
        public InertialMovementInput(IInputView input, IInertialMovement movement)
        {
            _input = input;
            _movement = movement;
        }

        private readonly IInputView _input;
        private readonly IInertialMovement _movement;

        public void OnEnabled()
        {
            _input.MovementPerformed += OnInput;
            _input.MovementCanceled += OnCanceled;
        }

        public void OnDisabled()
        {
            _input.MovementPerformed -= OnInput;
            _input.MovementCanceled -= OnCanceled;
        }

        private void OnInput(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                _movement.ResetDirection();
                return;
            }

            _movement.SetDirection(direction);
        }

        private void OnCanceled()
        {
            _movement.ResetDirection();
        }
    }
}