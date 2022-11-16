using Global.Services.Loggers.Runtime;
using VContainer;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public class PlayerBodyTransform : TransformView, IBodyTransform
    {
        [Inject]
        private void Construct(ILogger logger)
        {
            CreateLogger(logger);
        }
    }
}