using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Common.Abstract.Scenes;
using Global.Services.Network.Connection.Logs;
using Global.Services.Network.Connection.Runtime;
using Global.Services.Network.EventsRegistries.Runtime;
using Global.Services.Network.Instantiators.Logs;
using Global.Services.Network.Instantiators.Runtime;
using Global.Services.Network.Session.Join.Logs;
using Global.Services.Network.Session.Join.Runtime;
using Global.Services.Network.Session.Leave.Logs;
using Global.Services.Network.Session.Leave.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Network.Bootstrap
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "NetworkBootstrap",
        menuName = GlobalAssetsPaths.NetworkBootstrap)]
    public class NetworkAsset : GlobalServiceAsset
    {
        [SerializeField] private NetworkConnector _prefab;

        [SerializeField] private NetworkConnectionConfigAsset _connectionConfig;

        [SerializeField] [BoxGroup("Logs")] private NetworkConnectorLogSettings _connectorLogSettings;
        [SerializeField] [BoxGroup("Logs")] private NetworkInstantiatorLogSettings _instantiatorLogSettings;
        [SerializeField] [BoxGroup("Logs")] private NetworkSessionJoinLogSettings _sessionJoinLogSettings;
        [SerializeField] [BoxGroup("Logs")] private NetworkSessionLeaveLogSettings _sessionLeaveLogSettings;

        public override async UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks)
        {
            var connector = Instantiate(_prefab);
            connector.name = "Network";

            var joiner = connector.GetComponent<NetworkSessionJoiner>();
            var leaver = connector.GetComponent<NetworkSessionLeaver>();
            var instantiator = connector.GetComponent<NetworkInstantiator>();

            builder.Register<NetworkConnectorLogger>()
                .WithParameter(_connectorLogSettings);

            builder.Register<NetworkSessionJoinLogger>()
                .WithParameter(_sessionJoinLogSettings);

            builder.Register<NetworkSessionLeaveLogger>()
                .WithParameter(_sessionLeaveLogSettings);

            builder.Register<NetworkInstantiatorLogger>()
                .WithParameter(_instantiatorLogSettings);

            builder.RegisterComponent(instantiator)
                .As<INetworkInstantiator>()
                .AsCallbackListener();

            builder.RegisterComponent(connector)
                .WithParameter(_connectionConfig)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            builder.RegisterComponent(joiner)
                .As<INetworkSessionJoiner>();

            builder.RegisterComponent(leaver)
                .As<INetworkSessionLeaver>();

            var networkRegistry = new NetworkEventsRegistry();
            networkRegistry.Register();

            serviceBinder.AddToModules(connector);
        }
    }
}