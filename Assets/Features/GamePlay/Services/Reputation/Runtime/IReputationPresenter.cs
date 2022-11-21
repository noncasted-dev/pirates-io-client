namespace GamePlay.Services.Reputation.Runtime
{
    public interface IReputationPresenter
    {
        int ConvertFromMoney(int spend);
        void Add(int add);
        void Reduce(int reduce);
    }
}