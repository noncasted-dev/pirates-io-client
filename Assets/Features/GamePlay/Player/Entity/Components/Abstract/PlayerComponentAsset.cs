using Common.DiContainer.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Abstract
{
    public abstract class PlayerComponentAsset : ScriptableObject
    {
        public abstract void Register(IDependencyRegister builder);
    }
}