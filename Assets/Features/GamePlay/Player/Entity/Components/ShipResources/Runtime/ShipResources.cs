using GamePlay.Player.Entity.Components.Healths.Runtime;

namespace Features.GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public class ShipResources : IShipResources, IShipResourcesPresenter
    {
        public ShipResources(IHealth health)
        {
            _health = health;
        }
        
        private readonly IHealth _health;

        private int _weight;
        private int _cannons;
        private int _maxSpeed;
        private int _team;

        public int Health => _health.Amount;
        public int Weight => _weight;
        public int Cannons => _cannons;
        public int MaxSpeed => _maxSpeed;
        public int Team => _team;

        public void SetWeight(int weight)
        {
            _weight = weight;
        }

        public void SetCannons(int cannons)
        {
            _cannons = cannons;
        }

        public void SetTeam(int team)
        {
            _team = team;
        }

        public void SetSpeed(int speed)
        {
            _maxSpeed = speed;
        }
    }
}