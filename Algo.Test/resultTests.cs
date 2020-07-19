using System;
using System.Collections.Generic;
using Algo.Functional.Result;
using static Algo.Functional.Result.ResultExtensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Algo.Test
{
    public class ResultTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Examples()
        {
            //creating results
            var hi = Success<string,Exception>("hi");

            //binding results
            var err = hi
                .FlatMap(s => Success<string, Exception>("yay! no errors!"))
                .FlatMap((s) => Error<string, Exception>(new Exception("oops! all errors!")))
                .FlatMap(s => Success<int, Exception>(1337)); //never runs because previous bind returns error 

            //matching with callbacks
            err.OnSuccess(s => Console.WriteLine($"This will never happen! {s}"))
                .OnError(e => Console.WriteLine($"This will! {e}"));

            //same as above without callbacks
            if (!err.IsError)
            {
                var s = err.UnwrapSuccess(); //will throw if error
                Console.WriteLine($"This will never happen! {s}");
            }
            else
            {
                var e = err.UnwrapError(); //will throw if success
                Console.WriteLine($"this will! {e}");
            }

            //with no return value
            var single = Success<Exception>();

            //returns 2
            err.UnwrapSuccessOr(2);
        }
    }
}