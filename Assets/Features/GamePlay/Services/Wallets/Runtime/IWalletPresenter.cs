namespace GamePlay.Services.Wallets.Runtime
{
    public interface IWalletPresenter
    {
        void Set(int money);
        void Add(int add);
        void Reduce(int remove);
    }
}