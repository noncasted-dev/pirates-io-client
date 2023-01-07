using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Components.InertialMovements.Logs;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "InertialMovement",
        menuName = PlayerAssetsPaths.InertialMovement + "Component")]
    public class InertialMovementAsset : PlayerComponentAsset
    {
        [SerializeField] private InertialMovementConfigAsset _config;
        [SerializeField] private InertialMovementLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<InertialMovementLogger>()
                .WithParameter(_logSettings);

            builder.Register<InertialMovementInput>()
                .AsCallbackListener();

            builder.Register<InertialMovement>()
                .WithParameter(_config)
                .As<IInertialMovement>()
                .AsCallbackListener();
        }
    }
}