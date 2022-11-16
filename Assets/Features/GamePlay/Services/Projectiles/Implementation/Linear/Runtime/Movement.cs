#region

using System;
using GamePlay.Services.Projectiles.Mover;
using GamePlay.Services.Projectiles.Mover.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Services.Projectiles.Implementation.Linear.Runtime
{
    public class Movement : IProjectileMovement
    {
        public Movement(
            ProjectileRaycastData raycastData,
            Transform transform,
            Action droppedCallback)
        {
            _transform = transform;
            _droppedCallback = droppedCallback;
            RaycastData = raycastData;

            transform.rotation = Quaternion.Euler(0f, 0f, raycastData.Angle);
        }

        private readonly Action _droppedCallback;
        private readonly Transform _transform;

        private MovementData _data;

        private float _passedDistance;

        public ProjectileRaycastData RaycastData { get; }

        public ProjectileMoveData CreateMoveData(float delta)
        {
            var data = new ProjectileMoveData(
                _transform.position,
                _data.Direction,
                _data.Speed);

            return data;
        }

        public void SetPosition(Vector2 position)
        {
            _transform.position = position;
        }

        public void OnDistancePassed(float distance)
        {
            _passedDistance += distance;

            var progress = _passedDistance / _data.Distance;

            progress = Mathf.Clamp01(progress);
            progress = 1f - progress;

            var scale = Vector3.one * Mathf.Pow(progress, 1f / 10f);

            _transform.localScale = scale;

            if (_passedDistance > _data.Distance)
                _droppedCallback?.Invoke();
        }

        public void Setup(float angle, MovementData data)
        {
            RaycastData.Setup(angle);
            _data = data;
            _passedDistance = 0f;
        }
    }
}