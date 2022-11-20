using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Common.Areas.Common.Runtime;
using UnityEngine;

namespace GamePlay.Common.Areas.Implementation.Port
{
    public class PortArea : MonoBehaviour, IArea
    {
        [SerializeField] private CityPort _port;

        public void OnEntered(IAreaInteractor interactor)
        {
            Debug.Log("Entered");

            if (interactor.IsLocal == true)
                _port.Enter(interactor.Resources);

            interactor.OnPortEntered();
        }

        public void OnExited(IAreaInteractor interactor)
        {
            if (interactor.IsLocal == true)
                _port.Exit();

            interactor.OnPortExited();
        }
    }
}