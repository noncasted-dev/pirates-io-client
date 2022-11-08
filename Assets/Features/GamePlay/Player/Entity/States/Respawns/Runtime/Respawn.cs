using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.StateMachines.Runtime;
using GamePlay.Player.Entity.States.Abstract;
using GamePlay.Player.Entity.States.Common;
using GamePlay.Player.Entity.States.Respawns.Logs;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using GamePlay.Player.Entity.Views.Sprites.Runtime;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    public class Respawn : IState, IRespawn
    {
        public Respawn(
            IStateMachine stateMachine,
            IAnimatorView animator,
            ISpriteMaterial spriteMaterial,
            RespawnAnimatorCallbacks animatorCallbacks,
            RespawnLogger logger,
            StateDefinition definition,
            AnimationTrigger animation,
            RespawnConfigAsset config)
        {
            _stateMachine = stateMachine;
            _animator = animator;
            _spriteMaterial = spriteMaterial;
            _animatorCallbacks = animatorCallbacks;
            _logger = logger;
            Definition = definition;
            _animation = animation;
            _config = config;
        }

        private readonly AnimationTrigger _animation;
        private readonly IAnimatorView _animator;
        private readonly RespawnAnimatorCallbacks _animatorCallbacks;
        private readonly RespawnConfigAsset _config;

        private readonly RespawnLogger _logger;
        private readonly ISpriteMaterial _spriteMaterial;

        private readonly IStateMachine _stateMachine;

        private CancellationTokenSource _cancellation;

        public void Enter()
        {
            _stateMachine.Enter(this);

            _logger.OnEntered();

            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;

            Process().Forget();
        }

        public StateDefinition Definition { get; }

        public void Break()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;

            _logger.OnBroke();
        }

        private async UniTask Process()
        {
            var current = _spriteMaterial.Material;
            _spriteMaterial.SetMaterial(_config.Material);

            _cancellation = new CancellationTokenSource();
            await _animator.PlayAsync(_animation, _animatorCallbacks, _cancellation.Token);

            _spriteMaterial.SetMaterial(current);

            _stateMachine.Exit();
        }
    }
}