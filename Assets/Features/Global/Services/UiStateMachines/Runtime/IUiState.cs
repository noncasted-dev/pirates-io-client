namespace Global.Services.UiStateMachines.Runtime
{
    public interface IUiState
    {
        UiConstraints Constraints { get; }
        string Name { get; }

        void Recover();
        void Exit();
    }
}