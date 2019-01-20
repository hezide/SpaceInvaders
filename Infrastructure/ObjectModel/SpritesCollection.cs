using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Infrastructure.ObjectModel
{
    public abstract class SpritesCollection<T>
        where T : Sprite
    {
        protected readonly Vector2 r_DirectionChangeMultiplier;
        public List<T> Sprites { get; protected set; }
        protected Vector2 m_GroupDirection;
        protected GameScreen m_Screen;
        protected IGameSettings m_GameSettings;

        public SpritesCollection(Game i_Game, GameScreen i_Screen)
        {
            m_Screen = i_Screen;
            m_GameSettings = i_Game.Services.GetService(typeof(IGameSettings)) as IGameSettings;

            r_DirectionChangeMultiplier = new Vector2(-1);
            Sprites = new List<T>();
            AllocateSprites(i_Game);
        }

        protected abstract void AllocateSprites(Game i_Game);

        public virtual void Initialize(float i_InitialX = 0, float i_InitialY = 0)
        {
            m_GroupDirection = new Vector2(1, 0);
            InitPositions(i_InitialX, i_InitialY);
        }

        protected abstract void InitPositions(float i_InitialX, float i_InitialY);

        public virtual void Update(GameTime i_GameTime)
        {
            if (GroupHitBoundary())
            {
                m_GroupDirection *= r_DirectionChangeMultiplier;

                DoOnBoundaryHit(i_GameTime);
            }
        }

        protected virtual void DoOnBoundaryHit(GameTime i_GameTime)
        {
            foreach (T item in Sprites)
            {
                item.Velocity *= r_DirectionChangeMultiplier;
            }
        }

        protected virtual bool GroupHitBoundary()
        {
            return Sprites[GetEdgeSpriteIdxByDirection()].HitBoundary();
        }

        protected virtual int GetEdgeSpriteIdxByDirection()
        {
            return m_GroupDirection.X > 0 ? Sprites.Count - 1 : 0;
        }
    }
}
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
//  protected List<Sprite> m_SpritesOnBoundaries;
//   protected Rectangle Bounds { get; set; }

//private void setBoundaryNotifiers()
//{
//    Sprite leftBoundSprite = null;
//    Sprite rightBoundSprite = null;

//    foreach (Sprite sprite in Sprites)
//    {
//        if (sprite.Visible &&
//            ((leftBoundSprite == null) ||
//            (leftBoundSprite.Bounds.Left > sprite.Bounds.Left)
//            && !m_SpritesOnBoundaries.Contains(sprite)))
//        {
//           // Bounds.Left = sprite.Bounds.Left;
//            leftBoundSprite = sprite;
//        }
//        if (sprite.Visible &&
//            ((rightBoundSprite == null) ||
//            (rightBoundSprite.Bounds.Right < sprite.Bounds.Right)
//            && !m_SpritesOnBoundaries.Contains(sprite)))
//        {
//            rightBoundSprite = sprite;
//        }
//    }

//    //m_SpritesOnBoundaries.Add(rightBoundSprite);
//    //m_SpritesOnBoundaries.Add(leftBoundSprite);

//    activateBoundarySprite(leftBoundSprite);
//    activateBoundarySprite(rightBoundSprite);
//}

//private void activateBoundarySprite(Sprite i_BoundSprite)
//{
//    if (i_BoundSprite != null)
//    {
//        i_BoundSprite.BoundaryHitAffects = true;
//        i_BoundSprite.HitBoundaryEvent += notifier_OnBoundaryHit;
//        i_BoundSprite.Disposed += notifier_OnBoundSpriteDisposed;
//        m_SpritesOnBoundaries.Add(i_BoundSprite);
//    }
//}

//private void notifier_OnBoundSpriteDisposed(object i_Sender, EventArgs i_EventArgs)
//{
//    if (m_SpritesOnBoundaries.Contains(i_Sender as Sprite))
//    {
//        m_SpritesOnBoundaries.Remove(i_Sender as Sprite);
//    }

//    setBoundaryNotifiers();
//}

//private void notifier_OnBoundaryHit(object i_Sender, OffsetEventArgs i_EventArgs)
//{
//    m_GroupDirection *= k_DirectionChangeMultiplier;

//    foreach (Sprite sprite in Sprites)
//    {
//        //          if (i_Sender as Sprite != sprite)
//        //        {
//        DoOnBoundaryHit(sprite, i_EventArgs);
//        //}
//        //else
//        //{
//        //    DoOnBoundaryHit(sprite, new OffsetEventArgs { Offset = 0 });
//        //}
//    }
//    //     DoOnBoundaryHit(i_Sender as Sprite, i_EventArgs);
//}

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
