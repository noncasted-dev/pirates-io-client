using System;
using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Root.Runtime;
using Global.Services.UiStateMachines.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Factions.Selections.UI.Runtime
{
    public class FactionSelectionUI : MonoBehaviour, IFactionSelectionUI, IUiState
    {
        [Inject]
        private void Construct(
            IUiStateMachine uiStateMachine,
            UiConstraints constraints)
        {
            _constraints = constraints;
            _uiStateMachine = uiStateMachine;
        }
        
        [SerializeField] private GameObject _body;

        [SerializeField] private CitySelectionApprovement _approvement;
        [SerializeField] private FactionEntry[] _entries;

        private UniTaskCompletionSource<CityDefinition> _selectionCompletion;
        
        private UiConstraints _constraints;
        private IUiStateMachine _uiStateMachine;

        public UiConstraints Constraints => _constraints;
        public string Name => "FactionSelection";

        private void OnEnable()
        {
            foreach (var entry in _entries)
                entry.Selected += OnSelectedClicked;
        }

        private void OnDisable()
        {
            foreach (var entry in _entries)
                entry.Selected -= OnSelectedClicked;
        }
        
        public void Recover()
        {
            _body.SetActive(true);
        }

        public void Exit()
        {
        }

        public void Open()
        {
            _uiStateMachine.EnterAsSingle(this);
            _body.SetActive(true);
        }

        public void Close()
        {
            _uiStateMachine.Exit(this);
            _body.SetActive(false);
        }

        public async UniTask<CityDefinition> SelectAsync()
        {
            var result = ApprovementResult.Canceled;

            while (result == ApprovementResult.Canceled)
            {
                _selectionCompletion = new UniTaskCompletionSource<CityDefinition>();

                var city = await _selectionCompletion.Task;
                result = await _approvement.Approve(city);

                if (result == ApprovementResult.Applied)
                    return city;
            }

            throw new ArgumentException();
        }

        private void OnSelectedClicked(CityDefinition city)
        {
            _selectionCompletion.TrySetResult(city);
        }
    }
}