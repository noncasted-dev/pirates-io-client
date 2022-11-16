#region

using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Player.Entity.Components.Abstract
{
    public abstract class PlayerComponentAsset : ScriptableObject
    {
        public abstract void Register(IContainerBuilder builder);

        public virtual void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
        }
    }
}