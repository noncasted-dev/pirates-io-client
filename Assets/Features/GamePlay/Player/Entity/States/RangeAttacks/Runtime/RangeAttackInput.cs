using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash;
using Global.Services.InputViews.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime
{
    public class RangeAttackInput : ISwitchCallbacks
    {
        public RangeAttackInput(
            IInputView input,
            IRangeAttack rangeAttack,
            IDashDirection dash,
            RangeAttackLogger logger)
        {
            _input = input;
            _rangeAttack = rangeAttack;
            _dash = dash;
            _logger = logger;
        }

        private readonly IDashDirection _dash;

        private readonly IInputView _input;
        private readonly RangeAttackLogger _logger;
        private readonly IRangeAttack _rangeAttack;

        public void OnEnabled()
        {
            _input.MovementPerformed += OnDirectionPerformed;
            _input.MovementCanceled += OnDirectionCanceled;

            _input.RangeAttackPerformed += OnAttackPreformed;
            _input.RangeAttackCanceled += OnAttackCanceled;
        }

        public void OnDisabled()
        {
            _input.MovementPerformed -= OnDirectionPerformed;
            _input.MovementCanceled -= OnDirectionCanceled;

            _input.RangeAttackPerformed -= OnAttackPreformed;
            _input.RangeAttackCanceled -= OnAttackCanceled;
        }

        private void OnDirectionPerformed(Vector2 direction)
        {
            _logger.OnDirectionInput(direction);

            _dash.OnDirectionInput(direction);
            _rangeAttack.OnDirectionInput(direction);
        }

        private void OnDirectionCanceled()
        {
            _logger.OnDirectionCanceled();

            _dash.OnDirectionInputCanceled();
            _rangeAttack.OnDirectionInputCanceled();
        }

        private void OnAttackPreformed()
        {
            _logger.OnAttackInput();

            _rangeAttack.OnActionInput();
        }

        private void OnAttackCanceled()
        {
            _logger.OnAttackCanceled();

            _rangeAttack.OnAttackInputCanceled();
        }
    }
}