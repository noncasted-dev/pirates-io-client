﻿using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Abstract;
using GamePlay.Player.Entity.Setup.Flow;
using GamePlay.Player.Entity.States.None.Runtime;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Player.Entity.Views.RotationPoint;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Setup.Root
{
    public class PlayerRoot : MonoBehaviour, IPlayerRoot
    {
        [Inject]
        private void Construct(
            IRespawn respawn,
            INone none,
            IRotationPoint rotationPoint)
        {
            _respawn = respawn;
            _none = none;
            _rotationPoint = rotationPoint;
        }

        [SerializeField] private Collider2D[] _colliders;
        private IFlowHandler _flowHandler;

        private INone _none;

        private IRespawn _respawn;
        private IRotationPoint _rotationPoint;

        private bool _wasDisabled;

        private void OnEnable()
        {
            if (_wasDisabled == false)
                return;

            _flowHandler.InvokeEnable();
        }

        private void OnDisable()
        {
            _wasDisabled = true;
            _flowHandler.InvokeDisable();
        }

        private void OnDestroy()
        {
            _flowHandler.InvokeDestroy();
        }

        public Transform Transform => _rotationPoint.Transform;

        public async UniTask OnBootstrapped(IFlowHandler flowHandler, LifetimeScope parent)
        {
            _flowHandler = flowHandler;
            flowHandler.InvokeAwake();
            await flowHandler.InvokeAsyncAwake();
            flowHandler.InvokeEnable();
            flowHandler.InvokeStart();

            _none.Enter();
        }

        public void Respawn()
        {
            foreach (var switchableCollider in _colliders)
                switchableCollider.enabled = true;

            _respawn.Enter();
        }
    }
}