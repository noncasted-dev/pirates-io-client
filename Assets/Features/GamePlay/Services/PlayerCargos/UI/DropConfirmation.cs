using System;
using Cysharp.Threading.Tasks;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.UI.Events;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.PlayerCargos.UI
{
    [DisallowMultipleComponent]
    public class DropConfirmation : MonoBehaviour
    {
        [SerializeField] private Button _cancel;
        [SerializeField] private Button _apply;

        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;

        private UniTaskCompletionSource<DropConfirmationResultType> _completion;
        private IItem _item;

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
            _item = item;
            gameObject.SetActive(true);

            _name.text = item.BaseData.Name;
            _icon.sprite = item.BaseData.Icon;
            
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

            var data = new ItemDropCountChangedEvent(_item, count);
            MessageBroker.Default.Publish(data);
        }
    }
}