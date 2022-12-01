using System;
using UnityEngine;

namespace Common.VFX
{
    public class TakeItemAnimatorCallback : MonoBehaviour
    {
        private Action _endCallback;

        public void Construct(Action endCallback)
        {
            _endCallback = endCallback;
        }
        
        public void OnPlayed()
        {
            _endCallback?.Invoke();
        }
    }
}