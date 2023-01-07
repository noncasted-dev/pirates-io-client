using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Idles.Logs;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Idles.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Idle",
        menuName = PlayerAssetsPaths.Idle + "State")]
    public class IdleAsset : PlayerComponentAsset
    {
        [SerializeField] private IdleDefinition _definition;
        [SerializeField] private IdleLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<IdleLogger>()
                .WithParameter(_logSettings);

            builder.Register<Idle>()
                .As<IIdle>()
                .WithParameter(_definition);
        }
    }
}