using UnityEngine;

namespace Global.GameLoops.Abstract
{
    public abstract class GlobalGameLoop : MonoBehaviour
    {
        public abstract void Begin();

        public abstract void OnAwake();
    }
}