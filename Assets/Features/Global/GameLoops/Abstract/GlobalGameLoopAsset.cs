using System;
using UnityEngine;
using VContainer;

namespace Global.GameLoops.Abstract
{
    public abstract class GlobalGameLoopAsset : ScriptableObject
    {
        public abstract GlobalGameLoop Create(IContainerBuilder builder, Action<MonoBehaviour> addToModules);
    }
}