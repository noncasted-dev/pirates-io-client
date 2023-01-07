using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "ShipResources",
        menuName = PlayerAssetsPaths.ShipResources + "Component")]
    public class ShipResourcesAsset : PlayerComponentAsset
    {
        public override void Register(IDependencyRegister builder)
        {
            builder.Register<ShipResources>()
                .As<IShipResources>()
                .As<IShipResourcesPresenter>()
                .AsCallbackListener();
        }
    }
}