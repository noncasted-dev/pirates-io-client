using System;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Services.Wallets.Runtime
{
    public class Wallet : MonoBehaviour, IWallet, IWalletPresenter
    {
        [SerializeField] [ReadOnly] private int _money;

        public int Money => _money;

        public event Action<int> Changed;

        public void Add(int add)
        {
            if (add < 0)
            {
                Debug.LogError("Money interactions should be greater than zero.");
                return;
            }

            _money += add;

            Changed?.Invoke(_money);
        }

        public void Remove(int remove)
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

            Changed?.Invoke(_money);
        }
    }
}