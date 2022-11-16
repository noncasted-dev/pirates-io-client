#region

using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow;
using UnityEngine;
using VContainer.Unity;

#endregion

namespace GamePlay.Player.Entity.Setup.Root
{
    public interface IPlayerRoot
    {
        Transform Transform { get; }

        UniTask OnBootstrapped(IFlowHandler flowHandler, LifetimeScope parent);
        void Respawn();
    }
}