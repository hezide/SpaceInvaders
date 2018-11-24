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
            PlayerSpaceship spaceship = new PlayerSpaceship(i_graphics);
            spaceship.ShooterType = Utilities.eShooterType.PlayerSpaceship;
            return spaceship;
        }

        public static Enemy CreateEnemy(GraphicsDevice i_graphics, Utilities.eDrawableType eEnemyType,int i_randomSeed)
        {
            Enemy enemy = new Enemy(i_graphics, i_randomSeed);;
            enemy.ShooterType = Utilities.eShooterType.Enemy;

            switch (eEnemyType)
            {
                case Utilities.eDrawableType.PinkEnemy:
                    enemy.Type = Utilities.eDrawableType.PinkEnemy;
                    enemy.Color = Color.Pink;
                    break;
                case Utilities.eDrawableType.BlueEnemy:
                    enemy.Type = Utilities.eDrawableType.BlueEnemy;
                    enemy.Color = Color.Blue;
                    break;
                case Utilities.eDrawableType.YellowEnemy:
                    enemy.Type = Utilities.eDrawableType.YellowEnemy;
                    enemy.Color = Color.Yellow;
                    break;
                default:
                    enemy = null;
                    break;
            }

            return enemy;
        }

        public static Bullet CreateBullet(GraphicsDevice i_graphicsDevice,Utilities.eShooterType i_shooterType)
        {
            Bullet bullet = new Bullet(i_graphicsDevice);
            if(i_shooterType == Utilities.eShooterType.PlayerSpaceship)
                bullet.Color = Color.Red;
            else if(i_shooterType == Utilities.eShooterType.Enemy)
                bullet.Color = Color.Blue;

            return bullet;
        }

        public static MotherSpaceship CreateMotherSpaceship(GraphicsDevice i_graphicsDevice)
        {
            return new MotherSpaceship(i_graphicsDevice);
        }
    }
}
