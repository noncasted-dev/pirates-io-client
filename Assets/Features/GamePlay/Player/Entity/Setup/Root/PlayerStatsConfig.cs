using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Setup.Root
{
    [DisallowMultipleComponent]
    public class PlayerStatsConfig : MonoBehaviour, IAwakeCallback
    {
        [Inject]
        private void Construct(
            IShipResourcesPresenter resources,
            IHealth health)
        {
            _health = health;
            _resources = resources;
        }

        [SerializeField] private string _name = "Frigate";
        
        [SerializeField] private int _baseHealth;

        [SerializeField] private int _baseMaxSpeed;

        [SerializeField] private int _baseMaxWeight;

        [SerializeField] private int _baseMaxCannons;
        [SerializeField] private int _baseCannons;

        [SerializeField] private int _baseTeam;
        [SerializeField] private int _baseMaxTeam;

        private IShipResourcesPresenter _resources;
        private IHealth _health;

        public void OnAwake()
        {
            _health.SetMaxHealth(_baseHealth);

            _resources.SetName(_name);
            
            _resources.SetMaxWeight(_baseMaxWeight);
            _resources.SetMaxSpeed(_baseMaxSpeed);

            _resources.SetCannons(_baseCannons);
            _resources.SetMaxCannons(_baseMaxCannons);

            _resources.SetTeam(_baseTeam);
            _resources.SetMaxTeam(_baseMaxTeam);
        }
    }
}