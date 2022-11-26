using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Death",
        menuName = PlayerAssetsPaths.Death + "State")]
    public class DeathAsset : PlayerComponentAsset
    {
        [SerializeField]  private DeathStateDefinition _definition;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<Death>(Lifetime.Scoped)
                .WithParameter(_definition)
                .As<IDeath>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<Death>());
        }
    }
}