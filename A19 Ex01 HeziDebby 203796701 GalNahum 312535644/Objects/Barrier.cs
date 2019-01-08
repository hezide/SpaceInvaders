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
            Velocity = new Vector2(k_Velocitiy, 0);
            BoundaryHitAffects = true;
            Texture = GetTextureClone();
        }

        public override void Draw(GameTime gameTime)
        {
            DrawNonPremultiplied();
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is IExplodable)
            {
                Explode((i_Collidable as IExplodable).ExplosionRange);
            }
            else
            {
                OnPixelsCollision(i_Collidable);
            }
        }
        //protected override void OnPixelsCollision(int i_X, int i_Y, ICollidable2D i_Source)
        //{
        //    base.OnPixelsCollision(i_X, i_Y, i_Source);
        //}
        //public override void Collided(ICollidable i_Collidable)
        //{
        //    base.Collided(i_Collidable);
        ////    Texture.SetData<Color>(Pixels);
        //}

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}
    }
}
