using UnityEngine;

namespace Global.GameLoops.Abstract
{
    public abstract class GlobalGameLoop : MonoBehaviour
    {
        public abstract void Begin();

        public void OnAwake()
        {
        }

        public void OnBootstrapped()
        {
        }
    }
}