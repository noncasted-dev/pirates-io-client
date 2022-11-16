using System;
using GamePlay.Items.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    [DisallowMultipleComponent]
    public class DropMenu : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private Slider _slider;

        private IItem _selected;

        public event Action<IItem, int> Dropped;

        public void Enable(IItem item)
        {
            if (_selected != null)
                _selected.CountChanged -= OnCountChanged;

            _selected = item;
            _selected.CountChanged += OnCountChanged;

            _input.onValueChanged.AddListener(OnInputChanged);
            _slider.onValueChanged.AddListener(OnSliderChanged);
            _button.onClick.AddListener(OnDropClicked);

            gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (_selected != null)
                _selected.CountChanged -= OnCountChanged;

            _input.onValueChanged.RemoveListener(OnInputChanged);
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
            _button.onClick.RemoveListener(OnDropClicked);

            _selected = null;
            gameObject.SetActive(false);
        }

        private void OnDropClicked()
        {
            if (_selected == null)
            {
                Debug.LogError("No item to drop selected");
                return;
            }

            var count = int.Parse(_input.text);

            Dropped?.Invoke(_selected, count);
        }

        private void OnCountChanged(int count)
        {
            _slider.maxValue = count;
        }

        private void OnInputChanged(string input)
        {
            if (_selected == null)
                return;

            var count = int.Parse(input);

            var progress = count / (float)_selected.Count;

            _slider.value = progress;

            if (count <= _selected.Count)
                return;

            _input.text = _selected.Count.ToString();
        }

        private void OnSliderChanged(float value)
        {
            var count = (int)value;

            _input.text = count.ToString();
        }
    }
}