using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces
{
    interface IDestryoable
    {
        int Souls { get; }
        Rectangle Rectangle { get; } // maybe in gameObject - have duplications in defining

        void GetHit();
        bool IsDead();
    }
}
