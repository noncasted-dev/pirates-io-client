using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.Rotations.Logs;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "Rotation",
        menuName = PlayerAssetsPaths.Rotation + "Component")]
    public class RotationAsset : PlayerComponentAsset
    {
        [SerializeField]  private RotationAnimatorFloatAsset _animatorFloatAsset;
        [SerializeField]  private RotationLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            var animatorFloat = _animatorFloatAsset.CreateFloat();

            builder.Register<RotationLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.Register<Rotation>(Lifetime.Scoped)
                .As<IRotation>()
                .AsSelf();

            builder.Register<SpriteRotation>(Lifetime.Scoped)
                .As<ISpriteRotation>()
                .AsSelf();

            builder.Register<AnimatorRotation>(Lifetime.Scoped)
                .WithParameter(animatorFloat)
                .As<IAnimatorRotation>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<Rotation>());
        }
    }
}