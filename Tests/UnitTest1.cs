namespace Tests
{
    using BakaIterator;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class UnitTest1
    {
        List<int> _data = new List<int>
        {
            1,2,3,4,5,6,7,8,9,9,8,7,6,5,4,3,2,1
        };

        [Fact]
        public void FirstNext()
        {
            BakaIterator<int> iterator = new BakaIterator<int>(_data);
            iterator.FirstNext(x => x == 1, false);
            Assert.True(iterator.Position != 0 && iterator.Current == 1, $"real position is {iterator.Position}, value {iterator.Current}");
        }

        [Fact]
        public void First()
        {
            BakaIterator<int> iterator = new BakaIterator<int>(_data);
            iterator.First(x => x == 2, false);
            Assert.True(iterator.Position == 1 && iterator.Current == 2, $"real position is {iterator.Position}, value {iterator.Current}");
        }

        [Fact]
        public void Foreach()
        {
            var data = _data.Select(x => x * 2);
            BakaIterator<int> iterator = new BakaIterator<int>(_data);
            List<int> result = new List<int>();
            iterator.ForEach(x => result.Add(x * 2));

            Assert.True(result.Count == data.Count());
            Assert.Equal(data, result);
        }
    }
}
