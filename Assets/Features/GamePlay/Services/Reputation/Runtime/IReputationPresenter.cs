namespace GamePlay.Services.Reputation.Runtime
{
    public interface IReputationPresenter
    {
        void ConvertFromMoney(int spend);
        void Add(int add);
        void Reduce(int reduce);
    }
}