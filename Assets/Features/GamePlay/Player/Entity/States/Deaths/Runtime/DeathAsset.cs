#region

using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Death",
        menuName = PlayerAssetsPaths.Death + "State")]
    public class DeathAsset : PlayerComponentAsset
    {
        [SerializeField] [EditableObject] private DeathStateDefinition _definition;

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<Death>(Lifetime.Scoped)
                .WithParameter(_definition)
                .As<IDeath>();
        }
    }
}