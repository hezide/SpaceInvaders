using System;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Barrier : Sprite, ICollidableByPixels
    {
        private const string k_AssetName = @"Sprites\Barrier_44x32";
        private SoundEffect m_BarrierHitSoundEffect;

        public Barrier(Game i_Game, GameScreen i_Screen) : base(k_AssetName, i_Game, i_Screen)
        {
            PixelBasedCollisionComponent = new PixelBasedCollisionComponent(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            float barrierSpeed = (m_GameSettings as SpaceInvadersSettings).BarriersVelocity;
            Velocity = new Vector2(barrierSpeed, 0);
            Texture = GetTextureClone();
        }

        public override void Draw(GameTime gameTime)
        {
            DrawNonPremultiplied();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_BarrierHitSoundEffect = this.Game.Content.Load<SoundEffect>("Sounds/BarrierHit");

        }
        protected override void OnCollision(object i_Sender, EventArgs i_Args)
        {
            base.OnCollision(i_Sender, i_Args);
            m_BarrierHitSoundEffect.Play();
        }
    }
}
