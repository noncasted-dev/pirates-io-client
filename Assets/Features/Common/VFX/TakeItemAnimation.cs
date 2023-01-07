using System;
using GamePlay.Services.PlayerCargos.Storage.Events;
using Global.Services.MessageBrokers.Runtime;
using TMPro;
using UnityEngine;

namespace Common.VFX
{
    public class TakeItemAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private TMP_Text _tmp;
        [SerializeField] private GameObject _body;
        [SerializeField] private TakeItemAnimatorCallback _callback;

        private IDisposable _itemListener;

        private void OnEnable()
        {
            _itemListener = Msg.Listen<CargoAddEvent>(OnItemReceived);
            _callback.Construct(Deactivate);
        }

        private void OnDisable()
        {
            _itemListener?.Dispose();
        }

        private void OnItemReceived(CargoAddEvent data)
        {
            Debug.Log("item received");
            StartAnimation(data.Item.BaseData.Icon, data.Item.Count);
        }

        private void StartAnimation(Sprite icon, int add_count)
        {
            _body.SetActive(true);
            _icon.sprite = icon;
            _tmp.text = "+" + add_count;
        }

        private void Deactivate()
        {
            _body.SetActive(false);
        }
    }
}