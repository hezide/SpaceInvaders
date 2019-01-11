using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class SpinAnimator : SpriteAnimator
    {
        private TimeSpan m_SpinLength;
        private TimeSpan m_TimeLeftForNextSpin;
        private readonly float m_AngularVelocity;

        // CTORs
        public SpinAnimator(string i_Name, float i_AngularVelocity, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            m_AngularVelocity = i_AngularVelocity;
            m_SpinLength = i_AnimationLength;
            m_TimeLeftForNextSpin = i_AnimationLength;
        }

        public SpinAnimator(float i_SpinAngular, TimeSpan i_AnimationLength)
            : this("Spin", i_SpinAngular, i_AnimationLength)
        {
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.AngularVelocity = m_AngularVelocity;
            m_TimeLeftForNextSpin -= i_GameTime.ElapsedGameTime;

            if (m_TimeLeftForNextSpin.TotalSeconds < 0)
            {
                m_TimeLeftForNextSpin = m_SpinLength;
            }
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
            this.BoundSprite.AngularVelocity = m_OriginalSpriteInfo.AngularVelocity;
        }
    }
}

