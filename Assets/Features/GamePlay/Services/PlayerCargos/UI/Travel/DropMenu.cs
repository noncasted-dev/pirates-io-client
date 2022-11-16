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
        private bool _isChangedThisFrame;

        public event Action<IItem, int> Dropped;

        public void Enable(IItem item)
        {
            if (_selected != null)
                _selected.CountChanged -= OnCountChanged;

            _selected = item;
            _selected.CountChanged += OnCountChanged;
            
            _slider.maxValue = item.Count;
            _slider.value = 0;
            _input.text = "0";

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
            
            if (count == 0)
                return;

            Dropped?.Invoke(_selected, count);
            Disable();
        }

        private void OnCountChanged(int count)
        {
            _slider.maxValue = count;
        }

        private void OnInputChanged(string input)
        {
            if (_selected == null)
                return;
            
            if (_isChangedThisFrame == true)
                return;

            var count = int.Parse(input);

            _slider.value = _selected.Count;

            if (count <= _selected.Count)
                return;

            _isChangedThisFrame = true;
            _input.text = _selected.Count.ToString();
        }

        private void OnSliderChanged(float value)
        {
            if (_isChangedThisFrame == true)
                return;
            
            var count = (int)value;

            _isChangedThisFrame = true;
            _input.text = count.ToString();
        }

        private void Update()
        {
            _isChangedThisFrame = false;
        }
    }
}