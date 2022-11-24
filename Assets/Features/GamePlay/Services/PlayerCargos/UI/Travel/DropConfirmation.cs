using System;
using Cysharp.Threading.Tasks;
using GamePlay.Items.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    [DisallowMultipleComponent]
    public class DropConfirmation : MonoBehaviour
    {
        [SerializeField] private Button _cancel;
        [SerializeField] private Button _apply;

        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _count;
        
        private UniTaskCompletionSource<DropConfirmationResultType> _completion;

        private void OnEnable()
        {
            _cancel.onClick.AddListener(OnCancelClicked);
            _apply.onClick.AddListener(OnApplyClicked);
            _slider.onValueChanged.AddListener(OnSliderValueChange);
        }

        private void OnDisable()
        {
            _cancel.onClick.RemoveListener(OnCancelClicked);
            _apply.onClick.RemoveListener(OnApplyClicked);
            _slider.onValueChanged.RemoveListener(OnSliderValueChange);

            Cancel();
        }

        public async UniTask<DropConfirmationResult> Confirm(IItem item)
        {
            gameObject.SetActive(true);
            
            _slider.minValue = 1;
            _slider.maxValue = item.Count;
            
            _completion = new UniTaskCompletionSource<DropConfirmationResultType>();

            var result = await _completion.Task;
            _completion = null;

            gameObject.SetActive(false);

            return result switch
            {
                DropConfirmationResultType.Applied => DropConfirmationResult.Applied((int)_slider.value),
                DropConfirmationResultType.Canceled => DropConfirmationResult.Canceled(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void Cancel()
        {
            _completion?.TrySetCanceled();
            gameObject.SetActive(false);
        }

        private void OnCancelClicked()
        {
            _completion.TrySetResult(DropConfirmationResultType.Canceled);
        }

        private void OnApplyClicked()
        {
            _completion.TrySetResult(DropConfirmationResultType.Applied);
        }

        private void OnSliderValueChange(float value)
        {
            var count = (int)value;
            _count.text = count.ToString();
        }
    }
}