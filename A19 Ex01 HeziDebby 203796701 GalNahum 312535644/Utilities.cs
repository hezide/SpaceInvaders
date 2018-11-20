using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class Utilities
    {
        public enum eDirection { Up, Down, Left, Right };
        public enum eDrawableType { PinkEnemy, BlueEnemy, YellowEnemy, Spaceship, MotherSpaceship, Bullet, Background}

        public static readonly int k_InitialHightMultiplier = 3;
        public static readonly float k_EnemyGapMultiplier = 0.6f;
        public static readonly int k_EnemyMatRows = 5;
        public static readonly int k_EnemyMatCols = 9;

        public static readonly int k_PinkEnemyFirstRow = 0;
        public static readonly int k_PinkEnemyLastRow = 0;

        public static readonly int k_BlueEnemyFirstRow = 1;
        public static readonly int k_BlueEnemyLastRow = 2;

        public static readonly int k_YellowEnemyFirstRow = 3;
        public static readonly int k_YellowEnemyLastRow = 4;

        public static readonly int k_SpaceshipSouls = 3;
        public static readonly int k_EnemySouls = 1;

        public static readonly int k_SpaceshipVelocity = 120;
        public static readonly int k_EnemyVelocity = 30;
        public static readonly int k_BulletVelocity = 155;

    }
}
