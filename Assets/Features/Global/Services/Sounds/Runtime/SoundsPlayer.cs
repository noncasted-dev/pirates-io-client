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
        [Header("UI")]
        [SerializeField]
        public FMODUnity.EventReference UiOpenedEvent;
       //public FMOD.Studio.EventInstance UiOpenedInstance;

        public FMODUnity.EventReference ButtonClickedEvent;
        //public FMOD.Studio.EventInstance ButtonClickedInstance;

        public FMODUnity.EventReference OverButtonEvent;
        //public FMOD.Studio.EventInstance OverButtonInstance;


        [SerializeField]
        private GameObject Boat;

        public void OnCityEntered()
        {

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("amb_condition", 1f);
            Debug.Log("city_enter");
        }

        public void OnCityExited()
        {
            
        }

        public void OnPortEntered()
        {
            
        }

        public void OnPortExited()
        {
            
        }

        public void OnBattleEntered()
        {
            
        }

        public void OnBattleExited()
        {
            
        }

        public void OnCannonBallShot()
        {
            ShotInstance = FMODUnity.RuntimeManager.CreateInstance(ShotEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(ShotInstance, Boat.transform, gameObject.GetComponent<Rigidbody>());
            ShotInstance.setParameterByName("surface_type", 0f);
            ShotInstance.start();
            ShotInstance.release();
            Debug.Log("boom");
        }

        public void OnShrapnelShot()
        {
            
        }

        public void OnKnuppelShot()
        {
            
        }

        public void OnUiOpened()
        {
            
        }

        public void OnOverButton()
        {
            
        }

        public void OnButtonClicked()
        {
            
        }
    }
}