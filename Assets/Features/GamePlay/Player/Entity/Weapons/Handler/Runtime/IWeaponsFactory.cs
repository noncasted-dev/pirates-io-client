using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Weapons.Bow.Root;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    public interface IWeaponsFactory
    {
        UniTask<IBow> CreateBow(AssetReference reference);
    }
}