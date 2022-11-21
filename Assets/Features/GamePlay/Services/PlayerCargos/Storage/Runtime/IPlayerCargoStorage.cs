using System;
using System.Collections.Generic;
using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    public interface IPlayerCargoStorage
    {
        void Add(IItem item);
        void Reduce(ItemType type, int amount);
        void Delete(ItemType type);
        IItem[] ToArray();
        int GetWeight();

        event Action Changed; 

        public IReadOnlyDictionary<ItemType, IItem> Items { get; }
    }
}