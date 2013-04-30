using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace fuProjectGen
{
    internal class Program
    {
        private static bool ValidChars(string name)
        {
            if (name.Length == 0 || name.Length > 1023) return false;

            var validationRegex = new Regex(@"^(([_]+[a-zA-Z0-9]+\w*)|([a-zA-Z]+\w*))");
            return validationRegex.IsMatch(name);
        }

        private static string GetProjectName()
        {
            var validName = false;
            var projectName = "";

            while (!validName)
            {
                Console.WriteLine("Please enter a (valid) project name:");
                Console.Write("> ");

                projectName = Console.ReadLine();
                validName = ValidChars(projectName);

                Console.WriteLine();
            }

            return projectName;
        }

        private static void Error(string msg)
        {
            Console.WriteLine(">> ERROR: " + msg);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press enter to exit.");
            Console.WriteLine();
            Console.ReadLine();

            Environment.Exit(1);
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("#############################################");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("##        > FUSEE - fuProjectGen <         ##");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("#############################################");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("## WARNING: Always backup your Engine.sln! ##");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("#############################################");
            Console.WriteLine();
            Console.WriteLine();

            // pre-checks
            if (!File.Exists(@"../Engine.sln"))
                Error("Couldn't find ../Engine.sln!");

            // get a valid project name
            var validName = (args.Length == 0) || (!ValidChars(args[0]));
            var projectName = (validName) ? GetProjectName() : args[0];

            if (!ValidChars(projectName))
                Error("No valid project name!");

            // open Engine.sln and parse the file
            var engineSolution = File.ReadAllLines(@"../Engine.sln").ToList();
            var globalLine = engineSolution.IndexOf("Global");
            
            // create GUID for new project
            var guid = Guid.NewGuid();
            
            while (engineSolution.IndexOf(guid.ToString("B")) != -1)
                guid = Guid.NewGuid();
            
            // add project to Engine.sln
            var slnFile = new SolutionFile(guid, projectName);
            string slnContent = slnFile.TransformText();
            
            engineSolution.Insert(globalLine, slnContent);
            File.WriteAllLines(@"../Engine.sln", engineSolution);

            // done
            Console.WriteLine(">> DONE: Sucessfully created new project.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press enter to exit.");
            Console.WriteLine();
            Console.ReadLine();

            Environment.Exit(1);
        }
    }
}

