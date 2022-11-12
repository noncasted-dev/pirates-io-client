using System;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Services.Projectiles.Factory;
using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Network.Remote.Bootstrap
{
    public class PlayerRemoteView : MonoBehaviour, IPoolObject<PlayerRemoteView>
    {
        private Action<PlayerRemoteView> _returnToPool;
        
        public GameObject GameObject => gameObject;

        public void Construct(
            ILogger logger,
            IProjectilesPoolProvider projectiles)
        {
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetupPoolObject(Action<PlayerRemoteView> returnToPool)
        {
            _returnToPool = returnToPool;
        }

        public void OnTaken()
        {
            
        }

        public void OnReturned()
        {
            
        }
    }
}