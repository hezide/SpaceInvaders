using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces
{
    interface IMoveable
    {
        Vector2                 CurrentPosition { get; set; }
        int                     Velocity { get; set; }
        Texture2D               Texture { get; }
        Utilities.eDirection    CurrentDirection { get; set; }

        void Init();
        void Update(GameTime gameTime);
    }
}
