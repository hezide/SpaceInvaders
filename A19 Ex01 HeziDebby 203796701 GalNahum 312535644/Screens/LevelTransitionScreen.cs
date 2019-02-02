using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class LevelTransitionScreen : GameScreen
    {
        private float m_RemainingTime = 3f;
        private int m_LevelNum;
        private TextComponent m_CountDownText;
        private TextComponent m_LevelText;

        public LevelTransitionScreen(Game i_Game,int i_LevelNum) : base(i_Game)
        {
            m_LevelNum = i_LevelNum;
            m_CountDownText = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);
            m_LevelText = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);

        }
        public override void Initialize()
        {
            base.Initialize();

            m_CountDownText.Position = CenterOfViewPort;
            m_LevelText.Position = new Vector2(0, 0);
        }

        public override void Update(GameTime i_GameTime)
        {
            m_LevelText.Text = $"GET READY FOR LEVEL " + (m_LevelNum + 1).ToString();
            m_RemainingTime -= (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_CountDownText.Text = Convert.ToInt32(m_RemainingTime).ToString();//round up
            if (m_RemainingTime < 0)
            {
                this.ExitScreen();
            }
        }
    }
}
