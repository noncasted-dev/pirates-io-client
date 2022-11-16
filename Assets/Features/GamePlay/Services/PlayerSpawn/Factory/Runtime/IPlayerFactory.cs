#region

using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Root;
using UnityEngine;

#endregion

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public interface IPlayerFactory
    {
        UniTask<IPlayerRoot> Create(Vector2 position);
    }
}