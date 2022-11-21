using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using UnityEngine;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public abstract class TradeView : MonoBehaviour
    {
        public abstract void AssignItem(TradableItem tradable, ItemOrigin origin, IPriceProvider priceProvider);
        public abstract void Disable();
    }
}