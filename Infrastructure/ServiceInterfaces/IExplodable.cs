using Infrastructure.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceInterfaces
{
    public interface IExplodable
    {
        int ExplosionRange { get; }
        //void Explode(int i_XToExplode, int i_YToExplode, Sprite i_SpriteToExplode);
    }
}
