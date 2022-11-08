namespace Local.Services.Abstract.Callbacks
{
    public interface ILocalSwitchCallbackListener
    {
        void OnEnabled();
        void OnDisabled();
    }
}