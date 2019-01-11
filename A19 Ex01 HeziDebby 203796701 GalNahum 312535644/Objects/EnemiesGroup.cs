using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class EnemiesGroup : SpritesCollection<Enemy>
    {
        private const string k_AssetName = @"Sprites\EnemiesSpriteShit96x64";
        private const int k_Rows = 5;
        private const int k_Cols = 9;
        private const int k_TextureWidthDivider = 2;
        private const int k_TextureHeightDivider = 3;
        private const int k_MaxCollidedEnemies = 4;
        private const double k_BoundaryHitIncreaser = 0.08;
        private const double k_DeadEnemiesIncreaser = 0.04;

        public List<List<Enemy>> Enemies { get; private set; }

        public EnemiesGroup(Game i_Game) : base(i_Game)
        {
            base.Sprites = Enemies.SelectMany(enemy => enemy).ToList();
        }

        protected override void AllocateSprites(Game i_Game)
        {
            Enemies = new List<List<Enemy>>();

            int seed = 1;
            List<Enemy> currentCol;
            Vector2 enemyCellIdx;
            Color currentEnemyColor;

            for (int col = 0; col < k_Cols; col++)
            {
                currentCol = new List<Enemy>();
                enemyCellIdx = Vector2.Zero;
                currentEnemyColor = Color.Pink;

                for (int row = 0; row < k_Rows; row++)
                {
                    Enemy enemyToAdd = new Enemy(k_AssetName, i_Game)
                    {
                        Seed = ++seed,
                        CellIdx = enemyCellIdx,
                        TintColor = currentEnemyColor
                    };

                    addListeners(enemyToAdd);

                    currentCol.Add(enemyToAdd);

                    enemyCellIdx.X += row % 2 == 0 ? 1 : 0;
                    enemyCellIdx.Y = row % 2 == 0 ? 0 : 1;

                    currentEnemyColor = getEnemyColorByRow(row);
                }

                Enemies.Add(currentCol);
            }
        }

        private void addListeners(Enemy i_Enemy)
        {
            i_Enemy.Collision += enemyCollided;
            i_Enemy.Disposed += enemyDisposed;
        }

        private void enemyDisposed(object i_Sender, EventArgs i_EventArgs)
        {
            Enemy disposedEnemy = i_Sender as Enemy;
            List<Enemy> colToRemove = null;

            Sprites.Remove(disposedEnemy);

            foreach (List<Enemy> enemiesCol in Enemies)
            {
                if (enemiesCol.Contains(disposedEnemy))
                {
                    enemiesCol.Remove(disposedEnemy);

                    if (enemiesCol.Count == 0)
                    {
                        colToRemove = enemiesCol;
                    }
                }
            }

            if (colToRemove != null)
            {
                Enemies.Remove(colToRemove);
            }
        }

        private int m_CollidedEnemiesCounter = 0;

        private void enemyCollided(object i_Sender, EventArgs i_EventArgs)
        {
            if (++m_CollidedEnemiesCounter == k_MaxCollidedEnemies)
            {
                m_CollidedEnemiesCounter %= k_MaxCollidedEnemies;
                increaseEnemyVelocity(k_DeadEnemiesIncreaser);
            }
        }

        public override void Initialize(float i_InitialX = 0, float i_InitialY = 0)
        {
            base.Initialize(i_InitialX, i_InitialY);
            initJumpValues();
        }

        private Color getEnemyColorByRow(int i_CurrentRow)
        {
            return i_CurrentRow < k_Rows - 3 ? Color.LightBlue : Color.LightYellow;
        }

        protected override void InitPositions(float i_InitialX, float i_InitialY)
        {
            float x = i_InitialX;
            float y = i_InitialY;

            for (int col = 0; col < k_Cols; col++)
            {
                for (int row = 0; row < k_Rows; row++)
                {
                    x = col * Enemies[col][row].Width + 0.6f * col * Enemies[col][row].Width;
                    y = row * Enemies[col][row].Height + 0.6f * row * Enemies[col][row].Height + 3 * Enemies[col][row].Height;

                    Enemies[col][row].Position = new Vector2(x + 1, y + 1);
                }
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            if (GroupHitBoundary())
            {
                foreach (Enemy enemy in this.Sprites)
                {
                    stepDown(enemy);
                }

                m_GroupDirection *= r_DirectionChangeMultiplier;
                increaseEnemyVelocity(k_BoundaryHitIncreaser);
            }
            else
            {
                jumpGroup(i_GameTime);
            }
        }

        protected override bool GroupHitBoundary()
        {
            int colIndex = GetEdgeSpriteIdxByDirection();

            return Enemies[colIndex][0].HitBoundary();
        }

        protected override int GetEdgeSpriteIdxByDirection()
        {
            return m_GroupDirection.X > 0 ? Enemies.Count - 1 : 0;
        }

        private Enemy getEdgeEnemyByDirection()
        {
            return Enemies[GetEdgeSpriteIdxByDirection()][0];
        }

        // TODO: consider as an extension method
        private void stepDown(Sprite i_Sprite)
        {
            i_Sprite.Position = new Vector2(i_Sprite.Position.X, i_Sprite.Position.Y + ((i_Sprite.Height - 1) / 2));
        }

        private void increaseEnemyVelocity(double i_PrecentageToIncrease)
        {
            double currentTime = m_TimeToJump.TotalSeconds;

            currentTime -= currentTime * i_PrecentageToIncrease;
            m_TimeToJump = TimeSpan.FromSeconds(currentTime);
            m_TimeLeftForNextJump = m_TimeToJump;

            foreach (Enemy enemy in base.Sprites)
            {
                enemy.IncreaseCellAnimation(m_TimeToJump);
            }
        }

        public bool ReachedHeight(int i_Height)
        {
            bool heightReached = false;

            foreach (List<Enemy> enemiesCol in Enemies)
            {
                if (enemiesCol[enemiesCol.Count - 1].Bounds.Bottom >= i_Height)
                {
                    heightReached = true;
                }
            }

            return heightReached;
        }

        // **************************************************//
        // TODO: encapsulate
        private TimeSpan m_TimeToJump;
        private TimeSpan m_TimeLeftForNextJump;
        private Vector2 m_JumpDestination;
        private Rectangle m_JumpBounds;
        private Vector2 m_DirectionMultiplier;

        private void initJumpValues()
        {
            m_TimeToJump = TimeSpan.FromSeconds(0.5);
            m_TimeLeftForNextJump = TimeSpan.FromSeconds(0.5);

            Enemy boundEnemy = getEdgeEnemyByDirection();
            m_JumpDestination = new Vector2(boundEnemy.Width / 2, boundEnemy.Height / 2);
            m_JumpBounds = boundEnemy.GraphicsDevice.Viewport.Bounds;

            m_DirectionMultiplier = m_GroupDirection;
        }

        
        private void jumpGroup(GameTime i_GameTime)
        {
            m_TimeLeftForNextJump -= i_GameTime.ElapsedGameTime;

            Vector2 jumpDestination = m_JumpDestination * m_GroupDirection;
            Vector2 maxDistance = getEdgeEnemyByDirection().Position + jumpDestination;

            if (maxDistance.X + getEdgeEnemyByDirection().Width >= m_JumpBounds.Right)
            {
                jumpDestination.X = (m_JumpBounds.Right - getEdgeEnemyByDirection().Width) - getEdgeEnemyByDirection().Position.X;
            }
            else if (maxDistance.X <= m_JumpBounds.Left)
            {
                jumpDestination.X = m_JumpBounds.Left - getEdgeEnemyByDirection().Position.X;
            }

            if (m_TimeLeftForNextJump.TotalSeconds < 0)
            {
                foreach (List<Enemy> currentCol in Enemies)
                {
                    foreach (Enemy enemy in currentCol)
                    {
                        enemy.Position += jumpDestination;
                        m_TimeLeftForNextJump = m_TimeToJump;
                    }
                }
            }
        }
    }
}
