#region

using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Weapons.Cannon.Root;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    public interface IWeaponsFactory
    {
        UniTask<ICanon> CreateBow(AssetReference reference);
    }
}