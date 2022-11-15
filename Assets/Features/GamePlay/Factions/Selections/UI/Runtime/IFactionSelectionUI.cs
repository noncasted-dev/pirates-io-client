using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Root.Runtime;

namespace GamePlay.Factions.Selections.UI.Runtime
{
    public interface IFactionSelectionUI
    {
        void Open();
        void Close();
        UniTask<CityDefinition> SelectAsync();
    }
}