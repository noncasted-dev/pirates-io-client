using System;
using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Root.Runtime;
using NaughtyAttributes;
using Pathfinding;
using Ragon.Client;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Player.Bots
{
    public class BotMover : RagonBehaviour
    {
        private IAstarAI _ai;
        private bool _hasTarget = false;
        private Vector2 _target;

        private ICitiesRegistry _citiesRegistry;

        private bool _isBot;

        public override void OnCreatedEntity()
        {
            if (Entity.IsMine == true)
                _isBot = true;
        }

        private void Awake()
        {
            _ai = GetComponent<IAstarAI>();
            _citiesRegistry = FindObjectOfType<CitiesRegistry>();
        }

        private void OnEnable()
        {
            _ai.onSearchPath += OnCalculated;
        }

        private void OnDisable()
        {
            _ai.onSearchPath -= OnCalculated;
        }

        private int _frames;
        [SerializeField] private bool _debug;
        private bool _calculated;

        private float _timer;

        private void Update()
        {
            if (_isBot == false)
                return;
            
            if (_hasTarget == false)
            {
                SetTarget();
            }

            _ai.destination = _target;

            var distance = Vector2.Distance(transform.position, _target);
            if (_debug)
                Debug.Log($"Distance: {distance}");
            if (distance < 30f)
                _timer += Time.deltaTime;

            if (_timer > 100)
            {
                _timer = 0f;
                SetTarget();
            }
        }

        [Button("SetTarget")]
        private void SetTarget()
        {
            var city = (CityType)Random.Range(1, 29);

            var spawnPoints = _citiesRegistry.GetCity(city).SpawnPoints;
            _target = spawnPoints.GetRandom();
            _ai.destination = _target;
            Debug.Log("SetTarget");

            _hasTarget = true;

            _ai.canMove = true;
            _ai.SearchPath();
        }

        private void OnCalculated()
        {
            Debug.Log("Calculated");
            _calculated = true;
        }
    }
}