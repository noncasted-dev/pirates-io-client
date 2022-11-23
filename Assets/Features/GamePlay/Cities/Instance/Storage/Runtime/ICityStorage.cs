using System.Collections.Generic;
using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public interface ICityStorage
    {
        IReadOnlyDictionary<ItemType, IItem> Ships { get; }
        void Add(IItem item);
    }
}