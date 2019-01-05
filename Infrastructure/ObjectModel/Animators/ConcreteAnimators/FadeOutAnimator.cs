﻿using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class FadeOutAnimator : SpriteAnimator
    {
        private TimeSpan m_FadeOutLength;
        private readonly byte m_AlphaFactor;

        public FadeOutAnimator(string i_Name, TimeSpan i_FadeOutLength)
            : base(i_Name, i_FadeOutLength)
        {
            m_FadeOutLength = i_FadeOutLength;

            m_AlphaFactor = (byte)(255.0 / i_FadeOutLength.TotalSeconds);
        }

        public FadeOutAnimator(TimeSpan i_FadeOutLength)
            : this("FadeOut", i_FadeOutLength)
        {
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            byte alpha = BoundSprite.TintColor.A;

            alpha -= (byte)(m_AlphaFactor * i_GameTime.ElapsedGameTime.TotalSeconds);

            BoundSprite.TintColor = new Color(BoundSprite.TintColor, alpha);

            if (alpha <= 0)
            {// TODO: check the calcluation because the alpha is never 0
                int i = 0;
            }
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.TintColor = m_OriginalSpriteInfo.TintColor;
        }
    }
}