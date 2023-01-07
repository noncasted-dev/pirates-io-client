using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.DamageProcessors.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "DamageProcessor",
        menuName = PlayerAssetsPaths.Damage + "Component")]
    public class DamageProcessorAsset : PlayerComponentAsset
    {
        [SerializeField] private DamageConfigAsset _config;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<DamageProcessor>()
                .WithParameter(_config)
                .AsSelfResolvable();
        }
    }
}