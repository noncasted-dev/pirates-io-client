using System;
using GamePlay.Services.PlayerCargos.Storage.Events;
using TMPro;
using UniRx;
using UnityEngine;

namespace Common.VFX
{
    public class TakeItemAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private TMP_Text _tmp;
        
        private IDisposable _itemListener;

        private void OnEnable()
        {
            _itemListener = MessageBroker.Default.Receive<CargoAddEvent>().Subscribe(OnItemReceived);
        }

        private void OnDisable()
        {
            _itemListener?.Dispose();
        }

        private void OnItemReceived(CargoAddEvent data)
        {
            StartAnimation(data.Item.BaseData.Icon, data.Item.Count);    
        }

        public void StartAnimation(Sprite icon, int add_count)
        {
            _icon.sprite = icon;
            _tmp.text = "+" + add_count;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}