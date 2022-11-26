using Global.Common;
using Ragon.Client;
using UnityEngine;

namespace Global.Services.Network.Connection.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "NetworkConnection",
        menuName = GlobalAssetsPaths.NetworkConnection + "Config")]
    public class NetworkConnectionConfigAsset : ScriptableObject
    {
        [SerializeField] private string _ip;
        [SerializeField] private ushort _port;
        [SerializeField] private RagonSocketType _socketType;
        
        public string Ip => _ip;
        public ushort Port => _port;
        public RagonSocketType SocketType => _socketType;
    }
}