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
            spaceship.TypeOfGameObject = Utilities.eGameObjectType.Spaceship;
            return spaceship;
        }

        public static Enemy CreateEnemy(GraphicsDevice i_graphics, Utilities.eGameObjectType eEnemyType,int i_randomSeed)
        {
            Enemy enemy = new Enemy(i_graphics, i_randomSeed);;

            switch (eEnemyType)
            {
                case Utilities.eGameObjectType.PinkEnemy:
                    enemy.TypeOfGameObject = Utilities.eGameObjectType.PinkEnemy;
                    enemy.Color = Color.Pink;
                    break;
                case Utilities.eGameObjectType.BlueEnemy:
                    enemy.TypeOfGameObject = Utilities.eGameObjectType.BlueEnemy;
                    enemy.Color = Color.Blue;
                    break;
                case Utilities.eGameObjectType.YellowEnemy:
                    enemy.TypeOfGameObject = Utilities.eGameObjectType.YellowEnemy;
                    enemy.Color = Color.Yellow;
                    break;
                default:
                    enemy = null;
                    break;
            }

            return enemy;
        }

        public static Bullet CreateBullet(GraphicsDevice i_graphicsDevice, Utilities.eGameObjectType i_shooterType)
        {
            Bullet bullet = new Bullet(i_graphicsDevice);

            if (i_shooterType == Utilities.eGameObjectType.Spaceship)
            {
                bullet.Color = Color.Red;
                bullet.CurrentDirection = Utilities.eDirection.Up;
            }
            else if (i_shooterType == Utilities.eGameObjectType.PinkEnemy || i_shooterType == Utilities.eGameObjectType.BlueEnemy || i_shooterType == Utilities.eGameObjectType.YellowEnemy)
            {
                bullet.Color = Color.Blue;
                bullet.CurrentDirection = Utilities.eDirection.Down;
            }

            return bullet;
        }

        public static MotherSpaceship CreateMotherSpaceship(GraphicsDevice i_graphicsDevice)
        {
            return new MotherSpaceship(i_graphicsDevice);
        }
    }
}
