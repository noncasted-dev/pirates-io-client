namespace GamePlay.Player.Entity.Components.Boardings.Local.Events
{
    public readonly struct BoardingPreparationEvent
    {
        public BoardingPreparationEvent(float time)
        {
            Time = time;
        }
        
        public readonly float Time;
    }
}