using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class JumpAnimator : SpriteAnimator
    {
        private TimeSpan m_TimeToJump;
        private TimeSpan m_TimeLeftForNextJump;
        private Vector2 m_JumpDestination;

        public JumpAnimator(string i_Name, Vector2 i_JumpDestination, TimeSpan i_TimeToJump, TimeSpan i_AnimationLength)
    : base(i_Name, i_AnimationLength)
        {
            this.m_TimeToJump = i_TimeToJump;
            this.m_TimeLeftForNextJump = i_TimeToJump;
            this.m_JumpDestination = i_JumpDestination;
        }

        public JumpAnimator(Vector2 i_JumpDestination, TimeSpan i_TimeToJump, TimeSpan i_AnimationLength)
            : this("Jump", i_JumpDestination, i_TimeToJump, i_AnimationLength)
        {
            //this.m_BlinkLength = i_BlinkLength;
            //this.m_TimeLeftForNextBlink = i_BlinkLength;
        }
        // private double m_SecondsFromLastJump = 0;
        // m_SecondsFromLastJump += i_GameTime.ElapsedGameTime.TotalSeconds;

        //            if (m_SecondsFromLastJump > 0.5)
        //            {
        //                // float xPosition = CurrentPosition.X + (Velocity * (float)m_SecondsFromLastJump);
        //                float xPosition = Utilities.CalculateNewCoordinate(CurrentPosition.X, CurrentDirection, Velocity, m_SecondsFromLastJump);
        //                CurrentPosition = new Vector2(xPosition, CurrentPosition.Y);
        //                m_SecondsFromLastJump = 0;
        //            }
        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForNextJump -= i_GameTime.ElapsedGameTime;

            if (m_TimeLeftForNextJump.TotalSeconds < 0)
            {
                //BoundSprite.Visible = !BoundSprite.Visible;
                BoundSprite.Position += m_JumpDestination;
                m_TimeLeftForNextJump = m_TimeToJump;
            }
        }

        protected override void RevertToOriginal()
        {// TODO: is this right ?
            this.BoundSprite.Position = m_OriginalSpriteInfo.Position;
        }
    }
}
