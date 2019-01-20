using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class ScoreManager : TextComponent
    {
        private const string k_AssetName = @"Fonts\Comic Sans MS";
        private readonly string r_Name;

        Dictionary<Type, int> m_ScoreTable;
        public int Score { get; private set; } = 0;

        public string Name
        {
            get { return r_Name; }
        }

        public ScoreManager(string i_Name, Game i_Game, GameScreen i_Screen) : base(k_AssetName, i_Game, i_Screen)
        {
            r_Name = i_Name;
        }

        public ScoreManager(string i_AssetName , string i_Name, Game i_Game, GameScreen i_Screen) 
            : base(i_AssetName, i_Game, i_Screen)
        {
            r_Name = i_Name;
        }

        public override void Initialize()
        {
            base.Initialize();
            initScoreTable();
            Position = new Vector2(5, 3);//some spacing
        }

        private void initScoreTable()
        {
            int enemyBaseScore = (this.Game.Services.GetService(typeof(IGameSettings)) as SpaceInvadersSettings).BaseEnemyScorePoints;
            m_ScoreTable = new Dictionary<Type, int>
            {
                { typeof(Enemy), enemyBaseScore },
                { typeof(MotherShip), 850 },
                { typeof(Spaceship), -1100 },
                { typeof(Bullet), 0 },
                { typeof(Barrier), 0 }
            };
        }

        public void CollisionHandler(object i_Sender, EventArgs i_EventArgs)
        {
            Score += m_ScoreTable[i_Sender.GetType()];

            if (i_Sender is Enemy && (i_Sender as Enemy).TintColor != Color.LightYellow)
            {
                Score += (i_Sender as Enemy).TintColor == Color.LightBlue ? 30 : 150;
            }

            Score = MathHelper.Clamp(Score, 0, int.MaxValue);
        }

        public override string TextToString()
        {
            return $"{r_Name}: {Score}";
        }
    }
}
