//*** Guy Ronen © 2008-2011 ***//
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators
{
    public abstract class SpriteAnimator
    {
        private TimeSpan m_TimeLeft;
        private bool m_IsFinished = false;
        private bool m_Initialized = false;
        private readonly string r_Name;
        protected bool m_ResetAfterFinish = true;
        protected internal Sprite m_OriginalSpriteInfo;
        protected internal Sprite BoundSprite { get; set; }
        public bool Enabled { get; set; } = true;

        public string Name
        {
            get { return r_Name; }
        }

        public event EventHandler Finished;

        protected virtual void OnFinished()
        {
            if (m_ResetAfterFinish)
            {
                Reset();
                this.m_IsFinished = true;
            }

            if (Finished != null)
            {
                Finished(this, EventArgs.Empty);
            }
        }

        protected SpriteAnimator(string i_Name, TimeSpan i_AnimationLength)
        {
            r_Name = i_Name;
            AnimationLength = i_AnimationLength;
        }

        public bool IsFinite
        {
            get { return this.AnimationLength != TimeSpan.Zero; }
        }

        public bool ResetAfterFinish
        {
            get { return m_ResetAfterFinish; }
            set { m_ResetAfterFinish = value; }
        }

        public virtual void Initialize()
        {
            if (!m_Initialized)
            {
                m_Initialized = true;

                CloneSpriteInfo();

                Reset();
            }
        }

        protected virtual void CloneSpriteInfo()
        {
            if (m_OriginalSpriteInfo == null)
            {
                m_OriginalSpriteInfo = BoundSprite.ShallowClone();
            }
        }

        public void Reset()
        {
            reset(AnimationLength);
        }

        private void reset(TimeSpan i_AnimationLength)
        {
            if (!m_Initialized)
            {
                Initialize();
            }
            else
            {
                AnimationLength = i_AnimationLength;
                m_TimeLeft = AnimationLength;
                this.IsFinished = false;
            }

            RevertToOriginal();
        }

        protected abstract void RevertToOriginal();

        public void Pause()
        {
            this.Enabled = false;
        }

        public void Resume()
        {
            Enabled = true;
        }

        public virtual void Restart()
        {
            Restart(AnimationLength);
        }

        public virtual void Restart(TimeSpan i_AnimationLength)
        {
            reset(i_AnimationLength);
            Resume();
        }

        protected TimeSpan AnimationLength { get; private set; }

        public bool IsFinished
        {
            get { return this.m_IsFinished; }
            protected set
            {
                if (value != m_IsFinished)
                {
                    m_IsFinished = value;
                    if (m_IsFinished == true)
                    {
                        OnFinished();
                    }
                }
            }
        }

        public void Update(GameTime i_GameTime)
        {
            if (!m_Initialized)
            {
                Initialize();
            }

            if (this.Enabled && !this.IsFinished)
            {
                if (this.IsFinite)
                {
                    // check if we should stop animating:
                    m_TimeLeft -= i_GameTime.ElapsedGameTime;

                    if (m_TimeLeft.TotalSeconds < 0)
                    {
                        this.IsFinished = true;
                    }
                }

                if (!this.IsFinished)
                {
                    // we are still required to animate:
                    DoFrame(i_GameTime);
                }
            }
        }

        protected abstract void DoFrame(GameTime i_GameTime);
    }
}
