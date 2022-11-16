using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Respawns.Logs;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Respawn",
        menuName = PlayerAssetsPaths.Respawn + "State")]
    public class RespawnAsset : PlayerComponentAsset
    {
        [SerializeField] [EditableObject] private RespawnConfigAsset _config;
        [SerializeField] [EditableObject] private RespawnDefinition _definition;
        [SerializeField] [EditableObject] private RespawnLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<RespawnLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.Register<Respawn>(Lifetime.Scoped)
                .As<IRespawn>()
                .WithParameter(_config)
                .WithParameter(_definition);
        }
    }
}