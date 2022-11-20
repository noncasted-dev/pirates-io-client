using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ComponentPrefix + "ShipResources",
        menuName = PlayerAssetsPaths.ShipResources + "Component")]
    public class ShipResourcesAsset : PlayerComponentAsset
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<ShipResources>(Lifetime.Scoped)
                .As<IShipResources>()
                .As<IShipResourcesPresenter>();
        }
    }
}