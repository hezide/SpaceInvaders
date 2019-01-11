using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Infrastructure.ObjectModel
{
    public class PixelBasedCollisionComponent // TODO: interface ?
    {
        private ICllidableByPixels m_Collidable;

        public PixelBasedCollisionComponent(ICllidableByPixels i_Collidable)
        {
            m_Collidable = i_Collidable;
        }

        public bool CheckCollision(ICollidable2D i_Source, Rectangle i_IntersectionBounds)
        {
            return pixelsIntersects(i_Source, i_IntersectionBounds);
        }

        private bool pixelsIntersects(ICollidable2D i_Source, Rectangle i_Intersection)
        {// TODO: optimized
            bool intersects = false;
            Color[] sourcePixels = getPixelsData(i_Source); // TODO: this method is a helper of sprite. got nothing to do with sprite

            for (int y = i_Intersection.Top; y < i_Intersection.Bottom && !intersects; y++)
            {
                for (int x = i_Intersection.Left; x < i_Intersection.Right && !intersects; x++)
                {
                    if (!isTransparent(m_Collidable.Pixels, x, y, m_Collidable.Bounds) &&
                        !isTransparent(sourcePixels, x, y, i_Source.Bounds))
                    {
                        intersects = true;
                    }
                }
            }

            if (intersects)
            {
                m_Collidable.IntersectionRectangle = i_Intersection;
                IntersectionPoints = new List<Point>();
            }

            return intersects;
        }

        public void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is IExplodable)
            {
                collidePixelByRange(i_Collidable as IExplodable);
            }
            else
            {
                OnPixelsCollision(i_Collidable);
            }

            m_Collidable.Texture.SetData<Color>(m_Collidable.Pixels);
        }

        public virtual void OnPixelsCollision(ICollidable i_Collidable)
        {
            int pixelIdx;

            for (int y = m_Collidable.IntersectionRectangle.Top; y < m_Collidable.IntersectionRectangle.Bottom; y++)
            {
                for (int x = m_Collidable.IntersectionRectangle.Left; x < m_Collidable.IntersectionRectangle.Right; x++)
                {// TODO: remove trnasparent pixels from array
                 //  IntersectionPoints.Add(new Point(x, y));
                    pixelIdx = getPixel(x, y, m_Collidable.Bounds);
                    m_Collidable.Pixels[pixelIdx].A = 0;
                }
            }
        }

        private bool isTransparent(Color[] i_Pixels, int i_X, int i_Y, Rectangle i_Bounds)
        {
            return i_Pixels[getPixel(i_X, i_Y, i_Bounds)].A == 0;
        }

        private int getPixel(int i_X, int i_Y, Rectangle i_Bounds)
        {
            return (i_X - i_Bounds.Left) + ((i_Y - i_Bounds.Top) * i_Bounds.Width);
        }

        private void transparentPixel(int i_PixelIdx)
        {
            m_Collidable.Pixels[i_PixelIdx].A = 0;
        }

        private Color[] getPixelsData(ICollidable2D i_Source)
        {
            Color[] sourcePixels;

            if (i_Source is Sprite)
            {
                sourcePixels = (i_Source as Sprite).Pixels;
            }
            else
            {
                sourcePixels = new Color[i_Source.Bounds.Width * i_Source.Bounds.Height];
                i_Source.Texture.GetData<Color>(sourcePixels);
            }

            return sourcePixels;
        }

        private void collidePixelByRange(IExplodable i_Collidable)
        {
            Color[] sourcePixels = getPixelsData(i_Collidable as ICollidable2D);

            for (int y = m_Collidable.IntersectionRectangle.Top; y < m_Collidable.IntersectionRectangle.Bottom; y++)
            {
                for (int x = m_Collidable.IntersectionRectangle.Left; x < m_Collidable.IntersectionRectangle.Right; x++)
                {// TODO: remove trnasparent pixels from array
                    if (!isTransparent(m_Collidable.Pixels, x, y, m_Collidable.Bounds) &&
                        !isTransparent(sourcePixels, x, y, (i_Collidable as ICollidable2D).Bounds))
                    {
                        IntersectionPoints.Add(new Point(x, y));
                    }
                }
            }

            transpaerntIntersectionPoints(i_Collidable.ExplosionRange);
        }

        public List<Point> IntersectionPoints { get; set; }

        private void transpaerntIntersectionPoints(float i_ExplosionRange)
        {
            foreach (Point point in IntersectionPoints)
            {
                transparentPixelsInRange(point.X, point.Y, i_ExplosionRange);
            }
        }

        private void transparentPixelsInRange(int i_X, int i_Y, float i_ExplosionRange)
        {
            int direction = i_ExplosionRange > 0 ? 1 : -1;

            int destinateY = i_Y + (int)i_ExplosionRange;
            int currentY = i_Y;

            while (currentY != destinateY)
            {
                int pixelIdx = getPixel(i_X, currentY, m_Collidable.Bounds);

                if (pixelIdx < m_Collidable.Pixels.Length && pixelIdx >= 0)
                {
                    m_Collidable.Pixels[pixelIdx].A = 0;
                }

                currentY += destinateY < currentY ? -1 : 1; // += direction ?
            }
        }

    }
}
//public List<Point> GetIntersectionPoints(ICollidable2D i_Source, Rectangle i_Intersection)
//{
//    Color[] sourcePixels = getPixelsData(i_Source); // TODO: this method is a helper of sprite. got nothing to do with sprite
//    List<Point> intersections = new List<Point>();

//    for (int y = i_Intersection.Top; y < i_Intersection.Bottom; y++)
//    {
//        for (int x = i_Intersection.Left; x < i_Intersection.Right; x++)
//        {
//            if (!isTransparent(m_Collidable.Pixels, x, y, m_Collidable.Bounds) &&
//                !isTransparent(sourcePixels, x, y, i_Source.Bounds))
//            {
//                intersections.Add(new Point(x, y));
//            }
//        }
//    }

//    return intersections;
//}