﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Algo.Structures.List;
using System.Linq;
using Algo.Functional.Result;

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
            var manualList = new SLList<int>(1, 2, 3, 4, 5);
            //using IEnumerable
            var list = new SLList<int>(MakeInts());

            Console.WriteLine();

        }

        [Test]
        public void PopTest()
        {
            var list = new SLList<int>(1,2,3,4,5);
            Assert.AreEqual(5, list.Pop().UnwrapSuccess());
            Assert.AreEqual(4, list.Length); 

        }


    }
}
