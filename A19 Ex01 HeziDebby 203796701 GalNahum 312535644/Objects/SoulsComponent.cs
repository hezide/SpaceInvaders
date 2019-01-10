using Infrastructure.ObjectModel;
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
        private const int k_NumberOfSouls = 3;
        public int NumberOfSouls { get; set; }

        public SoulsComponent(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {
            NumberOfSouls = k_NumberOfSouls;
        }

        public override void Initialize()
        {
            base.Initialize();
            // TODO: *** for unshared sprite batch
            //m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            //m_UseSharedBatch = false;

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
        // protected override void InitBounds()
        // {
        //     PosOfOriginInScreen = Position;
        //     DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        //     // TODO: implementation required -> all the dependens on height and width and position
        ////     Rectangle bounds = new Rectangle(Position,Width;
        // }

        public override void Draw(GameTime gameTime)
        {
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            drawSouls();

            m_SpriteBatch.End();
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
