using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime
{
    public class TransferRequestedEvent
    {
        public TransferRequestedEvent(TradableItem tradable, ItemOrigin origin)
        {
            Tradable = tradable;
            Origin = origin;
        }
        
        public readonly TradableItem Tradable;
        public readonly ItemOrigin Origin;

        public ItemType Type => Tradable.Item.BaseData.Type;
    }
}