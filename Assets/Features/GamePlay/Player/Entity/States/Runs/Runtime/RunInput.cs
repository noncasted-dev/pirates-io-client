using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Runs.Logs;
using Global.Services.InputViews.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    public class RunInput : ISwitchCallbacks
    {
        public RunInput(IInputView input, IRun run, RunLogger logger)
        {
            _input = input;
            _run = run;
            _logger = logger;
        }

        private readonly IInputView _input;
        private readonly RunLogger _logger;
        private readonly IRun _run;

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
            _logger.OnInput(direction);

            if (direction == Vector2.zero)
            {
                _run.OnCancel();
                return;
            }

            _run.OnInput(direction);
        }

        private void OnCanceled()
        {
            _logger.OnCanceled();

            _run.OnCancel();
        }
    }
}