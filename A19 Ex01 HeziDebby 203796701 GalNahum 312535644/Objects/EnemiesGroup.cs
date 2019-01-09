using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class EnemiesGroup //: SpritesCollection
    {
        private const string k_AssetName = @"Sprites\EnemiesSpriteShit96x64";
        private const int k_Rows = 5;
        private const int k_Cols = 9;
        private const int k_TextureWidthDivider = 2;
        private const int k_TextureHeightDivider = 3;
        private Vector2 m_GroupDirection = new Vector2(1, 0);

        public List<List<Enemy>> Enemies { get; private set; }

        public EnemiesGroup(Game i_Game) // : base(i_Game)
        {
            Enemies = new List<List<Enemy>>();
            AllocateSprites(i_Game);
            //base.Sprites = Enemies.Cast<Sprite>().ToList();
        }

        //protected override void AllocateSpritesCollection()
        //{
        //    Enemies = new Enemy[k_Rows, k_Cols];

        //}

        //       protected override void AllocateSprites(Game i_Game)
        private void AllocateSprites(Game i_Game)
        {// TODO: encapsulate in methods
            int seed = 1;
            List<Enemy> currentCol;
            Vector2 enemyCellIdx; // = Vector2.Zero;
            Color currentEnemyColor; // = Color.Pink;

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
                    }; // TODO: temporary enemies index

                    currentCol.Add(enemyToAdd);
                    
                    enemyCellIdx.X += row % 2 == 0 ? 1 : 0;
                    enemyCellIdx.Y = row % 2 == 0 ? 0 : 1;
                    currentEnemyColor = getEnemyColorByRow(row);
                }

                Enemies.Add(currentCol);
            }
        }

        public void Initialize(float i_InitialX = 0, float i_InitialY = 0)
        {
            SetPositions(i_InitialX, i_InitialY);
            initJumpValues();
        }

        private Color getEnemyColorByRow(int i_CurrentRow)
        {
            return i_CurrentRow < k_Rows - 3 ? Color.LightBlue : Color.LightYellow;
        }

        // protected override void SetPositions(float i_InitialX, float i_InitialY)
        private void SetPositions(float i_InitialX, float i_InitialY)
        {
            float x = i_InitialX;
            float y = i_InitialY;
            // TODO: implement the positions calculation with initial values 
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
        // TODO: virtual ? override ? 
        // TODO: call this method
        public void Update(GameTime i_GameTime)
        {
            if (groupHitBoundary())
            {
                m_GroupDirection *= -1;

                foreach (List<Enemy> currentCol in Enemies)
                {
                    foreach (Enemy enemy in currentCol)
                    {
                        //enemy.UpdateDirection(m_GroupDirection);
                        enemy.Direction *= -1; // TODO: const for -1 directionChanger
                        stepDown(enemy);
                        // increase velocity
                    }
                }
            }

            jumpGroup(i_GameTime);
        }

        private TimeSpan m_TimeToJump;
        private TimeSpan m_TimeLeftForNextJump;
        private Vector2 m_JumpDestination;
        private Rectangle m_JumpBounds;

        private void initJumpValues()
        {
            m_TimeToJump = TimeSpan.FromSeconds(0.5);
            m_TimeLeftForNextJump = TimeSpan.FromSeconds(0.5);

            Enemy enemy = GetEdgeEnemyByDirection();
            Enemy boundEnemy = enemy; // TODO: the names arent so good
            m_JumpDestination = new Vector2(boundEnemy.Width / 2, boundEnemy.Height / 2);
            m_JumpBounds = boundEnemy.GraphicsDevice.Viewport.Bounds;
        }

        private Enemy GetEdgeEnemyByDirection()
        {
            return Enemies[getEdgeEnemiesColByDirection()][0];
        }

        private void jumpGroup(GameTime i_GameTime)
        {
            m_TimeLeftForNextJump -= i_GameTime.ElapsedGameTime;

            Vector2 jumpDestination = m_JumpDestination * m_GroupDirection;
            Vector2 maxDistance = GetEdgeEnemyByDirection().Position + jumpDestination;

            if (maxDistance.X >= m_JumpBounds.Right - GetEdgeEnemyByDirection().Width)
            {
                jumpDestination.X += (m_JumpBounds.Right - GetEdgeEnemyByDirection().Width) - maxDistance.X;
            }
            else if (maxDistance.X <= m_JumpBounds.Left)
            {
                jumpDestination.X -= m_JumpBounds.Left + maxDistance.X;
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

        private bool groupHitBoundary()
        {
            int colIndex = getEdgeEnemiesColByDirection();

            return Enemies[colIndex][0].HitBoundary();
        }

        private int getEdgeEnemiesColByDirection()
        {
            return m_GroupDirection.X > 0 ? Enemies.Count - 1 : 0;
        }

        //protected override void DoOnBoundaryHit(Sprite i_Sprite, OffsetEventArgs i_EventArgs)
        //{

        //    stepDown(i_Sprite);
        //    i_Sprite.Velocity *= k_DirectionChangeMultiplier;

        //}

        // TODO: consider as an extension method
        private void stepDown(Sprite i_Sprite)
        {
            i_Sprite.Position = new Vector2(i_Sprite.Position.X, i_Sprite.Position.Y + ((i_Sprite.Height - 1) / 2));
        }

        //private void fixOffset(Sprite i_Sprite, float i_Offset)
        //{

        //    i_Sprite.Position = new Vector2(i_Sprite.Position.X - i_Offset, i_Sprite.Position.Y);
        //}

    }
}

// Vector2 offset = new Vector2();

//for (int row = 0; row < k_Rows; row++)
//{
//    for (int col = 0; col < k_Cols; col++)
//    {
//        offset = calcOffset(row, col);
//        Enemies[row, col].Velocity *= k_DirectionChangeMultiplier;
//    }
//}
// each offset should be the last sprite position + 51.2 px unless its on the edge ...
// fixOffset(i_Sprite, i_EventArgs.Offset);
//if (i_Sprite.Position.X >= i_Sprite.GraphicsDevice.Viewport.Width - i_Sprite.Width)
//{
//    float offset = i_Sprite.GraphicsDevice.Viewport.Width - i_Sprite.Width - i_Sprite.Position.X - 1;
//    i_Sprite.Position = new Vector2(i_Sprite.Position.X + offset, i_Sprite.Position.Y);

//}
//else if (i_S)
//            if (CurrentPosition.X >= GraphicsDevice.Viewport.Width - Texture.Width)
//            {
//                isWallHit = true;
//                io_hitDirection = Utilities.eDirection.Right;
//                fixOffset = GraphicsDevice.Viewport.Width - Texture.Width - CurrentPosition.X - 1;
//            }
//            else if (CurrentPosition.X <= 0)
//            {
//                isWallHit = true;
//                io_hitDirection = Utilities.eDirection.Left;
//                fixOffset = -1 * (CurrentPosition.X - 1);
//            }


//private void setBoundaryNotifiers()
//{
//    Enemy leftBoundEnemy = null;
//    Enemy rightBoundEnemy = null;

//    foreach (Enemy enemy in Enemies)
//    {
//        if (enemy.Visible && 
//            ((leftBoundEnemy == null) ||
//            (leftBoundEnemy.Bounds.Left > enemy.Bounds.Left)))
//        {
//            leftBoundEnemy = enemy;
//        }
//        if (enemy.Visible &&
//            ((rightBoundEnemy == null) ||
//            (rightBoundEnemy.Bounds.Right < enemy.Bounds.Right)))
//        {
//            rightBoundEnemy = enemy;
//        }
//    }

//    activateBoundaryEnemy(leftBoundEnemy);
//    activateBoundaryEnemy(rightBoundEnemy);
//}

//protected Rectangle Bounds { get; set; }

//protected virtual void InitBounds()
//{
//    int x = Enemies[0, 0].Bounds.X;
//    int y = Enemies[0, 0].Bounds.Y;
//    int height = Enemies[0, 0].Bounds.Height * k_Rows; // bug ! you didnt added the offset(spaces)
//    int width = Enemies[0, 0].Bounds.Width * k_Cols;

//    Bounds = new Rectangle(x, y, width, height);
//}

//{
//    float minValue = i_Sprite.Position.X - (float)i_Sprite.Width * 0.6f;
//    float maxValue = i_Sprite.Position.X + (float)i_Sprite.Width * 0.6f;

//    minValue = Math.Max(0, minValue);
//    maxValue = Math.Min(i_Sprite.GraphicsDevice.Viewport.Width - i_Sprite.Width, maxValue);

//    i_Sprite.Position = new Vector2(MathHelper.Clamp(i_Sprite.Position.X, minValue, maxValue), i_Sprite.Position.Y);
//}

// TODO: set positions should get one initial position, and to the rest by this .. optimum
//public void SetPositions()
//{
//    for (int row = 0; row < k_Rows; row++)
//    {
//        for (int col = 0; col < k_Cols; col++)
//        {
//            float x = col * Enemies[row, col].Width + 0.6f * col * Enemies[row, col].Width;
//            float y = row * Enemies[row, col].Height + 0.6f * row * Enemies[row, col].Height + 3 * Enemies[row, col].Height;

//            Enemies[row, col].Position = new Vector2(x + 1, y + 1);
//        }
//    }

//    setBoundaryNotifiers();
//}

/*
 * public void InitPosition(int i_row, int i_col)
//        {
//            float height = Texture.Height;
//            float width = Texture.Width;

//            float x = i_col * width + width * Utilities.k_EnemyGapMultiplier * i_col;
//            float y = (i_row * height + height * Utilities.k_EnemyGapMultiplier * i_row) + Utilities.k_InitialHightMultiplier * height;

//            CurrentPosition = new Vector2(x + 1, y + 1);
//        }

  m_EnemiesMat = new Enemy[Utilities.k_EnemyMatRows, Utilities.k_EnemyMatCols];
        //    int randomSeedCounter = 0;

        //    for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
        //    {
        //        for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
        //        {
        //            Utilities.eGameObjectType eEnemyType = getCurrentEnemyType(row);

        //            m_EnemiesMat[row, col] = SpaceInvadersFactory.CreateEnemy(m_GraphicsDevice, eEnemyType, ++randomSeedCounter);
        //            m_EnemiesMat[row, col].Initialize(i_ContentManager);
        //            m_EnemiesMat[row, col].InitPosition(row, col);
        //            m_GameObjectsList.Add(m_EnemiesMat[row, col]);
        //        }
     */
