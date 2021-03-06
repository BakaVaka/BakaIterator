namespace BakaIterator
{
    using System;
    using System.Collections.Generic;

    public class BakaIterator<T> 
    {
        public T Current => _items[_index];
        public bool HasNext => _index + 1 < _items.Count;
        public int Position => _index;
        public int Count => _items.Count;
        public BakaIterator(IList<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (items.Count == 0)
            {
                throw new ArgumentException("Items must contain at least 1 element", nameof(items));
            }
            _items = items;
            _index = 0;
        }

        private readonly IList<T> _items;
        private int _index;
        public BakaIterator<T> MoveForward(int steps = 1)
        {
            if (steps < 0)
            {
                throw new ArgumentException("Invalid steps count. Must be 0 or positiv integer");
            }
            if (_index + steps > _items.Count - 1)
            {
                throw new IndexOutOfRangeException("To many steps");
            }
            _index += steps;
            return this;
        }
        public BakaIterator<T> MoveBackwards(int steps = 1)
        {
            if (steps < 0)
            {
                throw new ArgumentException("Invalid steps count. Must be 0 or positiv integer");
            }
            if (_index - steps < 0)
            {
                throw new IndexOutOfRangeException("To many steps");
            }
            _index -= steps;
            return this;
        }
        public BakaIterator<T> Reset()
        {
            _index = 0;
            return this;
        }

        public BakaIterator<T> First(Func<T, bool> predicate, bool throwIfNotFound = true)
        {
            do
            {
                if (predicate(Current))//Мы уже получили этот элемент
                {
                    return this;
                }

                if (HasNext)
                {
                    MoveForward();
                }
            } while (HasNext);

            if (throwIfNotFound)
            {
                throw new Exception("Item not found");
            }
            return this;
        }
        //Для случая если нам нужен не просто первый, но первый который не текущий
        public BakaIterator<T> FirstNext(Func<T, bool> predicate, bool throwIfNotFound = true)
        {
            if (HasNext)
            {
                MoveForward();
            }
            else if(throwIfNotFound)
            {
                throw new Exception("End reached");
            }
            return First((x) => predicate(x), throwIfNotFound);
            
        }

        public BakaIterator<T> ThrowIf(Func<T, bool> predicate)
        {
            if (predicate(Current))
            {
                throw new Exception($"Item {Current} is ");
            }
            return this;
        }
        public BakaIterator<T> ThrowIfNot(Func<T, bool> predicate)
        {
            if (!predicate(Current))
            {
                throw new Exception("Current item is not");
            }
            return this;
        }
        public BakaIterator<T> ForEach(Action<T> action)
        {
            foreach (var i in _items)
            {
                action(i);
            }
            return this;
        }
    }
}
