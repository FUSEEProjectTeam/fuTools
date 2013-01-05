using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fuHTMLGen
{
    class Program
    {
        static private void CreateDirectories(String targWeb)
        {
            if (!Directory.Exists(targWeb + @"\Assets\"))
                Directory.CreateDirectory(targWeb + @"\Assets\");

            if (!Directory.Exists(targWeb + @"\Assets\Scripts\"))
                Directory.CreateDirectory(targWeb + @"\Assets\Scripts");

            if (!Directory.Exists(targWeb + @"\Assets\Styles\"))
                Directory.CreateDirectory(targWeb + @"\Assets\Styles");

            if (!Directory.Exists(targWeb + @"\Assets\Config\"))
                Directory.CreateDirectory(targWeb + @"\Assets\Config\");
        }

        static int Main(string[] args)
        {
            if (args.Length != 3) return 1;

            var targDir = args[0];
            var targWeb = args[1];
            var targApp = args[2];

            String fileName = Path.GetFileNameWithoutExtension(targApp);

            // Create directories
            CreateDirectories(targWeb);

            // Does HTML already exists?
            var newHTML = !File.Exists(targWeb + @"\" + fileName + ".html");

            Console.WriteLine(newHTML
                                  ? "// No HTML file found - generating a simple HTML file"
                                  : "// HTML file already exists - delete it to create a new one");

            // Collecting all files
            var customManifest = Directory.Exists(targDir + @"Assets\");
            var customCSS = "";

            Console.WriteLine(customManifest
                                  ? "// Found an Assets folder - collecting all and write manifest"
                                  : "// No Assets folder - no additional files will be added");

            if (customManifest)
            {
                List<string> filePaths = Directory.GetFiles(targDir + @"Assets\").ToList();

                // load Fusee.Engine.Imp.WebGL.js first
                if (File.Exists(targWeb + @"\Assets\Scripts\Fusee.Engine.Imp.WebGL.js"))
                    filePaths.Insert(0, targWeb + @"\Assets\Scripts\Fusee.Engine.Imp.WebGL.js");
                else
                    return 1;

                // copy to output folder
                for (var ct = filePaths.Count - 1; ct > 0; ct--)
                {
                    String pathExt = "";
                    String filePath = filePaths.ElementAt(ct);

                    if (Path.GetExtension(filePath) == ".css")
                    {
                        customCSS = Path.GetFileName(filePath);
                        pathExt = @"Styles\";
                    }

                    if (Path.GetFileName(filePath) == "fusee_config.xml")
                        pathExt = @"Config\";

                    if (!File.Exists(targWeb + @"\Assets\" + Path.GetFileName(filePath)))
                        File.Copy(filePath, targWeb + @"\Assets\" + pathExt + Path.GetFileName(filePath));

                    if (pathExt != "")
                        filePaths.RemoveAt(ct);
                }

                // create manifest
                var manifest = new ManifestFile(fileName, filePaths);
                String manifestContent = manifest.TransformText();

                File.WriteAllText(targWeb + @"\Assets\Scripts\" + fileName + ".contentproj.manifest.js", manifestContent);
            }

            // Create HTML file
            if (newHTML)
            {
                Console.WriteLine(customCSS == ""
                                      ? "// No additional .css file found in Assets folder - using only default one"
                                      : "// Found an additional .css file in Assets folder - adding to HTML file");

                var page = new WebPage(targApp, customCSS);
                String pageContent = page.TransformText();

                File.WriteAllText(targWeb + @"\" + fileName + ".html", pageContent);
            }
            
            // Create config file
            var customConf = File.Exists(targDir + @"Assets\fusee_config.xml");

            Console.WriteLine(!customConf
                      ? "// No custom config file ('fusee_config.xml') found in Assets folder - using default settings"
                      : "// Found an custom config file in Assets folder - applying settings to webbuild");

            var conf = new JsilConfig(targApp, targDir, customManifest, customConf);
            String confContent = conf.TransformText();

            File.WriteAllText(targWeb + @"\Assets\Config\jsil_config.js", confContent);

            // Done
            Console.WriteLine("// Finished all tasks");

            return 0;
        }
    }
}
