using GamePlay.Common.Damages;
using Global.Services.Sounds.Runtime;
using Ragon.Client;
using UniRx;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.DamageProcessors.Runtime
{
    public class LocalHitbox : MonoBehaviour, IDamageReceiver
    {
        public bool IsLocal => true;
        public string Id => RagonNetwork.Room.LocalPlayer.Id;

        public void ReceiveDamage(Damage damage, bool isProjectileLocal)
        {
            MessageBroker.Default.TriggerSound(PositionalSoundType.DamageReceived, damage.Origin);
        }
    }
}