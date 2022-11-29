using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.Setup.Abstract;
using GamePlay.Player.Entity.Setup.Root;
using UnityEngine;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public interface IPlayerFactory
    {
        UniTask<IPlayerRoot> Create(Vector2 position, ShipType type);
    }
}