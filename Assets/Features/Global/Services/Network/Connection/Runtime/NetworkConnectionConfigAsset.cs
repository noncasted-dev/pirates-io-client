using Global.Common;
using UnityEngine;

namespace Global.Services.Network.Connection.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "NetworkConnection",
        menuName = GlobalAssetsPaths.NetworkConnection + "Config")]
    public class NetworkConnectionConfigAsset : ScriptableObject
    {
        [SerializeField] private string _ip;
        [SerializeField] private ushort _port;

        public string Ip => _ip;
        public ushort Port => _port;
    }
}