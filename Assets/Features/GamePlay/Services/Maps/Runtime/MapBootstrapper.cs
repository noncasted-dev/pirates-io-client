using Common.DiContainer.Abstract;
using UnityEngine;

namespace GamePlay.Services.Maps.Runtime
{
    [DisallowMultipleComponent]
    public class MapBootstrapper : MonoBehaviour
    {
        [SerializeField] private MapPlayerMover _mover;
        [SerializeField] private MonoBehaviour[] _callbacksListeners;
        
        public void Bootstrap(IDependencyRegister builder, ICallbackRegister callbacks)
        {
            builder.Inject(_mover);

            foreach (var listener in _callbacksListeners)
                callbacks.Listen(listener);
        }
    }
}