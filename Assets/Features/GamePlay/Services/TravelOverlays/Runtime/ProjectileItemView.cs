using System;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Selector.Runtime;
using Global.Services.MessageBrokers.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    [DisallowMultipleComponent]
    public class ProjectileItemView : MonoBehaviour
    {
        [Inject]
        private void Construct(IProjectileSelector selector)
        {
            _selector = selector;
        }

        [SerializeField] private GameObject _enabledPlate;
        [SerializeField] private GameObject _disabledPlate;
        [SerializeField] private TMP_Text _amount;
        [SerializeField] private Button _button;

        [SerializeField] private ProjectileType _type;

        private IDisposable _selectListener;
        private IDisposable _amountListener;

        private IProjectileSelector _selector;

        private void OnEnable()
        {
            _selectListener = Msg.Listen<ProjectileSelectedEvent>(OnSelected);
            _amountListener = Msg.Listen<ProjectileAmountChangedEvent>(OnAmountChanged);

            _button.onClick.AddListener(OnClicked);

            if (_selector == null)
                return;

            if (_selector.Selected == _type)
            {
                _disabledPlate.SetActive(false);
                _enabledPlate.SetActive(true);
            }

            if (_type != ProjectileType.Fishnet)
            {
                var amount = _selector.GetAmount(_type);
                _amount.text = amount.ToString();
            }
        }

        private void OnDisable()
        {
            _selectListener?.Dispose();
            _amountListener?.Dispose();

            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnAmountChanged(ProjectileAmountChangedEvent data)
        {
            if (_type != data.Type)
                return;

            if (data.Amount == 0)
            {
                _enabledPlate.SetActive(false);
                _disabledPlate.SetActive(true);
            }

            _amount.text = data.Amount.ToString();
        }

        private void OnSelected(ProjectileSelectedEvent data)
        {
            if (data.Type != _type)
            {
                _disabledPlate.SetActive(true);
                _enabledPlate.SetActive(false);
                return;
            }

            _disabledPlate.SetActive(false);
            _enabledPlate.SetActive(true);
        }

        private void OnClicked()
        {
            _selector.Select(_type);
        }
    }
}