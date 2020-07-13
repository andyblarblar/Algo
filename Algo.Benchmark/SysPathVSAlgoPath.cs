using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Algo.Benchmark
{
    [MemoryDiagnoser]
    public class SysPathVSAlgoPath
    {
        public SysPathVSAlgoPath() { }

        [Benchmark]
        public string AlgoUserDirAssets() 
        {
                return Util.IO.Path.UserFolderWin.Append("/assets").ToNtfsString();
        }

        [Benchmark] 
        public string BCLUserDirAssets() 
        { 
            return Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"),"\\Assets");
        }

    }

}

