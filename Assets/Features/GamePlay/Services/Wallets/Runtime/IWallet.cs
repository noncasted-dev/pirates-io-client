using System;

namespace GamePlay.Services.Wallets.Runtime
{
    public interface IWallet
    {
        int Money { get; }

        event Action<int> MoneyChanged;
    }
}