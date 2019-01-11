using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Barriers : SpritesCollection<Barrier>
    {
        private const int k_NumberOfBarriers = 4;
        private Vector2 m_BarrierBounds;

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
            float x = i_InitialX / (Sprites.Count) + Sprites[0].Width * 1.5f;
            float y = 0;

            foreach (Barrier sprite in Sprites)
            {
                y = i_InitialY - sprite.Height * 2;
                sprite.Position = new Vector2(x, y);
                x += sprite.Width * 2;
            }

            initGroupBounds();
        }

        private void initGroupBounds()
        {
            int leftBound = Sprites[0].Bounds.Left;
            int rightBound = Sprites[k_NumberOfBarriers - 1].Bounds.Right;

            m_BarrierBounds = new Vector2(leftBound, rightBound);
        }

        protected override bool GroupHitBoundary()
        {
            Barrier barrier = Sprites[GetEdgeSpriteIdxByDirection()];
            float offset = barrier.Width / 2;

            return barrier.Bounds.Right >= m_BarrierBounds.Y + offset ||
                barrier.Bounds.Left <= m_BarrierBounds.X - offset;
        }
    }
}
