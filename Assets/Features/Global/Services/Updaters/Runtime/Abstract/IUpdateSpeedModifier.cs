namespace Global.Services.Updaters.Runtime.Abstract
{
    public interface IUpdateSpeedModifier
    {
        float Speed { get; }
        void Add(IUpdateSpeedModifiable modifiable);
        void Remove(IUpdateSpeedModifiable modifiable);
    }
}