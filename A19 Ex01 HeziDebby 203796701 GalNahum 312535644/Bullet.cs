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
    public class Bullet : GameObject
    {
    //    public Vector2 CurrentPosition { get; set; }
    //    public int Velocity { get; set; }
    //    public Utilities.eDirection CurrentDirection { get; set; }
    //    public Texture2D Texture { get; private set; }
    //    public Color Color { get; private set; }
    //    public Rectangle Rectangle { get; private set; }
        public bool IsVisible { get; private set; }

        public Bullet(GraphicsDevice i_graphicsDevice) : base(i_graphicsDevice)
        {

        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);

            CurrentDirection = Utilities.eDirection.Up;
            Velocity = Utilities.k_BulletVelocity;
            IsVisible = true;
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

            Texture = i_content.Load<Texture2D>(@"Sprites\Bullet");
        }

        public void InitPosition(Vector2 i_initialPosition)
        {
            CurrentPosition = i_initialPosition;
        }

        public void Move(GameTime i_gameTime)
        {
            CurrentPosition = new Vector2(CurrentPosition.X, Utilities.CalculateNewCoordinate(CurrentPosition.Y,CurrentDirection,Velocity,i_gameTime));
        }

        public override void Update(GameTime i_gameTime)
        {
            Move(i_gameTime);
            base.Update(i_gameTime);
      //      updateRectangle();
        }

        //private void updateRectangle()
        //{
        //    Rectangle rectangle = new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, Texture.Width, Texture.Height);

        //    Rectangle = rectangle;
        //}

        public override void Draw(GameTime i_gameTime)
        {
            if (IsVisible)
            {
                SpriteBatch.Begin();

                SpriteBatch.Draw(Texture, CurrentPosition, Color);

                SpriteBatch.End();
            }
        }

        public void Hide()
        {
            IsVisible = false;
        }
    }
}
