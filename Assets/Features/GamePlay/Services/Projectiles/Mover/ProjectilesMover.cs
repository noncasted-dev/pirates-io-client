using System.Collections.Generic;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Logs;
using GamePlay.Services.Projectiles.Mover.Abstract;
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

        private readonly List<IMovableProjectile> _addQueue = new();
        private readonly ProjectilesMoverConfigAsset _config;
        private readonly ProjectilesLogger _logger;

        private readonly List<IMovableProjectile> _projectiles = new();
        private readonly List<IMovableProjectile> _removeQueue = new();

        private readonly IUpdater _updater;

        private Collider2D[] _buffer;

        public void OnFixedUpdate(float delta)
        {
            Fetch();

            _logger.OnUpdate(_projectiles.Count);

            if (_projectiles.Count == 0)
                return;

            var array = new NativeArray<ProjectileMoveData>(_projectiles.Count, Allocator.TempJob);

            for (var i = 0; i < array.Length; i++)
                array[i] = _projectiles[i].Movement.CreateMoveData(delta);

            var job = new MoveJob(array, delta);

            var jobHandle = job.Schedule(array.Length, 1);
            jobHandle.Complete();

            for (var i = 0; i < job.Projectiles.Length; i++)
            {
                var projectile = _projectiles[i];

                var data = job.Projectiles[i];

                var raycastData = projectile.Movement.RaycastData;
                var movement = projectile.Movement;
                
                var size = new Vector2(data.PassedDistance, raycastData.ColliderHeight);

                var result = Physics2D.OverlapBoxNonAlloc(
                    data.MiddlePoint,
                    size,
                    raycastData.Angle,
                    _buffer,
                    raycastData.LayerMask);

                if (result == 0)
                {
                    movement.SetPosition(data.CurrentPosition);
                    movement.OnDistancePassed(data.PassedDistance);
                    continue;
                }

                movement.SetPosition(data.MiddlePoint);

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

                        projectile.Actions.OnTriggered(damageReceiver);
                        _logger.OnTriggered(target.name);
                        break;
                    }
                }

                projectile.Actions.OnCollided();
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

        public void Add(IMovableProjectile projectile)
        {
            _addQueue.Add(projectile);
            _logger.OnAdd(_projectiles.Count);
        }

        public void Remove(IMovableProjectile projectile)
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