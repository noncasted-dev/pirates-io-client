using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Common.Areas.Common.Runtime;
using Global.Services.Sounds.Runtime;
using UniRx;
using UnityEngine;

namespace GamePlay.Common.Areas.Implementation.Cities
{
    public class CityArea : MonoBehaviour, IArea
    {
        [SerializeField] private CityRoot _root;
        
        public void OnEntered(IAreaInteractor interactor)
        {
            interactor.OnCityEntered();

            if (interactor.IsLocal == true)
                MessageBroker.Default.TriggerSound(SoundType.CityEnter);
            
            MessageBroker.Default.Publish(new CityEnteredEvent(_root.Definition));
        }

        public void OnExited(IAreaInteractor interactor)
        {
            interactor.OnCityExited();
            
            if (interactor.IsLocal == true)
                MessageBroker.Default.TriggerSound(SoundType.CityExit);
        }
    }
}