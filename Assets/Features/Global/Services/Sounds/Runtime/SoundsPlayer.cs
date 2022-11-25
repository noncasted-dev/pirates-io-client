using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Global.Services.Sounds.Runtime

{
    [DisallowMultipleComponent]
    public class SoundsPlayer : MonoBehaviour
    {
        [Space(30)] [Header("Shot")] [SerializeField]
        public EventReference ShotEvent;
        public FMOD.Studio.EventInstance ShotInstance;

        [Space(30)] [Header("Ambience")] [SerializeField]
        public EventReference AmbEvent;
        public FMOD.Studio.EventInstance AmbInstance;

        [Space(30)] [Header("Music")] [SerializeField]
        public EventReference MusicEvent;
        public FMOD.Studio.EventInstance MusicInstance;

        [Space(30)] [Header("UI")] [SerializeField]
        public EventReference UiOpenedEvent;
        //public FMOD.Studio.EventInstance UiOpenedInstance;

        public EventReference ButtonClickedEvent;
        //public FMOD.Studio.EventInstance ButtonClickedInstance;

        public EventReference OverButtonEvent;
        //public FMOD.Studio.EventInstance OverButtonInstance;

        private Transform _fmodInstance;

        private void Start()
        {
            AmbInstance = RuntimeManager.CreateInstance(AmbEvent);
            AmbInstance.start();

            var fmodObject = new GameObject("FmodInstance");
            _fmodInstance = fmodObject.transform;
        }

        public void OnCityEntered()
        {
            Debug.Log("city_enter");
        }

        public void OnCityExited()
        {
        }

        public void OnPortEntered()
        {
            AmbInstance.setParameterByName("amb_condition", 1f);
        }

        public void OnPortExited()
        {
            AmbInstance.setParameterByName("amb_condition", 0f);
            Debug.Log("portExit");
        }

        public void OnBattleEntered()
        {
        }

        public void OnBattleExited()
        {
        }

        public void OnCannonBallShot(Vector2 position)
        {
            PlayShot(0f, position);
            
            Debug.Log("boom");
        }

        public void OnShrapnelShot(Vector2 position)
        {
            PlayShot(1f, position);
            
            Debug.Log("boom");
        }

        public void OnKnuppelShot(Vector2 position)
        {
            PlayShot(2f, position);
            
            Debug.Log("boom");
        }

        private void PlayShot(float parameter, Vector2 position)
        {
            ShotInstance = RuntimeManager.CreateInstance(ShotEvent);
            AttachInstance(ShotInstance, position);
            
            ShotInstance.setParameterByName("shot_type", parameter);
            ShotInstance.start();
            ShotInstance.release();
        }

        public void OnProjectileDropped(Vector2 position)
        {
        }

        public void OnEnemyDamaged(Vector2 position)
        {
        }

        public void OnDamageReceived()
        {
        }

        public void OnUiOpened()
        {
            RuntimeManager.PlayOneShot(UiOpenedEvent);
        }

        public void OnOverButton()
        {
        }

        public void OnButtonClicked()
        {
        }

        public void OnMenuEntered()
        {
        }

        public void OnMenuExited()
        {
        }

        private void AttachInstance(EventInstance instance, Vector2 position)
        {
            _fmodInstance.position = position;
            RuntimeManager.AttachInstanceToGameObject(instance, _fmodInstance);
        }
    }
}