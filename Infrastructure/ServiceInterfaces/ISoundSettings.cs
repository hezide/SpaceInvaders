namespace Infrastructure.ObjectModel
{
    public interface ISoundSettings
    {
        bool IsMuted();
        void SetIsMuted(bool i_IsMuted);
        float GetMusicVolume();
        float GetEffectsVolume();
        void SetMusicVolume(float i_Volume);
        void SetEffectVolume(float i_Volume);
    }
}