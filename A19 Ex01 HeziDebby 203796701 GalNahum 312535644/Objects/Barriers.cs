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
    public class Barriers : SpritesCollection<Barrier>//<Barrier>
    {
        private const int k_NumberOfBarriers = 4;

        public Barriers(Game i_Game) : base(i_Game)
        {
        }

        protected override void AllocateSprites(Game i_Game)
        {
            for (int i = 0; i < k_NumberOfBarriers; i++)
            {
                Sprites.Add(new Barrier(i_Game));
            }
        }

        protected override void InitPositions(float i_InitialX, float i_InitialY)
        {
            // TODO: calculation ?
            float x = i_InitialX / (Sprites.Count) + Sprites[0].Width * 1.5f;
            float y = 0;

            foreach (Barrier sprite in Sprites)
            {
                y = i_InitialY - sprite.Height * 2;

                sprite.Position = new Vector2(x, y);

                x += sprite.Width * 2;
            }
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
