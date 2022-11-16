#region

using Cysharp.Threading.Tasks;

#endregion

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public interface IAimView
    {
        UniTask<AimResult> AimAsync();
        void OnBroke();
    }
}