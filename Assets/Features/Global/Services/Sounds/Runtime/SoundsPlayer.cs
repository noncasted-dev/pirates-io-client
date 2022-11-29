using System;
using FMOD.Studio;
using FMODUnity;
using GamePlay.Services.Projectiles.Entity;
using UnityEngine;

namespace Global.Services.Sounds.Runtime

{
    [DisallowMultipleComponent]
    public class SoundsPlayer : MonoBehaviour
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

        private float _health;

        private Transform _fmodInstance;

        private void Start()
        {
            AmbInstance = RuntimeManager.CreateInstance(AmbEvent);
            AmbInstance.start();

            var fmodObject = new GameObject("FmodInstance");
            _fmodInstance = fmodObject.transform;
        }

        //Amb
        public void OnCityExited()
        {
            AmbInstance.setParameterByName("amb_condition", 0f);
            Debug.Log("open sea");
        }

        public void OnPortEntered()
        {
            AmbInstance.setParameterByName("amb_condition", 1f);
            Debug.Log("port_enter");
        }

        public void OnCityEntered()
        {
            AmbInstance.setParameterByName("amb_condition", 2f);
            Debug.Log("city entered");
        }

        public void OnPortExited()
        {
            //AmbInstance.setParameterByName("amb_condition", 2f);
            Debug.Log("port exit");
        }

        //Music
        public void OnBattleEntered()
        {
            Debug.Log("BattleEntered");
        }

        public void OnBattleExited()
        {
            Debug.Log("BattleExited");
        }

        //Battle
        public void OnCannonBallShot(Vector2 position)
        {
            PlayShot(0f, position);
            Debug.Log("boom CannonBall");
        }

        public void OnShrapnelShot(Vector2 position)
        {
            PlayShot(1f, position);
            Debug.Log("boom Shrapnel");
        }

        public void OnKnuppelShot(Vector2 position)
        {
            PlayShot(2f, position);
            Debug.Log("boom Knuppel");
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
        }

        public void OnEnemyDamaged(GameObject target, ProjectileType type)
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

            //PlayDamage(1f, position);

            RuntimeManager.PlayOneShotAttached(DamageEvent, target);
            //PlayDamage(1f, position);
        }

        public void OnDamageReceived()
        {
            RuntimeManager.PlayOneShot(DamageReceivedEvent);
            Debug.Log("We are damaged!");
        }

        private void PlayDamage(float parameter, Vector2 position)
        {
            DamageInstance = RuntimeManager.CreateInstance(DamageEvent);
            AttachInstance(ShotInstance, position);
            DamageInstance.setParameterByName("damage_type", parameter);
            DamageInstance.start();
            DamageInstance.release();
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

        public void OnHealthChanged(float health)
        {
            _health = health;
        }

        private void AttachInstance(EventInstance instance, Vector2 position)
        {
            _fmodInstance.position = position;
            RuntimeManager.AttachInstanceToGameObject(instance, _fmodInstance);
        }
    }
}