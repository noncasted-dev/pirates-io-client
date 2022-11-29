using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Logs;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Components.StateMachines.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "StateMachine",
        menuName = PlayerAssetsPaths.StateMachine + "Component")]
    public class StateMachineAsset : PlayerComponentAsset
    {
        [SerializeField]  private StateMachineLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<StateMachineLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);
            
            builder.Register<StateMachine>(Lifetime.Scoped)
                .As<IStateMachine>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<StateMachine>());
        }
    }
}