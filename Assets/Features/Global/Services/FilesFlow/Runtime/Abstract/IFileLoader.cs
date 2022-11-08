namespace Global.Services.FilesFlow.Runtime.Abstract
{
    public interface IFileLoader
    {
        bool Load<T>(out T searched) where T : SaveData;
        bool Load<T>(string saveName, out T searched) where T : SaveData;
        T LoadOrCreate<T>() where T : SaveData, new();
    }
}