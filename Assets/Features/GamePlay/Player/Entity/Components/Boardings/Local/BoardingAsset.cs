using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Boardings.Local
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "Boarding",
        menuName = PlayerAssetsPaths.Boarding + "Component")]
    public class BoardingAsset : PlayerComponentAsset
    {
        [SerializeField] private BoardingConfigAsset _config;
        
        public override void Register(IDependencyRegister builder)
        {
            builder.Register<BoardingTargetsSearcher>()
                .As<IBoardingTargetSearcher>()
                .WithParameter(_config);
        }   
    }
}