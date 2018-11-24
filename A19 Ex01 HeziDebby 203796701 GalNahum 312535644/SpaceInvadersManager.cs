﻿using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
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
        private Enemy[,]                m_enemiesMat;
        private MotherSpaceship         m_motherSpaceship; //important to check if there is a spaceship in game before performing actions
        private PlayerSpaceship         m_player;
        private List<GameObject>        m_gameObjectsList;
        private int                     m_enemyHitCounter = 0;
        private GraphicsDevice          m_graphicsDevice;
        private ContentManager          m_contentManager;
        private RandomActionComponent   m_spaceShipRandomNotifier;
        private ScoreManager            m_scoreManager;
        public Action<int>              GameOver;
        public SpaceInvadersManager(GraphicsDevice i_graphicsDevice)
        {
            m_graphicsDevice = i_graphicsDevice;
            m_gameObjectsList = new List<GameObject>();
            m_scoreManager = new ScoreManager();
        }

        public void Init(ContentManager i_contentManager)
        {
            m_contentManager = i_contentManager;
            m_scoreManager.Initialize(m_graphicsDevice, i_contentManager);
            m_player = SpaceInvadersFactory.CreatePlayerSpaceship(m_graphicsDevice);
            m_player.Initialize(m_contentManager);
            m_gameObjectsList.Add(m_player);

            createEnemiesMat(m_contentManager);

            m_spaceShipRandomNotifier = new RandomActionComponent(1, 25);
            m_spaceShipRandomNotifier.RandomTimeAchived += createMotherSpaceship;
        }

        private void createEnemiesMat(ContentManager i_contentManager)
        {
            m_enemiesMat = new Enemy[Utilities.k_EnemyMatRows, Utilities.k_EnemyMatCols];
            int randomSeedCounter = 0;
            for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
            {
                for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
                {
                    Utilities.eGameObjectType eEnemyType = getCurrentEnemyType(row);

                    m_enemiesMat[row, col] = SpaceInvadersFactory.CreateEnemy(m_graphicsDevice, eEnemyType,++randomSeedCounter);
                    m_enemiesMat[row, col].Initialize(i_contentManager);
                    m_enemiesMat[row, col].InitPosition(row, col);
                    m_gameObjectsList.Add(m_enemiesMat[row, col]);
                }
            }
        }

        private Utilities.eGameObjectType getCurrentEnemyType(int i_row)
        {
            Utilities.eGameObjectType eEnemyType = Utilities.eGameObjectType.PinkEnemy; // default

            if (Utilities.k_BlueEnemyFirstRow <= i_row && Utilities.k_BlueEnemyLastRow >= i_row)
            {
                eEnemyType = Utilities.eGameObjectType.BlueEnemy;
            }
            else if (Utilities.k_YellowEnemyFirstRow <= i_row && Utilities.k_YellowEnemyLastRow >= i_row)
            {
                eEnemyType = Utilities.eGameObjectType.YellowEnemy;
            }

            return eEnemyType;
        }

        public void Draw(GameTime i_gameTime)
        {
            foreach (GameObject gameObject in m_gameObjectsList)
            {
                gameObject.Draw(i_gameTime);
            }
            m_scoreManager.Draw(i_gameTime);

        }

        public void Update(GameTime i_gameTime)
        {
            m_spaceShipRandomNotifier.Update(i_gameTime);

            enemyHitWallCase();

            foreach (GameObject gameObject in m_gameObjectsList)
            {
                gameObject.Update(i_gameTime);
            }

            checkForHits();
        }

        private void enemyHitWallCase()
        {
            foreach (Enemy enemy in m_enemiesMat)
            {
                bool isWalHit = false; ;
                Utilities.eDirection hitDirection = Utilities.eDirection.None;
                enemy.IsWallHit(ref isWalHit, ref hitDirection);

                if (isWalHit)
                {
                    if (hitDirection == Utilities.eDirection.Down)
                    {
                        GameOver(m_scoreManager.CurrentScore);
                    }

                    foreach (Enemy enemyToFixPos in m_enemiesMat)
                    {
                        enemyToFixPos.CurrentDirection = Utilities.ToggleDirection(hitDirection);
                        enemyToFixPos.StepDown();
                    }
                    break;
                }
            }
        }

        private void checkForHits()
        {
            List<GameObject> objectsToDestroy = new List<GameObject>();

            foreach (GameObject gameObject in m_gameObjectsList)
            {
                if (gameObject is IShooter)
                {
                    //concat the lists gathered so far 
                    objectsToDestroy.AddRange(getListOfObjectsToDestroy(((gameObject) as IShooter).GetBulletsList(), gameObject ));
                }
            }

            foreach (GameObject gameObject in objectsToDestroy)
            {
                if(gameObject is Enemy)
                {
                    if(++m_enemyHitCounter % 4 == 0)
                          increaseVelocity();
                }

                m_gameObjectsList.Remove(gameObject);
                if (gameObject is PlayerSpaceship)
                {
                    GameOver(m_scoreManager.CurrentScore);
                    return;
                }
            }
        }

        private List<GameObject> getListOfObjectsToDestroy(List<Bullet> i_bulletsList, GameObject i_shooter)
        {
            List<GameObject> objectsToDestroy = new List<GameObject>();

            foreach (Bullet bullet in i_bulletsList)
            {
                if (bullet.IsVisible)
                {
                    foreach (GameObject gameObject in m_gameObjectsList)
                    {
                        if (gameObject is IDestryoable && gameObject  != i_shooter)
                        {
                            //the type comparison prevents enemies to hit themselves
                            if (Utilities.IsEnemy(gameObject.Type) && Utilities.IsEnemy(i_shooter.Type))
                                continue;

                            if (bullet.Rectangle.Intersects((gameObject as IDestryoable).Rectangle))
                            {
                                (gameObject as IDestryoable).GetHit();
                                m_scoreManager.UpdateScore(gameObject.Type);
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
            foreach (Enemy enemy in m_enemiesMat)
            {
                enemy.IncreaseVelocity(Utilities.SpeedIncreaseMultiplier);
            }
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


            return itsTime;
        }

    }
}
