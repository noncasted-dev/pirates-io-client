using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.Rotations.Logs;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "Rotation",
        menuName = PlayerAssetsPaths.Rotation + "Component")]
    public class RotationAsset : PlayerComponentAsset
    {
        [SerializeField] private RotationAnimatorFloatAsset _animatorFloatAsset;
        [SerializeField] private RotationLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            var animatorFloat = _animatorFloatAsset.CreateFloat();

            builder.Register<RotationLogger>()
                .WithParameter(_logSettings);

            builder.Register<Rotation>()
                .As<IRotation>()
                .AsCallbackListener();

            builder.Register<SpriteRotation>()
                .As<ISpriteRotation>()
                .AsSelf();

            builder.Register<AnimatorRotation>()
                .WithParameter(animatorFloat)
                .As<IAnimatorRotation>()
                .AsSelf();
        }
    }
}