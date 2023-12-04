using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Models.Day7
{
    public class FilesystemNode
    {
        public FilesystemNode? Parent { get; }
        public List<string> ChildFolders { get; set; }
        public string Path { get; set; }
        public long Size { get; set; } = 0;

        public FilesystemNode(FilesystemNode? parent, string path) {
            Parent = parent;
            Path = path;
            ChildFolders = new List<string>();
        }

        public void UpdateSizeFromChild(string childPath, long size)
        {
            this.Size += size;
            ChildFolders.Remove(childPath);

            if (ChildFolders.Count == 0)
            {
                Parent?.UpdateSizeFromChild(Path, this.Size);
            }
        }
    }
}
