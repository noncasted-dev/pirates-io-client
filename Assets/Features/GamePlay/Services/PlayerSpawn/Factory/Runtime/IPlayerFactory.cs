using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.Setup.Abstract;
using UnityEngine;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public interface IPlayerFactory
    {
        UniTask<IPlayerRoot> Create(Vector2 position, ShipType type);
        UniTask<IPlayerRoot> CreateBot(Vector2 position, ShipType type);
    }
}