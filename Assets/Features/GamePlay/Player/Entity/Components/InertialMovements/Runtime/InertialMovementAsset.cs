using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.InertialMovements.Logs;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "InertialMovement",
        menuName = PlayerAssetsPaths.InertialMovement + "Component")]
    public class InertialMovementAsset : PlayerComponentAsset
    {
        [SerializeField] [EditableObject] private InertialMovementLogSettings _logSettings;
        [SerializeField] [EditableObject] private InertialMovementConfigAsset _config;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<InertialMovementLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.Register<InertialMovementInput>(Lifetime.Scoped);

            builder.Register<InertialMovement>(Lifetime.Scoped)
                .WithParameter(_config)
                .As<IInertialMovement>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<InertialMovement>());
            callbackRegister.Add(resolver.Resolve<InertialMovementInput>());
        }
    }
}