using UnityEngine;

namespace GamePlay.Common.Areas.Common.Runtime
{
    public class AreaRoot : MonoBehaviour
    {
        [SerializeField] private AreaTrigger _trigger;

        private IArea _area;

        private void Awake()
        {
            _area = GetComponent<IArea>();
        }

        private void OnEnable()
        {
            _trigger.Entered += OnEntered;
            _trigger.Exited += OnExited;
        }

        private void OnDisable()
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