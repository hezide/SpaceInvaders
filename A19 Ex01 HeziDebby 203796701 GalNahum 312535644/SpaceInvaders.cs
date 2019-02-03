using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class SpaceInvaders : Game
    {
        public readonly GraphicsDeviceManager r_Graphics;
        private readonly InputManager r_InputManager;
        private readonly CollisionsManager r_CollisionsManager;
        private readonly ScreensMananger r_ScreensManager;
        private readonly IGameSettings r_Settings;

        public SpaceInvaders()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Space Invaders";

            r_InputManager = new InputManager(this);
            r_CollisionsManager = new CollisionsManager(this);
            r_Settings = new SpaceInvadersSettings(this);
            
            r_ScreensManager = new ScreensMananger(this,r_Graphics);
            r_ScreensManager.Push(new GameOverScreen(this));
            r_ScreensManager.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Initialize()
        {
            SpriteBatch spriteBatch = new SpriteBatch(r_Graphics.GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            base.Initialize();
        }
    }
}
