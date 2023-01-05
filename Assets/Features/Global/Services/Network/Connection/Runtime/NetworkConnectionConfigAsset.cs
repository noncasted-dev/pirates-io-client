using System;
using Global.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Network.Connection.Runtime
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "NetworkConnection",
        menuName = GlobalAssetsPaths.NetworkConnection + "Config")]
    public class NetworkConnectionConfigAsset : ScriptableObject
    {
        [SerializeField] [Indent] private string _eu;
        [SerializeField] [Indent] private string _ny;
        [SerializeField] [Indent] private string _sf;

        public string GetRoute(TargetServer server)
        {
            return server switch
            {
                TargetServer.Europe => _eu,
                TargetServer.USA_NewYork => _ny,
                TargetServer.USA_SanFrancisco => _sf,
                _ => throw new ArgumentOutOfRangeException(nameof(server), server, null)
            };
        }
    }
}