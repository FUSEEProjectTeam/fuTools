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

                var manifest = new ManifestFile(fileName, filePaths);
                String manifestContent = manifest.TransformText();

                File.WriteAllText(targWeb + @"\Assets\Scripts\" + fileName + ".contentproj.manifest.js", manifestContent);

                foreach (var filePath in filePaths)
                {
                    if (filePath.Contains("Fusee.Engine.Imp.WebGL.js"))
                        continue;

                    if (Path.GetExtension(filePath) == ".css")
                    {
                        customCSS = Path.GetFileName(filePath);

                        if (!File.Exists(targWeb + @"\Assets\Styles\" + Path.GetFileName(filePath)))
                            File.Copy(filePath, targWeb + @"\Assets\Styles\" + Path.GetFileName(filePath));
                    }
                    else
                    {
                        if (!File.Exists(targWeb + @"\Assets\" + Path.GetFileName(filePath)))
                            File.Copy(filePath, targWeb + @"\Assets\" + Path.GetFileName(filePath));
                    }
                }
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
            var newConf = !File.Exists(targWeb + @"\Assets\Config\jsil_config.js");

            Console.WriteLine(customCSS == ""
                      ? "// No custom config file found in Assets folder - creating a default one"
                      : "// Found an custom config file in Assets folder - adding to output folder");

            if (newConf)
            {
                var conf = new JsilConfig(targApp, customManifest);
                String confContent = conf.TransformText();

                File.WriteAllText(targWeb + @"\Assets\Config\jsil_config.js", confContent);
            }

            // Done
            Console.WriteLine("// Finished all tasks");

            return 0;
        }
    }
}
