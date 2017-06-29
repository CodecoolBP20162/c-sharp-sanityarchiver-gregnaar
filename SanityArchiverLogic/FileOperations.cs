using SanityArchiver;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanityArchiverLogic
{
    public class FileOperation
    {

        public void Compress(FileInfo fi)
        {
            try
            {
                using (FileStream originalFileStream = fi.OpenRead())
                {
                    if ((File.GetAttributes(fi.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fi.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Compress(DirectoryInfo di)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                Compress(fi);
            }
        }

        public void Decompress(FileInfo fi)
        {
            try
            {
                using (FileStream inFile = fi.OpenRead())
                {

                    string curFile = fi.FullName;
                    string origName = curFile.Remove(curFile.Length -
                            fi.Extension.Length);

                    using (FileStream outFile = File.Create(origName))
                    {
                        using (GZipStream Decompress = new GZipStream(inFile,
                                CompressionMode.Decompress))
                        {
                            Decompress.CopyTo(outFile);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Decompress(DirectoryInfo di)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                Decompress(fi);
            }
        }

        public void Encrypt(FileInfo fi)
        {
            try
            {
                //File.Encrypt(fi.Name);
                fi.Encrypt();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Decrypt(FileInfo fi)
        {
            try
            {
                //File.Decrypt(fi.Name);
                fi.Decrypt();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Rename(FileInfo fi, string newName)
        {
            File.Move(fi.FullName, SelectedItem.currentDir + "\\" + newName);
        }

        public void Rename(DirectoryInfo dir, string newName)
        {
            Directory.Move(dir.FullName, SelectedItem.currentDir + "\\" + newName);
        }

        public void CreateNewFolder(string path)
        {
            Directory.CreateDirectory(path + "\\New Folder");
        }

        public void CreateNewFile(string path)
        {
            File.CreateText(path + "\\New File.txt");
        }

        public string GetFolderSize(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            long total = 0;
            foreach (FileInfo fi in files)
            {
                total += fi.Length;
            }
            return (total / 1024).ToString();
        }
    }
}