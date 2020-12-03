using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algo.Util.Calc;
using NUnit.Framework;

namespace Algo.Test
{
    class CalcTests
    {
        [Test]
        public void DifferentiationWorks()
        {
            //Ensure decimal overload works
            static decimal Fun(decimal x) => (decimal) Math.Pow((double) x, 2);
            var diff = Derivative.Diff(Fun);

            Assert.AreEqual(20,diff(10));

            //Ensure double overload works
            static double Fun2(double x) => Math.Pow(x,2);
            var diff2 = Derivative.Diff(Fun2);

            Assert.AreEqual(300,Math.Round(diff2(10)));

            //Ensure small numbers work
            Assert.AreEqual(Math.Round(Math.Cos(30),5), Math.Round(Derivative.Diff(Math.Sin)(30),5));
        } 
        
        [Test]
        public void SecondDifferentiationWorks()
        {
            //Ensure decimal overload works
            static decimal Fun(decimal x) => (decimal) Math.Pow((double) x, 2);
            var diff = Derivative.Diff2nd(Fun);

            Assert.AreEqual(2,diff(10));

            //Ensure double overload works
            static double Fun2(double x) => Math.Pow(x, 3);
            var diff2 = Derivative.Diff2nd(Fun2);

            Assert.AreEqual(300,Math.Round(diff2(10)));

            //Ensure small numbers work
            Assert.AreEqual(Math.Round(-Math.Sin(30),5), Math.Round(Derivative.Diff2nd(Math.Sin)(30),5));
        }

        [Test]
        public void Example()
        {
            static double Fun2(double x) => Math.Pow(x, 3);
            var diff2 = Derivative.Diff2nd(Fun2);

            foreach (var i in Enumerable.Range(0,100))
            {
                Console.WriteLine(" "+ diff2(i));
            }

        }

        [Test]
        public void BigRationalTest()
        {
            var br = new BigRational(1, 3);
            var br2 = new BigRational(-124, 4);

            Console.WriteLine(br * br2);
            Console.WriteLine(br / br2);
            Console.WriteLine(br + br2);
            Console.WriteLine(br - br2);
            Console.WriteLine(-(br - br2));
            Console.WriteLine((br - br2).ToDouble());
            Console.WriteLine(BigRational.NthRoot(br,3.5));
            Console.WriteLine(new BigRational(1d/3d,80));
        }
        
        [Test]
        public void BigRationalSignsTest()
        {
            var br2 = new BigRational(-124, 4);

            //Check for negative denominators. Because BigRationals are a readonly struct, this tests for all chances of being negative in the denom.
            Assert.AreEqual(br2,new BigRational(124,-4));
        }
        
        [Test]//TODO test rest of rationals
        public void BigRationalComparisonsWork()
        {
            var br2 = new BigRational(124, 4);
            var br = new BigRational(1, 4);

            Assert.Greater(br2,br);
            Assert.Greater(br,-br2);
        }

        [Test]
        public void BigRationalFactorial()
        {
            var zero = BigRational.Zero;
            var one = BigRational.One;
            var five = BigRational.One * 5;

            Assert.AreEqual(BigRational.Factorial(zero).ToDouble(),1); 
            Assert.AreEqual(BigRational.Factorial(one).ToDouble(),1); 
            Assert.AreEqual(BigRational.Factorial(five).ToDouble(),120);
        }
        
        [Test]
        public void BigRationalZerosDontBreakThings()
        {
            var zero = BigRational.Zero;
            var one = BigRational.One;
            var five = BigRational.One * 5;

            zero.Simplify();
            Assert.AreEqual(zero + one,one);
            Assert.AreEqual(zero + five,five);
            Assert.AreEqual(zero * five,zero);
            Assert.AreEqual(zero / five,zero);
            Assert.Throws( typeof(DivideByZeroException),() =>
            {
                var bigRational = five / zero;
            });
        }

        [Test]
        public void BigRationalTrigWorks()
        {
            var third = new BigRational(1 ,3);

            Assert.AreEqual(Math.Sin(1d/3d), BigRational.Sin(third).ToDouble());
            Assert.AreEqual(Math.Cos(1d/3d), BigRational.Cos(third).ToDouble());
            Assert.AreEqual(Math.Exp(1d/3d), BigRational.Exp(third).ToDouble());
            Assert.AreEqual(Math.Tan(1d/3d), BigRational.Tan(third).ToDouble());
            Assert.AreEqual(1/Math.Sin(1d/3d), BigRational.Csc(third).ToDouble());
            Assert.AreEqual(1/Math.Cos(1d/3d), BigRational.Sec(third).ToDouble());
            Assert.AreEqual(1/Math.Tan(1d/3d), BigRational.Cot(third).ToDouble());
            Assert.AreEqual(Math.Asin(1d/3d), BigRational.ArcSin(third).ToDouble());
            Assert.AreEqual(Math.Atan(1d/3d), BigRational.ArcTan(third).ToDouble());
            //Assert.AreEqual(Math.Acos(1d/3d), BigRational.ArcCos(third).ToDouble()); pi is irrational lol

        }
    }
}
