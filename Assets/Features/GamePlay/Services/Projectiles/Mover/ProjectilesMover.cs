using System.Collections.Generic;
using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
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

            _hitBoxLayer = LayerMask.NameToLayer(_config.HitBoxLayer);
        }

        private readonly List<IMovableProjectile> _addQueue = new();
        private readonly ProjectilesMoverConfigAsset _config;
        private readonly int _hitBoxLayer;
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
                CheckCollision(_projectiles[i], job.Projectiles[i]);
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

        private void CheckCollision(IMovableProjectile projectile, ProjectileMoveData data)
        {
            var raycastData = projectile.Movement.RaycastData;
            var movement = projectile.Movement;

            var size = new Vector2(data.PassedDistance, raycastData.ColliderHeight);

            if (projectile.Actions.Type == ProjectileType.Fishnet)
            {
                movement.SetPosition(data.CurrentPosition);
                movement.OnDistancePassed(data.PassedDistance);
                return;
            }

            var result = Physics2D.OverlapBoxNonAlloc(
                data.MiddlePoint,
                size,
                raycastData.Angle,
                _buffer,
                _config.AllInteractionsMask);

            if (result == 0)
            {
                movement.SetPosition(data.CurrentPosition);
                movement.OnDistancePassed(data.PassedDistance);
                return;
            }

            for (var i = 0; i < result; i++)
            {
                var target = _buffer[i];

                if (target.gameObject.layer != _hitBoxLayer)
                    continue;

                if (target.TryGetComponent(out IDamageReceiver damageReceiver) == false)
                {
                    _logger.OnWrongTrigger(target.name);
                    continue;
                }

                if (damageReceiver.IsLocal == true && projectile.Actions.CreatorId == damageReceiver.Id)
                {
                    movement.SetPosition(data.CurrentPosition);
                    movement.OnDistancePassed(data.PassedDistance);
                    return;
                }

                if (damageReceiver.IsLocal == true && projectile.Actions.IsLocal == false)
                {
                    projectile.Actions.Destroy();
                    return;
                }

                if (damageReceiver.IsLocal == false && projectile.Actions.CreatorId != damageReceiver.Id)
                {
                    movement.SetPosition(data.CurrentPosition);
                    projectile.Actions.OnTriggered(damageReceiver);
                    return;
                }

                if (damageReceiver.IsLocal == false && projectile.Actions.CreatorId == damageReceiver.Id)
                {
                    movement.SetPosition(data.CurrentPosition);
                    movement.OnDistancePassed(data.PassedDistance);
                    return;
                }

                if (damageReceiver.IsLocal == false && projectile.Actions.IsLocal == false)
                {
                    movement.SetPosition(data.CurrentPosition);
                    projectile.Actions.OnTriggered(damageReceiver);
                    return;
                }

                _logger.OnTriggered(_buffer[i].name);
                break;
            }

            movement.SetPosition(data.MiddlePoint);
            projectile.Actions.OnCollided();
            _logger.OnCollided(_buffer[0].name);
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