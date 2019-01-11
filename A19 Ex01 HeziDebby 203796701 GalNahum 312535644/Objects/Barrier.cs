using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Barrier : Sprite, ICllidableByPixels
    {
        private const string k_AssetName = @"Sprites\Barrier_44x32";
        private const float k_Velocitiy = 45;

        public Barrier(Game i_Game) : base(k_AssetName, i_Game)
        {
            PixelBasedCollisionComponent = new PixelBasedCollisionComponent(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            // TODO: *** for unshared sprite batch
            //m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            Velocity = new Vector2(k_Velocitiy, 0);
            Texture = GetTextureClone();
        }

        public override void Draw(GameTime gameTime)
        {
            DrawNonPremultiplied();
        }

        //public override bool HitBoundary()
        //{
        //    return (Bounds.Right >= GraphicsDevice.Viewport.Bounds.Right ||
        //            Bounds.Left <= GraphicsDevice.Viewport.Bounds.Left);
        //}
    }
}
