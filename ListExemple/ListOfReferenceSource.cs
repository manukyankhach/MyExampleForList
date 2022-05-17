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

        //public ListOfReferenceSource(IEnumerable<T> collection)
        //{
        //    if (collection == null)
        //        throw new Exception();

        //    ICollection<T> c = collection as ICollection<T>;
        //    if (c != null)
        //    {
        //        int count = c.Count;
        //        if (count == 0)
        //        {
        //            _items = _emptyArray;
        //        }
        //        else
        //        {
        //            _items = new T[count];
        //            c.CopyTo(_items, 0);
        //            _size = count;
        //        }
        //    }
        //    else
        //    {
        //        _size = 0;
        //        _items = _emptyArray;

        //        using (IEnumerator<T> en = collection.GetEnumerator())
        //        {
        //            while (en.MoveNext())
        //            {
        //                Add(en.Current);
        //            }
        //        }
        //    }
        //}

        // Gets and sets the capacity of this list.  The capacity is the size of
        // the internal array used to hold items.  When set, the internal 
        // array of the list is reallocated to the given capacity.
        // 
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

        // Read-only property describing how many elements are in the List.
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
                // Following trick can reduce the range check by one
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
                // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
                // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
                if ((uint)newCapacity > 0X7FEFFFFF) newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        public ListOfReferenceSource<T> GetRange(int index, int count)
        {
            if (index < 0)
            {
                
            }

            if (count < 0)
            {
               
            }

            if (_size - index < count)
            {
               
            }

            ListOfReferenceSource<T> list = new ListOfReferenceSource<T>(count);
            Array.Copy(_items, index, list._items, 0, count);
            list._size = count;
            return list;
        }


        public int IndexOf(T item, int index)
        {
            if (index > _size)
                throw new Exception();
            return Array.IndexOf(_items, item, index, _size - index);
        }


        public int LastIndexOf(T item)
        {
            if (_size == 0)
            {  // Special case for empty list
                return -1;
            }
            else
            {
                return LastIndexOf(item, _size - 1, _size);
            }
        }

        public int LastIndexOf(T item, int index, int count)
        {
            if ((Count != 0) && (index < 0))
            {
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if ((Count != 0) && (count < 0))
            {
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (_size == 0)
            {  // Special case for empty list
                return -1;
            }

            if (index >= _size)
            {
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
            }

            if (count > index + 1)
            {
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
            }

            return Array.LastIndexOf(_items, item, index, count);
        }

        //// Removes the element at the given index. The size of the list is
        //// decreased by one.
        //// 
        //public bool Remove(T item)
        //{
        //    int index = IndexOf(item);
        //    if (index >= 0)
        //    {
        //        RemoveAt(index);
        //        return true;
        //    }

        //    return false;
        //}

        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                //ThrowHelper.ThrowArgumentOutOfRangeException();
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
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (count < 0)
            {
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (_size - index < count)
                //ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);

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
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (count < 0)
            {
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (_size - index < count)
                //ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
            
            Array.Reverse(_items, index, count);
            _version++;
        }
    }
}

