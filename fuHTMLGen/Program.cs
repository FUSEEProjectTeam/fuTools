using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fuHTMLGen
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 3) return 1;

            var targDir = args[0];
            var targWeb = args[1];
            var targApp = args[2];

            String fileName = Path.GetFileNameWithoutExtension(targApp);

            // Does HTML already exists?
            var newFile = !File.Exists(targWeb + @"\" + fileName + ".html");

            Console.WriteLine(newFile
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
                filePaths.Insert(0, targWeb + @"\Fusee.Engine.Imp.WebGL.js");

                var manifest = new ManifestFile(fileName, filePaths);
                String manifestContent = manifest.TransformText();

                if (!Directory.Exists(targWeb + @"\Assets\"))
                    Directory.CreateDirectory(targWeb + @"\Assets\");

                File.WriteAllText(targWeb + @"\Assets\" + fileName + ".contentproj.manifest.js", manifestContent);

                foreach (var filePath in filePaths)
                {
                    if (Path.GetExtension(filePath) == ".css")
                        customCSS = Path.GetFileName(filePath);

                    if (!File.Exists(targWeb + @"\Assets\" + Path.GetFileName(filePath)))
                        File.Copy(filePath, targWeb + @"\Assets\" + Path.GetFileName(filePath));
                }
            }

            // Create HTML file
            if (newFile)
            {
                Console.WriteLine(customCSS == ""
                                      ? "// No additional .css file found in Assets folder - using only default one"
                                      : "// Found an additional .css file in Assets folder - adding to HTML file");

                var page = new WebPage(targApp, customManifest, customCSS);
                String pageContent = page.TransformText();

                File.WriteAllText(targWeb + @"\" + fileName + ".html", pageContent);
            }

            // Done
            Console.WriteLine("// Finished all tasks");

            return 0;
        }
    }
}
