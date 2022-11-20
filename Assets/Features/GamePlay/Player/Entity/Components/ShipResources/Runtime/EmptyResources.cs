namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public class EmptyResources : IShipResources
    {
        public int MaxHealth => 0;
        public int Health => 0;
        public int MaxWeight => 0;
        public int Weight => 0;
        public int MaxCannons => 0;
        public int Cannons => 0;
        public int MaxSpeed => 0;
        public int Speed => 0;
        public int MaxTeam => 0;
        public int Team => 0;
    }
}