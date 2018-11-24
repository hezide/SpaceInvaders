using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class RandomActionComponent
    {
        private TimeSpan m_randomSpanTime;
        private TimeSpan m_prevRandomSpanTime;
        private Random m_random;
        public Action RandomTimeAchived;
        private int m_seconds;
        int m_min;
        int m_max;

        public RandomActionComponent(int i_min, int i_max, int seed)
        {
            m_random = new Random(seed);
            initialize(i_min, i_max);

        }
        public RandomActionComponent(int i_min, int i_max)
        {
            m_random = new Random();
            initialize(i_min, i_max);
        }

        private void initialize(int i_min, int i_max)
        {
            m_min = i_min;
            m_max = i_max;
            randomize();
        }

        private void randomize()
        {
            m_seconds = m_random.Next(m_min, m_max); // TODO: contans Gal, what?
            setRandomTimeSpan();
        }

        public void Update(GameTime i_gameTime)
        {
            if (i_gameTime.TotalGameTime - m_prevRandomSpanTime > m_randomSpanTime)
            {
                m_prevRandomSpanTime = i_gameTime.TotalGameTime;
                RandomTimeAchived.Invoke();
                randomize();
            }
        }

        private void setRandomTimeSpan()
        {
            m_randomSpanTime = TimeSpan.FromSeconds(m_seconds);
        }

    }
}
