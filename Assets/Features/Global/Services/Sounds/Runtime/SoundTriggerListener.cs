using System;
using UniRx;
using UnityEngine;

namespace Global.Services.Sounds.Runtime
{
    public class SoundTriggerListener : MonoBehaviour
    {
        [SerializeField] private SoundsPlayer _player;

        private IDisposable _triggerListener;
        
        private void OnEnable()
        {
            _triggerListener = MessageBroker.Default.Receive<SoundEvent>().Subscribe(OnSoundTriggered);
        }

        private void OnDisable()
        {
            _triggerListener?.Dispose();
        }
        
        private void OnSoundTriggered(SoundEvent data)
        {
            switch(data.Type)
            {
                case SoundType.CityEnter:
                    _player.OnCityEntered();
                    break;
                case SoundType.CityExit:
                    _player.OnCityExited();
                    break;
                case SoundType.PortEnter:
                    _player.OnPortEntered();
                    break;
                case SoundType.PortExit:
                    _player.OnPortExited();
                    break;
                case SoundType.BattleEnter:
                    _player.OnBattleEntered();
                    break;
                case SoundType.BattleExit:
                    _player.OnBattleExited();
                    break;
                case SoundType.CannonBallShot:
                    _player.OnCannonBallShot();
                    break;
                case SoundType.ShrapnelShot:
                    _player.OnShrapnelShot();
                    break;
                case SoundType.KnuppelShot:
                    _player.OnKnuppelShot();
                    break;
                case SoundType.UiOpen:
                    _player.OnUiOpened();
                    break;
                case SoundType.OverButton:
                    _player.OnOverButton();
                    break;
                case SoundType.ButtonClick:
                    _player.OnButtonClicked();
                    break;
                default:
                    Debug.LogError($"Sound: {data.Type} is not implemented");
                    break;
            }
        }
    }
}