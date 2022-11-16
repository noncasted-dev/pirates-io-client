#region

using Ragon.Client;

#endregion

namespace Global.Services.Network.Instantiators.Runtime
{
    public class NetworkInstantiatableObject : RagonBehaviour
    {
        public override void OnCreatedEntity()
        {
            if (IsMine == false)
                return;

            var payload = Entity.GetSpawnPayload<NetworkPayload>();

            transform.position = payload.Position;

            NetworkInstantiator.Instance.OnInstantiated(payload.Id, gameObject);
        }
    }
}