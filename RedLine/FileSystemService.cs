using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;

namespace RedLine
{
    public interface IFileSystemService
    {
        string RedLineDataPath { get; }

        Stream CreateFile(string filePath);
        Stream ReadFile(string filePath);
        void DeleteFile(string filePath);
        IEnumerable<string> ListFiles(string pattern, string location = null);
        bool FileExists(string filePath);
    }

    [Export(typeof(IFileSystemService))]
    public class FileSystemService: IFileSystemService
    {
        public string RedLineDataPath
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RedLine");
            }
        }

        public Stream CreateFile(string filePath)
        {
            filePath = MakeAbsolutePath(filePath);

            var folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return File.Create(filePath);
        }

        public Stream ReadFile(string filePath)
        {
            filePath = MakeAbsolutePath(filePath);

            return File.OpenRead(filePath);
        }

        public void DeleteFile(string filePath)
        {
            filePath = MakeAbsolutePath(filePath);

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public IEnumerable<string> ListFiles(string pattern, string location = null)
        {
            location = location ?? RedLineDataPath;

            if (Directory.Exists(location))
                return Directory.EnumerateFiles(location, pattern);
            else
                return Enumerable.Empty<string>();
        }

        public bool FileExists(string filePath)
        {
            filePath = MakeAbsolutePath(filePath);

            return File.Exists(filePath);
        }

        private string MakeAbsolutePath(string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(RedLineDataPath, filePath);
            }
            return filePath;
        }
    }
}
