using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.Storage.Events
{
    public readonly struct CargoAddEvent
    {
        public CargoAddEvent(IItem item)
        {
            Item = item;
        }
        
        public readonly IItem Item;
    }
}