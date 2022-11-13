using Cysharp.Threading.Tasks;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public interface IAimView
    {
        UniTask<AimResult> AimAsync();
        void OnBroke();
    }
}