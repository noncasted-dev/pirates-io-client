using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events
{
    public class TransferRequestedEvent
    {
        public TransferRequestedEvent(TradableItem tradable, ItemOrigin origin)
        {
            Tradable = tradable;
            Origin = origin;
        }

        public readonly ItemOrigin Origin;

        public readonly TradableItem Tradable;

        public ItemType Type => Tradable.Item.BaseData.Type;
    }
}