using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Floating.Logs;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Floating.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Floating",
        menuName = PlayerAssetsPaths.Floating + "State")]
    public class FloatingStateAsset : PlayerComponentAsset
    {
        [SerializeField] private FloatingStateLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<FloatingStateLogger>()
                .WithParameter(_logSettings);

            builder.Register<FloatingState>()
                .As<IFloatingState>()
                .AsCallbackListener();
        }
    }
}