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
    public class Barriers : SpritesCollection
    {
        private const int k_NumberOfBarriers = 4;

        public Barriers(Game i_Game) : base(i_Game)
        {
        }

        protected override void AllocateSpritesCollection()
        {
            base.Sprites = new List<Sprite>();
        }

        protected override void AllocateSprites(Game i_Game)
        {
            //Barrier barrierToAdd;

            for (int i = 0; i < k_NumberOfBarriers; i++)
            {
            //    barrierToAdd = new Barrier(i_Game);
                Sprites.Add(new Barrier(i_Game));

            //    Texture2D texture = new Texture2D(barrierToAdd.GraphicsDevice, (int)barrierToAdd.Width, (int)barrierToAdd.Height);
            //    texture.SetData<Color>(barrierToAdd.Pixels);
            }
        }

        //private Barrier createBarrierWithClonedTexture(Game i_Game)
        //{// TODO: i dont use the original texture - all textures are cloned .. is this ok ?
        //    Barrier barrierToAdd = new Barrier(i_Game);
        //    Texture2D texture = new Texture2D(i_Game.GraphicsDevice, (int)barrierToAdd.Texture.Width, (int)barrierToAdd.Texture.Height);

        //    texture.SetData<Color>(barrierToAdd.Pixels);
        //    barrierToAdd.Texture = texture;

        //    return barrierToAdd;
        //}

        protected override void SetPositions(float i_InitialX, float i_InitialY)
        {
            // TODO: calculation ?
            float x = i_InitialX / (Sprites.Count) + Sprites[0].Width * 1.5f;
            float y = 0;

            foreach (Sprite sprite in Sprites)
            {
                y = i_InitialY - sprite.Height * 2;

                sprite.Position = new Vector2(x, y);

                x += sprite.Width * 2;
            }
        }

        protected override void DoOnBoundaryHit(Sprite i_Sprite)
        {
            i_Sprite.Velocity *= k_DirectionChangeMultiplier;
        }
    }
}
    //{
    //    private const int k_NumberOfBarriers = 4;
    //    public List<Barrier> BarriersList { get; private set; }

    //    public Barriers(Game i_Game)
    //    {
    //        BarriersList = new List<Barrier>();
    //        initBarriers(i_Game);
    //    }

    //    private void initBarriers(Game i_Game)
    //    {
    //        Barrier barrierToAdd;

    //        for (int i = 0; i < k_NumberOfBarriers; i++)
    //        {
    //            barrierToAdd = new Barrier(i_Game);
    //            barrierToAdd.BoundaryHitAffects = true;
    //            barrierToAdd.HitBoundaryAction += onBoundaryHit;
    //            BarriersList.Add(barrierToAdd);
    //        }
    //    }

    //    private void onBoundaryHit()
    //    {
    //        // *** changing barriers direction ***
    //        foreach (Barrier barrier in BarriersList)
    //        {
    //            barrier.Velocity *= -1;
    //        }
    //    }

    //    public void SetPositions(float i_FullScreenWidth, float i_HeightOffset)
    //    {
    //        // TODO: calculation ?
    //        float x = i_FullScreenWidth / (BarriersList.Count) + BarriersList[0].Width * 1.5f;
    //        float y = 0;

    //        foreach (Barrier barrier in BarriersList)
    //        {
    //            //   barrier.Velocity = k_Velocitiy;
    //            y = i_HeightOffset - barrier.Height * 2;

    //            barrier.Position = new Vector2(x, y);

    //            x += barrier.Width * 2;
    //        }
    //    }
    //}
//}
