using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using Global.Services.InputViews.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UniRx;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public class Aim : IUpdatable
    {
        public Aim(
            Transform left,
            Transform right,
            Transform middle,
            SpriteRenderer leftCircle,
            SpriteRenderer rightCircle,
            Transform body,
            IRotation rotation,
            IUpdater updater,
            IInputView input,
            AimParams parameters,
            CancellationToken cancellation)
        {
            _left = left;
            _right = right;
            _middle = middle;
            _leftCircle = leftCircle;
            _rightCircle = rightCircle;
            _body = body;
            _rotation = rotation;
            _updater = updater;
            _input = input;
            _parameters = parameters;
            _cancellation = cancellation;

            _handle = new AimHandle();
        }

        private readonly Transform _body;

        private readonly CancellationToken _cancellation;
        private readonly UniTaskCompletionSource<AimResult> _completion = new();
        private readonly IInputView _input;
        private readonly AimHandle _handle;

        private readonly Transform _left;
        private readonly AimParams _parameters;
        private readonly Transform _right;
        private readonly Transform _middle;
        private readonly SpriteRenderer _leftCircle;
        private readonly SpriteRenderer _rightCircle;
        private readonly IRotation _rotation;
        private readonly IUpdater _updater;

        private float _currentTime;
        private bool _isBroke;
        private bool _isCanceled;

        public void OnUpdate(float delta)
        {
            if (_cancellation.IsCancellationRequested == true)
            {
                Break();
                return;
            }

            _body.rotation = _rotation.Quaternion;

            _currentTime += delta;

            var progress = _currentTime / _parameters.Time;
            var angle = Mathf.Lerp(_parameters.StartAngle, _parameters.EndAngle, progress);
            
            _handle.OnProgress(progress);
            
            _left.localRotation = Quaternion.Euler(0f, 0f, angle);
            _leftCircle.material.SetFloat("_FillAmount",  angle / 360f);

            _right.localRotation = Quaternion.Euler(0f, 0f, -angle);
            _rightCircle.material.SetFloat("_FillAmount", angle / 360f);

            if (_isCanceled == true)
            {
                var spread = _parameters.EndAngle - angle + _parameters.AdditionalSpread;
                var result = new AimResult(_rotation.Angle, spread, AimResultType.Shot);
                _completion.TrySetResult(result);

                return;
            }

            if (_currentTime > _parameters.Time + _parameters.OverTime)
            {
                var result = new AimResult(AimResultType.Broke);
                _completion.TrySetResult(result);
            }
        }

        public async UniTask<AimResult> Process()
        {
            _updater.Add(this);
            
            _input.RangeAttackCanceled += OnRangeAttackCanceled;

            var result = await _completion.Task;

            Break();

            return result;
        }

        private void Break()
        {
            _handle.OnCanceled();
            _updater.Remove(this);
            _input.RangeAttackCanceled -= OnRangeAttackCanceled;
        }

        private void OnRangeAttackCanceled()
        {
            _isCanceled = true;
        }
    }
}