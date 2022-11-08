using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorTriggerPrefix + "Respawn",
        menuName = PlayerAssetsPaths.Respawn + "AnimatorTrigger")]
    public class RespawnAnimationTriggerAsset : AnimationTriggerAsset
    {
    }
}