using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Factions.Common;

namespace GamePlay.Services.Reputation.Runtime
{
    public interface IReputationPresenter
    {
        int ConvertFromMoney(int spend);
        void Add(int add);
        void Reduce(int reduce);
        void OnFactionSelected(FactionType faction);
    }
}