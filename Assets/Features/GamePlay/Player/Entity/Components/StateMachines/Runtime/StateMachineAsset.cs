using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.StateMachines.Logs;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.StateMachines.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "StateMachine",
        menuName = PlayerAssetsPaths.StateMachine + "Component")]
    public class StateMachineAsset : PlayerComponentAsset
    {
        [SerializeField] private StateMachineLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<StateMachineLogger>()
                .WithParameter(_logSettings);

            builder.Register<StateMachine>()
                .As<IStateMachine>()
                .AsCallbackListener();
        }
    }
}