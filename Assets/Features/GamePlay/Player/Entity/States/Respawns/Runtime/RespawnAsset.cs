using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Respawns.Logs;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Respawn",
        menuName = PlayerAssetsPaths.Respawn + "State")]
    public class RespawnAsset : PlayerComponentAsset
    {
        [SerializeField] private RespawnConfigAsset _config;
        [SerializeField] private RespawnDefinition _definition;
        [SerializeField] private RespawnLogSettings _logSettings;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<RespawnLogger>()
                .WithParameter(_logSettings);

            builder.Register<Respawn>()
                .As<IRespawn>()
                .WithParameter(_config)
                .WithParameter(_definition);
        }
    }
}