﻿using System;
using System.IO;
using System.Reflection;

namespace MastermindVanHackathon
{
    public class RootPathProvider
    {
        public string GetRootPath()
        {
            // This is similar to the Self/Wcf Host FileSystemRootPathProvider
            var assembly =
                Assembly.GetEntryAssembly() ??
                Assembly.GetExecutingAssembly();

            var assemblyPath =
                Path.GetDirectoryName(assembly.Location) ??
                Environment.CurrentDirectory;

            // We want to move two directories up (to the project root).
            // Nancy does not allow relative paths for static content,
            // so we make sure we have an absolute path using GetFullPath()
            var rootPath =
                Path.GetFullPath(Path.Combine(assemblyPath, "..", "..") + @"\wwwroot");

            return rootPath;
        }
    }
}