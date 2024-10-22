﻿using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.RangeAttacks.Logs;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack;
using Global.Services.InputViews.Runtime;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime
{
    public class RangeAttackInput : IPlayerSwitchListener
    {
        public RangeAttackInput(
            IInputView input,
            IRangeAttack rangeAttack,
            RangeAttackLogger logger)
        {
            _input = input;
            _rangeAttack = rangeAttack;
            _logger = logger;
        }

        private readonly IInputView _input;
        private readonly RangeAttackLogger _logger;
        private readonly IRangeAttack _rangeAttack;

        public void OnEnabled()
        {
            _input.RangeAttackPerformed += OnAttackPreformed;
            _input.RangeAttackCanceled += OnAttackCanceled;
            _input.RangeAttackBreakPerformed += OnAttackBroke;
        }

        public void OnDisabled()
        {
            _input.RangeAttackPerformed -= OnAttackPreformed;
            _input.RangeAttackCanceled -= OnAttackCanceled;
            _input.RangeAttackBreakPerformed -= OnAttackBroke;
        }

        private void OnAttackPreformed()
        {
            _logger.OnAttackInput();

            _rangeAttack.OnInput();
        }

        private void OnAttackCanceled()
        {
            _logger.OnAttackCanceled();

            _rangeAttack.OnInputCanceled();
        }

        private void OnAttackBroke()
        {
            _logger.OnAttackBroke();

            _rangeAttack.OnInputBroke();
        }
    }
}