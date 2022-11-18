using Global.Services.UiStateMachines.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    public class TravelOverlay : MonoBehaviour, IUiState, ITravelOverlay
    {
        [Inject]
        private void Construct(
            IUiStateMachine stateMachine,
            UiConstraints constraints)
        {
            _stateMachine = stateMachine;
            _constraints = constraints;
        }
        
        [SerializeField] private GameObject _body;
        
        private UiConstraints _constraints;
        private IUiStateMachine _stateMachine;

        public UiConstraints Constraints => _constraints;
        public string Name => "TravelOverlay";

        private void Awake()
        {
            _body.SetActive(false);
        }

        public void Open()
        {
            _body.SetActive(true);

            _stateMachine.EnterAsSingle(this);
        }
        
        public void Recover()
        {
            _body.SetActive(true);
        }

        public void Exit()
        {
            _body.SetActive(false);
        }
    }
}