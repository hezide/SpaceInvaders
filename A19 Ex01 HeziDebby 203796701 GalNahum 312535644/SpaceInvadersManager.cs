using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class SpaceInvadersManager
    {
        private Enemy[,] m_enemiesMat;
        private MotherSpaceship m_motherSpaceship; //important to check if there is a spaceship in game before performing actions
        private PlayerSpaceship m_player;
        private List<GameObject> m_gameObjectsList;
        private int m_hitCounter = 0; // HEZI which hit ?
        private TimeSpan m_randomSpanTime; //could be gametime
        private TimeSpan m_prevRandomSpanTime;
        private Random m_randomMotherSpaceship;
     //   private SpriteBatch m_spriteBatch;
        private GraphicsDevice m_graphicsDevice;
        private ContentManager m_contentManager;

        public SpaceInvadersManager(GraphicsDevice i_graphicsDevice)
        {
            m_graphicsDevice = i_graphicsDevice;
            m_gameObjectsList = new List<GameObject>();
        }

        public void Init(ContentManager i_contentManager)
        {
            m_contentManager = i_contentManager;

            m_player = SpaceInvadersFactory.CreatePlayerSpaceship(m_graphicsDevice);
            m_player.Initialize(m_contentManager);
            m_gameObjectsList.Add(m_player);

            createEnemiesMat(m_contentManager);

            m_randomMotherSpaceship = new Random(); ///
            setRandomTimeSpan();

        }

        private void createEnemiesMat(ContentManager i_contentManager)
        {
            m_enemiesMat = new Enemy[Utilities.k_EnemyMatRows, Utilities.k_EnemyMatCols];

            //float enemyHeight = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Height;
            //float enemyWidth = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Width;
            //float enemyX, enemyY;

            for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
            {
                for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
                {
                    //enemyX = col * enemyWidth + enemyWidth * Utilities.k_EnemyGapMultiplier * col;
                    //enemyY = (row * enemyHeight + enemyHeight * Utilities.k_EnemyGapMultiplier * row) + Utilities.k_InitialHightMultiplier * enemyHeight;

                    Utilities.eDrawableType eEnemyType = getCurrentEnemyType(row);
                    //    Texture2D texture = i_texturesByType[eEnemyType];
                    //      Vector2 position = new Vector2(enemyX, enemyY);

                    m_enemiesMat[row, col] = SpaceInvadersFactory.CreateEnemy(m_graphicsDevice, eEnemyType);
                    m_enemiesMat[row, col].Initialize(i_contentManager);
                    m_enemiesMat[row, col].InitPosition(row, col);
                    // m_drawablesList.Add(m_enemiesMat[row, col]);
                    // m_moveablesList.Add(m_enemiesMat[row, col]);
                    m_gameObjectsList.Add(m_enemiesMat[row, col]);
                }
            }
        }

        private Utilities.eDrawableType getCurrentEnemyType(int i_row)
        {
            Utilities.eDrawableType eEnemyType = Utilities.eDrawableType.PinkEnemy; // default

            if (Utilities.k_BlueEnemyFirstRow <= i_row && Utilities.k_BlueEnemyLastRow >= i_row)
            {
                eEnemyType = Utilities.eDrawableType.BlueEnemy;
            }
            else if (Utilities.k_YellowEnemyFirstRow <= i_row && Utilities.k_YellowEnemyLastRow >= i_row)
            {
                eEnemyType = Utilities.eDrawableType.YellowEnemy;
            }

            return eEnemyType;
        }

        public void Draw(GameTime i_gameTime)
        {

            foreach (GameObject gameObject in m_gameObjectsList)
            {
                gameObject.Draw(i_gameTime);
            }
        }

        public void Update(GameTime i_gameTime)
        {
            if (itsTimeForMotherSpaceship(i_gameTime))
            {
                createMotherSpaceship();
            }

            foreach (GameObject gameObject in m_gameObjectsList)
            {
                gameObject.Update(i_gameTime);
            }

            checkForHits();
        }

        private void checkForHits()
        {
            List<GameObject> objectsToDestroy = new List<GameObject>(); ;

            foreach (GameObject gameObject in m_gameObjectsList)
            {
                if (gameObject is IShooter)
                {
                    objectsToDestroy = getListOfObjectsToDestroy(((gameObject) as IShooter).BulletsList, gameObject as IShooter);
                }
            }

            foreach (GameObject gameObject in objectsToDestroy)
            {
                m_gameObjectsList.Remove(gameObject);
            }
        }

        private List<GameObject> getListOfObjectsToDestroy(List<Bullet> i_bulletsList, IShooter i_shooter)
        {
            List<GameObject> objectsToDestroy = new List<GameObject>();

            foreach (Bullet bullet in i_bulletsList)
            {
                if (bullet.IsVisible)
                {
                    foreach (GameObject gameObject in m_gameObjectsList)
                    {
                        if (gameObject is IDestryoable && gameObject as IShooter != i_shooter)
                        {
                            if (bullet.Rectangle.Intersects((gameObject as IDestryoable).Rectangle))
                            {
                                (gameObject as IDestryoable).GetHit();
                                bullet.Hide();
                            }

                            if ((gameObject as IDestryoable).IsDead())
                            {
                                objectsToDestroy.Add(gameObject);
                            }
                        }
                    }
                }
            }

            return objectsToDestroy;
        }

        private void increaseVelocity()
        {
            throw new NotImplementedException();
        }

        private void createMotherSpaceship()
        {
            if (m_motherSpaceship == null)
            {
                m_motherSpaceship = SpaceInvadersFactory.CreateMotherSpaceship(m_graphicsDevice);
                m_motherSpaceship.Initialize(m_contentManager);
                m_gameObjectsList.Add(m_motherSpaceship);
            }
            else
            {
                m_motherSpaceship.SetToInitialPosition();
            }
            // TODO: if hit - remove from list of objects
        }

        private bool isGameOver()
        {
            throw new NotImplementedException();
        }

        private bool itsTimeForMotherSpaceship(GameTime i_gameTime)
        {
            bool itsTime = false;

            if (i_gameTime.TotalGameTime - m_prevRandomSpanTime > m_randomSpanTime)
            {
                m_prevRandomSpanTime = i_gameTime.TotalGameTime;
                setRandomTimeSpan();
                itsTime = true;
            }

            return itsTime;
        }

        private void setRandomTimeSpan()
        {
            int spanSeconds = m_randomMotherSpaceship.Next(1, 25); // TODO: contans

            m_randomSpanTime = TimeSpan.FromSeconds(spanSeconds);
        }
    }
}
