using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Factions.Selections.UI.Runtime;
using GamePlay.Services.Reputation.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Factions.Selections.Loops.Runtime
{
    public class FactionSelectionLoop : MonoBehaviour, IFactionSelectionLoop
    {
        [Inject]
        private void Construct(
            IFactionSelectionUI ui,
            IReputationPresenter reputation)
        {
            _reputation = reputation;
            _ui = ui;
        }

        private IReputationPresenter _reputation;

        private IFactionSelectionUI _ui;

        public async UniTask<CityDefinition> SelectAsync()
        {
            _ui.Open();

            var city = await _ui.SelectAsync();

            _reputation.OnFactionSelected(city.Faction);

            _ui.Close();

            return city;
        }
    }
}