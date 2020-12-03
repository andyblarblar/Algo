using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Util.Calc
{
    public class Derivative
    {
        /// <summary>
        /// Computes the first derivative of the passed function.
        /// </summary>
        /// <param name="f">The function to differentiate</param>
        /// <param name="dx">an arbitrary small number</param>
        /// <returns>The derivative in the form of a lambda</returns>
        public static Func<decimal, decimal> Diff(Func<decimal, decimal> f, decimal dx = 1e-5m)
        {
            return x => (f(x + dx) - f(x)) / dx;
        }

        /// <summary>
        /// Computes the second derivative of the passed function.
        /// </summary>
        /// <param name="f">The function to differentiate</param>
        /// <param name="dx">an arbitrary small number</param>
        /// <returns>The derivative in the form of a lambda</returns>
        public static Func<decimal, decimal> Diff2nd(Func<decimal, decimal> f, decimal dx = 1e-5m)
        {
            return Diff(Diff(f, dx),dx);
        }

        /// <inheritdoc cref="Diff(System.Func{decimal,decimal},decimal)"/>
        public static Func<double, double> Diff(Func<double, double> f, double dx = 1e-7)
        {
            return x => (f(x + dx) - f(x)) / dx;
        }

        /// <inheritdoc cref="Diff2nd(System.Func{decimal,decimal},decimal)"/>
        public static Func<double, double> Diff2nd(Func<double, double> f, double dx = 1e-7)
        {
            return Diff(Diff(f, dx), dx);
        }

    }
}
