using System.IO;

namespace fuHTMLGen
{
    partial class WebPage
    {
        private readonly string _fileName;
        private readonly string _fileNameWOext;
        private readonly string _nameSpace;
        private readonly string _customCSS;

        public WebPage(string target, string customCSS)
        {
            _fileName = Path.GetFileName(target);
            _fileNameWOext = Path.GetFileNameWithoutExtension(target);
            _nameSpace = Path.GetExtension(_fileNameWOext);

            if (customCSS != "")
                _customCSS = @"Assets/" + customCSS;
            else
                _customCSS = "";
        }
    }
}
