using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Util.Calc
{
    /// <summary>
    /// Struct representing an arbitrary sized rational number.
    /// </summary>
    /// <remarks>
    /// The sign of a BigRational is always reflected in its numerator. This is enforced at
    /// time of creation, and sense it cannot be mutated, is always true.
    /// BigRationals are not gaurrentied to be simplified, but will function properly
    /// even without simplification. 
    /// </remarks>
    public readonly struct BigRational : IEquatable<BigRational>, IComparable<BigRational>, IComparable
    {
        public BigInteger Num { get; }
        public BigInteger Denom { get; }

        public BigRational(double numerator, double denominator)
        {
            Num = new BigInteger(numerator);
            Denom = new BigInteger(denominator);

            //Swap sign to numerator
            if (denominator < 0)
            {
                Denom = -Denom;
                Num = -Num;
            }
        }

        /// <summary>
        /// Converts a non integer double into fractional form, accounting for variable decimal precision.
        /// </summary>
        /// <param name="decimalPrecision">The number of decimal places to take into account. Raises precision but lowers efficiency. Must be less than the exponent of <see cref="double.MaxValue"/>.</param>
        public BigRational(double value, int decimalPrecision)
        {
            var temp = new BigRational(value * Math.Pow(10, decimalPrecision), 1 * Math.Pow(10, decimalPrecision));
            this = temp.Simplify();
        }

        public BigRational(int numerator, int denominator)
        {
            Num = new BigInteger(numerator);
            Denom = new BigInteger(denominator);

            //Swap sign to numerator
            if (denominator < 0)
            {
                Denom = -Denom;
                Num = -Num;
            }
        }
        
        /// <summary>
        /// Sets up a rational with numerator and a denominator of 1.
        /// </summary>
        /// <param name="numerator"></param>
        public BigRational(long numerator)
        {
            Num = new BigInteger(numerator);
            Denom = BigInteger.One;
        }

        private BigRational(in BigInteger numerator, in BigInteger denominator)
        {
            Num = numerator;
            Denom = denominator;

            //Swap sign to numerator
            if (denominator < 0)
            {
                Denom = -Denom;
                Num = -Num;
            }
        }

        /// <summary>
        /// Simplifies the BigRational. 
        /// </summary>
        /// <returns>A new simplified BigRational</returns>
        public BigRational Simplify()
        {
            if (Denom == 1 || Num == 1 || IsZero())
            {
                return this;
            }

            var gcd = BigInteger.GreatestCommonDivisor(Num, Denom);
            var num = Num / gcd;
            var denom = Denom / gcd;
            return new BigRational(num, denom);
        }

        /// <summary>
        /// Returns the Least Common Denominator of the two BigRationals.
        /// </summary>
        public static BigInteger LeastCommonMultiple(in BigRational lhs, in BigRational rhs)
        {
            return BigInteger.Abs(lhs.Denom * rhs.Denom) / BigInteger.GreatestCommonDivisor(lhs.Denom, rhs.Denom);
        }

        public double ToDouble()
        {
            var temp = Simplify();
            return ((double)temp.Num / (double)temp.Denom);
        }

        /// <summary>
        /// Divides the numerator and denominator, flooring the result to the nearest int.
        /// </summary>
        public long ToLong()
        {
            var temp = Simplify();
            return (long) (temp.Num / temp.Denom);
        }
        
        /// <summary>
        /// Converts this BigRational into a floored BigInteger.
        /// </summary>
        public BigInteger ToBigInt()
        {
            var temp = Simplify();
            return (temp.Num / temp.Denom);
        }

        public decimal ToDecimal()
        {
            var temp = Simplify();
            return (decimal) temp.Num / (decimal) temp.Denom;
        }

        public bool IsZero() => Num == 0 || Denom == 0;

        public static readonly BigRational One = new BigRational(1); 
        public static readonly BigRational Ten = new BigRational(10); 
        public static readonly BigRational Zero = new BigRational(0, 0); 
        public static readonly BigRational NegOne = new BigRational(-1); 
        public static readonly BigRational Pi = new BigRational(355,113); //TODO find more accurate pi

        #region ops

        /// <summary>
        /// Computes Sine of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational Sin(in BigRational x, int cycles = 10)
        {
            var acc = Zero;

            for (var i = 1; i < cycles; i++)
            {
                var top = Pow(NegOne, i - 1) * Pow(x, 2 * i - 1);
                var bottom = Factorial(new BigRational(2 * i - 1));

                acc += (top / bottom);
            }

            return acc;
        }

        /// <summary>
        /// Computes Cosine of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational Cos(in BigRational x, int cycles = 11)
        {
            var acc = Zero;

            for (var i = 0; i < cycles; i++)
            {
                var top = Pow(NegOne, i) * Pow(x, 2 * i);
                var bottom = Factorial(new BigRational(2 * i));

                acc += (top / bottom);
            }

            return acc;
        }
        
        /// <summary>
        /// Computes Co-secant of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational Csc(in BigRational x, int cycles = 11)
        {
            return Sin(x,cycles).Flip();
        }
        
        /// <summary>
        /// Computes secant of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational Sec(in BigRational x, int cycles = 13)
        {
            return Cos(x,cycles).Flip();
        }
        
        /// <summary>
        /// Computes Co-tangent of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational Cot(in BigRational x, int cycles = 12)
        {
            return Tan(x,cycles).Flip();
        }
        
        /// <summary>
        /// Computes Tangent of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational Tan(in BigRational x, int cycles = 10)
        {
            //I ain't messing with Bernoulli numbers yet lol
            var sin = Sin(x,cycles);
            var cos = Cos(x,cycles);

            return sin/cos;
        }

        /// <summary>
        /// Computes ArcSine of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational ArcSin(in BigRational x, int cycles = 15)
        {
            var acc = Zero;

            for (var i = 0; i < cycles; i++)
            {
                var top = Factorial(2 * i);
                var bottom = Pow(4, i) * Pow(Factorial(i), 2) * (2 * i + 1);

                acc += (top / bottom)*Pow(x,2*i+1);
            }

            return acc;
        }

        /// <summary>
        /// Computes ArcCosine of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational ArcCos(in BigRational x, int cycles = 12)
        {
            return (Pi / 2) - ArcSin(x, cycles);
        }

        /// <summary>
        /// Computes ArcTangent of a BigRational.
        /// </summary>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational ArcTan(in BigRational x, int cycles = 15)
        {
            var acc = Zero;

            for (var i = 0; i < cycles; i++)
            {
                var top = Pow(NegOne, i);
                var bottom = 2 * i + 1;

                acc += (top / bottom) * Pow(x, 2 * i + 1);
            }

            return acc;
        }

        /// <summary>
        /// Computes e^x on a BigRational.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="cycles">Sets iterations of the taylor series. Large numbers may cause difficulties converting to other formats and dramatic increase computation time.</param>
        /// <returns></returns>
        public static BigRational Exp(in BigRational x, int cycles = 13)
        {
            var acc = Zero;

            for (var i = 0; i < cycles; i++)
            {
                var top = Pow(x, i);
                var bottom = Factorial(i);

                acc += (top / bottom);
            }

            return acc;
        }

        public static BigRational Factorial(in BigRational x)
        {
            if (x < Zero)
            {
                throw new ArgumentException("value must be >= 0");
            }

            var acc = One;

            for (var i = 1; i < x.ToDouble() + 1; i++)
            {
                acc = acc * i;
            }

            return acc;
        }

        /// <summary>
        /// Flips the numerator and denominator.
        /// </summary>
        public BigRational Flip()
        {
            return new (Denom, Num);
        }

        /// <summary>
        /// Raises BigRational to power exp.
        /// </summary>
        public static BigRational Pow(in BigRational n, int exp)
        {
            return new(BigInteger.Pow(n.Num, exp), BigInteger.Pow(n.Denom, exp));
        }

        /// <summary>
        /// Computes the log of a BigRational.
        /// </summary>
        public static double Logb(in BigRational n, double b)
        {
            var num = BigInteger.Log(n.Num, b);
            var den = BigInteger.Log(n.Denom, b);

            return num - den;
        }
        
        /// <summary>
        /// Computes the natural log of a BigRational.
        /// </summary>
        public static double Log(in BigRational n)
        {
            var num = BigInteger.Log(n.Num);
            var den = BigInteger.Log(n.Denom);

            return num - den;
        }

        /// <summary>
        /// Computes the square root of the BigRational.
        /// </summary>
        public static double Sqrt(in BigRational n)
        {
            return Math.Exp(BigInteger.Log(n.Num) / 2) / Math.Exp(BigInteger.Log(n.Denom) / 2);
        }
        
        /// <summary>
        /// Computes the nth root of the BigRational.
        /// </summary>
        public static double NthRoot(in BigRational n, double root)
        {
            return Math.Exp(BigInteger.Log(n.Num) / root) / Math.Exp(BigInteger.Log(n.Denom) / root);
        }

        public static BigRational Add(in BigRational first, in BigRational second)
        {
            //return if already the same
            if (first.Denom == second.Denom)
            {
                return new BigRational(first.Num + second.Num, first.Denom);
            }

            //Check for zeros
            if (first.IsZero())
            {
                return second;
            }
            else if (second.IsZero())
            {
                return first;
            }

            var lhs = first.Simplify();
            var rhs = second.Simplify();

            //simplify and check again
            if (lhs.Denom == rhs.Denom)
            {
                return new BigRational(lhs.Num + rhs.Num, rhs.Denom);
            }

            //GCD will always be an int, no loss of precision in division.
            var lcm = LeastCommonMultiple(lhs, rhs);

            //remember, the lcm can never be smaller than the larger number.The lcm is always an int, and a multiple. This means we cannot lose precision by dividing here.
            var newLhsNum = lhs.Denom == lcm ? lhs.Num : lhs.Num * (lcm / lhs.Denom);
            var newRhsNum = rhs.Denom == lcm ? rhs.Num : rhs.Num * (lcm / rhs.Denom);

            return new BigRational(newLhsNum + newRhsNum, lcm);
        }

        public static BigRational Subtract(in BigRational first, in BigRational second)
        {
            //return if already the same
            if (first.Denom == second.Denom)
            {
                return new BigRational(first.Num - second.Num, first.Denom);
            }

            //Check for zeros
            if (first.IsZero())
            {
                return second;
            }
            else if (second.IsZero())
            {
                return first;
            }

            var lhs = first.Simplify();
            var rhs = second.Simplify();

            //simplify and check again
            if (lhs.Denom == rhs.Denom)
            {
                return new BigRational(lhs.Num - rhs.Num, rhs.Denom);
            }

            //GCD will always be an int, no loss of precision in division.
            var lcm = LeastCommonMultiple(lhs, rhs);

            //remember, the lcm can never be smaller than the larger number.The lcm is always an int, and a multiple. This means we cannot lose precision by dividing here.
            var newLhsNum = lhs.Denom == lcm ? lhs.Num : lhs.Num * (lcm / lhs.Denom);
            var newRhsNum = rhs.Denom == lcm ? rhs.Num : rhs.Num * (lcm / rhs.Denom);

            return new BigRational(newLhsNum - newRhsNum, lcm);
        }
        
        public static BigRational Multiply(in BigRational first, in BigRational second)
        {
            var num = first.Num * second.Num;
            var denom = first.Denom * second.Denom;
            return new BigRational(num, denom);
        }  
        
        public static BigRational Divide(in BigRational first, in BigRational second)
        {
            if (second.IsZero())
            {
                throw new DivideByZeroException();
            }

            var num = first.Num * second.Denom;
            var denom = first.Denom * second.Num;
            return new BigRational(num, denom);
        }

        public static BigRational operator +(in BigRational first, in BigRational second) => Add(first, second);
        public static BigRational operator -(in BigRational first, in BigRational second) => Subtract(first, second);
        public static BigRational operator *(in BigRational first, in BigRational second) => Multiply(first, second);
        public static BigRational operator /(in BigRational first, in BigRational second) => Divide(first, second);
        /// <summary>
        /// Unary negation.
        /// </summary>
        public static BigRational operator -(in BigRational first) => new (-first.Num, first.Denom);

        public static implicit operator BigRational(int value) => new (value);
        public static implicit operator BigRational(byte value) => new (value);
        public static implicit operator BigRational(short value) => new (value);
        public static implicit operator BigRational(long value) => new (value);

        public static explicit operator double(BigRational value) => value.ToDouble();
        public static explicit operator long(BigRational value) => value.ToLong();
        public static explicit operator BigInteger(BigRational value) => value.ToBigInt();

        #endregion

        #region overrides
        public bool Equals(BigRational other)
        {
            return Num.Equals(other.Num) && Denom.Equals(other.Denom);
        }

        public override bool Equals(object? obj)
        {
            return obj is BigRational other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Num, Denom);
        }

        public static bool operator ==(in BigRational left, in BigRational right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(in BigRational left, in BigRational right)
        {
            return !left.Equals(right);
        }

        //TODO overload CompareTo for numeric types
        public int CompareTo(BigRational other)
        {
            if (other.IsZero())
            {
                return Num.Sign;
            }
            else if (IsZero())
            {
                return other.Num.Sign;
            }

            //Normalize numerators
            var lcm = LeastCommonMultiple(this, other);
            var newLhsNum = Denom == lcm ? Num : Num * (lcm / Denom);
            var newRhsNum = other.Denom == lcm ? other.Num : other.Num * (lcm / other.Denom);

            var numComparison = newLhsNum.CompareTo(newRhsNum);
            if (numComparison != 0) return numComparison;
            return Denom.CompareTo(other.Denom);
        }

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is BigRational other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(BigRational)}");
        }

        public static bool operator <(BigRational left, BigRational right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(BigRational left, BigRational right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(BigRational left, BigRational right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(BigRational left, BigRational right)
        {
            return left.CompareTo(right) >= 0;
        }

        public override string ToString()
        {
            return Num + "/" + Denom;
        }
        #endregion
    }
}
