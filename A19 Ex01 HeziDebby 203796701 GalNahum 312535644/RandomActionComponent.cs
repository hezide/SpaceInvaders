using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class RandomActionComponent
    {
        private TimeSpan m_RandomSpanTime;
        private Random m_Random;
        public Action RandomTimeAchieved;
        private int m_SecondsToSpawn;
        private int m_MinTimeSpan = 1; // default value
        private int m_MaxTimeSpan = 20; // default value
        public bool Enabled { get; set; } = true;
        public float m_timePassedSinceLastRandomAction = 0;
        public RandomActionComponent()
        {
            m_Random = new Random();
            randomize();
        }

        public RandomActionComponent(int i_MinTimeSpan, int i_MaxTimeSpan, int i_Seed)
        {
            m_Random = new Random(i_Seed);

            initialize(i_MinTimeSpan, i_MaxTimeSpan);
        }

        public RandomActionComponent(int i_MinTimeSpan, int i_MaxTimeSpan)
        {
            m_Random = new Random();

            initialize(i_MinTimeSpan, i_MaxTimeSpan);
        }

        private void initialize(int i_MinTimeSpan, int i_MaxTimeSpan)
        {
            m_MinTimeSpan = i_MinTimeSpan;
            m_MaxTimeSpan = i_MaxTimeSpan;
            randomize();
        }

        private void randomize()
        {
            m_SecondsToSpawn = m_Random.Next(m_MinTimeSpan, m_MaxTimeSpan);
            m_RandomSpanTime = TimeSpan.FromSeconds(m_SecondsToSpawn);
        }

        public void Update(GameTime i_GameTime)
        {
            if (Enabled)
            {
                m_timePassedSinceLastRandomAction += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                if (m_timePassedSinceLastRandomAction > m_RandomSpanTime.TotalSeconds)
                {
                    RandomTimeAchieved.Invoke();
                    randomize();
                    m_timePassedSinceLastRandomAction = 0;
                }
            }
        }
    }
}
