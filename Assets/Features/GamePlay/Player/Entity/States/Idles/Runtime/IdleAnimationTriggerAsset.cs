#region

using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Idles.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorTriggerPrefix + "Idle",
        menuName = PlayerAssetsPaths.Idle + "AnimationTrigger")]
    public class IdleAnimationTriggerAsset : AnimationTriggerAsset
    {
    }
}