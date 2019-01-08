using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Infrastructure.ObjectModel
{
    public abstract class SpritesCollection
    {
        protected const float k_DirectionChangeMultiplier = -1f;
        public List<Sprite> Sprites { get; protected set; }
        // public Rectangle Bounds { get; private set; }
        private float m_GroupDirection = 1f; // right

        public SpritesCollection(Game i_Game)
        {
            AllocateSpritesCollection();
            AllocateSprites(i_Game);
            m_SpritesOnBoundaries = new List<Sprite>();
        }

        protected abstract void AllocateSpritesCollection();

        protected abstract void AllocateSprites(Game i_Game);

        public void Initialize(float i_InitialX = 0, float i_InitialY = 0)
        {
            SetPositions(i_InitialX, i_InitialY);
            setBoundaryNotifiers();
            //   setLitenerOnDisposed();
        }

        //private void setLitenerOnDisposed()
        //{
        //    foreach (Sprite sprite in Sprites)
        //    {
        //        sprite.Disposed += notifier_OnDisposed;
        //    }
        //}

        // TODO: is this the best way ? every time an enemy is dead your checking them all .. isnt it the same as before ?
        // TODO: probably need to save the edges sprites as reffernced to when they are dead .. or add as listener only to them
        //private void notifier_OnDisposed(object i_Sender, EventArgs i_EventArgs)
        //{
        //    setBoundaryNotifiers();
        //}

        protected abstract void SetPositions(float i_InitialX, float i_InitialY);

        // TODO: idea - make setPositions as a template method and make an injection point to adjust positions
        //private virtual float adjustPosition(float x, float y, Sprite sprite)
        //{
        //    sprite.Position = new Vector2(x, y);
        //    x += sprite.Width * 2;

        //    return x;
        //}

        //protected struct CollectionBounds
        //{
        //    Sprite LeftBoundSprite;
        //    Sprite RightBoundSprite;

        //}
        // TODO: havent checked 
        protected List<Sprite> m_SpritesOnBoundaries;
     //   protected Rectangle Bounds { get; set; }

        private void setBoundaryNotifiers()
        {
            Sprite leftBoundSprite = null;
            Sprite rightBoundSprite = null;

            foreach (Sprite sprite in Sprites)
            {
                if (sprite.Visible &&
                    ((leftBoundSprite == null) ||
                    (leftBoundSprite.Bounds.Left > sprite.Bounds.Left)
                    && !m_SpritesOnBoundaries.Contains(sprite)))
                {
                   // Bounds.Left = sprite.Bounds.Left;
                    leftBoundSprite = sprite;
                }
                if (sprite.Visible &&
                    ((rightBoundSprite == null) ||
                    (rightBoundSprite.Bounds.Right < sprite.Bounds.Right)
                    && !m_SpritesOnBoundaries.Contains(sprite)))
                {
                    rightBoundSprite = sprite;
                }
            }

            //m_SpritesOnBoundaries.Add(rightBoundSprite);
            //m_SpritesOnBoundaries.Add(leftBoundSprite);

            activateBoundarySprite(leftBoundSprite);
            activateBoundarySprite(rightBoundSprite);
        }

        private void activateBoundarySprite(Sprite i_BoundSprite)
        {
            if (i_BoundSprite != null)
            {
                i_BoundSprite.BoundaryHitAffects = true;
                i_BoundSprite.HitBoundaryEvent += notifier_OnBoundaryHit;
                i_BoundSprite.Disposed += notifier_OnBoundSpriteDisposed;
                m_SpritesOnBoundaries.Add(i_BoundSprite);
            }
        }

        private void notifier_OnBoundSpriteDisposed(object i_Sender, EventArgs i_EventArgs)
        {
            if (m_SpritesOnBoundaries.Contains(i_Sender as Sprite))
            {
                m_SpritesOnBoundaries.Remove(i_Sender as Sprite);
            }

            setBoundaryNotifiers();
        }

        private void notifier_OnBoundaryHit(object i_Sender, OffsetEventArgs i_EventArgs)
        {
            m_GroupDirection *= k_DirectionChangeMultiplier;

            foreach (Sprite sprite in Sprites)
            {
                //          if (i_Sender as Sprite != sprite)
                //        {
                DoOnBoundaryHit(sprite, i_EventArgs);
                //}
                //else
                //{
                //    DoOnBoundaryHit(sprite, new OffsetEventArgs { Offset = 0 });
                //}
            }
            //     DoOnBoundaryHit(i_Sender as Sprite, i_EventArgs);
        }

        protected abstract void DoOnBoundaryHit(Sprite i_Sprite, OffsetEventArgs i_EventArgs);
    }
}
