namespace Local.Services.Abstract.Callbacks
{
    public interface ILocalSwitchListener
    {
        void OnEnabled();
        void OnDisabled();
    }
}