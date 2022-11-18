using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using GamePlay.Player.Entity.States.None.Logs;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.States.None.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "None",
        menuName = PlayerAssetsPaths.None + "State")]
    public class NoneAsset : PlayerComponentAsset
    {
        [SerializeField] [EditableObject] private NoneLogSettings _logSettings;
        [SerializeField] [EditableObject] private NoneDefinition _definition;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<NoneLogger>(Lifetime.Scoped)
                .WithParameter(_logSettings);

            builder.Register<None>(Lifetime.Scoped)
                .WithParameter(_definition)
                .As<INone>();
        }
    }
}