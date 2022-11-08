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
        [SerializeField] [EditableObject] private RespawnLogSettings _logSettings;
        [SerializeField] [EditableObject] private RespawnAnimationTriggerAsset _animation;
        [SerializeField] [EditableObject] private RespawnConfigAsset _configAsset;
        [SerializeField] [EditableObject] private RespawnDefinition _definition;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<RespawnLogger>(Lifetime.Scoped)
                .WithParameter("settings", _logSettings);

            var animation = _animation.CreateTrigger();

            builder.Register<Respawn>(Lifetime.Scoped)
                .As<IRespawn>()
                .WithParameter("definition", _definition)
                .WithParameter("animation", animation)
                .WithParameter("config", _configAsset);
        }
    }
}