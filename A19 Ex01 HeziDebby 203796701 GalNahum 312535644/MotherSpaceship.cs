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
    public class MotherSpaceship : GameObject, IDestryoable
    {
        public int Souls { get; set; }

        public MotherSpaceship(GraphicsDevice i_GraphicsDevice) : base(i_GraphicsDevice)
        {

        }

        public override void Initialize(ContentManager i_Content)
        {
            base.Initialize(i_Content);

       //     CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_MotherSpaceshipVelocity;
            Color = Color.Red;
            Souls = Utilities.k_MotherSpaceshipSouls;
            CurrentPosition = SetToInitialPosition();
        }

        public Vector2 SetToInitialPosition()
        {
            return new Vector2(GraphicsDevice.Viewport.Bounds.Left - Texture.Width, Texture.Height);
        }

        protected override void LoadContent(ContentManager i_Content)
        {
            base.LoadContent(i_Content);

            Texture = i_Content.Load<Texture2D>(@"Sprites\MotherShip_32x120");
        }

        private void move(GameTime i_GameTime)
        {
            CurrentPosition = new Vector2(CurrentPosition.X + Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds, CurrentPosition.Y);
        }

        public override void Update(GameTime i_GameTime)
        {
            move(i_GameTime);
            base.Update(i_GameTime);
        }

        public override void Draw(GameTime i_GameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();
        }

        // TODO: Ex2 code duplication
        public void GetHit()
        {
            Souls--;
        }
        // TODO: Ex2 code duplication
        public bool IsDead()
        {
            return Souls == 0;
        }
    }
}
