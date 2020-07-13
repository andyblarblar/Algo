using System;

namespace Algo.Util.IO
{
    public readonly struct Path
    {
        public string InternalPath { get; }

        public Path(string path)
        {
            InternalPath = path;
        }

        public static Path operator /(Path lhs, string rhs) => new Path(System.IO.Path.Combine(lhs.InternalPath,$"/{rhs}"));
        
        public static Path operator /(string lhs, Path rhs) => new Path(System.IO.Path.Combine(lhs, rhs.InternalPath));
        
        public static Path operator /(Path lhs, Path rhs) => new Path(System.IO.Path.Combine(lhs.InternalPath, rhs.InternalPath));
        
        /// <summary>
        /// Creates a path with the current working directory
        /// </summary>
        public static Path Cwd => new Path(Environment.CurrentDirectory);

        /// <summary>
        /// Creates a path with a ~
        /// </summary>
        public static Path UnixHome => new Path("~");

        /// <summary>
        /// Creates a path with the current users windows profile folder
        /// </summary>
        public static Path UserFolderWin => new Path(Environment.GetEnvironmentVariable("USERPROFILE"));

        public Path Append(string path) => new Path(System.IO.Path.Combine(InternalPath, path));
        
        public Path Append(Path path) => new Path(System.IO.Path.Combine(InternalPath, path.InternalPath));

        public bool ContainsSpaces() => InternalPath.Contains(' ');

        public string ToNtfsString() => InternalPath.Replace('/', '\\');
        
        public static Path FromLogicalDrive(char driveLetter) => new Path(driveLetter + ":"); 

        /// <summary>
        /// returns the internal path as a unix path 
        /// </summary>
        public override string ToString()
        {
            return InternalPath;
        }
    }
}
