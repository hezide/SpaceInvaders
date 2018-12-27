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
        // $G$ CSS-011 (-2) This should be called "Bounds" and not "Retangle".
        // $G$ XNA-002 (-2) No need for both Bounds and Position
        public Rectangle Rectangle { get; protected set; }
        // $G$ XNA-002 (-2) A. This should be called "Bounds" and not "Retangle".   B. No need for both Bounds and Position
        // $G$ CSS-011 (-2) Why CurrentPosition and not just Poistion?
        public Vector2 CurrentPosition { get; set; }
        public float Velocity { get; set; }
        public Texture2D Texture { get; protected set; }
        // $G$ DSN-002 (-5) "Direction" is derived from the Velocity positive/negative value
        public Utilities.eDirection CurrentDirection { get; set; }
        // $G$ DSN-001 (-5) The "type" of a game objet should be infered by the type (class) that represents it.
        public Utilities.eGameObjectType TypeOfGameObject { get; set; }
        public Color Color { get; set; }

      

        public GameObject(GraphicsDevice i_graphics)
        {
            GraphicsDevice = i_graphics;
        }

        public virtual void Initialize(ContentManager i_Content)
        {
            Content = i_Content;
            LoadContent(i_Content);
        }

        protected virtual void LoadContent(ContentManager i_Content)
        {
            // $G$ XNA-001 (-5) Holding a separate SpriteBatch for each game object is bad practice
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void UnloadContent(ContentManager i_Content)
        {
            // $G$ XNA-001 (-5) you are performing this action for each game object, while the ContentManager is a singelton!
            i_Content.Unload();
        }

        public virtual void Update(GameTime i_GameTime)
        {
            updateRectangle();
        }

        private void updateRectangle()
        {
            // $G$ XNA-002 (-5) you could have just use a Vector2 for the draw position, instead for creating a rectangle with the same widht/height
            Rectangle = new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, Texture.Width, Texture.Height);
        }

        public virtual void Draw(GameTime i_GameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();
        }
    }
}
