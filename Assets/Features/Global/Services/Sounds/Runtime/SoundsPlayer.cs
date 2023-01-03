using System;
using Cysharp.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using GamePlay.Common.Damages;
using Global.Services.Common.Abstract;
using UnityEngine;

namespace Global.Services.Sounds.Runtime

{
    [DisallowMultipleComponent]
    public class SoundsPlayer : MonoBehaviour, IGlobalBootstrapListener
    {
        [Space(30)] [Header("Battle")] [SerializeField]
        public EventReference ShotEvent;
        public FMOD.Studio.EventInstance ShotInstance;

        public EventReference DamageEvent;
        public FMOD.Studio.EventInstance DamageInstance;

        public EventReference DamageReceivedEvent;
        public FMOD.Studio.EventInstance DamageReceivedInstance;

        [Space(30)] [Header("Ambience")] [SerializeField]
        public EventReference AmbEvent;
        public FMOD.Studio.EventInstance AmbInstance;

        [Space(30)] [Header("Music")] [SerializeField]
        public EventReference MusicEvent;
        public FMOD.Studio.EventInstance MusicInstance;

        [Space(30)] [Header("UI")] [SerializeField]
        public EventReference UiOpenedEvent;
        public EventReference ButtonClickedEvent;
        public EventReference OverButtonEvent;
        public EventReference MenuEnteredEvent;
        public EventReference MenuExitedEvent;

        [Space(30)] [Header("FX")] [SerializeField]
        public EventReference BurningEvent;
        public EventInstance BurningInstance;

        [SerializeField] private float _fightExitSpeed = 1f;
        [SerializeField] private float _fightSecondsPerHit = 1f;

        private float _fightTime;
        private bool _isInFight;

        private float _health = 1f;

        private Transform _fmodInstance;

        public void OnBootstrapped()
        {
            Setup().Forget();
        }

        private async UniTaskVoid Setup()
        {
            await UniTask.Delay(100);

            while ((Input.GetMouseButton(0) == false && Input.GetMouseButton(1) == false)
                   || RuntimeManager.HaveAllBanksLoaded == false || RuntimeManager.HaveMasterBanksLoaded == false)
                await UniTask.Yield();

            AmbInstance = RuntimeManager.CreateInstance(AmbEvent);
            AmbInstance.start();

            MusicInstance = RuntimeManager.CreateInstance(MusicEvent);
            MusicInstance.start();

            BurningInstance = RuntimeManager.CreateInstance(BurningEvent);
            BurningInstance.start();
        }

        private void Update()
        {
            BurningInstance.setParameterByName("health", _health);
            _fightTime -= _fightExitSpeed * Time.deltaTime;

            if (_fightTime < 0f && _isInFight == true)
            {
                _isInFight = false;
                OnBattleExited();
            }
        }

        //Amb
        public void OnCityExited()
        {
            RuntimeManager.StudioSystem.setParameterByName("amb_condition", 0f);
        }

        public void OnPortEntered()
        {
            RuntimeManager.StudioSystem.setParameterByName("music_condition", 1f);
            AmbInstance.setParameterByName("amb_condition", 1f);
        }

        public void OnCityEntered()
        {
            RuntimeManager.StudioSystem.setParameterByName("music_condition", 0f);
            AmbInstance.setParameterByName("amb_condition", 2f);
        }

        public void OnPortExited()
        {
            AmbInstance.setParameterByName("amb_condition", 2f);
        }

        //Music
        public void OnBattleEntered()
        {
            RuntimeManager.StudioSystem.setParameterByName("music_condition", 2f);
        }

        public void OnBattleExited()
        {
            RuntimeManager.StudioSystem.setParameterByName("music_condition", 0f);
        }

        //Battle
        public void OnCannonBallShot(Vector2 position)
        {
            PlayShot(0f, position);
        }

        public void OnShrapnelShot(Vector2 position)
        {
            PlayShot(1f, position);
        }

        public void OnKnuppelShot(Vector2 position)
        {
            PlayShot(2f, position);
        }

        private void PlayShot(float parameter, Vector2 position)
        {
            ShotInstance = RuntimeManager.CreateInstance(ShotEvent);

            AttachInstance(ShotInstance, position);

            ShotInstance.setParameterByName("shot_type", parameter);

            ShotInstance.start();

            ShotInstance.release();
        }

        //Damage
        public void OnProjectileDropped(Vector2 position)
        {
            ////PlayDamage(0f, position);
            //DamageInstance = RuntimeManager.CreateInstance(DamageEvent);
            //DamageInstance.setParameterByName("damage_type", 0f);
            //RuntimeManager.PlayOneShot(DamageEvent);
        }

        public void OnDeath(Vector2 position)
        {
            BurningInstance.release();
        }

        public void OnEnemyDamaged(GameObject target, ProjectileType type) //реализовать переключение типа урона
        {
            switch (type)
            {
                case ProjectileType.Ball:
                {
                    break;
                }
                case ProjectileType.Knuppel:
                {
                    break;
                }
                case ProjectileType.Shrapnel:
                {
                    break;
                }
                case ProjectileType.Fishnet:
                {
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            RuntimeManager.PlayOneShotAttached(DamageEvent, target);
        }

        public void OnDamageReceived()
        {
            RuntimeManager.PlayOneShot(DamageReceivedEvent);

            _fightTime = _fightSecondsPerHit;

            if (_isInFight == false)
                OnBattleEntered();

            _isInFight = true;
        }

        //UI
        public void OnUiOpened()
        {
            RuntimeManager.PlayOneShot(UiOpenedEvent);
        }

        public void OnOverButton()
        {
            RuntimeManager.PlayOneShot(OverButtonEvent);
        }

        public void OnButtonClicked()
        {
            RuntimeManager.PlayOneShot(ButtonClickedEvent);
        }

        public void OnMenuEntered()
        {
            RuntimeManager.PlayOneShot(MenuEnteredEvent);
        }

        public void OnMenuExited()
        {
            RuntimeManager.PlayOneShot(MenuExitedEvent);
        }

        //HP
        public void OnHealthChanged(float health, GameObject target)
        {
            _health = health;

            if (health < 0.5)
            {
                MusicInstance.setParameterByName("music_intencity", 2f);
            }
        }

        private void AttachInstance(EventInstance instance, Vector2 position)
        {
            if (_fmodInstance == null)
            {
                var fmodObject = new GameObject("FmodInstance");
                _fmodInstance = fmodObject.transform;
            }

            _fmodInstance.position = position;
            RuntimeManager.AttachInstanceToGameObject(instance, _fmodInstance);
        }
    }
}