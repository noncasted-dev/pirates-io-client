namespace Global.Services.Updaters.Logs
{
    public enum UpdaterLogType
    {
        PreUpdatableAdd,
        PreUpdatableRemove,
        PreUpdateCalled,

        UpdatableAdd,
        UpdatableRemove,
        UpdateCalled,

        PreFixedUpdatableAdd,
        PreFixedUpdatableRemove,
        PreFixedUpdateCalled,

        FixedUpdatableAdd,
        FixedUpdatableRemove,
        FixedUpdateCalled,

        PostFixedUpdatableAdd,
        PostFixedUpdatableRemove,
        PostFixedUpdateCalled,

        SpeedModified,
        SpeedModifyError,
        SpeedModifiableAdd,
        SpeedModifiableRemove
    }
}