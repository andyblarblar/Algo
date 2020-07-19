using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BenchmarkDotNet.Attributes;
using Algo.Functional.Result;

namespace Algo.Benchmark
{
    [MemoryDiagnoser]
    public class ResultVsNoResult
    {

        [Benchmark]
        public Result<List<int>, InvalidOperationException> WrappedListWithResult()
        {
            var list = new List<int>{1,2,3,4,5,6,7,8,9,10};
            return new Result<List<int>,InvalidOperationException>(list);
        }

        [Benchmark]
        public List<int> ListWithoutResult()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        
        [Benchmark]
        public double DivideWithResultErrorHandling()
        {
            var res = divideByZeroWithRes(1.1, 0);

            return res.UnwrapSuccessOr(0);
        } 
        
        [Benchmark]
        public double DivideWithResultErrorHandlingAndCallbacks()
        {
            var res = divideByZeroWithRes(1.1, 0);

            res.FlatMap(r => divideByZeroWithRes(r, 2))
                .FlatMap(res1 => divideByZeroWithRes(res1, 2))
                .FlatMap(res2 => divideByZeroWithRes(res2, 2))
                .FlatMap(res3 => divideByZeroWithRes(res3, 2))
                .FlatMap(res4 => divideByZeroWithRes(res4, 2));

            return res.UnwrapSuccessOr(0);
        }
        
        [Benchmark]
        public double DivideWithResultErrorHandlingAndLocalCallbacks()
        {
            static Result<double, DivideByZeroException> DivideByZeroWithResLocal(double x, double y)
            {
                try
                {
                    return new Result<double, DivideByZeroException>(x / y);
                }
                catch (DivideByZeroException e)
                {
                    return new Result<double, DivideByZeroException>(e);
                }
            }

            var res = divideByZeroWithRes(1.1, 0);

            res.FlatMap(r => DivideByZeroWithResLocal(r, 2))
                .FlatMap(res1 => DivideByZeroWithResLocal(res1, 2))
                .FlatMap(res2 => DivideByZeroWithResLocal(res2, 2))
                .FlatMap(res3 => DivideByZeroWithResLocal(res3, 2))
                .FlatMap(res4 => DivideByZeroWithResLocal(res4, 2));

            return res.UnwrapSuccessOr(0);
        }


        [Benchmark] public double DivideWithoutResultErrorHandling()
        {
            try
            {
                return 1.1 / 0;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }

        }


        private Result<double, DivideByZeroException> divideByZeroWithRes(double x, double y)
        {
            try
            {
                return new Result<double, DivideByZeroException>(x / y);
            }
            catch (DivideByZeroException e)
            {
                return new Result<double, DivideByZeroException>(e);
            }
        }

    }
}
