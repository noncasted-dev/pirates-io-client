using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Runs.Logs;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Run",
        menuName = PlayerAssetsPaths.Run + "State")]
    public class RunAsset : PlayerComponentAsset
    {
        [SerializeField] private RunConfigAsset _config;
        [SerializeField] private RunDefinition _definition;
        [SerializeField] private RunLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<RunLogger>()
                .WithParameter(_logSettings);

            builder.Register<RunConfig>()
                .WithParameter(_config)
                .As<IRunConfig>();

            builder.Register<RunInput>()
                .AsCallbackListener();

            builder.Register<SpeedCalculator>()
                .As<ISpeedCalculator>();

            builder.Register<Run>()
                .WithParameter(_definition)
                .As<IRun>();
        }
    }
}