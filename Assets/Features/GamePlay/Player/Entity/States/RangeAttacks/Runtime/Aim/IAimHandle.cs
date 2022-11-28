namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public interface IAimHandle
    {
        float Progress { get; }
        bool IsCanceled { get; }
    }
}