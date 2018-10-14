﻿using HephaestusCommon.Classes;
using System;
using System.Diagnostics;
using System.IO;
using Hephaestus.Classes.Exceptions;

namespace Hephaestus.Classes
{
    public class AddonBuilder
    {
        public Process Process { get; }

        public AddonBuilder(string sourceCodeDirectory, Project project)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        RedirectStandardError = true,
                        RedirectStandardOutput = false,

                        UseShellExecute = false,

                        CreateNoWindow = true,

                        FileName = project.AddonBuilderFile,

                        Arguments =
                            $@"""{sourceCodeDirectory}"" ""{project.TargetDirectory}"" " +
                                $@"-prefix=""{project.ProjectPrefix}\{Path.GetFileName(sourceCodeDirectory)}"" -sign=""{project.PrivateKeyFile}"" " +
                                $@"-temp=""{Path.GetTempPath()}\{project.ProjectPrefix}"" " +
                                $@"-include=""{AppDomain.CurrentDomain.BaseDirectory}\Hephaestus.AddonBuilderIncludes.txt"" -binarizeFullLogs"
                    },

                    EnableRaisingEvents = true
                };

                process.Start();

                Process = process;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Addon Builder failed to launch for {Path.GetFileName(sourceCodeDirectory)} folder because {e.Message}");
            }
        }
    }
}