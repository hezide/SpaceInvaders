using Infrastructure.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceInterfaces
{
    public interface IPixelCollidable : ICollidable2D
    {
        PixelCollisionManager PixelCollisionManager { get; set; }
    }
}
