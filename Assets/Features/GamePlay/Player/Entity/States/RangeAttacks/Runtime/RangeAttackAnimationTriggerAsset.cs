using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorTriggerPrefix + "RangeAttack",
        menuName = PlayerAssetsPaths.RangeAttack + "AnimationTrigger")]
    public class RangeAttackAnimationTriggerAsset : AnimationTriggerAsset
    {
    }
}