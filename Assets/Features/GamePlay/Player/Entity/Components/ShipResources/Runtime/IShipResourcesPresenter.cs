namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public interface IShipResourcesPresenter
    {
        public void SetMaxWeight(int maxWeight);
        public void SetWeight(int weight);

        public void SetMaxCannons(int maxCannons);
        public void SetCannons(int cannons);

        public void SetMaxTeam(int maxTeam);
        public void SetTeam(int team);

        public void SetMaxSpeed(int maxSpeed);
        public void SetSpeed(int speed);
    }
}