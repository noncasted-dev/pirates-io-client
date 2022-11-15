using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Root.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Factions.Selections.UI.Runtime
{
    [DisallowMultipleComponent]
    public class CitySelectionApprovement : MonoBehaviour
    {
        [SerializeField] private Button _apply;
        [SerializeField] private Button _cancel;
        [SerializeField] private TMP_Text _cityName;
        
        private UniTaskCompletionSource<ApprovementResult> _completion;

        private void OnEnable()
        {
            _apply.onClick.AddListener(OnApplyClicked);
            _cancel.onClick.AddListener(OnCancelClicked);
        }
        
        private void OnDisable()
        {
            _apply.onClick.RemoveListener(OnApplyClicked);
            _cancel.onClick.RemoveListener(OnCancelClicked);
        }

        public async UniTask<ApprovementResult> Approve(CityDefinition city)
        {
            gameObject.SetActive(true);
            _cityName.text = $"Respawn in {city.Name.AsString()}?";
            
            _completion = new UniTaskCompletionSource<ApprovementResult>();

            var result = await _completion.Task;
            
            gameObject.SetActive(false);

            return result;
        }

        private void OnApplyClicked()
        {
            _completion.TrySetResult(ApprovementResult.Applied);
        }

        private void OnCancelClicked()
        {
            _completion.TrySetResult(ApprovementResult.Canceled);
        }
    }
}