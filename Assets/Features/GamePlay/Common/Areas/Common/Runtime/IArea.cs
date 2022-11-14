namespace GamePlay.Common.Areas.Common.Runtime
{
    public interface IArea
    {
        void OnEntered(IAreaInteractor interactor);
        void OnExited(IAreaInteractor interactor);
    }
}