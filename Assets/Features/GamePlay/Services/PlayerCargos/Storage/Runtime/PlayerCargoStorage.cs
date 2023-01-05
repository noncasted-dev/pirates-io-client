using System.Collections.Generic;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Events;
using GamePlay.Services.Saves.Definitions;
using Global.Services.FilesFlow.Runtime.Abstract;
using Global.Services.MessageBrokers.Runtime;
using UniRx;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    public class PlayerCargoStorage : MonoBehaviour, IPlayerCargoStorage
    {
        [Inject]
        private void Construct(IFileLoader fileLoader, IFileSaver fileSaver)
        {
            _fileSaver = fileSaver;
            _fileLoader = fileLoader;
        }
    
        private readonly Dictionary<ItemType, IItem> _items = new();
        private IFileLoader _fileLoader;
        private IFileSaver _fileSaver;

        public IReadOnlyDictionary<ItemType, IItem> Items => _items;

        public void Add(IItem item)
        {
            var type = item.BaseData.Type;

            if (_items.ContainsKey(type) == true)
            {
                _items[type].Add(item.Count);
                OnChanged();
                Msg.Publish(new CargoAddEvent(item));

                return;
            }

            _items[type] = item;
            OnChanged();
            
            Msg.Publish(new CargoAddEvent(item));
        }

        public void Reduce(ItemType type, int amount)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items[type].Reduce(amount);
            
            OnChanged();

            if (_items[type].Count == 0)
                Delete(type);
        }

        public void Delete(ItemType type)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items.Remove(type);
            OnChanged();
        }

        public void Clear()
        {
            _items.Clear();
            OnChanged();
        }
        
        public IItem[] ToArray()
        {
            var items = new IItem[_items.Count];

            var counter = 0;

            foreach (var (_, value) in _items)
            {
                items[counter] = value;
                counter++;
            }

            return items;
        }
        
        public int GetWeight()
        {
            var weight = 0;

            foreach (var (_, item) in _items)
                weight += item.Count * item.BaseData.Weight;

            return weight;
        }
        
        public void UpdateState()
        {
            OnChanged();   
        }

        private void OnChanged()
        {
            var data = new CargoChangedEvent(Items, GetWeight());
            Msg.Publish(data);

            var save = _fileLoader.LoadOrCreate<ShipSave>();
            
            save.Items.Clear();
            save.Count.Clear();

            foreach (var (type, item) in _items)
            {
                save.Items.Add(type);
                save.Count.Add(item.Count);
            }

            _fileSaver.Save(save);
        }
    }
}