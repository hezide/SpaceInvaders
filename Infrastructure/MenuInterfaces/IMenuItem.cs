using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Infrastructure
{
    public interface IMenuItem
    {
        void SetActive(bool i_Active);
        void SetPosition(Vector2 i_ItemPosition);
        void Initialize();
        Rectangle Bounds();
    }
}
