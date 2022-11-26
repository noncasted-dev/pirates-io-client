using Common.EditableScriptableObjects.Attributes;
using Global.Common;
using Global.Services.Common.Abstract;
using Global.Services.Network.Connection.Logs;
using Global.Services.Network.Connection.Runtime;
using Global.Services.Network.EventsRegistries.Runtime;
using Global.Services.Network.Instantiators.Logs;
using Global.Services.Network.Instantiators.Runtime;
using Global.Services.Network.Session.Join.Logs;
using Global.Services.Network.Session.Join.Runtime;
using Global.Services.Network.Session.Leave.Logs;
using Global.Services.Network.Session.Leave.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Global.Services.Network.Bootstrap
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "NetworkBootstrap",
        menuName = GlobalAssetsPaths.NetworkBootstrap)]
    public class NetworkAsset : GlobalServiceAsset
    {
        [SerializeField]  private NetworkConnectionConfigAsset _connectionConfig;
        [SerializeField]  private NetworkConnectorLogSettings _connectorLogSettings;
        [SerializeField]  private NetworkInstantiatorLogSettings _instantiatorLogSettings;

        [SerializeField] private NetworkConnector _prefab;
        [SerializeField]  private NetworkSessionJoinLogSettings _sessionJoinLogSettings;
        [SerializeField]  private NetworkSessionLeaveLogSettings _sessionLeaveLogSettings;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var connector = Instantiate(_prefab);
            connector.name = "Network";

            var joiner = connector.GetComponent<NetworkSessionJoiner>();
            var leaver = connector.GetComponent<NetworkSessionLeaver>();
            var instantiator = connector.GetComponent<NetworkInstantiator>();

            builder.Register<NetworkConnectorLogger>(Lifetime.Scoped)
                .WithParameter(_connectorLogSettings);

            builder.Register<NetworkSessionJoinLogger>(Lifetime.Scoped)
                .WithParameter(_sessionJoinLogSettings);

            builder.Register<NetworkSessionLeaveLogger>(Lifetime.Scoped)
                .WithParameter(_sessionLeaveLogSettings);

            builder.Register<NetworkInstantiatorLogger>(Lifetime.Scoped)
                .WithParameter(_instantiatorLogSettings);

            builder.RegisterComponent(instantiator)
                .As<INetworkInstantiator>();

            builder.RegisterComponent(connector)
                .WithParameter(_connectionConfig)
                .AsImplementedInterfaces();

            builder.RegisterComponent(joiner)
                .As<INetworkSessionJoiner>();

            builder.RegisterComponent(leaver)
                .As<INetworkSessionLeaver>();

            var networkRegistry = new NetworkEventsRegistry();
            networkRegistry.Register();

            serviceBinder.AddToModules(connector);
            serviceBinder.ListenCallbacks(connector);
            serviceBinder.ListenCallbacks(instantiator);
        }
    }
}