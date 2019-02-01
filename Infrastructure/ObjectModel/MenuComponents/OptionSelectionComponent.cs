using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class OptionSelectionComponent<T> : IOptionSelectionable<T> ,ICollection<T>
     {
        private LinkedList<T> m_Items = new LinkedList<T>();
        public T ActiveItem { get; private set; }

        public int Count => m_Items.Count;

        public bool IsReadOnly => false;

        public void SetActiveItem(T i_Item)
        {
            if(m_Items.Contains(i_Item))
            {
                activateItem(i_Item);
            }
        }

        private void activateItem(T i_Item)
        {
            //ActiveItem.SetActive(false);//previous Item
            ActiveItem = i_Item;
            //ActiveItem.SetActive(true);
        }

        public T MoveToNextOption()
        {
            LinkedListNode<T> nextMenuItem = m_Items.Find(ActiveItem).Next;
            if (nextMenuItem == null)
            {
                activateItem(m_Items.First.Value);
            }
            else
            {
                activateItem(nextMenuItem.Value);
            }
            return ActiveItem;
        }

        public T MoveToPrevOption()
        {
            LinkedListNode<T> prevMenuItem = m_Items.Find(ActiveItem).Previous;
            if (prevMenuItem == null)
            {
                activateItem(m_Items.Last.Value);
            }
            else
            {
                activateItem(prevMenuItem.Value);
            }
            return ActiveItem;
        }

        public void AddItem(T i_ItemToAdd,bool i_IsActive = false)
        {
            if(i_IsActive)
            {
                activateItem(i_ItemToAdd);
            }
            m_Items.AddLast(i_ItemToAdd);
        }
        
        public int GetCount()
        {
            return m_Items.Count;
        }

        public void Add(T item)
        {
            AddItem(item);
        }

        public void Clear()
        {
            m_Items.Clear();
        }

        public bool Contains(T item)
        {
            return m_Items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_Items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return m_Items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Items.GetEnumerator();
        }
    }
}
