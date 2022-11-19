using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public class TradableItem
    {
        public TradableItem(IItem item, int cost)
        {
            Item = item;
            Cost = cost;
        }
        
        public readonly IItem Item;
        public readonly int Cost;
    }
}