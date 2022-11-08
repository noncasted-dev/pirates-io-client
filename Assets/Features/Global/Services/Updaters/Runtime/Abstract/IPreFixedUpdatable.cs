namespace Global.Services.Updaters.Runtime.Abstract
{
    public interface IPreFixedUpdatable
    {
        void OnPreFixedUpdate(float delta);
    }
}