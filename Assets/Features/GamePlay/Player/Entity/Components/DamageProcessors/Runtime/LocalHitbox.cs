using GamePlay.Common.Damages;
using UnityEngine;

namespace Features.GamePlay.Player.Entity.Components.DamageProcessors.Runtime
{
    public class LocalHitbox : MonoBehaviour, IDamageReceiver
    {
        public bool IsLocal => true;
        
        public void ReceiveDamage(Damage damage, bool isProjectileLocal)
        {
            
        }
    }
}