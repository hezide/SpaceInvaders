using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class GameObject
    {
        protected GraphicsDevice GraphicsDevice { get; private set; }
        protected SpriteBatch SpriteBatch { get; private set; }
        protected ContentManager Content { get; private set; }

        public Vector2 CurrentPosition { get; set; }
        public float Velocity { get; set; }
        public Texture2D Texture { get; protected set; }
        public Utilities.eDirection CurrentDirection { get; set; }
        public Utilities.eGameObjectType TypeOfGameObject { get; set; }
        public Color Color { get; set; }
        public Rectangle Rectangle { get; protected set; }

        public GameObject(GraphicsDevice i_graphics)
        {
            GraphicsDevice = i_graphics;
        }

        public virtual void Initialize(ContentManager i_content)
        {
            Content = i_content;
            LoadContent(i_content);
        }

        protected virtual void LoadContent(ContentManager i_content)
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private void UnloadContent(ContentManager i_content)
        {
            i_content.Unload();
        }

        public virtual void Update(GameTime i_gameTime)
        {
            updateRectangle();
        }

        private void updateRectangle()
        {
            Rectangle rectangle = new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, Texture.Width, Texture.Height);

            Rectangle = rectangle;
        }

        public virtual void Draw(GameTime i_gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();
        }
    }
}
