using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces
{
    public interface IShooter
    {
        int Ammo { get; set; }
        List<Bullet> BulletsList { get; }
        void Shoot();
    }
}
