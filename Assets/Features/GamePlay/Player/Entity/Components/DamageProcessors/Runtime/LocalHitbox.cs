using GamePlay.Common.Damages;
using Ragon.Client;
using UnityEngine;

namespace Features.GamePlay.Player.Entity.Components.DamageProcessors.Runtime
{
    public class LocalHitbox : MonoBehaviour, IDamageReceiver
    {
        public bool IsLocal => true;
        public string Id => RagonNetwork.Room.LocalPlayer.Id;

        public void ReceiveDamage(Damage damage, bool isProjectileLocal)
        {
            
        }
    }
}