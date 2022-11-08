using System.Threading;
using Cysharp.Threading.Tasks;

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    public interface IAnimatorView
    {
        UniTask PlayAsync(AnimationTrigger trigger, IAnimationEndCallback endCallback, CancellationToken cancellation);
        void SetTrigger(AnimationTrigger trigger);
        void SetFloat(AnimatorFloat data, float value);
        void AddSubAnimator(SubAnimator subAnimator);
        void RemoveSubAnimator(SubAnimator subAnimator);
    }
}