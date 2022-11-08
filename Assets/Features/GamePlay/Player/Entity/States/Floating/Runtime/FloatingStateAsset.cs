using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.Floating.Logs;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.States.Floating.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Floating",
        menuName = PlayerAssetsPaths.Floating + "State")]
    public class FloatingStateAsset : PlayerComponentAsset
    {
        [SerializeField] [EditableObject] private FloatingStateLogSettings _logSettings;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<FloatingStateLogger>(Lifetime.Scoped).WithParameter("settings", _logSettings);
            builder.Register<FloatingState>(Lifetime.Scoped)
                .As<IFloatingState>()
                .AsSelf();
        }

        public override void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(resolver.Resolve<FloatingState>());
        }
    }
}