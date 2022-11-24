using UniRx;

namespace Global.Services.Sounds.Runtime
{
    public static class SoundsMessageBrokerExtensions
    {
        public static void TriggerSound(this MessageBroker messageBroker, SoundType sound)
        {
            var data = new SoundEvent(sound);
            messageBroker.Publish(data);
        }
    }
}