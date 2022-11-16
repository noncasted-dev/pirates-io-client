#region

using GamePlay.Common.Damages;

#endregion

namespace GamePlay.Services.Projectiles.Mover.Abstract
{
    public interface IProjectileActions
    {
        bool IsLocal { get; }
        string CreatorId { get; }
        void OnTriggered(IDamageReceiver damageReceiver);
        void Destroy();
        void OnCollided();
    }
}