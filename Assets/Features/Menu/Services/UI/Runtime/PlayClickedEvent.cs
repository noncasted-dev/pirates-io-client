namespace Menu.Services.UI.Runtime
{
    public class PlayClickedEvent
    {
        public PlayClickedEvent(string name)
        {
            Name = name;
        }
        
        public readonly string Name;
    }
}