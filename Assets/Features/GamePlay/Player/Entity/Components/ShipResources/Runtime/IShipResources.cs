namespace Features.GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public interface IShipResources
    {
        int Health { get; }
        int Weight { get; }
        int Cannons { get; }
        int MaxSpeed { get; }
        int Team { get; }
    }
}