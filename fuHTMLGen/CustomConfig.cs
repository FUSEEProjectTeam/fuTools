using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace fuHTMLGen
{
    partial class JsilConfig
    {
        private readonly string _fileName;
        private readonly string _fileNameWOext;
        private readonly string _customManifest;

        private readonly string _useProgrBar;

        public JsilConfig(string targApp, string targDir, bool customManifest, bool customConf)
        {
            _fileName = Path.GetFileName(targApp);
            _fileNameWOext = Path.GetFileNameWithoutExtension(targApp);

            _customManifest = (customManifest) ? _fileNameWOext + ".contentproj" : "";

            if (!customConf)
            {
                _useProgrBar = "true";
            }
            else
            {
                var xmlSer = new XmlSerializer(typeof(ConfXMLReader));

                StreamReader confReader = File.OpenText(Path.Combine(targDir, "Assets", "fusee_config.xml"));
                var conf = (ConfXMLReader)xmlSer.Deserialize(confReader);
                confReader.Close();

                // read settings
                _useProgrBar = (conf.WebBuildConf.UseProgressBar == "")
                                   ? "true"
                                   : conf.WebBuildConf.UseProgressBar.ToLower();
            }
        }
    }
}
