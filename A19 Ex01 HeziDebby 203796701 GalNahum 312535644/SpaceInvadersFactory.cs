using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public static class SpaceInvadersFactory
    {
        public static PlayerSpaceship CreatePlayerSpaceship(Texture2D i_texture, SpriteBatch i_spriteBatch)
        {
            return new PlayerSpaceship(i_texture, i_spriteBatch, Color.White);
        }

        public static Enemy CreateEnemy(Texture2D i_texture, SpriteBatch i_spriteBatch, Utilities.eDrawableType i_drawableType)
        {
            Enemy enemy;

            switch (i_drawableType)
            {
                case Utilities.eDrawableType.PinkEnemy:
                    enemy = new Enemy(i_texture, i_spriteBatch, Color.Pink);
                    break;
                case Utilities.eDrawableType.BlueEnemy:
                    enemy = new Enemy(i_texture, i_spriteBatch, Color.Blue);
                    break;
                case Utilities.eDrawableType.YellowEnemy:
                    enemy = new Enemy(i_texture, i_spriteBatch, Color.Yellow);
                    break;
                default:
                    enemy = null;
                    break;
            }

            return enemy;
        }

        public static Bullet CreateBullet(Texture2D i_texture, SpriteBatch i_spriteBatch)
        {
            return new Bullet(i_texture, i_spriteBatch, Color.White);
        }
    }
}
