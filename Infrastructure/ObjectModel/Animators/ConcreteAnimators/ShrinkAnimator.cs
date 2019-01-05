using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ShrinkAnimator : SpriteAnimator
    {// TODO:
        // shrinker should either get a scale factor or calculate the scale by the given animation length
        private TimeSpan m_ShrinkLength;
        private readonly float m_ScaleFactor;

        public ShrinkAnimator(TimeSpan i_ShrinkLength)
            : base("Shrink", i_ShrinkLength)
        {
            this.m_ShrinkLength = i_ShrinkLength;
            m_ScaleFactor = (float)(1 / i_ShrinkLength.TotalSeconds); // should be scale / shrinkLength
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            this.BoundSprite.Scales -= new Vector2(m_ScaleFactor * (float)i_GameTime.ElapsedGameTime.TotalSeconds);

            if (BoundSprite.Scales.X <= 0)
            {
                BoundSprite.Visible = false;
            }
        }

        //protected override void OnFinished()
        //{
        //    base.OnFinished();
        //    BoundSprite.Visible = false;
        //}

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
        }
    }
}
