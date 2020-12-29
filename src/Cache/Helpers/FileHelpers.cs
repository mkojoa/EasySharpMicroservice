using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySharp.Cache.Helpers
{
    internal static class FileHelpers
    {
        internal static string GetLocalStoreFilePath(string folder, string filename)
        {
            CreateDirectoryIfDoesNotExist(folder);

            return Path.Combine(folder, filename);
            //return Path.Combine(System.AppContext.BaseDirectory, filename);
        }

        internal static void CreateDirectoryIfDoesNotExist(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        internal static void CreateFileIfDoesNotExist(string file)
        {
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
        }
    }
}
