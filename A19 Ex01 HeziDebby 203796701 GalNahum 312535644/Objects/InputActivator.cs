using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{ // TODO: should be in infrastructure ?
    public class InputActivator
    {
        IInputManager m_InputManager;
        public List<Keys> ActivationKeysList { private get; set; }
        public bool ActivateByMouse { private get; set; }

        public void Initialize()
        {

        }
    }
}
