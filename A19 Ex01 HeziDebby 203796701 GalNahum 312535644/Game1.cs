using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class Game1 : Game
    {
        GraphicsDeviceManager           graphics;
        private SpaceInvadersManager    m_SpaceInvadersManager;
        private SpriteBatch             m_SpriteBatch;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            m_SpaceInvadersManager = new SpaceInvadersManager();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_SpaceInvadersManager.Init();
            this.Window.Title = "Space Invaders";
            base.Initialize();

        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            Texture2D pinkTexture = Content.Load<Texture2D>(@"Sprites\Enemy0101_32x32");
            Texture2D blueTexture = Content.Load<Texture2D>(@"Sprites\Enemy0201_32x32");
            Texture2D yellowTexture = Content.Load<Texture2D>(@"Sprites\Enemy0301_32x32");
            Texture2D bulletTexture = Content.Load<Texture2D>(@"Sprites\Enemy0301_32x32");
            Texture2D motherShipTexture = Content.Load<Texture2D>(@"Sprites\MotherShip_32x120");
            Texture2D playerTexture = Content.Load<Texture2D>(@"Sprites\Ship01_32x32");

            Texture2D backgroundTexture = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");

            m_SpaceInvadersManager.InitAllTextures(pinkTexture, blueTexture, yellowTexture, bulletTexture, motherShipTexture, playerTexture);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
