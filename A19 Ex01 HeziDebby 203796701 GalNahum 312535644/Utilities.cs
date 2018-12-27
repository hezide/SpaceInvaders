using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class Utilities // : Sprite
    {
        // $G$ DSN-001 (-2) Why these enum are nested in the Utitilties class?!
        public enum eDirection { Up, Down,  Left, Right , None };
        public enum eGameObjectType { PinkEnemy, BlueEnemy, YellowEnemy, Spaceship, MotherSpaceship, Bullet, Background }

        // $G$-DSN-001 (-3) aggregating all these consts in one class, instead of puting each of them in its relevant class, is bad practice.

        // $G$ XNA-999 (-10) Why static readonly and not const?!        
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
        public static readonly int k_MotherSpaceshipSouls = 1;
        public static readonly int k_BulletSouls = 1;

        public static readonly int k_SpaceshipVelocity = 120;
        public static readonly int k_EnemyVelocity = 120;
    //    public static readonly int k_BulletVelocity = 155;
        public static readonly int k_MotherSpaceshipVelocity = 110;

        public static readonly float SpeedIncreaseMultiplier = 1.04f;
        public static readonly float k_EnemyHitWallVelocityMultiplier = 1.08f;
        public static readonly int k_Ammo = 3;

        public static readonly int k_heartStartingLocationX = 600;
        public static readonly int k_heartStartingLocationY = 10;
        public static readonly int k_hitsToIncreaseVelocity = 4;

        // ******************************************** //
     //   public Utilities(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
       // {
        //}

        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
        public static float CalculateNewCoordinate(float i_oldCoord, eDirection i_currentDirection, float i_velocity, double i_elaspedSeconds)
        {
            //Direction left is -1 and right is +1, this makes the enemies jump to correct side
            float directionMultiplier;
            if (i_currentDirection == Utilities.eDirection.Down || i_currentDirection == Utilities.eDirection.Right)
                directionMultiplier = 1f;
            else
                directionMultiplier = -1f;
            return i_oldCoord + directionMultiplier * (i_velocity * (float)i_elaspedSeconds);
        }
        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
        public static bool IsEnemy(eGameObjectType i_typeOfGameObject)
        {
            bool isEnemy = false;

            if (i_typeOfGameObject == eGameObjectType.PinkEnemy || i_typeOfGameObject == eGameObjectType.BlueEnemy || i_typeOfGameObject == eGameObjectType.YellowEnemy)
            {
                isEnemy = true;
            }

            return isEnemy;
        }
        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
        public static eDirection ToggleDirection(eDirection i_Direction)
        {
            return i_Direction == eDirection.Right ? eDirection.Left : eDirection.Right;
        }
        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
        public static bool IsSameType(GameObject i_gameObject1, GameObject i_gameObject2)
        {
            bool typeIsTheSame = false;

            if (i_gameObject1.TypeOfGameObject == i_gameObject2.TypeOfGameObject)
            {
                typeIsTheSame = true;
            }
            else if (IsEnemy(i_gameObject1.TypeOfGameObject) && IsEnemy(i_gameObject2.TypeOfGameObject))
            {
                typeIsTheSame = true;
            }

            return typeIsTheSame;
        }
    }
}
