using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel
{
    public abstract class GameSettings : IGameSettings,ISoundSettings
    {
        public int CurrentLevel { get; private set; } = 0;
        private SoundSettings m_SoundSettings;

        public GameSettings(Game i_Game)
        {
            i_Game.Services.AddService(typeof(IGameSettings), this);
            m_SoundSettings = new SoundSettings();
        }

        protected virtual void setLevel(int i_Level)
        {
            CurrentLevel = i_Level;
        }

        public void GoToNextLevel()
        {
            setLevel(++CurrentLevel);
        }

        public void ResetLevel()
        {
            setLevel(0);
        }

        //Sound Interface
        public bool IsMuted()
        {
            return m_SoundSettings.IsSoundMuted;
        }

        public void SetIsMuted(bool i_IsMuted)
        {
            m_SoundSettings.SetIsMuted(i_IsMuted);
        }

        public float GetMusicVolume()
        {
            return m_SoundSettings.BackgroundMusicVolume;
        }

        public float GetEffectsVolume()
        {
            return m_SoundSettings.SoundEffectsVolume;
        }

        public void SetMusicVolume(float i_Volume)
        {
            m_SoundSettings.SetBackgroundMusicVolume(i_Volume);
        }

        public void SetEffectVolume(float i_Volume)
        {
            m_SoundSettings.SetSoundEffectVolume(i_Volume);
        }
    }
}