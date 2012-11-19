using System.Collections.Generic;
using System.IO;

namespace fuHTMLGen
{
    partial class ManifestFile
    {
        private readonly string _projName;
        private readonly string[] _fileNames;
        private readonly long[] _fileSize;

        public ManifestFile(string projName, ICollection<string> filePaths)
        {
            _projName = projName;

            var ct = 0;

            _fileNames = new string[filePaths.Count];
            _fileSize = new long[filePaths.Count];

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
