namespace GamePlay.Services.Reputation.Runtime
{
    public interface IReputationPresenter
    {
        void Add(int add);
        void Reduce(int reduce);
    }
}