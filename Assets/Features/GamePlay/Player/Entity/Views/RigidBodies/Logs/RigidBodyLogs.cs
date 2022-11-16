#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace GamePlay.Player.Entity.Views.RigidBodies.Logs
{
    [Serializable]
    public class RigidBodyLogs : ReadOnlyDictionary<RigidBodyLogType, bool>
    {
    }
}