using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IOptionSelectionable<T>
    {
        T MoveToNextOption();
        T MoveToPrevOption();
        void AddItem(T i_ItemToAdd,bool i_IsActive);
    }
}
