namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{

    public class Gun
    {
        int m_ammo;

        public Gun(int i_startingAmmo)
        {
            m_ammo = i_startingAmmo;
        }

        public bool Fire()
        {
            if (m_ammo == 0)
                return false;

            m_ammo--;
            return true;
        }

        public void Reload(int i_ammount)
        {
            m_ammo += 1;
        }
    }
}