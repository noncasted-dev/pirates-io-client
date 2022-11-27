using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Components.DamageProcessors.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "DamageProcessor",
        menuName = PlayerAssetsPaths.Damage + "Component")]
    public class DamageProcessorAsset : PlayerComponentAsset
    {
        [SerializeField]  private DamageConfigAsset _config;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<DamageProcessor>(Lifetime.Scoped)
                .WithParameter(_config)
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            resolver.Resolve<DamageProcessor>();
        }
    }
}