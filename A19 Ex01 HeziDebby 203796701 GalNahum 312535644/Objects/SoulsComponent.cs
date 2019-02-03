using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class SoulsComponent : Sprite
    {
        private const int k_NumberOfSouls = 1; // TODO: debug only should be 3
        public int NumberOfSouls { get; set; }

        public SoulsComponent(string i_AssetName, Game i_Game, GameScreen i_Screen) : base(i_AssetName, i_Game, i_Screen)
        {
            NumberOfSouls = k_NumberOfSouls;
        }

        public override void Initialize()
        {
            base.Initialize();

            Scales = new Vector2(0.5f);

            setTintColor();
            setInitialPosition();
        }

        private void setTintColor()
        {
            Vector4 tintVector = this.TintColor.ToVector4();
            tintVector.W = 0.5f;
            TintColor = new Color(tintVector);
        }

        private void setInitialPosition()
        {
            float x = GraphicsDevice.Viewport.Width - Width * 1.5f;
            float y = Position.Y + Height;

            Position = new Vector2(x, y);
        }
     
        public override void Draw(GameTime gameTime)
        {
            drawSouls();
        }

        private void drawSouls()
        {
            Vector2 initialPosition = Position;

            for (int i = 0; i < NumberOfSouls; i++)
            {
                DrawWithParameters();

                Position = new Vector2(Position.X - 1.5f * Width, Position.Y);
            }

            Position = initialPosition;
        }
    }
}
