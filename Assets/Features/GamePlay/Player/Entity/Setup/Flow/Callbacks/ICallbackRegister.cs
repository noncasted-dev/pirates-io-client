namespace GamePlay.Player.Entity.Setup.Flow.Callbacks
{
    public interface ICallbackRegister
    {
        void Listen<T>(T component);
    }
}