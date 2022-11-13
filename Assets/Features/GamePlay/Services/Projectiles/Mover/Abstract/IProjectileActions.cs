using GamePlay.Common.Damages;

namespace GamePlay.Services.Projectiles.Mover.Abstract
{
    public interface IProjectileActions
    {
        bool IsLocal { get; }
        void OnTriggered(IDamageReceiver damageReceiver);
        void OnCollided();
    }
}