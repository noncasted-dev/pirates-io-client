using UnityEngine;

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    public class AnimationTrigger
    {
        public AnimationTrigger(
            string trigger,
            bool mirrorToSubAnimators,
            bool overrideTime,
            float time)
        {
            Name = trigger;
            Hash = Animator.StringToHash(trigger);
            MirrorToSubAnimators = mirrorToSubAnimators;
            OverrideTime = overrideTime;
            Time = time;
        }

        public readonly int Hash;
        public readonly bool MirrorToSubAnimators;

        public readonly string Name;
        public readonly bool OverrideTime;
        public readonly float Time;
    }
}