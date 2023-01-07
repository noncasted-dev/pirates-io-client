using GamePlay.Player.Entity.Components.Rotations.Runtime;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Idles.Runtime;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Player.Entity.States.Runs.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Setup.Editor
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Animations",
        menuName = PlayerAssetsPaths.Config + "Animations")]
    public class PlayerAnimationsConfig : ScriptableObject
    {
        [SerializeField] private IdleAnimationTriggerAsset _idle;
        [SerializeField] private RespawnAnimationTriggerAsset _respawn;
        [SerializeField] private RotationAnimatorFloatAsset _rotation;
        [SerializeField] private RunAnimationTriggerAsset _run;
    }
}