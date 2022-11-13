namespace GamePlay.Common.Damages
{
    public interface IDamageReceiver
    {
        bool IsLocal { get; }
        void ReceiveDamage(Damage damage);
    }
}