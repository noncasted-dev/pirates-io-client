namespace Common.Local.Services.Abstract.Callbacks
{
    public interface ILocalSwitchListener
    {
        void OnEnabled();
        void OnDisabled();
    }
}