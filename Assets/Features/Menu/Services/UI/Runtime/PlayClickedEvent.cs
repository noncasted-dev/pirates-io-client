using Global.Services.Network.Connection.Runtime;

namespace Menu.Services.UI.Runtime
{
    public readonly struct PlayClickedEvent
    {
        public PlayClickedEvent(string name, TargetServer server)
        {
            Name = name;
            Server = server;
        }

        public readonly string Name;
        public readonly TargetServer Server;
    }
}