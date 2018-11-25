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
        SpriteFont          m_spriteFont;
        SpriteBatch         m_spriteBatch;

        public void Initialize(GraphicsDevice i_graphicsDevice,ContentManager i_contentManager)
        {
            m_spriteFont = i_contentManager.Load<SpriteFont>("Score");
            m_spriteBatch = new SpriteBatch(i_graphicsDevice);
        }

        public void UpdateScore(Utilities.eGameObjectType i_TypeOfHitObject)
        {
            if(i_TypeOfHitObject == Utilities.eGameObjectType.BlueEnemy)
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

        public void Draw(GameTime i_gameTime)
        {
            m_spriteBatch.Begin();
            m_spriteBatch.DrawString(m_spriteFont, "Score is: " + CurrentScore, new Vector2(0, 0), Color.White);
            m_spriteBatch.End();
        }
    }
}
