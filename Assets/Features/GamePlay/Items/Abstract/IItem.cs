using System;

namespace GamePlay.Items.Abstract
{
    public interface IItem
    {
        BaseItemData BaseData { get; }
        int Count { get; }
        event Action<int> CountChanged;

        void Add(int amount);
        void Reduce(int amount);
    }
}