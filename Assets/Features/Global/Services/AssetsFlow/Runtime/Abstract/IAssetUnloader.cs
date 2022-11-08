namespace Global.Services.AssetsFlow.Runtime.Abstract
{
    public interface IAssetUnloader
    {
        void Unload<T>(AssetLoadResult<T> result);
    }
}