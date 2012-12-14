using System.Collections.Generic;
using System.IO;

namespace fuHTMLGen
{
    partial class ManifestFile
    {
        private readonly int _fileCount;

        private readonly string _projName;
        private readonly string[] _fileNames;
        private readonly long[] _fileSize;
        private readonly string[] _fileTypes;

        public ManifestFile(string projName, ICollection<string> filePaths)
        {
            _projName = projName;
            _fileCount = filePaths.Count;

            _fileNames = new string[_fileCount];
            _fileSize = new long[_fileCount];
            _fileTypes = new string[_fileCount];

            var ct = 0;

            foreach (var filePath in filePaths)
            {
                // name
                _fileNames[ct] = Path.GetFileName(filePath);

                // size
                var f = new FileInfo(filePath);
                _fileSize[ct] = f.Length;

                // type
                _fileTypes[ct] = FileTypes.GetFileType(_fileNames[ct]);

                ct++;
            }
        }
    }
}
