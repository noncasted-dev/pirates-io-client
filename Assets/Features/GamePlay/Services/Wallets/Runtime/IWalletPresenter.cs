namespace GamePlay.Services.Wallets.Runtime
{
    public interface IWalletPresenter
    {
        void Add(int add);
        void Reduce(int remove);
    }
}