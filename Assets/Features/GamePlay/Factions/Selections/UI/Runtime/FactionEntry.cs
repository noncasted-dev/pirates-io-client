using System;
using GamePlay.Cities.Instance.Root.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Factions.Selections.UI.Runtime
{
    [DisallowMultipleComponent]
    public class FactionEntry : MonoBehaviour
    {
        [SerializeField] private CityDefinition _definition;
        [SerializeField] private Button _button;

        public event Action<CityDefinition> Selected;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Selected?.Invoke(_definition);
        }
    }
}