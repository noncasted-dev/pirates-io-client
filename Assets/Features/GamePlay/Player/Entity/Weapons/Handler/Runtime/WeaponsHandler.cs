using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Weapons.Cannon.Root;

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    public class WeaponsHandler : IWeaponsHandler, IAsyncAwakeCallback
    {
        public WeaponsHandler(
            DefaultWeaponsConfig config,
            IWeaponsFactory factory)
        {
            _config = config;
            _factory = factory;
        }

        private readonly DefaultWeaponsConfig _config;
        private readonly IWeaponsFactory _factory;

        private ICanon _canon;

        public async UniTask OnAsyncAwake()
        {
            var rangeTask = UniTask.Create(async () => { _canon = await _factory.CreateBow(_config.Canon); });
            
            await rangeTask;
        }

        public ICanon Canon => _canon;
    }
}