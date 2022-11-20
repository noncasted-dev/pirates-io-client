using GamePlay.Common.Areas.Common.Runtime;
using GamePlay.Player.Entity.Components.ActionsStates.Runtime;
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
            IShipResources resources)
        {
            _resources = resources;
            _actionsStatePresenter = actionsStatePresenter;
        }

        private IActionsStatePresenter _actionsStatePresenter;
        private IShipResources _resources;

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
    }
}