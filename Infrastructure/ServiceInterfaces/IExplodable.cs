using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceInterfaces
{
    public interface IExplodable : ICollidable2D
    {
        float ExplosionRange { get; }
    }
}
