using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Factions.Selections.UI.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Factions.Selections.Loops.Runtime
{
    public class FactionSelectionLoop : MonoBehaviour, IFactionSelectionLoop
    {
        [Inject]
        private void Construct(IFactionSelectionUI ui)
        {
            _ui = ui;
        }

        private IFactionSelectionUI _ui;

        public async UniTask<CityDefinition> SelectAsync()
        {
            _ui.Open();

            var city = await _ui.SelectAsync();

            _ui.Close();

            return city;
        }
    }
}