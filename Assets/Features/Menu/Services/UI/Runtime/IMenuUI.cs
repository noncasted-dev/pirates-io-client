namespace Menu.Services.UI.Runtime
{
    public interface IMenuUI
    {
        void OnLogin();
        void OnLoginWithError(string error);
        void OnLoading();
    }
}