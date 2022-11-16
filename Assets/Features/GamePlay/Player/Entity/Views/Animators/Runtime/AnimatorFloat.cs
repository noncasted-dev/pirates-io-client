#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    public class AnimatorFloat
    {
        public AnimatorFloat(string floatName, bool mirrorToSubAnimators)
        {
            Name = floatName;
            Hash = Animator.StringToHash(floatName);
            MirrorToSubAnimators = mirrorToSubAnimators;
        }

        public readonly int Hash;
        public readonly bool MirrorToSubAnimators;

        public readonly string Name;
    }
}