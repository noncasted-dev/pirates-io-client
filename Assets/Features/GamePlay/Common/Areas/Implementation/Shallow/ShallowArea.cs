using GamePlay.Common.Areas.Common.Runtime;
using UnityEngine;

namespace GamePlay.Common.Areas.Implementation.Shallow
{
    public class ShallowArea : MonoBehaviour, IArea
    {
        public void OnEntered(IAreaInteractor interactor)
        {
            interactor.OnShallowEntered();
        }

        public void OnExited(IAreaInteractor interactor)
        {
            interactor.OnShallowExited();
        }
    }
}