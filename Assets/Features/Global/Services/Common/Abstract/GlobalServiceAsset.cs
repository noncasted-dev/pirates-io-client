using UnityEngine;
using VContainer;

namespace Global.Services.Common.Abstract
{
    public abstract class GlobalServiceAsset : ScriptableObject
    {
        public abstract void Create(
            IContainerBuilder builder,
            IServiceBinder serviceBinder);
    }
}