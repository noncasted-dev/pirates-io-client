using GamePlay.Common.Areas.Common.Runtime;
using UnityEngine;

namespace GamePlay.Common.Areas.Implementation.Port
{
    public class PortArea : MonoBehaviour, IArea
    {
        public void OnEntered(IAreaInteractor interactor)
        {
            interactor.OnPortEntered();
        }

        public void OnExited(IAreaInteractor interactor)
        {
            interactor.OnPortExited();
        }
    }
}