using System;
using UnityEngine;

namespace Common.VFX
{
    public class TakeItemAnimatorCallback : MonoBehaviour
    {
        public void Construct(Action endCallback)
        {
            _endCallback = endCallback;
        }

        private Action _endCallback;

        public void OnPlayed()
        {
            _endCallback?.Invoke();
        }
    }
}