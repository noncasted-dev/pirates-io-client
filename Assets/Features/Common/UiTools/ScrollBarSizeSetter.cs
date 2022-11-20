using UnityEngine;
using UnityEngine.UI;

namespace Common.UiTools
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Scrollbar))]
    public class ScrollBarSizeSetter : MonoBehaviour
    {
        [SerializeField] private float _size;

        private Scrollbar _scrollbar;

        private void Awake()
        {
            _scrollbar = GetComponent<Scrollbar>();
        }

        private void Update()
        {
            _scrollbar.size = _size;
        }
    }
}