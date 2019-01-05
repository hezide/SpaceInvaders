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
        }

        protected abstract void AllocateSpritesCollection();

        protected abstract void AllocateSprites(Game i_Game);

        public void Initialize(float i_InitialX = 0, float i_InitialY = 0)
        {
            SetPositions(i_InitialX, i_InitialY);
            setBoundaryNotifiers();
        }

        protected abstract void SetPositions(float i_InitialX, float i_InitialY);

        // TODO: idea - make setPositions as a template method and make an injection point to adjust positions
        //private virtual float adjustPosition(float x, float y, Sprite sprite)
        //{
        //    sprite.Position = new Vector2(x, y);
        //    x += sprite.Width * 2;

        //    return x;
        //}

        private void setBoundaryNotifiers()
        {
            Sprite leftBoundSprite = null;
            Sprite rightBoundSprite = null;

            foreach (Sprite sprite in Sprites)
            {
                if (sprite.Visible &&
                    ((leftBoundSprite == null) ||
                    (leftBoundSprite.Bounds.Left > sprite.Bounds.Left)))
                {
                    leftBoundSprite = sprite;
                }
                if (sprite.Visible &&
                    ((rightBoundSprite == null) ||
                    (rightBoundSprite.Bounds.Right < sprite.Bounds.Right)))
                {
                    rightBoundSprite = sprite;
                }
            }

            activateBoundarySprite(leftBoundSprite);
            activateBoundarySprite(rightBoundSprite);
        }

        private void activateBoundarySprite(Sprite i_BoundSprite)
        {
            i_BoundSprite.BoundaryHitAffects = true;
            i_BoundSprite.HitBoundaryEvent += notifier_onBoundaryHit;
        }

        private void notifier_onBoundaryHit(object i_Sender, EventArgs i_EventArgs)
        {
            m_GroupDirection *= k_DirectionChangeMultiplier;

            foreach (Sprite sprite in Sprites)
            {
                DoOnBoundaryHit(sprite);
            }
        }

        protected abstract void DoOnBoundaryHit(Sprite i_Sprite);
    }
}
