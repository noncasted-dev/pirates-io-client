using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Deaths.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.StatePrefix + "Death",
        menuName = PlayerAssetsPaths.Death + "State")]
    public class DeathAsset : PlayerComponentAsset
    {
        [SerializeField] private DeathStateDefinition _definition;

        public override void Register(IDependencyRegister builder)
        {
            builder.Register<Death>()
                .WithParameter(_definition)
                .As<IDeath>()
                .AsCallbackListener();
        }
    }
}