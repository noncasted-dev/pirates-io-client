namespace Global.Services.AssetsFlow.Logs
{
    public enum AssetsFlowLogType
    {
        Load,
        LoadFail,
        StorageAdd,
        StorageRemove,
        StorageGetResult,
        InstantiatorCreated,
        Preload,
        PreloadFail,
        Instantiate,
        InstantiateFail,
        Release,
        Unload
    }
}