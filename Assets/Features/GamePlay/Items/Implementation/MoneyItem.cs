using System;
using GamePlay.Items.Abstract;

namespace GamePlay.Items.Implementation
{
    public class MoneyItem : IItem
    {
        public MoneyItem()
        {
            BaseData = new BaseItemData("Money", 0, ItemType.Money, null, false);
        }
        
        public BaseItemData BaseData { get; }
        public int Count => 0;
        
        public event Action<int> CountChanged;
        
        public void Add(int amount)
        {
            
        }

        public void SetCount(int amount)
        {
            
        }

        public void Reduce(int amount)
        {
            
        }

        public IItem Copy()
        {
            return this;
        }
    }
}