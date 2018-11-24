using Microsoft.Xna.Framework;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class WallHitResponse
    {
        public Utilities.eDirection m_hitDirection;
        public bool m_hit;

        public WallHitResponse(bool i_isWallHit, Utilities.eDirection i_direction)
        {
            m_hit = i_isWallHit;
            m_hitDirection = i_direction;
        }
    }
}