#region

using Cysharp.Threading.Tasks;

#endregion

namespace GamePlay.Services.TransitionScreens.Runtime
{
    public interface ITransitionScreen
    {
        void ToPlayerRespawn();
        void ToPlayerDeath();
        UniTask FadeIn();
        UniTask FadeOut();
    }
}