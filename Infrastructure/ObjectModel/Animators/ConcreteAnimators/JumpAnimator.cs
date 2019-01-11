using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class JumpAnimator : SpriteAnimator
    {
        private TimeSpan m_TimeToJump;
        private TimeSpan m_TimeLeftForNextJump;
        private Vector2 m_JumpDestination;
        private Rectangle m_JumpBounds;

        public JumpAnimator(
            string i_Name,
            Vector2 i_JumpDestination,
            Rectangle i_JumpBounds,
            TimeSpan i_TimeToJump,
            TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.m_TimeToJump = i_TimeToJump;
            this.m_TimeLeftForNextJump = i_TimeToJump;
            this.m_JumpDestination = i_JumpDestination;
            this.m_JumpBounds = i_JumpBounds;
        }

        public JumpAnimator(
            Vector2 i_JumpDestination,
            Rectangle i_JumpBounds,
            TimeSpan i_TimeToJump,
            TimeSpan i_AnimationLength)
            : this("Jump", i_JumpDestination, i_JumpBounds, i_TimeToJump, i_AnimationLength)
        {}

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForNextJump -= i_GameTime.ElapsedGameTime;

            Vector2 jumpDestination = m_JumpDestination;
            Vector2 maxDistance = BoundSprite.Position + jumpDestination;

            if (maxDistance.X >= m_JumpBounds.Right - BoundSprite.Width)
            {
                jumpDestination.X +=  (m_JumpBounds.Right - BoundSprite.Width) - maxDistance.X;
            }
            else if (maxDistance.X <= m_JumpBounds.Left)
            {
                jumpDestination.X -= m_JumpBounds.Left + maxDistance.X;
            }

            if (m_TimeLeftForNextJump.TotalSeconds < 0)
            {
                BoundSprite.Position += jumpDestination;
                m_TimeLeftForNextJump = m_TimeToJump;
            }
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Position = m_OriginalSpriteInfo.Position;
        }
    }
}
