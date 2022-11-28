using System;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.Reputation.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    public class OverlayShipView : MonoBehaviour
    {
        [Inject]
        private void Construct(IReputation reputation, IPlayerEntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
            _reputation = reputation;
        }

        [SerializeField] private Image _flag;
        [SerializeField] private TMP_Text _reputationAmount;
        [SerializeField] private TMP_Text _sail;
        [SerializeField] private TMP_Text _load;
        [SerializeField] private TMP_Text _speed;

        private IDisposable _reputationListener;
        private IDisposable _resourcesListener;

        private IReputation _reputation;
        private IPlayerEntityProvider _entityProvider;

        private void OnEnable()
        {
            UpdateData();

            _reputationListener =
                MessageBroker.Default.Receive<ReputationChangedEvent>().Subscribe(OnReputationChanged);
            
            _resourcesListener =
                MessageBroker.Default.Receive<ResourcesChangedEvent>().Subscribe(OnResourcesChanged);
        }

        private void OnDisable()
        {
            _reputationListener?.Dispose();
            _resourcesListener?.Dispose();
        }

        private void OnReputationChanged(ReputationChangedEvent data)
        {
            _reputationAmount.text = data.Reputation.ToString();
        }

        private void OnResourcesChanged(ResourcesChangedEvent data)
        {
            var resources = data.Resources;

            OnWeightChanged(resources.Weight, resources.MaxWeight);
            OnSpeedChanged(resources.Speed);
            OnSailChanged(resources.Sail);
        }
        
        private void UpdateData()
        {
            if (_reputation == null || _entityProvider == null || _entityProvider.Resources == null)
                return;

            _flag.sprite = _reputation.Flag;
            _reputationAmount.text = _reputation.Value.ToString();

            var resources = _entityProvider.Resources;

            OnWeightChanged(resources.Weight, resources.MaxWeight);
            OnSpeedChanged(resources.Speed);
            OnSailChanged(resources.Sail);
        }

        private void OnSpeedChanged(int current)
        {
            _speed.text = current.ToString();
        }
   
        private void OnWeightChanged(int current, int max)
        {
            var load = (current / (float)max).ToString("#0%");
            _load.text = load;
        }

        private void OnSailChanged(int current)
        {
            _sail.text = $"{current}%";
        }
    }
}