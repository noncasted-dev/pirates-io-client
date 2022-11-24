using System.Collections.Generic;
using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.Storage.Events
{
    public readonly struct CargoChangedEvent
    {
        public CargoChangedEvent(IReadOnlyDictionary<ItemType, IItem> items, int weight)
        {
            Items = items;
            Weight = weight;
        }
        
        public readonly IReadOnlyDictionary<ItemType, IItem> Items;
        public readonly int Weight;

        public IItem[] ToArray()
        {
            var items = new IItem[Items.Count];

            var counter = 0;

            foreach (var (_, value) in Items)
            {
                items[counter] = value;
                counter++;
            }

            return items;
        }
    }
}