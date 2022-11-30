using System;
using Global.Common;
using Ragon.Client;
using UnityEngine;

namespace Global.Services.Network.Connection.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "NetworkConnection",
        menuName = GlobalAssetsPaths.NetworkConnection + "Config")]
    public class NetworkConnectionConfigAsset : ScriptableObject
    {
        [SerializeField] private string _eu;
        [SerializeField] private string _ny;
        [SerializeField] private string _sf;

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