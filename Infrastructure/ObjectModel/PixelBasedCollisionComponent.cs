using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel
{
    public class PixelBasedCollisionComponent
    {
        private ICllidableByPixels m_Collidable;

        public PixelBasedCollisionComponent(ICllidableByPixels i_Collidable)
        {
            m_Collidable = i_Collidable;
        }

        public bool PixelsIntersects(ICollidable2D i_Source, Rectangle i_Intersection)
        {
            m_Collidable.IntersectionPoints = GetIntersectionPoints(i_Source, i_Intersection);

            return m_Collidable.IntersectionPoints.Count > 0;
        }

        public List<Point> GetIntersectionPoints(ICollidable2D i_Source, Rectangle i_Intersection)
        {
            Color[] sourcePixels = getPixelsData(i_Source); // TODO: this method is a helper of sprite. got nothing to do with sprite
            List<Point> intersections = new List<Point>();

            for (int y = i_Intersection.Top; y < i_Intersection.Bottom; y++)
            {
                for (int x = i_Intersection.Left; x < i_Intersection.Right; x++)
                {
                    if (m_Collidable.Pixels[getPixel(x, y, m_Collidable.Bounds)].A != 0 &&
                         sourcePixels[getPixel(x, y, i_Source.Bounds)].A != 0)
                    {
                        intersections.Add(new Point(x, y));
                    }
                }
            }

            return intersections;
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
            // int pixelIndex = getPixel(i_X, i_Y, this.Bounds);

            //if (pixelIndex >= 0 && pixelIndex < Pixels.Length) // TODO: throws exception out of bounds
            //{
            m_Collidable.Pixels[i_PixelIdx].A = 0;
            //}
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

        public void TransperantPixelsInRange(Point i_Point, int i_ExplosionRange)
        {
            int direction = i_ExplosionRange > 0 ? 1 : -1;
            int destinateY = i_Point.Y + i_ExplosionRange;
            int currentY = i_Point.Y;

            while (currentY != destinateY)
            {
                int pixelIdx = getPixel(i_Point.X, currentY, m_Collidable.Bounds);

                if (pixelIdx < m_Collidable.Pixels.Length && pixelIdx >= 0)
                {
                    transparentPixel(pixelIdx);
                }

                currentY += destinateY < currentY ? -1 : 1;
            }
        }

    }
}
