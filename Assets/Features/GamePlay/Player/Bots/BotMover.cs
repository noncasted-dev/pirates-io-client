using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Root.Runtime;
using NaughtyAttributes;
using Pathfinding;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Bots
{
    public class BotMover : RagonBehaviour
    {
        [SerializeField] private bool _debug;
        private IAstarAI _ai;
        private bool _calculated;

        private ICitiesRegistry _citiesRegistry;

        private int _frames;
        private bool _hasTarget = false;

        private bool _isBot;
        private Vector2 _target;

        private float _timer;

        private void Awake()
        {
            _ai = GetComponent<IAstarAI>();
            _citiesRegistry = FindObjectOfType<CitiesRegistry>();
        }

        private void Update()
        {
            if (_isBot == false)
                return;

            if (_hasTarget == false) SetTarget();

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

        private void OnEnable()
        {
            _ai.onSearchPath += OnCalculated;
        }

        private void OnDisable()
        {
            _ai.onSearchPath -= OnCalculated;
        }

        public override void OnCreatedEntity()
        {
            if (Entity.IsMine == true)
                _isBot = true;
        }

        [Button("SetTarget")]
        private void SetTarget()
        {
            var city = (CityType)Random.Range(1, 29);

            var spawnPoints = _citiesRegistry.GetCity(city).SpawnPoints;
            _target = spawnPoints.GetRandom();
            _ai.destination = _target;

            _hasTarget = true;

            _ai.canMove = true;
            _ai.SearchPath();
        }

        private void OnCalculated()
        {
            _calculated = true;
        }
    }
}