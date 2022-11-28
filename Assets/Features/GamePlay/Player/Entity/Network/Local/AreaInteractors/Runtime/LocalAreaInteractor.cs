using GamePlay.Common.Areas.Common.Runtime;
using GamePlay.Player.Entity.Components.ActionsStates.Runtime;
using GamePlay.Player.Entity.Components.DamageProcessors.Runtime;
using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Network.Local.AreaInteractors.Runtime
{
    [DisallowMultipleComponent]
    public class LocalAreaInteractor : MonoBehaviour, IAreaInteractor
    {
        [Inject]
        private void Construct(
            IActionsStatePresenter actionsStatePresenter,
            IShipResources resources,
            ISpeedCalculator speedCalculator,
            DamageProcessor damageProcessor)
        {
            _damageProcessor = damageProcessor;
            _speedCalculator = speedCalculator;
            _resources = resources;
            _actionsStatePresenter = actionsStatePresenter;
        }

        private IActionsStatePresenter _actionsStatePresenter;
        private IShipResources _resources;
        private ISpeedCalculator _speedCalculator;
        private DamageProcessor _damageProcessor;

        public bool IsLocal => true;
        public IShipResources Resources => _resources;

        public void OnCityEntered()
        {
            _actionsStatePresenter.DisableShooting();
        }

        public void OnCityExited()
        {
            _actionsStatePresenter.EnableShooting();
        }

        public void OnPortEntered()
        {
        }

        public void OnPortExited()
        {
        }

        public void OnShallowEntered()
        {
            if (_resources.IsShallowIgnored == true)
                return;
            
            _speedCalculator.OnShallowEntered();
            _damageProcessor.OnShallowEntered();
        }

        public void OnShallowExited()
        {
            if (_resources.IsShallowIgnored == true)
                return;

            _speedCalculator.OnShallowExited();
            _damageProcessor.OnShallowExited();
        }
    }
}