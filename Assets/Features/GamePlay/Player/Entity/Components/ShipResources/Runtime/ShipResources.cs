using GamePlay.Player.Entity.Components.Healths.Runtime;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public class ShipResources : IShipResources, IShipResourcesPresenter
    {
        public ShipResources(IHealth health)
        {
            _health = health;
        }
        
        private readonly IHealth _health;

        private int _maxWeight;
        private int _weight;

        private int _maxCannons;
        private int _cannons;
        
        private int _maxSpeed;
        private int _speed;
        
        private int _maxTeam;
        private int _team;

        public int MaxHealth => _health.Max;
        public int Health => _health.Amount;
        public int Weight => _weight;
        public int MaxWeight => _maxWeight;

        public int MaxCannons => _maxCannons;
        public int Cannons => _cannons;
        
        public int MaxSpeed => _maxSpeed;
        public int Speed => _speed;
        
        public int MaxTeam => _maxTeam;
        public int Team => _team;

        public void SetMaxWeight(int maxWeight)
        {
            _maxWeight = maxWeight;
        }

        public void SetWeight(int weight)
        {
            _weight = weight;
        }

        public void SetMaxCannons(int maxCannons)
        {
            _maxCannons = maxCannons;
        }

        public void SetCannons(int cannons)
        {
            _cannons = cannons;
        }

        public void SetMaxTeam(int maxTeam)
        {
            _maxTeam = maxTeam;
        }
        
        public void SetTeam(int team)
        {
            _team = team;
        }

        public void SetMaxSpeed(int maxSpeed)
        {
            _maxSpeed = maxSpeed;
        }

        public void SetSpeed(int speed)
        {
            _maxSpeed = speed;
        }
    }
}