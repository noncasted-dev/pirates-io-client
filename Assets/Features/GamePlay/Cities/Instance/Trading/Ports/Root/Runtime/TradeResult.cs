using GamePlay.Player.Entity.Components.Definition;
using UnityEngine;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public readonly struct TradeResult
    {
        public TradeResult(ShipItem shipItem)
        {
            if (shipItem == null)
                IsContainingShip = false;
            else
                IsContainingShip = true;
            Debug.Log($"Create result: {IsContainingShip}");

            Ship = shipItem;
        }
        
        public readonly bool IsContainingShip;
        public readonly ShipItem Ship;
    }
}