using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorTriggerPrefix + "Run",
        menuName = PlayerAssetsPaths.Run + "AnimationTrigger")]
    public class RunAnimationTriggerAsset : AnimationTriggerAsset
    {
    }
}