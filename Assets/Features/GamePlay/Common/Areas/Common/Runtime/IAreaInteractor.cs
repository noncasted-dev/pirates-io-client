namespace GamePlay.Common.Areas.Common.Runtime
{
    public interface IAreaInteractor
    {
        bool IsLocal { get; }
        
        void OnCityEntered();
        void OnCityExited();

        void OnPortEntered();
        void OnPortExited();
    }
}