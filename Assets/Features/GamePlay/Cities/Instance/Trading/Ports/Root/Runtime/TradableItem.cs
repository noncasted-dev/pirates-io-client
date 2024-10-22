﻿using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public class TradableItem
    {
        public TradableItem(IItem item, int cost)
        {
            Item = item;
            Cost = cost;

            Type = item.BaseData.Type;
        }

        public readonly int Cost;

        public readonly IItem Item;
        public readonly ItemType Type;
    }
}