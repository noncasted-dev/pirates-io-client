using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    public interface IPlayerEntityPresenter
    {
        void AssignPlayer(
            RagonEntity entity,
            Transform playerTransform,
            IShipResources shipResources);
        void DestroyPlayer();
    }
}