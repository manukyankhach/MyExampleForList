using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListExemple
{
    public class ListOfReferenceSource<T>
    {
        private const int _defaultCapacity = 4;

        private T[] _items;
        private int _size;
        private int _version;

        static readonly T[] _emptyArray = new T[0];
        public ListOfReferenceSource()
        {
            _items = _emptyArray;
        }

        public ListOfReferenceSource(int capacity)
        {
            if (capacity < 0)
                throw new Exception();
            if (capacity == 0)
                _items = _emptyArray;
            else
                _items = new T[capacity];
        }

        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        public T this[int index]
        {
            get
            {
                return _items[index];
            }

            set
            {
                if ((uint)index >= (uint)_size)
                {
                    _items[index] = value;
                    _version++;
                }
            }
        }
        
        public void Add(T item)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size++] = item;
            _version++;
        }

        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > 0X7FEFFFFF) newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        public int IndexOf(T item, int index)
        {
            if (index > _size)
                throw new Exception();
            return Array.IndexOf(_items, item, index, _size - index);
        }

        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                throw new Exception();
            }
            
            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _items[_size] = default(T);
            _version++;
        }

        public void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
                throw new Exception();
            }

            if (count < 0)
            {
                throw new Exception();
            }

            if (_size - index < count)
                throw new Exception();

            if (count > 0)
            {
                int i = _size;
                _size -= count;
                if (index < _size)
                {
                    Array.Copy(_items, index + count, _items, index, _size - index);
                }
                Array.Clear(_items, _size, count);
                _version++;
            }
        }

        public void Reverse(int index, int count)
        {
            if (index < 0)
            {
                throw new Exception();
            }

            if (count < 0)
            {
                throw new Exception();
            }

            if (_size - index < count)
                throw new Exception();

            Array.Reverse(_items, index, count);
            _version++;
        }
    }
}

