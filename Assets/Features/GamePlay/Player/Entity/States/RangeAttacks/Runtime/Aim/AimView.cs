using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using Global.Services.InputViews.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    [DisallowMultipleComponent]
    public class AimView : MonoBehaviour, IAimView
    {
        [Inject]
        private void Construct(
            IRotation rotation,
            IUpdater updater,
            IInputView input,
            IRangeAttackConfig config)
        {
            _input = input;
            _updater = updater;
            _config = config;
            _rotation = rotation;
        }

        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;

        private CancellationTokenSource _cancellation;
        private IRangeAttackConfig _config;
        private IInputView _input;

        private IRotation _rotation;
        private IUpdater _updater;

        public async UniTask<AimResult> AimAsync()
        {
            _left.gameObject.SetActive(true);
            _right.gameObject.SetActive(true);

            _cancellation = new CancellationTokenSource();
            var parameters = _config.CreateAimParams();

            var aim = new Aim(
                _left,
                _right,
                transform,
                _rotation,
                _updater,
                _input,
                parameters,
                _cancellation.Token);

            var result = await aim.Process();

            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);

            return result;
        }

        public void OnBroke()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;

            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
        }
    }
}