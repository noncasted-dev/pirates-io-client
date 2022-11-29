using GamePlay.Common.Areas.Common.Runtime;
using Global.Services.Sounds.Runtime;
using UniRx;
using UnityEngine;

namespace GamePlay.Common.Areas.Implementation.Cities
{
    public class CityArea : MonoBehaviour, IArea
    {
        public void OnEntered(IAreaInteractor interactor)
        {
            interactor.OnCityEntered();

            if (interactor.IsLocal == true)
                MessageBroker.Default.TriggerSound(SoundType.CityEnter);
        }

        public void OnExited(IAreaInteractor interactor)
        {
            interactor.OnCityExited();
            
            if (interactor.IsLocal == true)
                MessageBroker.Default.TriggerSound(SoundType.CityExit);
        }
    }
}