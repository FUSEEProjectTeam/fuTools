using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fuHTMLGen
{
    partial class ManifestFile
    {
        private readonly string _projName;
        private readonly string[] _fileNames;
        private readonly long[] _fileSize;

        public ManifestFile(string projName, string[] filePaths)
        {
            _projName = projName;

            int ct = 0;

            _fileNames = new string[filePaths.Length];
            _fileSize = new long[filePaths.Length];

            foreach (var filePath in filePaths)
            {
                _fileNames[ct] = Path.GetFileName(filePath);

                var f = new FileInfo(filePath);
                _fileSize[ct] = f.Length;

                ct++;
            }
        }
    }
}
