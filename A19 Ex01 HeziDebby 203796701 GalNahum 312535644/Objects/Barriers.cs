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
    public class Barriers : SpritesCollection<Barrier>
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
