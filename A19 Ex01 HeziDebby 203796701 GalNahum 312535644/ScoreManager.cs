using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class ScoreManager : TextComponent
    {
        private const string k_AssetName = @"Fonts\Comic Sans MS";
        private readonly string r_Name;

        Dictionary<Type, int> m_ScoreTable;

        private int m_Score = 0;
        public int Score
        {
            get
            {
                return m_Score;
            }
        }

        public string Name
        {
            get { return r_Name; }
        }

        public ScoreManager(string i_Name, Game i_Game) : base(k_AssetName, i_Game)
        {
            r_Name = i_Name;
        }

        public ScoreManager(string i_AssetName , string i_Name, Game i_Game) 
            : base(i_AssetName, i_Game)
        {
            r_Name = i_Name;
        }

        public override void Initialize()
        {
            base.Initialize();
            initScoreTable();
        }
        // TODO: think how to make general
        private void initScoreTable()
        {
            m_ScoreTable = new Dictionary<Type, int>
            {
                { typeof(Enemy), 110 },
                { typeof(MotherShip), 850 },
                { typeof(PlayerSpaceship), -1100 },
                { typeof(Bullet), 0 },
                { typeof(Barrier), 0 }
            };
        }

        public void CollisionHandler(object i_Sender, EventArgs i_EventArgs)
        {
            m_Score += m_ScoreTable[i_Sender.GetType()];

            if (i_Sender is Enemy && (i_Sender as Enemy).TintColor != Color.LightYellow)
            {
                m_Score += (i_Sender as Enemy).TintColor == Color.LightBlue ? 30 : 150;
            }

            m_Score = MathHelper.Clamp(m_Score, 0, int.MaxValue);
        }

        public override string TextToString()
        {
            return $"{r_Name}: {Score}";
        }
    }
}
