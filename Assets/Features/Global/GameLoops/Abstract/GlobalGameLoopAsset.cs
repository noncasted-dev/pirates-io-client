using Common.DiContainer.Abstract;
using Global.Services.Common.Abstract;
using UnityEngine;

namespace Global.GameLoops.Abstract
{
    public abstract class GlobalGameLoopAsset : ScriptableObject
    {
        public abstract GlobalGameLoop Create(IDependencyRegister register, IGlobalServiceBinder binder);
    }
}