namespace GamePlay.Player.Entity.Setup.Flow.Callbacks
{
    public interface ICallbackRegister
    {
        void Add<T>(T component);
    }
}