using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class ScoreManager
    {
        public int          CurrentScore { get; private set; }
        SpriteFont          m_SpriteFont;
        SpriteBatch         m_SpriteBatch;

        public void Initialize(GraphicsDevice i_GraphicsDevice, ContentManager i_ContentManager)
        {
            // TODO: Ex2
            m_SpriteFont = i_ContentManager.Load<SpriteFont>("Score");
            m_SpriteBatch = new SpriteBatch(i_GraphicsDevice);
        }

        public void UpdateScore(Utilities.eGameObjectType i_TypeOfHitObject)
        {
            if (i_TypeOfHitObject == Utilities.eGameObjectType.BlueEnemy)
            {
                CurrentScore += 140;
            }
            else if (i_TypeOfHitObject == Utilities.eGameObjectType.PinkEnemy)
            {
                CurrentScore += 260;
            }
            else if (i_TypeOfHitObject == Utilities.eGameObjectType.YellowEnemy)
            {
                CurrentScore += 110;
            }
            else if (i_TypeOfHitObject == Utilities.eGameObjectType.MotherSpaceship)
            {
                CurrentScore += 850;
            }
            else if (i_TypeOfHitObject == Utilities.eGameObjectType.Spaceship)
            {
                CurrentScore -= 1100;
                if(CurrentScore < 0)
                {
                    CurrentScore = 0;
                }
            }
        }

        public void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.Begin();
            // TODO: Ex2
            m_SpriteBatch.DrawString(m_SpriteFont, "Score is: " + CurrentScore, new Vector2(0, 0), Color.White);
            m_SpriteBatch.End();
        }
    }
}
