using GamePlay.Cities.Global.Registry.Runtime;
using GamePlay.Cities.Instance.Root.Runtime;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Player.Bots
{
    public class BotMover : MonoBehaviour
    {
        private IAstarAI _ai;
        private bool _hasTarget = false;
        private Vector2 _target;
        
        private ICitiesRegistry _citiesRegistry;
        
        private void Awake()
        {
            _ai = GetComponent<IAstarAI>();
            _citiesRegistry = FindObjectOfType<CitiesRegistry>();
        }

        private int _frames;

        private void Update()
        {
            _frames++;
            
            if (_frames < 200)
                return;

            _frames = 0;
            
            _ai.SearchPath();

            _ai.destination = _target;
            
            if (_hasTarget == false)
                SetTarget();

            var distance = Vector2.Distance(transform.position, _target);
            
            if (distance < 30f)
                SetTarget();
        }

        private void SetTarget()
        {
            var city = (CityType)Random.Range(1, 29);

            var spawnPoints = _citiesRegistry.GetCity(city).SpawnPoints;
            _target = spawnPoints.GetRandom();
            _ai.destination = _target;
            

            _hasTarget = true;
        }
    }
}