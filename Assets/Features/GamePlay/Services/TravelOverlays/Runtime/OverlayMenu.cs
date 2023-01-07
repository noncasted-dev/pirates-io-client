using GamePlay.Services.Maps.Runtime;
using GamePlay.Services.PlayerCargos.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    public class OverlayMenu : MonoBehaviour
    {
        [Inject]
        private void Construct(PlayerCargoUI cargo, Map map)
        {
            _map = map;
            _cargo = cargo;
        }

        [SerializeField] private Settings _settings;

        [SerializeField] private Button _cargoButton;
        [SerializeField] private Button _mapButton;
        [SerializeField] private Button _settingsButton;

        private PlayerCargoUI _cargo;
        private Map _map;

        private void OnEnable()
        {
            _cargoButton.onClick.AddListener(OnCargoClicked);
            _mapButton.onClick.AddListener(OnMapClicked);
            _settingsButton.onClick.AddListener(OnSettingsClicked);
        }

        private void OnDisable()
        {
            _cargoButton.onClick.RemoveListener(OnCargoClicked);
            _mapButton.onClick.RemoveListener(OnMapClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsClicked);
        }

        private void OnCargoClicked()
        {
            _cargo.Switch();
        }

        private void OnMapClicked()
        {
            _map.Switch();
        }

        private void OnSettingsClicked()
        {
            _settings.Switch();
        }
    }
}