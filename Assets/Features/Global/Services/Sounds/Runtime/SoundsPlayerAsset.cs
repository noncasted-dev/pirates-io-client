using Global.Common;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;

namespace Global.Services.Sounds.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "SoundsPlayer",
        menuName = GlobalAssetsPaths.SoundsPlayer + "Service")]
    public class SoundsPlayerAsset : GlobalServiceAsset
    {
        [SerializeField] private SoundsPlayer _prefab;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            var soundsPlayer = Instantiate(_prefab);
            soundsPlayer.name = "SoundsPlayer";

            serviceBinder.AddToModules(soundsPlayer);
            serviceBinder.ListenCallbacks(soundsPlayer);
        }
    }
}