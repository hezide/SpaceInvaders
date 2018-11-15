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
        private Enemy[,]        Enemies { get; set; }
        private MotherSpaceship m_MotherSpaceship;//important to check if there is a spaceship in game before performing actions
        private Player          m_Player;
        private int             m_HitCounter = 0;
        private int             m_RandomMotherSpaceshipSpawnTime;//could be gametime
        private readonly int    k_EnemyMatRows = 9;
        private readonly int    k_EnemyMatCols = 5;

        public void Init()
        {
            int enemyX = 0, enemyY = 96;
            for (int row = 0 ; row < k_EnemyMatRows; row++)
            {
                for (int col = 0 ; col < k_EnemyMatCols ; col++)
                {
                    Enemies[row, col] = new Enemy();
                    Enemies[row, col].Init();

                }
            }
        }

        
        public void Update(GameTime gameTime)
        {

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

        internal void InitAllTextures(
            Texture2D i_pinkTexture,
            Texture2D i_blueTexture,
            Texture2D i_yellowTexture, 
            Texture2D i_bulletTexture,
            Texture2D i_motherShipTexture, 
            Texture2D i_playerTexture)
        {
            foreach (Enemy enemy in Enemies)
            {
                switch (enemy.Type )
                {
                    case Utilities.eType.BlueEnemy:
                        enemy.Texture = i_blueTexture;
                        break;
                    case Utilities.eType.PinkEnemy:
                        enemy.Texture = i_pinkTexture;
                        break;
                    case Utilities.eType.YellowEnemy:
                        enemy.Texture = i_yellowTexture;
                        break;
                }
            }
            m_Player.Texture = i_playerTexture;
            m_MotherSpaceship.Texture = i_motherShipTexture;
        }
    }
}
