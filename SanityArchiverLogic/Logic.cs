using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanityArchiver
{
    public class SelectedItem
    {
        public string filePath { get; set; }
        public string dirPath { get; set; }
        public string parentPath { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public FileInfo file { get; set; }
        public DirectoryInfo dir { get; set; }
        public DriveInfo drive { get; set; }

        public static string currentDir {get; set;}

        SelectedItem()
        {

        }

        public SelectedItem(FileInfo file)
        {
            filePath = file.FullName;
            dirPath = file.DirectoryName;
            name = file.Name;
            type = "File";
            this.file = file;
            setParent();
            SetDrive();
        }

        public SelectedItem(DirectoryInfo dir)
        {
            filePath = dir.FullName;
            dirPath = dir.FullName;
            name = dir.Name;
            type = "Directory";
            this.dir = dir;
            setParent();
            SetDrive();
        }

        public bool IsItDirectory()
        {
            if (this.type == "Directory")
                return true;
            else
                return false;
        }

        public void Delete()
        {
            try
            {
                if (IsItDirectory())
                    this.dir.Delete();
                else
                    this.file.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public FileInfo GetFIle()
        {
            if (!IsItDirectory())
                return this.file;
            else
                return null;
        }

        public DirectoryInfo GetDir()
        {
            if (IsItDirectory())
                return this.dir;
            else
                return null;
        }

        private void setParent()
        {
            try
            {
                if (IsItDirectory())
                {
                    if (dir.Parent.Parent.FullName != null)
                    {
                        parentPath = dir.Parent.Parent.FullName;
                    }
                    if (dir.Parent.FullName != null)
                    {
                        parentPath = dir.Parent.FullName;
                    }
                }
                else
                {
                    parentPath = file.Directory.Parent.FullName;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SetDrive()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                try
                {
                    if (d.Name == dir.Root.ToString())
                    {
                        drive = d;
                        break;
                    }
                    if (d.Name == file.Directory.Root.ToString())
                    {
                        drive = d;
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }


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
            File.Move(fi.FullName, SelectedItem.currentDir+ "\\" + newName);
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
            foreach(FileInfo fi in files)
            {
                total += fi.Length;
            }
            return (total / 1024).ToString();
        }
    }
}
