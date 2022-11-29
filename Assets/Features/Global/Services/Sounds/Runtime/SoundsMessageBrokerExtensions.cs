using UniRx;
using UnityEngine;

namespace Global.Services.Sounds.Runtime
{
    public static class SoundsMessageBrokerExtensions
    {
        public static void TriggerSound(this IMessageBroker messageBroker, SoundType sound)
        {
            var data = new SoundEvent(sound);
            messageBroker.Publish(data);
        }
        
        public static void TriggerSound(
            this IMessageBroker messageBroker,
            PositionalSoundType sound,
            Vector2 position,
            GameObject target = null)
        {
            var data = new PositionalSoundEvent(sound, position, target);
            messageBroker.Publish(data);
        }
    }
}