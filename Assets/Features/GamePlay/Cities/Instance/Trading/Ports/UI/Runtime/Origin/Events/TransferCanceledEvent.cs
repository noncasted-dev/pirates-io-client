using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events
{
    public class TransferCanceledEvent
    {
        public TransferCanceledEvent(IItem type, ItemOrigin origin)
        {
            Item = type;
            Origin = origin;
        }

        public readonly IItem Item;
        public readonly ItemOrigin Origin;

        public ItemType Type => Item.BaseData.Type;
    }
}