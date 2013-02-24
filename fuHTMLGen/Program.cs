using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fuHTMLGen
{
    class Program
    {
        static private void CreateDirectories(string targWeb)
        { 
            if (!Directory.Exists(Path.Combine(targWeb, "Assets")))
                Directory.CreateDirectory(Path.Combine(targWeb, "Assets"));

            if (!Directory.Exists(Path.Combine(targWeb, "Assets", "Scripts")))
                Directory.CreateDirectory(Path.Combine(targWeb, "Assets", "Scripts"));

            if (!Directory.Exists(Path.Combine(targWeb, "Assets", "Styles")))
                Directory.CreateDirectory(Path.Combine(targWeb, "Assets", "Styles"));

            if (!Directory.Exists(Path.Combine(targWeb, "Assets", "Config")))
                Directory.CreateDirectory(Path.Combine(targWeb, "Assets", "Config"));
        }

        static int Main(string[] args)
        {
            if (args.Length != 3) return 1;

            var targDir = args[0];
            var targWeb = args[1];
            var targApp = args[2];

            string fileName = Path.GetFileNameWithoutExtension(targApp);

            // Create directories
            CreateDirectories(targWeb);

            // Does HTML already exists?
            var newHTML = !File.Exists(targWeb + fileName + ".html");

            Console.WriteLine(newHTML
                                  ? "// No HTML file found - generating a simple HTML file"
                                  : "// HTML file already exists - delete it to create a new one");

            // Collecting all files
            var customManifest = Directory.Exists(Path.Combine(targDir, "Assets"));
            var customCSS = "";

            Console.WriteLine(customManifest
                                  ? "// Found an Assets folder - collecting all and write manifest"
                                  : "// No Assets folder - no additional files will be added");

            if (customManifest)
            {
                List<string> filePaths = Directory.GetFiles(Path.Combine(targDir, "Assets")).ToList();
                filePaths.Sort(string.Compare);

                // Load custom implementations first
                var fileCount = 0;

                var exFile1 = File.Exists(Path.Combine(targWeb, "Assets", "Scripts", "Fusee.Engine.Imp.WebAudio.js"));
                var exFile2 = File.Exists(Path.Combine(targWeb, "Assets", "Scripts", "Fusee.Engine.Imp.WebGL.js"));

                if (exFile1)
                {
                    filePaths.Insert(fileCount, Path.Combine(targWeb, "Assets", "Scripts", "Fusee.Engine.Imp.WebAudio.js"));
                    fileCount++;
                }
                
                if (exFile2)
                {
                    filePaths.Insert(fileCount, Path.Combine(targWeb, "Assets", "Scripts", "Fusee.Engine.Imp.WebGL.js"));
                    fileCount++;
                }
                else
                    return 1;

                // Copy to output folder
                for (var ct = filePaths.Count - 1; ct > fileCount-1; ct--)
                {
                    string pathExt = "";
                    string filePath = filePaths.ElementAt(ct);

                    // style or config
                    if (Path.GetExtension(filePath) == ".css")
                    {
                        customCSS = Path.GetFileName(filePath);
                        pathExt = "Styles";
                    }

                    if (Path.GetFileName(filePath) == "fusee_config.xml")
                        pathExt = "Config";

                    // Copy files to output if they not exist yet
                    var tmpFileName = Path.GetFileName(filePath);

                    if (tmpFileName != null && !File.Exists(Path.Combine(targWeb, "Assets", pathExt, tmpFileName)))
                        File.Copy(filePath, Path.Combine(targWeb, "Assets", pathExt, tmpFileName));

                    if (pathExt != "")
                        filePaths.RemoveAt(ct);
                }

                // Create manifest
                var manifest = new ManifestFile(fileName, filePaths, fileCount);
                string manifestContent = manifest.TransformText();

                File.WriteAllText(Path.Combine(targWeb, "Assets", "Scripts", fileName + ".contentproj.manifest.js"),
                                  manifestContent);
            }

            // Create HTML file
            if (newHTML)
            {
                Console.WriteLine(customCSS == ""
                                      ? "// No additional .css file found in Assets folder - using only default one"
                                      : "// Found an additional .css file in Assets folder - adding to HTML file");

                var page = new WebPage(targApp, customCSS);
                string pageContent = page.TransformText();

                File.WriteAllText(Path.Combine(targWeb, fileName + ".html"), pageContent);
            }
            
            // Create config file
            var customConf = File.Exists(Path.Combine(targDir, "Assets", "fusee_config.xml"));

            Console.WriteLine(!customConf
                      ? "// No custom config file ('fusee_config.xml') found in Assets folder - using default settings"
                      : "// Found an custom config file in Assets folder - applying settings to webbuild");

            var conf = new JsilConfig(targApp, targDir, customManifest, customConf);
            string confContent = conf.TransformText();

            File.WriteAllText(Path.Combine(targWeb, "Assets", "Config", "jsil_config.js"), confContent);
            
            // Done
            Console.WriteLine("// Finished all tasks");

            return 0;
        }
    }
}
