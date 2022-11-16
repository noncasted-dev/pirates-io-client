using GamePlay.Common.SceneObjects.Runtime;
using UnityEngine;

namespace GamePlay.Common.Areas.Common.Runtime
{
    public class AreaRoot : SceneObject
    {
        private IArea _area;
        [SerializeField] private AreaTrigger _trigger;

        protected override void OnAwake()
        {
            _area = GetComponent<IArea>();
        }

        protected override void OnEnabled()
        {
            _trigger.Entered += OnEntered;
            _trigger.Exited += OnExited;
        }

        protected override void OnDisabled()
        {
            _trigger.Entered -= OnEntered;
            _trigger.Exited -= OnExited;
        }

        private void OnEntered(IAreaInteractor interactor)
        {
            _area.OnEntered(interactor);
        }

        private void OnExited(IAreaInteractor interactor)
        {
            _area.OnExited(interactor);
        }
    }
}