using GamePlay.Common.Areas.Common.Runtime;
using UnityEngine;

namespace GamePlay.Common.Areas.Implementation.Cities
{
    public class CityArea : MonoBehaviour, IArea
    {
        public void OnEntered(IAreaInteractor interactor)
        {
            interactor.OnCityEntered();
        }

        public void OnExited(IAreaInteractor interactor)
        {
            interactor.OnAreaExited();
        }
    }
}