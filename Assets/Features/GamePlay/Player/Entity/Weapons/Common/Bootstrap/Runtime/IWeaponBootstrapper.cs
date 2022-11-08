using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Weapons.Common.Root;
using UnityEngine;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Weapons.Common.Bootstrap.Runtime
{
    public interface IWeaponBootstrapper
    {
        void ToChild(Transform parent);
        UniTask<IWeapon> Build(LifetimeScope parent);
    }
}