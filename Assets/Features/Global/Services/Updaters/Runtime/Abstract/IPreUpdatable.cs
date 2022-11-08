namespace Global.Services.Updaters.Runtime.Abstract
{
    public interface IPreUpdatable
    {
        void OnPreUpdate(float delta);
    }
}