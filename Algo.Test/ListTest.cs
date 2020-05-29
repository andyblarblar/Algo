using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using NUnit.Framework;
using List = Algo.Structures.List.List;

namespace Algo.Test
{
    class ListTest
    {
        [Test]
        public void Example()
        {
            IEnumerable<int> MakeInts()
            {
                var i = 0;
                while (i < 100)
                {
                    yield return i++;
                }

            }

            //list construction
            var manualList = List.MakeList(1, 2, 3, 4, 5);
            //using IEnumerable
            var list = List.MakeList(MakeInts());

            //list deconstruction with the ~ operator
            var (head, tail) = ~list;

            Console.WriteLine(list[10]);

        }

        [Test]
        public void InsertTests()
        {
            var xs = List.MakeList(1, 2, 3, 4);
            xs.Insert(1, 10);
            Assert.True(xs[0] == 1);
            Assert.True(xs[1] == 10);
            Assert.True(xs[2] == 2);
        }

        [Test]
        public void DeleteTests()
        {
            var xs = List.MakeList(1, 2, 3, 4);
            xs.Delete(1);
            Assert.True(xs[0] == 1);
            Assert.True(xs[1] == 3);
            Assert.True(xs[2] == 4);
        }

    }
}
