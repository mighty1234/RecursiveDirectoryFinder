using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MultytreadingDirectotyFinder
{

    [AttributeUsage(AttributeTargets.Property,
       Inherited = false,
       AllowMultiple = false)]
    internal sealed class OptionalAttribute : Attribute { }

    public class FileDirectoryModel
    {
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }
        public DateTime DateOfLastAccess { get; set; }
        public string Attributes { get; set; }
        public long Size { get; set; }

        [Optional]
        public string Directiory { get; set; }
        public int Owner { get; set; }
        public int Credentials { get; set; }
        public string Extention { get; set; }
       
    }
}
