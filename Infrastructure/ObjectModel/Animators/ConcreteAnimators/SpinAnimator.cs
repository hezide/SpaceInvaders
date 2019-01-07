using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class SpinAnimator : SpriteAnimator
    {
        private TimeSpan m_SpinLength;
        private TimeSpan m_TimeLeftForNextSpin;
        private readonly float m_AngularVelocity;

        //public TimeSpan SpinLength
        //{
        //    get { return m_SpinLength; }
        //    set { m_SpinLength = value; }
        //}

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

        //public SpinAnimator(string i_Name, TimeSpan i_SpinLength, TimeSpan i_AnimationLength)
        //    : base(i_Name, i_AnimationLength)
        //{
        //    this.m_SpinLength = i_SpinLength;
        //    this.m_TimeLeftForNextSpin = i_SpinLength;
        ////    this.m_SpinAngular = MathHelper.TwoPi / (float)i_SpinLength.TotalSeconds;
        //}

        //public SpinAnimator(TimeSpan i_SpinLength, TimeSpan i_AnimationLength)
        //    : this("Spin", i_SpinLength, i_AnimationLength)
        //{
        //    this.m_SpinLength = i_SpinLength;
        //    this.m_TimeLeftForNextSpin = i_SpinLength;
        //}

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.AngularVelocity = m_AngularVelocity;
          //  BoundSprite.Rotation += m_SpinAngular * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_TimeLeftForNextSpin -= i_GameTime.ElapsedGameTime;

            if (m_TimeLeftForNextSpin.TotalSeconds < 0)
            {//TODO: something happens every spin ?
                //   this.BoundSprite.AngularVelocity 
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

