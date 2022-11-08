namespace Global.Services.FilesFlow.Runtime.Abstract
{
    public interface IFileSaver
    {
        public bool Save<T>(T save) where T : SaveData;
    }
}