using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            var newFile = false;
            var customManifest = false;

            if (!File.Exists(targWeb + @"\" + fileName + ".html"))
            {
                Console.WriteLine("// No HTML file found - generating a simple HTML file");
                newFile = true;
            }
            else
            {
                Console.WriteLine("// HTML file already exists - delete it to create a new one");               
            }

            // Collecting all files
            if (Directory.Exists(targDir + @"Files\"))
            {
                Console.WriteLine("// Found a Files folder - collecting all and write manifest");

                string[] filePaths = Directory.GetFiles(targDir + @"Files\");
                customManifest = true;

                var manifest = new ManifestFile(fileName, filePaths);
                String manifestContent = manifest.TransformText();

                if (!Directory.Exists(targWeb + @"\Files\"))
                    Directory.CreateDirectory(targWeb + @"\Files\");

                File.WriteAllText(targWeb + @"\Files\" + fileName + ".contentproj.manifest.js", manifestContent);

                foreach (var filePath in filePaths)
                    if (!File.Exists(targWeb + @"\Files\" + Path.GetFileName(filePath)))
                        File.Copy(filePath, targWeb + @"\Files\" + Path.GetFileName(filePath));
            }
            else
            {
                Console.WriteLine("// No Files folder - no additional files will be added");
            }

            if (newFile)
            {
                var page = new WebPage(targApp, customManifest);
                String pageContent = page.TransformText();

                File.WriteAllText(targWeb + @"\" + fileName + ".html", pageContent);
            }

            Console.WriteLine("// Finished all tasks");

            return 0;
        }
    }
}
