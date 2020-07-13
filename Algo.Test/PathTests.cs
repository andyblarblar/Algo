using System;
using System.Collections.Generic;
using System.Text;
using Algo.Util.IO;
using NUnit.Framework;

namespace Algo.Test
{
    class PathTests
    {

        [Test]
        public void Example()
        {
           var path = new Path("this/is/a/path");
           var newPath = path/"to/a/newPlace";

           Console.WriteLine(newPath);

           var path2 = Path.Cwd / "assets";
           var path3 = (Path.FromLogicalDrive('C') / "Windows" / "System32").ToNtfsString();

           Console.WriteLine(path3);

           var path4 = Path.Cwd.Append("/ assets");
           Console.WriteLine(path4.ToNtfsString() + path4.ContainsSpaces());



        }

        [Test]
        public void PathsConcat()
        {
            var path = new Path("path");
            var newPath = path/"upDir";

            Assert.True( "path/upDir" == newPath.ToString());
        }


    }
}
