using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.None.Logs;
using UnityEngine;

namespace GamePlay.Player.Entity.States.None.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "None",
        menuName = PlayerAssetsPaths.None + "State")]
    public class NoneAsset : PlayerComponentAsset
    {
        [SerializeField] private NoneLogSettings _logSettings;
        [SerializeField] private NoneDefinition _definition;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<NoneLogger>()
                .WithParameter(_logSettings);

            builder.Register<None>()
                .WithParameter(_definition)
                .As<INone>();
        }
    }
}