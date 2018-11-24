using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class Enemy : GameObject, IDestryoable
    {
     //   public Vector2 CurrentPosition { get; set; }
      //  public int Velocity { get; set; }
     //   public Texture2D Texture { get; set; }
      //  public Utilities.eDirection CurrentDirection { get; set; }
        public Utilities.eDrawableType Type { get; set; }
        private int m_ShootingFrequency;
     //   public Color Color { get; set; }
        public int Souls { get; set; }
     //   public Rectangle Rectangle { get; private set; }
     //   private readonly float k_jumpIndicator = 0.5f;
        private float m_timeToJump = 0;

        public Enemy(GraphicsDevice i_graphics) : base(i_graphics)
        {
        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);

            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_EnemyVelocity;
            Souls = Utilities.k_EnemySouls;
        }

        public void InitPosition(int i_row, int i_col)
        {
            float x;
            float y;
            float height = Texture.Height;
            float width = Texture.Width;

            x = i_col * width + width * Utilities.k_EnemyGapMultiplier * i_col;
            y = (i_row * height + height * Utilities.k_EnemyGapMultiplier * i_row) + Utilities.k_InitialHightMultiplier * height;

            CurrentPosition = new Vector2(x, y);
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

          string folder = @"Sprites\";
          string enemy = String.Format("Enemy0{0}01_32x32", (int)Type + 1);
          Texture = Content.Load<Texture2D>(folder + enemy);

        }

        private void Move(GameTime i_gameTime) // need to be jump
        {
            CurrentPosition = new Vector2(CurrentPosition.X + (float)Velocity * (float)i_gameTime.ElapsedGameTime.TotalSeconds, CurrentPosition.Y);
        }

        public override void Update(GameTime i_gameTime)
        {
            Move(i_gameTime);
            base.Update(i_gameTime);
            //         updateRectangle();
        }

        //private void updateRectangle()
        //{
        //    Rectangle rectangle = new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, Texture.Width, Texture.Height);

        //    Rectangle = rectangle;
        //}

        //     public override void Draw(GameTime i_gameTime)
        //   {
        //     if (m_timeToJump >= k_jumpIndicator)
        //     {
        //         m_timeToJump -= k_jumpIndicator;

        //SpriteBatch.Begin();

        //SpriteBatch.Draw(Texture, CurrentPosition, Color);

        //SpriteBatch.End();
        //    }

        //    m_timeToJump += (float)i_gameTime.ElapsedGameTime.TotalSeconds;
        //     }

        public void GetHit()
        {
            Souls--;
        }

        public bool IsDead()
        {
            return Souls == 0;
        }
    }
}
