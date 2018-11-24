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
        private TimeSpan m_randomSpanTime;
        private TimeSpan m_prevRandomSpanTime;
        private Random m_random;
        public Action RandomTimeAchieved;
        private int m_secondsToSpawn;
        private int m_minTimeSpan;
        private int m_maxTimeSpan;

        public RandomActionComponent(int i_minTimeSpan, int i_maxTimeSpan, int i_seed)
        {
            m_random = new Random(i_seed);
            initialize(i_minTimeSpan, i_maxTimeSpan);

        }

        public RandomActionComponent(int i_minTimeSpan, int i_maxTimeSpan)
        {
            m_random = new Random();
            initialize(i_minTimeSpan, i_maxTimeSpan);
        }

        private void initialize(int i_minTimeSpan, int i_maxTimeSpan)
        {
            m_minTimeSpan = i_minTimeSpan;
            m_maxTimeSpan = i_maxTimeSpan;
            randomize();
        }

        private void randomize()
        {
            m_secondsToSpawn = m_random.Next(m_minTimeSpan, m_maxTimeSpan); // TODO: contans Gal, what?
            setRandomTimeSpan();
        }

        private void setRandomTimeSpan()
        {
            m_randomSpanTime = TimeSpan.FromSeconds(m_secondsToSpawn);
        }

        public void Update(GameTime i_gameTime)
        {
            if (i_gameTime.TotalGameTime - m_prevRandomSpanTime > m_randomSpanTime)
            {
                m_prevRandomSpanTime = i_gameTime.TotalGameTime;
                RandomTimeAchieved.Invoke();
                randomize();
            }
        }
    }
}
