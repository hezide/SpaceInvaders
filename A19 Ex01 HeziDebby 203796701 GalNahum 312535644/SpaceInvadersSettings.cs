﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class SpaceInvadersSettings : GameSettings
    {
        private readonly int k_RepeatingLevelDifficulty = 7;

        //Constants
        private readonly float r_InitialBarriersVelocity = 45f;
        private readonly float r_BarriersVelocityPercantageFactor = 7f;
        private readonly int r_InitialNumOfEnemyCols = 9;
        private readonly int r_InitialBaseEnemyScorePoints = 110;
        private readonly int r_EnemyExtraScorePointsPerLevel = 120;
        private readonly int r_InitialBulletAmountForEnemies = 1;
        private readonly int r_ExtraBulletAmountForEnemiesPerLevel = 2;

        //Props For Objects
        public float BarriersVelocity { get; private set; }
        public int NumOfEnemyColumns { get; private set; }
        public int BaseEnemyScorePoints { get; private set; }
        public int EnemyBulletsAmount { get; private set; }
        public int BulletAmountForEnemies { get; private set; }

        public int NumberOfPlayers { get; set; } = 2;

        public SpaceInvadersSettings(Game i_Game) : base(i_Game){}

        protected override void setLevel(int i_Level)
        {
            base.setLevel(i_Level);
            setBarrierVelocity();
            setNumOfEnemyColumns();
            setBaseEnemyHitScore();
            setBulletAmountForEnemies();
        }
        
        private void setBarrierVelocity()
        {
            if (CurrentLevel % k_RepeatingLevelDifficulty == 0)
            {
                BarriersVelocity = 0;
            }
            else if (CurrentLevel % k_RepeatingLevelDifficulty == 1)
            {
                BarriersVelocity = r_InitialBarriersVelocity;
            }
            else
            {
                BarriersVelocity = BarriersVelocity * (100f - r_BarriersVelocityPercantageFactor) / 100f;
            }
        }

        private void setNumOfEnemyColumns()
        {
            NumOfEnemyColumns = CurrentLevel % k_RepeatingLevelDifficulty + r_InitialNumOfEnemyCols;
        }

        private void setBaseEnemyHitScore()
        {
            if (CurrentLevel % k_RepeatingLevelDifficulty == 0)
            {
                BaseEnemyScorePoints = r_InitialBaseEnemyScorePoints;
            }
            else
            {
                BaseEnemyScorePoints += r_EnemyExtraScorePointsPerLevel;
            }

        }

        private void setBulletAmountForEnemies()
        {
            if (CurrentLevel % k_RepeatingLevelDifficulty == 0)
            {
                BulletAmountForEnemies = r_InitialBulletAmountForEnemies;
            }
            else
            {
                BulletAmountForEnemies += r_ExtraBulletAmountForEnemiesPerLevel;
            }
        }
    }
}
