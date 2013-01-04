using System.IO;

namespace fuHTMLGen
{
    partial class JsilConfig
    {
        private readonly string _fileName;
        private readonly string _fileNameWOext;
        private readonly string _customManifest;

        public JsilConfig(string target, bool customManifest)
        {
            _fileName = Path.GetFileName(target);
            _fileNameWOext = Path.GetFileNameWithoutExtension(target);

            if (customManifest)
                _customManifest = _fileNameWOext + ".contentproj";
            else
                _customManifest = "";
        }
    }
}
