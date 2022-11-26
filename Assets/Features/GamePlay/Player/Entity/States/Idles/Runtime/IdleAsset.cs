using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Idles.Logs;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.States.Idles.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Idle",
        menuName = PlayerAssetsPaths.Idle + "State")]
    public class IdleAsset : PlayerComponentAsset
    {
        [SerializeField]  private IdleDefinition _definition;
        [SerializeField]  private IdleLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<IdleLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.Register<Idle>(Lifetime.Scoped)
                .As<IIdle>()
                .WithParameter(_definition);
        }
    }
}