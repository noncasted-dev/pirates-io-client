using System;
using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Root.Runtime;
using UnityEngine;

namespace GamePlay.Factions.Selections.UI.Runtime
{
    public class FactionSelectionUI : MonoBehaviour, IFactionSelectionUI
    {
        [SerializeField] private GameObject _body;

        [SerializeField] private CitySelectionApprovement _approvement;
        [SerializeField] private FactionEntry[] _entries;

        private UniTaskCompletionSource<CityDefinition> _selectionCompletion;

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

        public void Open()
        {
            _body.SetActive(true);
        }

        public void Close()
        {
            _body.SetActive(false);
        }

        public async UniTask<CityDefinition> SelectAsync()
        {
            var result = ApprovementResult.Canceled;

            while (result == ApprovementResult.Canceled)
            {
                _selectionCompletion = new UniTaskCompletionSource<CityDefinition>();
                
                Debug.Log("Await");
                var city = await _selectionCompletion.Task;
                result = await _approvement.Approve(city);

                if (result == ApprovementResult.Applied)
                    return city;
            }

            throw new ArgumentException();
        }

        private void OnSelectedClicked(CityDefinition city)
        {
            Debug.Log("On click received");

            _selectionCompletion.TrySetResult(city);
        }
    }
}