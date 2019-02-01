using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Infrastructure.ObjectModel
{
    public class SoundSettings
    {
        public SoundSettings()
        {
            IsSoundMuted = false;
            BackgroundMusicVolume = MediaPlayer.Volume;
            SoundEffectsVolume = SoundEffect.MasterVolume;
        }
        public void SetIsMuted(bool i_IsMuted)
        {
            IsSoundMuted = !IsSoundMuted;
            if (i_IsMuted)
            {
                MediaPlayer.IsMuted = true;
                SoundEffect.MasterVolume = 0;
            }
            else
            {
                MediaPlayer.IsMuted = false;
                SoundEffect.MasterVolume = SoundEffectsVolume;
            }
        }
        public bool IsSoundMuted { get; set; }
        public float BackgroundMusicVolume { get; private set; }
        public float SoundEffectsVolume { get; private set; }

        internal void SetSoundEffectVolume(float i_Volume)
        {
            SoundEffectsVolume = i_Volume;
            SoundEffect.MasterVolume = i_Volume;
        }

        internal void SetBackgroundMusicVolume(float i_Volume)
        {
            BackgroundMusicVolume = i_Volume;
            MediaPlayer.Volume = i_Volume;
        }
    }
}
