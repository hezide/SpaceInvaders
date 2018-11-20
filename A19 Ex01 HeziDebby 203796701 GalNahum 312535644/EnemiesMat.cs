using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
//{
//    public class EnemiesMat : IMoveable, Interfaces.IDrawable
//    {
//        public static readonly int k_EnemyMatRows = 5;
//        public static readonly int k_EnemyMatCols = 9;
//        public int Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//        public Utilities.eDirection CurrentDirection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//        //   public int Souls { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//        //   public Texture2D Texture { get; set; } // ?????
//        //   Dictionary<Utilities.eDrawableType, Texture2D> Textures;
//        public Vector2 CurrentPosition { get; set; }
//        //   public SpriteBatch SpriteBatch { get; private set; }
//        public Enemy[,] Enemies { get; private set; }

//        //public EnemiesMat(Vector2 i_initialPosition)
//        //{
//        //    CurrentPosition = i_initialPosition;
//        //    //  Textures = i_Textures;
//        //    Enemies = new Enemy[k_EnemyMatRows, k_EnemyMatCols];
//        //}

//        public void Init(Dictionary<Utilities.eDrawableType, Texture2D> i_texturesByType, SpriteBatch i_spriteBatch)
//        {
//            initAllEnemies(i_texturesByType, i_spriteBatch);
//        }

//        //private void initAllEnemies(Dictionary<Utilities.eDrawableType, Texture2D> i_texturesByType, SpriteBatch i_spriteBatch)
//        //{
//        //    float enemyHeight = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Height;
//        //    float enemyWidth = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Width;
//        //    float enemyX, enemyY;

//        //    for (int row = 0; row < k_EnemyMatRows; row++)
//        //    {
//        //        for (int col = 0; col < k_EnemyMatCols; col++)
//        //        { // TODO : fix the formula
//        //            enemyX = col * enemyWidth + enemyWidth * Utilities.k_EnemyGapMultiplier * col;
//        //            enemyY = (row * enemyHeight + enemyHeight * Utilities.k_EnemyGapMultiplier * row) + Utilities.k_InitialHightMultiplier * enemyHeight;

//        //            Utilities.eDrawableType eEnemyType = getCurrentEnemyType(row);
//        //            Texture2D texture = i_texturesByType[eEnemyType];
//        //            Vector2 position = new Vector2(enemyX, enemyY);
//        //            Color color = getColorByType(eEnemyType);

//        //            Enemies[row, col] = SpaceInvadersFactory.CreateEnemy(texture, i_spriteBatch, position, eEnemyType, color);
//        //            Enemies[row, col].Init();
//        //        }
//        //    }
//        //}

//        //private Utilities.eDrawableType getCurrentEnemyType(int i_row)
//        //{
//        //    Utilities.eDrawableType eEnemyType = Utilities.eDrawableType.PinkEnemy; // default

//        //    if (Utilities.k_BlueEnemyFirstRow <= i_row && Utilities.k_BlueEnemyLastRow >= i_row)
//        //    {
//        //        eEnemyType = Utilities.eDrawableType.BlueEnemy;
//        //    }
//        //    else if (Utilities.k_YellowEnemyFirstRow <= i_row && Utilities.k_YellowEnemyLastRow >= i_row)
//        //    {
//        //        eEnemyType = Utilities.eDrawableType.YellowEnemy;
//        //    }

//        //    return eEnemyType;
//        //}

//        //private Color getColorByType(Utilities.eDrawableType i_eType)
//        //{
//        //    Color color = Color.White; // default value

//        //    switch (i_eType)
//        //    {
//        //        case Utilities.eDrawableType.PinkEnemy:
//        //            color = Color.Pink;
//        //            break;
//        //        case Utilities.eDrawableType.BlueEnemy:
//        //            color = Color.Blue;
//        //            break;
//        //        case Utilities.eDrawableType.YellowEnemy:
//        //            color = Color.Yellow;
//        //            break;
//        //        default:
//        //            break;
//        //    }

//        //    return color;
//        //}

//        public void Draw(GameTime i_gameTime)
//        {
//            throw new NotImplementedException();
//        }

//        public void Move(GameTime i_gameTime)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
