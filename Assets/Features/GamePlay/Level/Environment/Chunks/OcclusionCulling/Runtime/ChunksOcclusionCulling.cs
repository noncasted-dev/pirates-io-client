using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Level.Environment.Chunks.Instance;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace GamePlay.Level.Environment.Chunks.OcclusionCulling.Runtime
{
    public class ChunksOcclusionCulling : SceneObject
    {
        [Inject]
        private void Construct(IPlayerEntityProvider playerEntity)
        {
            _playerEntity = playerEntity;
        }

        [SerializeField] private ChunkHandler[] _chunks;
        [SerializeField] private float _cullingRate = 5f;
        [SerializeField] private float _cullingDistance = 100f;

        private WaitForSeconds _wait;

        private IPlayerEntityProvider _playerEntity;

        private Coroutine _process;
        private IDisposable _playerSpawnListener;

        private readonly List<ChunkHandler> _loaded = new();

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
            var toUnload = new List<ChunkHandler>();
            
            foreach (var chunk in _loaded)
            {
                var distance = Vector2.Distance(chunk.Position, _playerEntity.Position);

                if (distance <= _cullingDistance)
                    continue;

                if (chunk.IsLoaded == true)
                    toUnload.Add(chunk);
            }

            foreach (var unloadTarget in toUnload)
                UnloadScene(unloadTarget);

            foreach (var chunk in _chunks)
            {
                var distance = Vector2.Distance(chunk.Position, _playerEntity.Position);

                if (distance > _cullingDistance)
                    continue;

                if (chunk.IsLoaded == false)
                    LoadScene(chunk).Forget();
            }

            yield return _wait;

            _process = StartCoroutine(DisableOnDistance());
        }

        private async UniTaskVoid LoadScene(ChunkHandler chunk)
        {
            chunk.MarkAsLoaded();
            
            var handle = Addressables.LoadSceneAsync(chunk.Scene, LoadSceneMode.Additive);
            var result = await handle.ToUniTask();

            chunk.OnLoaded(result);

            _loaded.Add(chunk);
        }

        private void UnloadScene(ChunkHandler chunk)
        {
            Addressables.UnloadSceneAsync(chunk.Loaded);
            _loaded.Remove(chunk);
            chunk.OnUnloaded();
        }

        private async UniTaskVoid UnloadAll()
        {
            foreach (var loaded in _loaded)
                Addressables.UnloadSceneAsync(loaded.Loaded);
        }

        [Button("Scan")]
        private void Scan()
        {
            _chunks = FindObjectsOfType<ChunkHandler>();
        }
    }
}