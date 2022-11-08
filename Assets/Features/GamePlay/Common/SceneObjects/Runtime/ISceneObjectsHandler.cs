namespace GamePlay.Common.SceneObjects.Runtime
{
    public interface ISceneObjectsHandler
    {
        void InvokeAwake();
        void InvokeEnable();
        void InvokeStart();
        void InvokeDisable();
        void InvokeDestroy();

        void InvokeFullStartup();
        void InvokeFullUnloading();
    }
}