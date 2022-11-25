using UnityEngine;


namespace Global.Services.Sounds.Runtime

{
    [DisallowMultipleComponent]
    public class SoundsPlayer : MonoBehaviour
    {
        [SerializeField]
        public FMODUnity.EventReference Shot;
        public FMOD.Studio.EventInstance ShotEvent;
        

        public void OnCityEntered()
        {
            
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