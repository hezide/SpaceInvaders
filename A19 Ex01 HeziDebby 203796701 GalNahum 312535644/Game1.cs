using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
            //m_SpaceInvadersManager = new SpaceInvadersManager(this.GraphicsDevice.Viewport);
            m_SpaceInvadersManager = new SpaceInvadersManager();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Space Invaders";
            base.Initialize();
            IsMouseVisible = true;
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            LoadTextures();
        }

        private void LoadTextures()
        {
            Dictionary<Utilities.eDrawableType, Texture2D> texturesDictionary = new Dictionary<Utilities.eDrawableType, Texture2D>();

            texturesDictionary[Utilities.eDrawableType.PinkEnemy] = Content.Load<Texture2D>(@"Sprites\Enemy0101_32x32");
            texturesDictionary[Utilities.eDrawableType.BlueEnemy] = Content.Load<Texture2D>(@"Sprites\Enemy0201_32x32");
            texturesDictionary[Utilities.eDrawableType.YellowEnemy] = Content.Load<Texture2D>(@"Sprites\Enemy0301_32x32");
            texturesDictionary[Utilities.eDrawableType.Bullet] = Content.Load<Texture2D>(@"Sprites\Bullet");
            texturesDictionary[Utilities.eDrawableType.MotherSpaceship] = Content.Load<Texture2D>(@"Sprites\MotherShip_32x120");
            texturesDictionary[Utilities.eDrawableType.Spaceship] = Content.Load<Texture2D>(@"Sprites\Ship01_32x32");
            texturesDictionary[Utilities.eDrawableType.Background] = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");

            m_SpaceInvadersManager.Init(texturesDictionary, m_SpriteBatch, this.GraphicsDevice.Viewport);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }
 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            //this is an attempt to make the jumps only for specific objects
            //this.TargetElapsedTime = TimeSpan.FromSeconds(0.5);
            m_SpaceInvadersManager.Update(gameTime);

           // this.TargetElapsedTime = TimeSpan.FromMilliseconds(16);
            base.Update(gameTime);
        }
 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
                // TODO: Add your drawing code here
            m_SpaceInvadersManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
