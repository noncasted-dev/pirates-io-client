using System.Collections.Generic;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Logs;
using Global.Services.Updaters.Runtime.Abstract;
using Local.Services.Abstract.Callbacks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Mover
{
    public class ProjectilesMover :
        IProjectilesMover,
        IFixedUpdatable,
        ILocalAwakeListener,
        ILocalSwitchListener
    {
        public ProjectilesMover(
            IUpdater updater,
            ProjectilesMoverConfigAsset config,
            ProjectilesLogger logger)
        {
            _updater = updater;
            _config = config;
            _logger = logger;
        }

        private readonly List<IProjectile> _addQueue = new();
        private readonly ProjectilesMoverConfigAsset _config;
        private readonly ProjectilesLogger _logger;

        private readonly List<IProjectile> _projectiles = new();
        private readonly List<IProjectile> _removeQueue = new();

        private readonly IUpdater _updater;

        private Collider2D[] _buffer;

        public void OnFixedUpdate(float delta)
        {
            Fetch();

            _logger.OnUpdate(_projectiles.Count);

            if (_projectiles.Count == 0)
                return;

            var array = new NativeArray<LinearProjectileData>(_projectiles.Count, Allocator.TempJob);

            for (var i = 0; i < array.Length; i++)
            {
                var projectile = _projectiles[i];

                array[i] = new LinearProjectileData(
                    projectile.Position,
                    projectile.Direction,
                    projectile.Speed * delta);
            }

            var job = new MoveJob(array);

            var jobHandle = job.Schedule(array.Length, 1);
            jobHandle.Complete();

            for (var i = 0; i < job.Projectiles.Length; i++)
            {
                var projectile = _projectiles[i];

                var data = job.Projectiles[i];

                var size = new Vector2(data.PassedDistance, projectile.ColliderHeight);

                var result = Physics2D.OverlapBoxNonAlloc(
                    data.MiddlePoint,
                    size,
                    projectile.Angle,
                    _buffer,
                    projectile.LayerMask);

                if (result == 0)
                {
                    projectile.SetPosition(data.CurrentPosition);
                    continue;
                }

                projectile.SetPosition(data.MiddlePoint);

                for (var j = 0; j < result; j++)
                {
                    var target = _buffer[j];

                    if (target == null)
                        continue;

                    if (target.IsTouchingLayers(_config.HitBoxMask) == true)
                    {
                        if (target.TryGetComponent(out IDamageReceiver damageReceiver) == false)
                        {
                            _logger.OnWrongTrigger(target.name);
                            break;
                        }

                        projectile.OnTriggered(damageReceiver);
                        _logger.OnTriggered(target.name);
                        break;
                    }
                }

                projectile.OnCollided();
                _logger.OnCollided(_buffer[0].name);
            }

            array.Dispose();
        }

        public void OnAwake()
        {
            _buffer = new Collider2D[_config.BufferSize];
        }

        public void OnEnabled()
        {
            _updater.Add(this);
        }

        public void OnDisabled()
        {
            _updater.Remove(this);
        }

        public void Add(IProjectile projectile)
        {
            _addQueue.Add(projectile);
            _logger.OnAdd(_projectiles.Count);
        }

        public void Remove(IProjectile projectile)
        {
            _removeQueue.Add(projectile);
            _logger.OnRemove(_projectiles.Count - _removeQueue.Count);
        }

        private void Fetch()
        {
            _projectiles.AddRange(_addQueue);

            foreach (var remove in _removeQueue)
                _projectiles.Remove(remove);

            _addQueue.Clear();
            _removeQueue.Clear();
        }
    }
}