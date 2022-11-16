#region

using System.Collections.Generic;
using System.Threading;
using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.Animators.Logs;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    [DisallowMultipleComponent]
    public class PlayerAnimatorView : MonoBehaviour, IAwakeCallback, IAnimatorView
    {
        [Inject]
        private void Construct(ILogger logger)
        {
            _logger = new AnimatorLogger(logger, _logSettings);
        }

        [SerializeField] [EditableObject] private AnimatorLogSettings _logSettings;

        [SerializeField] private List<SubAnimator> _subAnimators;

        private Animator _animator;
        private AnimatorLogger _logger;

        public async UniTask PlayAsync(
            AnimationTrigger trigger,
            IAnimationEndCallback endCallback,
            CancellationToken cancellation)
        {
            ApplyTrigger(trigger);
            SetTime(trigger);

            var isPlayed = false;

            void OnPlayed()
            {
                isPlayed = true;
            }

            endCallback.Played += OnPlayed;

            await UniTask.WaitUntil(() => isPlayed == true, PlayerLoopTiming.Update, cancellation);

            endCallback.Played -= OnPlayed;
        }

        public void SetTrigger(AnimationTrigger trigger)
        {
            ApplyTrigger(trigger);
            SetTime(trigger);
        }

        public void SetFloat(AnimatorFloat data, float value)
        {
            _animator.SetFloat(data.Hash, value);

            if (data.MirrorToSubAnimators == true)
                foreach (var subAnimator in _subAnimators)
                    subAnimator.SetFloat(data.Hash, value);

            _logger.OnFloat(data.Name, value);
        }

        public void AddSubAnimator(SubAnimator subAnimator)
        {
            subAnimator.OnAwake();
            _subAnimators.Add(subAnimator);
        }

        public void RemoveSubAnimator(SubAnimator subAnimator)
        {
            _subAnimators.Remove(subAnimator);
        }

        public void OnAwake()
        {
            _animator = GetComponent<Animator>();

            foreach (var subAnimator in _subAnimators)
                subAnimator.OnAwake();
        }

        private void ApplyTrigger(AnimationTrigger trigger)
        {
            _animator.SetTrigger(trigger.Hash);

            if (trigger.MirrorToSubAnimators == true)
                foreach (var subAnimator in _subAnimators)
                    subAnimator.Trigger(trigger.Hash);

            _logger.OnTrigger(trigger.Name);
        }

        private void SetTime(AnimationTrigger trigger)
        {
            if (trigger.OverrideTime == true)
            {
                var time = 1f / trigger.Time;

                _animator.speed = time;

                foreach (var subAnimator in _subAnimators)
                    subAnimator.SetSpeed(time);
            }
            else
            {
                _animator.speed = 1f;

                foreach (var subAnimator in _subAnimators)
                    subAnimator.SetSpeed(1f);
            }
        }
    }
}