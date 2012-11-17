using System.IO;

namespace fuHTMLGen
{
    partial class WebPage
    {
        private readonly string _fileName;
        private readonly string _fileNameWOext;
        private readonly string _nameSpace;
        private readonly string _customManifest;

        public WebPage(string target, bool customManifest)
        {
            _fileName = Path.GetFileName(target);
            _fileNameWOext = Path.GetFileNameWithoutExtension(target);
            _nameSpace = Path.GetExtension(_fileNameWOext);

            if (customManifest)
                _customManifest = @"Assets/" + _fileNameWOext + ".contentproj";
            else
                _customManifest = "";
        }
    }
}
