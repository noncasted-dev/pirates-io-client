using UnityEngine;


namespace Global.Services.Sounds.Runtime

{
    [DisallowMultipleComponent]
    public class SoundsPlayer : MonoBehaviour
    {
        [Space(30)]
        [Header("Shot")]
        [SerializeField]
        public FMODUnity.EventReference ShotEvent;
        public FMOD.Studio.EventInstance ShotInstance;

        [Space(30)]
        [Header("Ambience")]
        [SerializeField]
        public FMODUnity.EventReference AmbEvent;
        public FMOD.Studio.EventInstance AmbInstance;

        [Space(30)]
        [Header("Music")]
        [SerializeField]
        public FMODUnity.EventReference MusicEvent;
        public FMOD.Studio.EventInstance MusicInstance;

        [Space(30)]
        [Header("UI")]
        [SerializeField]
        public FMODUnity.EventReference UiOpenedEvent;
       //public FMOD.Studio.EventInstance UiOpenedInstance;

        public FMODUnity.EventReference ButtonClickedEvent;
        //public FMOD.Studio.EventInstance ButtonClickedInstance;

        public FMODUnity.EventReference OverButtonEvent;
        //public FMOD.Studio.EventInstance OverButtonInstance;



         void Start()
        {
            AmbInstance = FMODUnity.RuntimeManager.CreateInstance(AmbEvent);
            AmbInstance.start();
           
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
            ShotInstance = FMODUnity.RuntimeManager.CreateInstance(ShotEvent);
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(ShotInstance, Transform.po);
            ShotInstance.setParameterByName("shot_type", 0f);
            ShotInstance.start();
            ShotInstance.release();
            Debug.Log("boom");
        }

        public void OnShrapnelShot(Vector2 position)
        {
            ShotInstance = FMODUnity.RuntimeManager.CreateInstance(ShotEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(ShotInstance, Transform.po);
            ShotInstance.setParameterByName("shot_type", 1f);
            ShotInstance.start();
            ShotInstance.release();
            Debug.Log("boom");
        }

        public void OnKnuppelShot(Vector2 position)
        {
                        ShotInstance = FMODUnity.RuntimeManager.CreateInstance(ShotEvent);
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(ShotInstance, Transform.po);
            ShotInstance.setParameterByName("shot_type", 1f);
            ShotInstance.start();
            ShotInstance.release();
            Debug.Log("boom");
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
           FMODUnity.RuntimeManager.PlayOneShot(UiOpenedEvent); 
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
    }
}