namespace GamePlay.Common.Damages
{
    public interface IDamageReceiver
    {
        bool IsLocal { get; }
        string Id { get; }
        void ReceiveDamage(Damage damage, bool isProjectileLocal);
    }
}