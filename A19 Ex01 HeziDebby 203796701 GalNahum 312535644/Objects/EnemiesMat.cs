using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using System.Linq;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class EnemiesMat : SpritesCollection
    {
        private const string k_AssetName = @"Sprites\EnemiesSpriteShit96x64";
        private const int k_Rows = 5;
        private const int k_Cols = 9;
        private const int k_TextureWidthDivider = 2;
        private const int k_TextureHeightDivider = 3;

        public Enemy[,] Enemies { get; set; }

        public EnemiesMat(Game i_Game) : base(i_Game)
        {
            base.Sprites = Enemies.Cast<Sprite>().ToList();
        }

        protected override void AllocateSpritesCollection()
        {
            Enemies = new Enemy[k_Rows, k_Cols];
        }

        protected override void AllocateSprites(Game i_Game)
        {
            int seed = 1;
            Vector2 enemyCellIdx = Vector2.Zero;
            Color currentEnemyColor = Color.Pink;

            for (int row = 0; row < k_Rows; row++)
            {
                for (int col = 0; col < k_Cols; col++)
                {
                    Enemies[row, col] = new Enemy(k_AssetName, i_Game)
                    {
                        Seed = ++seed,
                        CellIdx = enemyCellIdx,
                        TintColor = currentEnemyColor
                    }; // TODO: temporary enemies index

                }

                enemyCellIdx.X += row % 2 == 0 ? 1 : 0;
                enemyCellIdx.Y = row % 2 == 0 ? 0 : 1;
                currentEnemyColor = getEnemyColorByRow(row);
            }
        }

        private Color getEnemyColorByRow(int i_CurrentRow)
        {
            return i_CurrentRow < k_Rows - 3 ? Color.LightBlue : Color.LightYellow;
        }

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

        protected override void SetPositions(float i_InitialX, float i_InitialY)
        {
            float x = i_InitialX;
            float y = i_InitialY;
            // TODO: implement the positions calculation with initial values 
            for (int row = 0; row < k_Rows; row++)
            {
                for (int col = 0; col < k_Cols; col++)
                { // TODO: 6 is const of numberofcells
                    x = col * Enemies[row, col].Width + 0.6f * col * Enemies[row, col].Width;
                    y = row * Enemies[row, col].Height + 0.6f * row * Enemies[row, col].Height + 3 * Enemies[row, col].Height;

                    Enemies[row, col].Position = new Vector2(x + 1, y + 1);
                }
            }
        }

        protected override void DoOnBoundaryHit(Sprite i_Sprite, OffsetEventArgs i_EventArgs)
        {
        //    fixOffset(i_Sprite, i_EventArgs.Offset);
            stepDown(i_Sprite);
            i_Sprite.Velocity *= k_DirectionChangeMultiplier;
        }
        // TODO: consider as an extension method
        private void stepDown(Sprite i_Sprite)
        {
            i_Sprite.Position = new Vector2(i_Sprite.Position.X, i_Sprite.Position.Y + ((i_Sprite.Height - 1) / 2));
        }

        private void fixOffset(Sprite i_Sprite, float i_Offset)
        {
            i_Sprite.Position = new Vector2(i_Sprite.Position.X - i_Offset, i_Sprite.Position.Y);
        }

    }
}
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
