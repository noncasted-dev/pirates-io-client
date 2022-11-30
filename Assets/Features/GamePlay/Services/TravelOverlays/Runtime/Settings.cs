using UnityEngine;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    [DisallowMultipleComponent]
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject _body;

        public void Switch()
        {
            if (_body.activeSelf == true)
                _body.SetActive(false);
            else
                _body.SetActive(true);
        }
    }
}