using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
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
        private Bullet m_bullet;
        private List<Interfaces.IDrawable> m_drawablesList;
        private List<Interfaces.IMoveable> m_moveablesList;
        private int m_hitCounter = 0; // HEZI which hit ?
        private int m_randomMotherSpaceshipSpawnTime; //could be gametime
        private SpriteBatch m_spriteBatch;
        private Viewport m_viewPort;
        private MouseState? m_prevMouseState;
        //private KeyboardState? m_prevKeyboardState;

        public SpaceInvadersManager()
        {
            m_drawablesList = new List<Interfaces.IDrawable>();
            m_moveablesList = new List<Interfaces.IMoveable>();
        }

        public void Init(Dictionary<Utilities.eDrawableType, Texture2D> i_TexturesByType, SpriteBatch i_SpriteBatch, Viewport i_viewport)
        {
            m_spriteBatch = i_SpriteBatch;
            m_viewPort = i_viewport;

            Texture2D texture = i_TexturesByType[Utilities.eDrawableType.Spaceship];
            Vector2 initialPosition = getCenterOfScreen(texture);

            m_player = SpaceInvadersFactory.CreatePlayerSpaceship(texture, m_spriteBatch);
            m_player.Init(initialPosition);
            m_moveablesList.Add(m_player);
            m_drawablesList.Add(m_player);

            createEnemiesMat(i_TexturesByType);

            // create mothership
            texture = i_TexturesByType[Utilities.eDrawableType.Bullet];
            m_bullet = SpaceInvadersFactory.CreateBullet(texture, m_spriteBatch);
        }

        private void createEnemiesMat(Dictionary<Utilities.eDrawableType, Texture2D> i_texturesByType)
        {
            m_enemiesMat = new Enemy[Utilities.k_EnemyMatRows, Utilities.k_EnemyMatCols];

            float enemyHeight = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Height;
            float enemyWidth = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Width;
            float enemyX, enemyY;

            for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
            {
                for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
                { // TODO : fix the formula
                    enemyX = col * enemyWidth + enemyWidth * Utilities.k_EnemyGapMultiplier * col;
                    enemyY = (row * enemyHeight + enemyHeight * Utilities.k_EnemyGapMultiplier * row) + Utilities.k_InitialHightMultiplier * enemyHeight;

                    Utilities.eDrawableType eEnemyType = getCurrentEnemyType(row);
                    Texture2D texture = i_texturesByType[eEnemyType];
                    Vector2 position = new Vector2(enemyX, enemyY);
                    
                    m_enemiesMat[row, col] = SpaceInvadersFactory.CreateEnemy(texture, m_spriteBatch, eEnemyType);
                    m_enemiesMat[row, col].Init(position);
                    m_drawablesList.Add(m_enemiesMat[row, col]);
                    m_moveablesList.Add(m_enemiesMat[row, col]);
                }
            }
        }

        private Vector2 getCenterOfScreen(Texture2D i_texture)
        {
            Vector2 center = new Vector2(m_viewPort.Width / 2, m_viewPort.Height);

            center.X -= i_texture.Width / 2;
            center.Y -= (i_texture.Height / 2) + 30;

            return center;
        }

        //private void initAllEnemies(Dictionary<Utilities.eDrawableType, Texture2D> i_Textures)
        //{
        //   // float enemyHeight = i_Textures[Utilities.eDrawableType.PinkEnemy].Height;
        //   // float enemyWidth = i_Textures[Utilities.eDrawableType.PinkEnemy].Width;
        //  //  float enemyX, enemyY;

        //    for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
        //    {
        //        for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
        //        {
        //            enemyX = col * enemyWidth + enemyWidth * Utilities.k_EnemyGapMultiplier * col;
        //            enemyY = (row * enemyHeight + enemyHeight * Utilities.k_EnemyGapMultiplier * row) + Utilities.k_InitialHightMultiplier * enemyHeight;

        //            m_Enemies[row, col] = new Enemy();

        //            Utilities.eDrawableType eEnemyType = getCurrentEnemyType(row);
        //            Color color = getColorByType(eEnemyType);
        //            Vector2 position = new Vector2(enemyX, enemyY);

        //            m_Enemies[row, col].Init(eEnemyType, i_Textures[eEnemyType], color, position, Utilities.eDirection.Right, i_Textures[eEnemyType].Width, m_SpriteBatch);
        //            //Initialize pink enemies
        //            //if (Utilities.k_PinkEnemyFirstRow <= row && Utilities.k_PinkEnemyLastRow >= row)
        //            //{
        //            //    m_Enemies[row, col].Init(
        //            //        Utilities.eDrawableType.PinkEnemy,
        //            //        i_Textures[Utilities.eDrawableType.PinkEnemy],
        //            //        Color.Pink,
        //            //        new Vector2(enemyX, enemyY),
        //            //        Utilities.eDirection.Right,
        //            //        i_Textures[Utilities.eDrawableType.PinkEnemy].Width,
        //            //        m_SpriteBatch);
        //            //}
        //            //else if (Utilities.k_BlueEnemyFirstRow <= row && Utilities.k_BlueEnemyLastRow >= row)
        //            //{
        //            //    m_Enemies[row, col].Init(
        //            //        Utilities.eDrawableType.BlueEnemy,
        //            //        i_Textures[Utilities.eDrawableType.BlueEnemy],
        //            //        Color.Blue,
        //            //        new Vector2(enemyX, enemyY),
        //            //        Utilities.eDirection.Right,
        //            //        i_Textures[Utilities.eDrawableType.BlueEnemy].Width,
        //            //        m_SpriteBatch);
        //            //}
        //            //else if (Utilities.k_YellowEnemyFirstRow <= row && Utilities.k_YellowEnemyLastRow >= row)
        //            //{
        //            //    m_Enemies[row, col].Init(
        //            //        Utilities.eDrawableType.YellowEnemy,
        //            //        i_Textures[Utilities.eDrawableType.YellowEnemy],
        //            //        Color.Yellow,
        //            //        new Vector2(enemyX, enemyY),
        //            //        Utilities.eDirection.Right,
        //            //        i_Textures[Utilities.eDrawableType.YellowEnemy].Width,
        //            //        m_SpriteBatch);
        //            //}

        //          //  m_DrawablesList.Add(m_Enemies[row, col]);
        //          //  m_MoveablesList.Add(m_Enemies[row, col]);
        //        }
        //    }
        //}

        //private void initAllEnemies(Dictionary<Utilities.eDrawableType, Texture2D> i_texturesByType, SpriteBatch i_spriteBatch)
        //{
        //    float enemyHeight = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Height;
        //    float enemyWidth = i_texturesByType[Utilities.eDrawableType.PinkEnemy].Width;
        //    float enemyX, enemyY;

        //    for (int row = 0; row < Utilities.k_EnemyMatRows; row++)
        //    {
        //        for (int col = 0; col < Utilities.k_EnemyMatCols; col++)
        //        { // TODO : fix the formula
        //            enemyX = col * enemyWidth + enemyWidth * Utilities.k_EnemyGapMultiplier * col;
        //            enemyY = (row * enemyHeight + enemyHeight * Utilities.k_EnemyGapMultiplier * row) + Utilities.k_InitialHightMultiplier * enemyHeight;

        //            Utilities.eDrawableType eEnemyType = getCurrentEnemyType(row);
        //            Texture2D texture = i_texturesByType[eEnemyType];
        //            Vector2 position = new Vector2(enemyX, enemyY);

        //            m_enemies[row, col] = SpaceInvadersFactory.CreateSpaceship(texture, i_spriteBatch, position, eEnemyType)
        //                                as Enemy;
        //        }
        //    }
        //}

        private Color getColorByType(Utilities.eDrawableType i_eType)
        {
            Color color = Color.White; // default value

            switch (i_eType)
            {
                case Utilities.eDrawableType.PinkEnemy:
                    color = Color.Pink;
                    break;
                case Utilities.eDrawableType.BlueEnemy:
                    color = Color.Blue;
                    break;
                case Utilities.eDrawableType.YellowEnemy:
                    color = Color.Yellow;
                    break;
                default:
                    break;
            }

            return color;
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

        private float getMousePositionDeltaX()
        {
            // Vector2 retVal = Vector2.Zero;
            float x = 0;
            MouseState currState = Mouse.GetState();

            if (m_prevMouseState != null)
            {
                x = (currState.X - m_prevMouseState.Value.X);
                //  retVal.Y = (currState.Y - m_PrevMouseState.Value.Y);

            }

            //  retVal.Y = CurrentPosition.Y;
            m_prevMouseState = currState;

            return x;
        }

        public void Draw(GameTime i_gameTime)
        {
            foreach (Interfaces.IDrawable drawable in m_drawablesList)
            {
                drawable.Draw(i_gameTime);
            }
        }

        public void Update(GameTime i_gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
 //               m_bullet.Init()
            }

            m_player.CurrentPosition = getNewPositionByInput(m_player); // TODO: should be inside the player

            foreach (IMoveable moveable in m_moveablesList)
            {
                moveable.Move(i_gameTime);
            }

            //if (m_Player.CurrentPosition.X == 0 || m_Player.CurrentPosition.X == m_ViewPort.Width - m_Player.Texture.Width)
            //{
            //    m_Player.m_direction *= -1f;
            //}
        }

        private Vector2 getNewPositionByInput(IMoveable i_moveable)
        {
            Vector2 newPosition = new Vector2(i_moveable.CurrentPosition.X + getMousePositionDeltaX(), i_moveable.CurrentPosition.Y);

            return (new Vector2(MathHelper.Clamp(newPosition.X, 0, m_viewPort.Width - i_moveable.Texture.Width), newPosition.Y));
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
    }
}
