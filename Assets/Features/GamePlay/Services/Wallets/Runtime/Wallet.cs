using System;
using GamePlay.Services.Saves.Definitions;
using Global.Services.FilesFlow.Runtime.Abstract;
using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.Wallets.Runtime
{
    public class Wallet : MonoBehaviour, IWallet, IWalletPresenter
    {
        [Inject]
        private void Construct(IFileLoader fileLoader, IFileSaver fileSaver)
        {
            _fileSaver = fileSaver;
            _fileLoader = fileLoader;
        }
        
        [SerializeField] [ReadOnly] private int _money = 1000000;
        private IFileLoader _fileLoader;
        private IFileSaver _fileSaver;

        public int Money => _money;

        public event Action<int> Changed;

        public void Set(int money)
        {
            _money = money;
        }

        public void Add(int add)
        {
            if (add < 0)
            {
                Debug.LogError("Money interactions should be greater than zero.");
                return;
            }

            _money += add;
            
            var save = _fileLoader.LoadOrCreate<ShipSave>();
            save.Money = _money;
            _fileSaver.Save(save);

            Changed?.Invoke(_money);
        }

        public void Reduce(int remove)
        {
            if (remove < 0)
            {
                Debug.LogError("Money interactions should be greater than zero.");
                return;
            }

            _money -= remove;

            if (_money < 0)
            {
                Debug.Log("Money dropped below zero.");
                _money = 0;
            }
            
            var save = _fileLoader.LoadOrCreate<ShipSave>();
            save.Money = _money;
            _fileSaver.Save(save);

            Changed?.Invoke(_money);
        }
    }
}