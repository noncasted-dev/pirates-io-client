namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public interface IShipResources
    {
        int MaxHealth { get; }
        int Health { get; }
        
        int MaxWeight { get; }
        int Weight { get; }
        
        int MaxCannons { get; }
        int Cannons { get; }
        
        int MaxSpeed { get; }
        int Speed { get; }
        
        int MaxTeam { get; }
        int Team { get; }
    }
}