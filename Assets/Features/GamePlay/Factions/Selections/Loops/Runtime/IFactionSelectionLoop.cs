using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Root.Runtime;

namespace GamePlay.Factions.Selections.Loops.Runtime
{
    public interface IFactionSelectionLoop
    {
        UniTask<CityDefinition> SelectAsync();
    }
}