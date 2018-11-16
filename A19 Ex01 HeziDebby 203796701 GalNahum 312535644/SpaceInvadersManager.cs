using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class SpaceInvadersManager
    {
        private Enemy[,]                    Enemies { get; set; }
        private MotherSpaceship             m_MotherSpaceship;//important to check if there is a spaceship in game before performing actions
        private Player                      m_Player;
        private List<Interfaces.IDrawable>  m_Drawables;
        private List<Interfaces.IMoveable>  m_Moveables;
        private int                         m_HitCounter = 0;
        private int                         m_RandomMotherSpaceshipSpawnTime;//could be gametime
        SpriteBatch                         m_SpriteBatch;

        public SpaceInvadersManager()
        {
            m_Drawables = new List<Interfaces.IDrawable>();
            m_Moveables = new List<Interfaces.IMoveable>();

            Enemies = new Enemy[Utilities.k_EnemyMatRows, Utilities.k_EnemyMatCols];
        }
        public void Init(Dictionary<Utilities.eDrawableType,Texture2D> i_Textures, SpriteBatch i_SpriteBatch)
        {
            m_SpriteBatch = i_SpriteBatch;
            InitAllEnemies(i_Textures);
            //TODO:: init all the rest        
        }

        private void InitAllEnemies(Dictionary<Utilities.eDrawableType, Texture2D> i_Textures)
        {
            float enemyHeight = i_Textures[Utilities.eDrawableType.PinkEnemy].Height;
            float enemyWidth = i_Textures[Utilities.eDrawableType.PinkEnemy].Width;
            float enemyX, enemyY;
            for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
            {
                for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
                {
                    enemyX = col * enemyWidth + enemyWidth * Utilities.k_EnemyGapMultiplier * col;
                    enemyY = (row * enemyHeight + enemyHeight * Utilities.k_EnemyGapMultiplier* row )+ Utilities.k_InitialHightMultiplier * enemyHeight;

                    Enemies[row, col] = new Enemy();
                    //Initialize pink enemies
                    if (Utilities.k_PinkEnemyFirstRow <= row && Utilities.k_PinkEnemyLastRow >= row)
                    {
                        Enemies[row, col].Init(
                            Utilities.eDrawableType.PinkEnemy,
                            i_Textures[Utilities.eDrawableType.PinkEnemy],
                            Color.Pink,
                            new Vector2(enemyX, enemyY),
                            Utilities.eDirection.Right,
                            i_Textures[Utilities.eDrawableType.PinkEnemy].Width,
                            m_SpriteBatch);
                    }
                    else if (Utilities.k_BlueEnemyFirstRow <= row && Utilities.k_BlueEnemyLastRow >= row)
                    {
                        Enemies[row, col].Init(
                            Utilities.eDrawableType.BlueEnemy,
                            i_Textures[Utilities.eDrawableType.BlueEnemy],
                            Color.Blue,
                            new Vector2(enemyX, enemyY),
                            Utilities.eDirection.Right,
                            i_Textures[Utilities.eDrawableType.BlueEnemy].Width,
                            m_SpriteBatch);
                    }
                    else if (Utilities.k_YellowEnemyFirstRow <= row && Utilities.k_YellowEnemyLastRow >= row)
                    {
                        Enemies[row, col].Init(
                            Utilities.eDrawableType.YellowEnemy,
                            i_Textures[Utilities.eDrawableType.YellowEnemy],
                            Color.Yellow,
                            new Vector2(enemyX, enemyY),
                            Utilities.eDirection.Right,
                            i_Textures[Utilities.eDrawableType.YellowEnemy].Width,
                            m_SpriteBatch);
                    }
                    m_Drawables.Add(Enemies[row, col]);
                    m_Moveables.Add(Enemies[row, col]);
                    //enemyX += 30;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Interfaces.IDrawable drawable in m_Drawables)
            {
                drawable.Draw(gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Interfaces.IMoveable moveable in m_Moveables)
            {
                moveable.Update(gameTime);
            }
        }

        private void increaseVelocity()
        {
            throw new NotImplementedException();
        }
        private void createMotherSpaceship()
        {
            throw new NotImplementedException();
        }
        private bool isGameOver()
        {
            throw new NotImplementedException();
        }

        internal void GetAllDrawings()
        {
            throw new NotImplementedException();
        }
    }
}
