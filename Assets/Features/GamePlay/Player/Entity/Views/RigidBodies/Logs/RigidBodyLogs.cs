using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.Views.RigidBodies.Logs
{
    [Serializable]
    public class RigidBodyLogs : ReadOnlyDictionary<RigidBodyLogType, bool>
    {
    }
}