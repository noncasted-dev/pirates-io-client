using Global.Services.Common.Abstract;
using UnityEngine;

namespace Global.Services.Common.Config.Abstract
{
    public abstract class GlobalServicesConfig : ScriptableObject
    {
        public abstract GlobalServiceAsset[] GetAssets();
    }
}