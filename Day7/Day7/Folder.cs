using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class Folder
    {
        public string Name { get; set; } = string.Empty;
        public Folder? Parent { get; set; } = null;
        public List<Folder> Children { get; set; } = new();
        public List<UnitFiles> UnitFiles { get; set; } = new();
        public ulong TotalSize { get; set; } = 0;

        public Folder(string name, Folder parent)
        {
            Name = name;
            Parent = parent;
        }

        public Folder()
        {
        }
    }

    internal class UnitFiles
    {
        public ulong Size { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

        public UnitFiles(string fileName, ulong fileSize)
        {
            Size = fileSize;
            Name = fileName;
        }

        public UnitFiles()
        {
        }
    }
}
