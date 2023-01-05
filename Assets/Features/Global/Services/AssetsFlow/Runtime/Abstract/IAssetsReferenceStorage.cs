namespace Global.Services.AssetsFlow.Runtime.Abstract
{
    public interface IAssetsReferenceStorage
    {
        bool Contains(object key);
        void Add<T>(AssetLoadResult<T> result);
        void Remove<T>(AssetLoadResult<T> result);
        AssetLoadResult<T> GetResult<T>(object key);
    }
}