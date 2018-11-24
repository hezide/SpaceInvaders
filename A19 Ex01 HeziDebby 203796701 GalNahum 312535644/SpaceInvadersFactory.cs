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
        public static PlayerSpaceship CreatePlayerSpaceship(GraphicsDevice i_graphics)
        {
            return new PlayerSpaceship(i_graphics);
        }

        public static Enemy CreateEnemy(GraphicsDevice i_graphics, Utilities.eDrawableType eEnemyType)
        {
            Enemy enemy;

            switch (eEnemyType)
            {
                case Utilities.eDrawableType.PinkEnemy:
                    enemy = new Enemy(i_graphics);
                    enemy.Type = Utilities.eDrawableType.PinkEnemy;
                    enemy.Color = Color.Pink;
                    break;
                case Utilities.eDrawableType.BlueEnemy:
                    enemy = new Enemy(i_graphics);
                    enemy.Type = Utilities.eDrawableType.BlueEnemy;
                    enemy.Color = Color.Blue;
                    break;
                case Utilities.eDrawableType.YellowEnemy:
                    enemy = new Enemy(i_graphics);
                    enemy.Type = Utilities.eDrawableType.YellowEnemy;
                    enemy.Color = Color.Yellow;
                    break;
                default:
                    enemy = null;
                    break;
            }

            return enemy;
        }

        public static Bullet CreateBullet(GraphicsDevice i_graphicsDevice)
        {
            return new Bullet(i_graphicsDevice);
        }

        public static MotherSpaceship CreateMotherSpaceship(GraphicsDevice i_graphicsDevice)
        {
            return new MotherSpaceship(i_graphicsDevice);
        }
    }
}
