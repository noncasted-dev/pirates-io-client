using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Common.Areas.Common.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Sounds.Runtime;
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
                MessageBrokerSoundExtensions.TriggerSound(SoundType.CityEnter);

            Msg.Publish(new CityEnteredEvent(_root.Definition));
        }

        public void OnExited(IAreaInteractor interactor)
        {
            interactor.OnCityExited();

            if (interactor.IsLocal == true)
                MessageBrokerSoundExtensions.TriggerSound(SoundType.CityExit);
        }
    }
}