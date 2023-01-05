using Global.Services.MessageBrokers.Runtime;
using UniRx;
using UnityEngine;

namespace Global.Services.Sounds.Runtime
{
    public static class MessageBrokerSoundExtensions
    {
        public static void TriggerSound(SoundType sound)
        {
            var data = new SoundEvent(sound);
            Msg.Publish(data);
        }
        
        public static void TriggerSound(
            PositionalSoundType sound,
            Vector2 position,
            GameObject target = null)
        {
            var data = new PositionalSoundEvent(sound, position, target);
            Msg.Publish(data);
        }
    }
}