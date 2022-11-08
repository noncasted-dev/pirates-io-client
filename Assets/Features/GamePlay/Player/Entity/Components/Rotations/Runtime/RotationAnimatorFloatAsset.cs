using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorFloatPrefix + "Rotation",
        menuName = PlayerAssetsPaths.Rotation + "AnimatorFloat")]
    public class RotationAnimatorFloatAsset : AnimatorFloatAsset
    {
    }
}