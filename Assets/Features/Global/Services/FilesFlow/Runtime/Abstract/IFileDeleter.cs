namespace Global.Services.FilesFlow.Runtime.Abstract
{
    public interface IFileDeleter
    {
        public void Delete<T>() where T : SaveData;
        public void Delete(string saveName);
        public void DeleteAll();
    }
}