using System;
using UnityEngine;

namespace GamePlay.Common.Areas.Common.Runtime
{
    [DisallowMultipleComponent]
    public class AreaTrigger : MonoBehaviour
    {
        public event Action<IAreaInteractor> Entered; 
        public event Action<IAreaInteractor> Exited; 

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsInteractor(other, out var interactor) == false)
                return;
            
            Entered?.Invoke(interactor);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsInteractor(other, out var interactor) == false)
                return;
            
            Exited?.Invoke(interactor);
        }

        
        private bool IsInteractor(Component other, out IAreaInteractor interactor)
        {
            if (other.TryGetComponent(out interactor) == false)
            {
                Debug.LogError($"Wrong area trigger with {other.name}");
                return false;
            }

            return true;
        }
    }
}