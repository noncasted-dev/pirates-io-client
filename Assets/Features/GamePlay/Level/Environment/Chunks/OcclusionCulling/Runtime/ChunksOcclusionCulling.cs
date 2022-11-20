using System;
using System.Collections;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Level.Environment.Chunks.Instance;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using VContainer;

namespace GamePlay.Level.Environment.Chunks.OcclusionCulling.Runtime
{
    public class ChunksOcclusionCulling : SceneObject
    {
        [Inject]
        private void Construct(IPlayerPositionProvider playerPosition)
        {
            _playerPosition = playerPosition;
        }

        [SerializeField] private Chunk[] _chunks;
        [SerializeField] private float _cullingRate = 5f;
        [SerializeField] private float _cullingDistance = 100f;

        private WaitForSeconds _wait;

        private IPlayerPositionProvider _playerPosition;

        private Coroutine _process;
        private IDisposable _playerSpawnListener;

        private void Awake()
        {
            _wait = new WaitForSeconds(_cullingRate);
        }

        protected override void OnEnabled()
        {
            _playerSpawnListener = MessageBroker.Default.Receive<PlayerSpawnedEvent>().Subscribe(Begin);
        }

        protected override void OnDisabled()
        {
            _playerSpawnListener?.Dispose();
            Stop();
        }

        private void Begin(PlayerSpawnedEvent data)
        {
            if (_process != null)
                StopCoroutine(_process);

            _process = StartCoroutine(DisableOnDistance());
        }

        private void Stop()
        {
            if (_process == null)
                return;

            StopCoroutine(_process);
            _process = null;
        }

        private IEnumerator DisableOnDistance()
        {
            foreach (var chunk in _chunks)
            {
                var distance = Vector2.Distance(chunk.position, _playerPosition.Position);

                if (distance > _cullingDistance)
                {
                    chunk.Culling.Disable();

                    continue;
                }

                chunk.Culling.Enable();
            }

            yield return _wait;

            _process = StartCoroutine(DisableOnDistance());
        }

        [Button("Scan")]
        private void Scan()
        {
            _chunks = FindObjectsOfType<Chunk>();
        }
    }
}