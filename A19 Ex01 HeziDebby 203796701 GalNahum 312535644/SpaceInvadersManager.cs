using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class SpaceInvadersManager
    {
        // $G$ DSN-001 (-5) An Enemies/EnemyMatrix class was expected.
        private Enemy[,] m_EnemiesMat;
        private MotherSpaceship m_MotherSpaceship;
        private PlayerSpaceship m_Player;
        private List<GameObject> m_GameObjectsList;
        private int m_EnemyHitCounter = 0;
        private readonly GraphicsDevice m_GraphicsDevice;
        private ContentManager m_ContentManager;
        private RandomActionComponent m_SpaceShipRandomNotifier;
        private ScoreManager m_ScoreManager;
        public bool IsGameOver { get; private set; }

        public SpaceInvadersManager(GraphicsDevice i_GraphicsDevice)
        {
            m_GraphicsDevice = i_GraphicsDevice;
            m_GameObjectsList = new List<GameObject>();
            m_ScoreManager = new ScoreManager();
        }

        public void Init(ContentManager i_ContentManager)
        {
            m_ContentManager = i_ContentManager;
            m_ScoreManager.Initialize(m_GraphicsDevice, i_ContentManager);
            m_Player = SpaceInvadersFactory.CreatePlayerSpaceship(m_GraphicsDevice);
            m_Player.Initialize(m_ContentManager);
            m_GameObjectsList.Add(m_Player);

            createEnemiesMat(m_ContentManager);

            m_SpaceShipRandomNotifier = new RandomActionComponent(1, 25);
            m_SpaceShipRandomNotifier.RandomTimeAchieved += notifier_CreateMotherSpaceship;
        }

        // $G$ DSN-001 (0) This should have been handled by an EnemyMatrix class
        private void createEnemiesMat(ContentManager i_ContentManager)
        {
            m_EnemiesMat = new Enemy[Utilities.k_EnemyMatRows, Utilities.k_EnemyMatCols];
            int randomSeedCounter = 0;

            for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
            {
                for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
                {
                    Utilities.eGameObjectType eEnemyType = getCurrentEnemyType(row);

                    m_EnemiesMat[row, col] = SpaceInvadersFactory.CreateEnemy(m_GraphicsDevice, eEnemyType, ++randomSeedCounter);
                    m_EnemiesMat[row, col].Initialize(i_ContentManager);
                    m_EnemiesMat[row, col].InitPosition(row, col);
                    m_GameObjectsList.Add(m_EnemiesMat[row, col]);
                }
            }
        }

        public int GetScore()
        {
            return m_ScoreManager.CurrentScore;
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

        public void Draw(GameTime i_GameTime)
        {
            foreach (GameObject gameObject in m_GameObjectsList)
            {
                gameObject.Draw(i_GameTime);
            }
            // $G$ SFN-999 (-5) you should have not display any score
            m_ScoreManager.Draw(i_GameTime);
        }

        public void Update(GameTime i_GameTime)
        {
            m_SpaceShipRandomNotifier.Update(i_GameTime);

            checkEnemyCollision();

            foreach (GameObject gameObject in m_GameObjectsList)
            {
                gameObject.Update(i_GameTime);
            }

            updateHits();
        }

        public void UnloadContent(ContentManager i_Content)
        {
            foreach (GameObject gameObject in m_GameObjectsList)
            {
                gameObject.UnloadContent(i_Content);
            }
        }

        // $G$ XNA-001 (-4) Not efficient to go over all the enemies all the time.. Just Hold a ref to the ones in the edges to check boundries collision..
        // $G$ XNA-001 (-4) Not efficient to go over all the enemies all the time.. Just Hold a ref to the ones in the edges to check boundries collision..
        private void checkEnemyCollision()
        {
            foreach (Enemy enemy in m_EnemiesMat)
            {
                if (!IsGameOver)
                {

                    bool isWallHit = false;
                    float fixOffset = 0;
                    Utilities.eDirection hitDirection = Utilities.eDirection.None;
                    isWallHit = enemy.IsWallHit(ref hitDirection, ref fixOffset);

                    if (isWallHit)
                    {
                        //enemy hit the lower wall or player spaceship
                        if (hitDirection == Utilities.eDirection.Down)
                        {
                            IsGameOver = true;
                        }

                        foreach (Enemy enemyToFixPos in m_EnemiesMat)
                        {
                            enemyToFixPos.CurrentDirection = Utilities.ToggleDirection(hitDirection);
                            enemyToFixPos.CurrentPosition = new Vector2(enemyToFixPos.CurrentPosition.X + fixOffset, enemyToFixPos.CurrentPosition.Y);
                            enemyToFixPos.StepDown();
                            enemyToFixPos.IncreaseVelocity(Utilities.k_EnemyHitWallVelocityMultiplier);
                        }
                    }
                    if (enemy.Rectangle.Intersects(m_Player.Rectangle))
                    {
                        IsGameOver = true;
                    }
                }
            }
        }


        // $G$ XNA-001 (-4) Not efficient to go over all the enemies all the time.. Just Hold a ref to the ones in the edges to check boundries collision..
        // private void checkEnemyCollision()
        // {
        ////     bool isWallHit = false;
        //     float fixOffset = 0;

        //     foreach (Enemy enemy in m_EnemiesMat)
        //     {
        //         if (!IsGameOver)
        //         {
        //             fixOffset = 0;
        //             //   Utilities.eDirection hitDirection = Utilities.eDirection.None;
        //             //        isWallHit = enemy.IsWallHit(ref hitDirection, ref fixOffset);

        //             if (enemy.IsHittingBoundris())
        //             {
        //                 fixOffset = enemy.CalcOffset();
        //                 //enemy hit the lower wall or player spaceship
        //                 if (enemy.CurrentPosition.Y >= m_GraphicsDevice.Viewport.Height - enemy.Texture.Height)
        //                 {
        //                     IsGameOver = true;
        //                 }

        //                 foreach (Enemy enemyToFixPos in m_EnemiesMat)
        //                 {
        //                     // enemyToFixPos.CurrentDirection = Utilities.ToggleDirection(hitDirection);
        //                     enemyToFixPos.Velocity *= -1;
        //                     enemyToFixPos.CurrentPosition = new Vector2(enemyToFixPos.CurrentPosition.X + fixOffset, enemyToFixPos.CurrentPosition.Y);
        //                     enemyToFixPos.StepDown();
        //                     enemyToFixPos.IncreaseVelocity(Utilities.k_EnemyHitWallVelocityMultiplier);
        //                 }
        //             }

        //             if (enemy.Rectangle.Intersects(m_Player.Rectangle))
        //             {
        //                 IsGameOver = true;
        //             }
        //         }
        //     }
        // }

        private void updateHits()
        {
            List<GameObject> objectsToDestroy = new List<GameObject>();

            // getting all the game objects to destroy
            foreach (GameObject gameObject in m_GameObjectsList)
            {
                if (gameObject is IShooter)
                {
                    //concat the lists gathered so far 
                    objectsToDestroy.AddRange(getListOfObjectsToDestroy(gameObject));

                    if (gameObject != m_Player)
                    {
                        destroyBulletsPlayerHitted(gameObject as IShooter);
                    }
                }
            }

            destroyHittedObjects(objectsToDestroy);
        }

            private void destroyHittedObjects(List<GameObject> i_objectsToDestroy)
            {
                foreach (GameObject gameObject in i_objectsToDestroy)
                {
                    if (gameObject is Enemy)
                    {
                        if (++m_EnemyHitCounter % Utilities.k_hitsToIncreaseVelocity == 0)
                        {
                            increaseVelocity();
                        }
                    }

                    if (gameObject is PlayerSpaceship)
                    {
                        IsGameOver = true;
                        // return;//I'm not removing it because it can cause troubles, it can continue running while the game was over, we need to stop running here
                    }

                    m_GameObjectsList.Remove(gameObject);
                }
            }

            private void destroyBulletsPlayerHitted(IShooter i_shooter)
            {
                foreach (Bullet playerBullet in m_Player.GetBulletsList())
                {
                    foreach (Bullet enemyBullet in i_shooter.GetBulletsList())
                    {
                        if (playerBullet.IsVisible && enemyBullet.IsVisible &&
                            playerBullet.Hits(enemyBullet as IDestryoable))
                        {
                            playerBullet.Hide();
                            enemyBullet.Hide();
                        }
                    }
                }
            }

            private List<GameObject> getListOfObjectsToDestroy(GameObject i_shooterGameObject)
            {
                List<GameObject> objectsToDestroy = new List<GameObject>();

                foreach (Bullet bullet in (i_shooterGameObject as IShooter).GetBulletsList())
                {
                    if (bullet.IsVisible)
                    {
                        foreach (GameObject gameObject in m_GameObjectsList)
                        {
                            if (bulletShouldHit(gameObject, i_shooterGameObject) && bullet.Hits(gameObject as IDestryoable))
                            {
                                (gameObject as IDestryoable).GetHit();
                                m_ScoreManager.UpdateScore(gameObject.TypeOfGameObject);
                                bullet.Hide();

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

            private bool bulletShouldHit(GameObject i_gameObject, GameObject i_shooterGameObject)
            {
                bool shouldHit = false;

                if (i_gameObject is IDestryoable && !Utilities.IsSameType(i_gameObject, i_shooterGameObject))
                {
                    shouldHit = true;
                }

                return shouldHit;
            }

            // $G$ DSN-001 (0) This should have been handled by an EnemyMatrix class
            private void increaseVelocity()
            {
                foreach (Enemy enemy in m_EnemiesMat)
                {
                    enemy.IncreaseVelocity(Utilities.SpeedIncreaseMultiplier);
                }
            }

            private void notifier_CreateMotherSpaceship()
            {
                if (m_MotherSpaceship == null)
                {
                    m_MotherSpaceship = SpaceInvadersFactory.CreateMotherSpaceship(m_GraphicsDevice);
                    m_MotherSpaceship.Initialize(m_ContentManager);
                    m_GameObjectsList.Add(m_MotherSpaceship);
                }
                else
                {
                    m_MotherSpaceship.SetToInitialPosition();
                }
                // TODO: if hit - remove from list of objects
            }
        }
    }
