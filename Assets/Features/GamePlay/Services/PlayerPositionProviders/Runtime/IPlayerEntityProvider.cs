using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using UnityEngine;

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    public interface IPlayerEntityProvider
    {
        Vector2 Position { get; }
        IShipResources Resources { get; }
    }
}