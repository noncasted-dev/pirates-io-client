#region

using UnityEngine;

#endregion

namespace GamePlay.Common.SceneObjects.Runtime
{
    public abstract class SceneObject : MonoBehaviour
    {
        private bool _isEnabled = false;
        private bool _requiresEnable = false;

        private void OnEnable()
        {
            if (_isEnabled == false)
                return;

            if (_requiresEnable == false)
                return;

            _requiresEnable = false;

            OnEnabled();
        }

        private void OnDisable()
        {
            _requiresEnable = true;

            OnDisabled();
        }

        internal void InvokeAwake()
        {
            OnAwake();
        }

        internal void InvokeEnabled()
        {
            _isEnabled = true;

            if (gameObject.activeInHierarchy == false)
                return;

            OnEnabled();
        }

        internal void InvokeStart()
        {
            OnStart();
        }

        internal void InvokeDisabled()
        {
            if (gameObject.activeInHierarchy == false)
                return;

            OnDisabled();
        }

        internal void InvokeDestroyed()
        {
            OnDestroyed();
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnEnabled()
        {
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnDisabled()
        {
        }

        protected virtual void OnDestroyed()
        {
        }
    }
}