using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events
{
    public class TradeLot
    {
        public TradeLot(IItem item)
        {
            Item = item;
            Type = item.BaseData.Type;
        }
        
        public readonly IItem Item;
        public readonly ItemType Type;

        private int _count;
        private int _totalPrice;
        
        public int Count => _count;
        public int TotalPrice => _totalPrice;

        public void SetData(int count, int totalPrice)
        {
            _count = count;
            _totalPrice = totalPrice;
        }
    }
}