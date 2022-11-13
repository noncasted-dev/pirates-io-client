using GamePlay.Common.Damages;

namespace GamePlay.Services.Projectiles.Mover.Abstract
{
    public interface IProjectileActions
    {
        void OnTriggered(IDamageReceiver damageReceiver);
        void OnCollided();
    }
}