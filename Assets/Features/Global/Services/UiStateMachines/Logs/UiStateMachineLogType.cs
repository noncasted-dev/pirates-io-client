namespace Global.Services.UiStateMachines.Logs
{
    public enum UiStateMachineLogType
    {
        EnterSingle,
        EnterStack,
        Exit,
        ExitCurrent,
        ExitStack,
        ExitHead,
        NoPreviousStates,
        NoStateInStackToExit,
        Recovered,
        ReturnToPrevious
    }
}