namespace Global.Services.Sounds.Runtime
{
    public readonly struct SoundEvent
    {
        public SoundEvent(SoundType type)
        {
            Type = type;
        }
        
        public readonly SoundType Type;
    }
}