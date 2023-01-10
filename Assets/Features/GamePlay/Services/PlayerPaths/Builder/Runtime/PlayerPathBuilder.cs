using System;
using Common.Local.Services.Abstract.Callbacks;
using Features.GamePlay.Services.PlayerPaths.TargetSelection.Runtime;
using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Player.Entity.States.Runs.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Pathfinding;
using UnityEngine;
using VContainer;

namespace Features.GamePlay.Services.PlayerPaths.Builder.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Seeker))]
    public class PlayerPathBuilder : MonoBehaviour, ILocalSwitchListener
    {
        [Inject]
        private void Construct(
            IPlayerEntityProvider player,
            ICitiesRegistry citiesRegistry)
        {
            _citiesRegistry = citiesRegistry;
            _player = player;
            _seeker = GetComponent<Seeker>();
        }

        private IDisposable _targetRequestListener;
        private IDisposable _moveEnterListener;
        
        private IPlayerEntityProvider _player;
        private ICitiesRegistry _citiesRegistry;
        private Seeker _seeker;
        
        public void OnEnabled()
        {
            _targetRequestListener = Msg.Listen<TargetRequestedEvent>(OnTargetRequested);
            _moveEnterListener = Msg.Listen<PlayerMovementStateEnteredEvent>(OnMoveEntered);
        }

        public void OnDisabled()
        {
            _targetRequestListener?.Dispose();
            _moveEnterListener?.Dispose();
        }

        private void OnMoveEntered(PlayerMovementStateEnteredEvent data)
        {
            var cancel = new PlayerPathCancelEvent();
            Msg.Publish(cancel);
        }
        
        private void OnTargetRequested(TargetRequestedEvent data)
        {
            var city = _citiesRegistry.GetCity(data.Target);
            var target = city.SpawnPoints.GetRandom();

            var current = _player.Position;

            var constructedPath = _seeker.StartPath(current, target);
            
            constructedPath.callback += path =>
            {
                var resultPath = new Vector2[path.vectorPath.Count];

                for (var i = 0; i < resultPath.Length; i++)
                    resultPath[i] = path.vectorPath[i];
                    
                var buildEvent = new PlayerPathBuildEvent(data.Target, resultPath);
                Msg.Publish(buildEvent);
            };
        }
    }
}