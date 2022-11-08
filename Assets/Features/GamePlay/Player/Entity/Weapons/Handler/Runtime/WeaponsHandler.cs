using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Weapons.Bow.Root;

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

        private IBow _bow;

        public async UniTask OnAsyncAwake()
        {
            var rangeTask = UniTask.Create(async () => { _bow = await _factory.CreateBow(_config.Range); });

            // var meleeTask = UniTask.Create(async () =>
            // {
            //     _melee = await _factory.Create(_config.Melee);
            // });

            //await UniTask.WhenAll(rangeTask, meleeTask);
            await rangeTask;
        }
        //private IWeapon _melee;

        public IBow Bow => _bow;
    }
}