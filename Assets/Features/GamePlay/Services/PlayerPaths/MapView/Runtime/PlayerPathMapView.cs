using System;
using System.Collections.Generic;
using Common.Local.Services.Abstract.Callbacks;
using Common.Structs;
using GamePlay.Services.Maps.Runtime;
using GamePlay.Services.PlayerPaths.Builder.Runtime;
using Global.Services.MessageBrokers.Runtime;
using UnityEngine;

namespace GamePlay.Services.PlayerPaths.MapView.Runtime
{
    [DisallowMultipleComponent]
    public class PlayerPathMapView : MonoBehaviour, ILocalSwitchListener
    {
        [SerializeField] private float _distanceBetweenPoints = 1f;
        [SerializeField] private GameObject _dashPrefab;

        [SerializeField] private Transform _root;
        [SerializeField] private WorldToMapPositionConverter _positionConverter;

        private readonly List<PathPoint> _all = new();

        private IDisposable _buildListener;
        private IDisposable _cancelListener;

        public void OnEnabled()
        {
            _buildListener = Msg.Listen<PlayerPathBuildEvent>(OnBuild);
            _cancelListener = Msg.Listen<PlayerPathCancelEvent>(OnCanceled);
        }

        public void OnDisabled()
        {
            _buildListener?.Dispose();
            _cancelListener?.Dispose();
        }

        private void OnBuild(PlayerPathBuildEvent data)
        {
            _root.gameObject.SetActive(true);

            foreach (var point in _all)
                point.Disable();
            
            var path = new Vector2[data.Path.Length];

            for (var i = 0; i < path.Length; i++)
                path[i] = _positionConverter.ConvertWorldToMap(data.Path[i]);

            var length = CalculateLength(path);
            var count = CalculateRequiredCount(length);
            
            AddPointsOnRequire(count + 10);

            var previous = path[0];
            
            var pathIndex = 1;
            var pointIndex = 0;
            var availableDistance = 0f;

            while (pathIndex < path.Length)
            {
                var current = path[pathIndex];
                var distance = Vector2.Distance(previous, current) + availableDistance;

                if (distance >= _distanceBetweenPoints)
                {
                    var direction = (current - previous).normalized;
                    
                    var angle = direction.ToAngle();
                    var position = previous + direction * _distanceBetweenPoints;

                    var point = _all[pointIndex];
                    point.Enable(position, angle);
                    
                    pointIndex++;
                    availableDistance = 0f;
                    continue;
                }

                previous = current;
                availableDistance = distance;
                pathIndex++;
            }
        }

        private void OnCanceled(PlayerPathCancelEvent data)
        {
            _root.gameObject.SetActive(false);
        }

        private int CalculateRequiredCount(float distance)
        {
            return Mathf.FloorToInt(distance / _distanceBetweenPoints);
        }

        private float CalculateLength(Vector2[] points)
        {
            var length = 0f;

            for (var i = 1; i < points.Length; i++)
            {
                var distance = Vector2.Distance(points[i - 1], points[i]);
                length += distance;
            }

            return length;
        }

        private void AddPointsOnRequire(int required)
        {
            if (_all.Count >= required)
                return;

            var delta = required - _all.Count;

            for (var i = 0; i < delta; i++)
            {
                var instance = Instantiate(_dashPrefab, _root);
                var point = new PathPoint(instance);
                _all.Add(point);
            }
        }
    }
}