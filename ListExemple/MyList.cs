using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListExemple
{
    public class MyList<T>
    {
        private T[] _items;
        private int _size;
        private int _capacity = 4;

        public MyList()
        {
            _items = new T[_capacity];
        }
        public MyList(int capacity)
        {
            _items = new T[capacity];
        }
        public void Add(T item)
        {
            if (_items.Length > _size)
            {
                _items[_size] = item;
                _size++;
            }
            else
            {
                _capacity = _capacity * 2;
                Array.Resize<T>(ref _items, _capacity);
                _items[_size] = item;
            }
        }
        public T this[int i]
        {
            get { return _items[i]; }
            set { _items[i] = value; }
        }
    }
}
