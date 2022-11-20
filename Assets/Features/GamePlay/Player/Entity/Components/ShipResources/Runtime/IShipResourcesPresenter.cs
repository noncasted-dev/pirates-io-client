namespace Features.GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public interface IShipResourcesPresenter
    {
        public void SetWeight(int weight);
        public void SetCannons(int cannons);
        public void SetTeam(int team);
        public void SetSpeed(int speed);
    }
}