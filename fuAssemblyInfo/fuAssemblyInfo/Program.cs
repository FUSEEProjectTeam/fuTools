using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;

namespace fuAssemblyInfo
{
    internal class Program
    {
        private static void Error(string msg)
        {
            var action = "Press enter to exit.\n";

            try
            {
                if (File.Exists(@"../Engine.orig"))
                {
                    File.Copy(@"../Engine.orig", @"../Engine.sln", true);
                    File.Delete(@"../Engine.orig");

                    action = "Engine.sln restored. " + action;
                }
            }
            catch (Exception e)
            {
                msg += "\n\n" + e.Message;
            }

            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine(">> ERROR: " + msg + "\n");
            Console.WriteLine("---------------------------------------------\n\n");
            Console.WriteLine(action);
            Console.ReadLine();

            Environment.Exit(1);
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("#############################################");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("##       > FUSEE - fuAssemblyInfo <        ##");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("#############################################\n\n");

            try
            {
                // pre-checks
                if (args.Count() < 2)
                    Error("Not enough arguments!\n\n> fuAssemblyInfo.exe PathToCommonAssembly JSFile1 [JSFile2 ...]");

                if (!Directory.Exists(args[0]))
                    Error("Directory doesn't exist!");

                if (!File.Exists(Path.Combine(args[0], "Fusee.Engine.Common.dll")))
                    Error("File 'Fusee.Engine.Common.dll' not found!");

                for (int i = 1; i < args.Count(); i++)
                    if (!File.Exists(args[i]))
                        Error("File '" + args[i] + "' not found!");

                // get all classes and methods from the given JavaScript files
                var clList = new List<string>();
                var mtList = new List<string>();

                for (int i = 1; i < args.Count(); i++)
                {
                    var fInfo = new FileInfo(args[i]);
                    StreamReader reader = fInfo.OpenText();

                    string line;
                    var clCheckNext = false;
                    var mtCheckNext = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        // classes
                        var clIndex = line.IndexOf("$.ImplementInterfaces", StringComparison.OrdinalIgnoreCase);
                        if (clIndex > -1)
                        {
                            clCheckNext = true;
                            continue;
                        }

                        if (clCheckNext)
                        {
                            var clIndex2 = line.IndexOf("TypeRef(", StringComparison.OrdinalIgnoreCase);

                            var clTmp = line.Remove(0, clIndex2 + 9);
                            clTmp = clTmp.Substring(0, clTmp.Length - 2);
                            clList.Add(clTmp.Trim());

                            clCheckNext = false;
                        }

                        // methods
                        var mtIndex = line.IndexOf("JSIL.MethodSignature", StringComparison.OrdinalIgnoreCase);
                        if (mtIndex > -1)
                        {
                            mtCheckNext = true;
                            continue;
                        }

                        if (mtCheckNext)
                        {
                            var mtIndex2 = line.IndexOf("function", StringComparison.OrdinalIgnoreCase);
                            if (mtIndex2 == -1) continue;
                            
                            var mtTmp = line.Remove(0, mtIndex2 + 9);
                            mtIndex2 = mtTmp.IndexOf("(", StringComparison.OrdinalIgnoreCase);
                            mtTmp = mtTmp.Substring(0, mtIndex2);

                            mtTmp = mtTmp.Replace(" ", "");
                            mtList.Add(mtTmp.Trim());

                            mtCheckNext = false;
                        }
                    }
                }

                // read assembly and cycle through interface methods
                var assembly = AssemblyDefinition.ReadAssembly(Path.Combine(args[0], "Fusee.Engine.Common.dll"));

                foreach (var type in assembly.MainModule.Types)
                    if (type.IsInterface)
                    {
                        string clName = type.FullName;

                        while (clName.Length < 40)
                            clName += ".";

                        Console.Write("Interface: " + clName);

                        if (clList.IndexOf(type.FullName) > -1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.SetCursorPosition(40, Console.CursorTop);
                            Console.WriteLine(" FOUND      ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.SetCursorPosition(40, Console.CursorTop);
                            Console.WriteLine(" NOT FOUND  ");
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;

                        // methods
                        foreach (var interMt in type.Methods)
                        {
                            var mtName = interMt.Name;

                            if (interMt.CustomAttributes.Count > 0)
                                if (interMt.CustomAttributes[0].AttributeType.Name == "JSChangeName")
                                    mtName = interMt.CustomAttributes[0].ConstructorArguments[0].Value.ToString();

                            var mtNameDisp = mtName;

                            while (mtNameDisp.Length < 40)
                                mtNameDisp += ".";

                            Console.Write("  > " + mtNameDisp);

                            if (mtList.IndexOf(mtName) > -1)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.SetCursorPosition(40, Console.CursorTop);
                                Console.WriteLine(" FOUND      ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.SetCursorPosition(40, Console.CursorTop);
                                Console.WriteLine(" NOT FOUND  ");
                            }

                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine();
                    }

                Console.WriteLine("\n\nDone.");
                Console.ReadLine();

                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Error("Creating new project failed!\n\n" + e.Message);
            }
        }
    }
}