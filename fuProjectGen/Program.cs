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
        private static List<string> _engineSolution; 

        private static bool ValidChars(string name)
        {
            if (name.Length == 0 || name.Length > 1023) return false;

            var validationRegex = new Regex(@"^(([_]+[a-zA-Z0-9]+\w*)|([a-zA-Z]+\w*))");
            return validationRegex.IsMatch(name);
        }

        private static bool DuplicateName(string name)
        {
            return (_engineSolution.FindAll(s => s.ToLower().Contains(name.ToLower() + ".csproj")).Count > 0) ||
                   Directory.Exists("../src/Engine/Examples/" + name);
        }

        private static string GetValidGUID()
        {
            var guid = Guid.NewGuid().ToString("B").ToUpper();

            while (_engineSolution.FindAll(s => s.ToUpper().Contains(guid)).Count > 0)
                guid = Guid.NewGuid().ToString("B").ToUpper();

            return guid;
        }

        private static string GetProjectName()
        {
            var validName = false;
            var projectName = "";

            while (!validName)
            {
                Console.WriteLine("Please enter a valid and unique project name:");
                Console.Write("> ");

                projectName = Console.ReadLine();
                validName = ValidChars(projectName) && !DuplicateName(projectName);

                Console.WriteLine();
            }

            return projectName;
        }

        private static int GetLine(string search, int start = 0)
        {
            var line = _engineSolution.IndexOf(search, start);

            if (line == -1)
                Error("Error while parsing Engine.sln!");

            return line;
        }

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
            Console.WriteLine("##        > FUSEE - fuProjectGen <         ##");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("#############################################");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("## WARNING: Always backup your Engine.sln! ##");
            Console.WriteLine("##                                         ##");
            Console.WriteLine("#############################################");
            Console.WriteLine();
            Console.WriteLine();

            try
            {
                // pre-checks
                if (!File.Exists(@"../Engine.sln"))
                    Error("Couldn't find ../Engine.sln!");

                // backup of Engine.sln
                File.Copy(@"../Engine.sln", @"../Engine.orig", true);

                if (!File.Exists(@"../Engine.orig"))
                    Error("Couldn't backup Engine.sln!");

                // open Engine.sln
                _engineSolution = File.ReadAllLines(@"../Engine.sln").ToList();

                // get a valid project name
                var validName = (args.Length == 0) || (!ValidChars(args[0]));
                var projectName = (validName) ? GetProjectName() : args[0];

                if (!ValidChars(projectName))
                    Error("No valid project name!");

                // get GUID for new project
                var guid = GetValidGUID();

                // add project to Engine.sln (Part1)
                var globalLine = GetLine("Global");

                var slnFilePt1 = new SolutionFilePt1(guid, projectName);
                var slnContentPt1 = slnFilePt1.TransformText();

                _engineSolution.Insert(globalLine, slnContentPt1);

                // add project to Engine.sln (Part2)
                var postSlnLine = GetLine("	GlobalSection(ProjectConfigurationPlatforms) = postSolution");
                var postSlnEndLine = GetLine("	EndGlobalSection", postSlnLine);

                var slnFilePt2 = new SolutionFilePt2(guid);
                var slnContentPt2 = slnFilePt2.TransformText();

                _engineSolution.Insert(postSlnEndLine, slnContentPt2);

                // add project to Engine.sln (Part3)
                var preSlnLine = GetLine("	GlobalSection(NestedProjects) = preSolution");
                var preSlnEndLine = GetLine("	EndGlobalSection", preSlnLine);

                var slnContentPt3 = "		" + guid + " = {2DC1CA2C-F4F6-4779-B000-597CB6A54A04}";
                _engineSolution.Insert(preSlnEndLine, slnContentPt3);

                // save new Engine.sln
                File.WriteAllLines(@"../Engine.sln", _engineSolution);

                // done
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine(">> DONE: Sucessfully created new project.\n");
                Console.WriteLine("---------------------------------------------\n\n");
                Console.WriteLine("Engine.sln saved. Press enter to exit.\n");
                Console.ReadLine();

                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Error("Creating new project failed!\n\n" + e.Message);
            }
        }
    }
}

