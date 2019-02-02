using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.MenuItems;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class SoundSettingsMenuScreen : MenuGameScreen
    {
        public SoundSettingsMenuScreen(Game i_Game) : base(i_Game, "Sound Settings"){}
        ISoundSettings m_SoundSettings;

        public override void Initialize()
        {
            m_SoundSettings = this.Game.Services.GetService(typeof(IGameSettings)) as ISoundSettings;
            createToggleSoundMenuItem(m_SoundSettings.IsMuted());
            createBackgroundMusicVolumeMenuItem();
            createSoundEffectsVolumeMenuItem();
            createDoneMenuItem();

            base.Initialize();
        }

        private void createToggleSoundMenuItem(bool i_IsMuted)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Toggle Sound");
            multipleSelectionCreatedItem.AddOption("On", () => { mute(!i_IsMuted); }, !i_IsMuted);
            multipleSelectionCreatedItem.AddOption("Off", () => { mute(i_IsMuted); }, i_IsMuted);
            AddItem(multipleSelectionCreatedItem, true);
        }

        private void mute(bool i_Mute)
        {
            m_SoundSettings.SetIsMuted(true);
        }

        private void createBackgroundMusicVolumeMenuItem()
        {
            int volume = Convert.ToInt32(m_SoundSettings.GetMusicVolume() * 100);

            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Background Music Volume");
            multipleSelectionCreatedItem.AddOption("0", () => { setBackgroundVolume(0); }, 0 <= volume && volume > 5);
            multipleSelectionCreatedItem.AddOption("10", () => { setBackgroundVolume(10); }, 5 <= volume && volume > 15);
            multipleSelectionCreatedItem.AddOption("20", () => { setBackgroundVolume(20); }, 15 <= volume && volume > 25);
            multipleSelectionCreatedItem.AddOption("30", () => { setBackgroundVolume(30); }, 25 <= volume && volume > 35);
            multipleSelectionCreatedItem.AddOption("40", () => { setBackgroundVolume(40); }, 35 <= volume && volume > 45);
            multipleSelectionCreatedItem.AddOption("50", () => { setBackgroundVolume(50); }, 45 <= volume && volume > 55);
            multipleSelectionCreatedItem.AddOption("60", () => { setBackgroundVolume(60); }, 55 <= volume && volume > 65);
            multipleSelectionCreatedItem.AddOption("70", () => { setBackgroundVolume(70); }, 65 <= volume && volume > 75);
            multipleSelectionCreatedItem.AddOption("80", () => { setBackgroundVolume(80); }, 75 <= volume && volume > 85);
            multipleSelectionCreatedItem.AddOption("90", () => { setBackgroundVolume(90); }, 85 <= volume && volume > 95);
            multipleSelectionCreatedItem.AddOption("100", () => { setBackgroundVolume(100); }, 95 <= volume && volume > 100);

            AddItem(multipleSelectionCreatedItem, false);
        }

        private void setBackgroundVolume(int i_Volume)
        {
            m_SoundSettings.SetMusicVolume((float)i_Volume / 100);
        }

        private void createSoundEffectsVolumeMenuItem()
        {
            int volume = Convert.ToInt32(m_SoundSettings.GetEffectsVolume()*100);

            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Sounds Effect Music Volume");
            multipleSelectionCreatedItem.AddOption("0", () =>   { setSoundEffectVolume(0)   ;}, 0 <= volume && volume > 5   );
            multipleSelectionCreatedItem.AddOption("10", () =>  { setSoundEffectVolume(10)  ;}, 5 <= volume && volume > 15  );
            multipleSelectionCreatedItem.AddOption("20", () =>  { setSoundEffectVolume(20)  ;}, 15 <= volume && volume > 25 );
            multipleSelectionCreatedItem.AddOption("30", () =>  { setSoundEffectVolume(30)  ;}, 25 <= volume && volume > 35 );
            multipleSelectionCreatedItem.AddOption("40", () =>  { setSoundEffectVolume(40)  ;}, 35 <= volume && volume > 45 );
            multipleSelectionCreatedItem.AddOption("50", () =>  { setSoundEffectVolume(50)  ;}, 45 <= volume && volume > 55 );
            multipleSelectionCreatedItem.AddOption("60", () =>  { setSoundEffectVolume(60)  ;}, 55 <= volume && volume > 65 );
            multipleSelectionCreatedItem.AddOption("70", () =>  { setSoundEffectVolume(70)  ;}, 65 <= volume && volume > 75 );
            multipleSelectionCreatedItem.AddOption("80", () =>  { setSoundEffectVolume(80)  ;}, 75 <= volume && volume > 85 );
            multipleSelectionCreatedItem.AddOption("90", () =>  { setSoundEffectVolume(90)  ;}, 85 <= volume && volume > 95 );
            multipleSelectionCreatedItem.AddOption("100", () => { setSoundEffectVolume(100) ;}, 95 <= volume && volume > 100);

            AddItem(multipleSelectionCreatedItem, false);
        }

        private void setSoundEffectVolume(int i_Volume)
        {
            m_SoundSettings.SetEffectVolume((float)i_Volume/100);
        }
    }
}
