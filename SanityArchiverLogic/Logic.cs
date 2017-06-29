using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanityArchiverLogic
{
    public class SelectedItem
    {
        public string filepath { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public FileInfo file { get; set; }
        public DirectoryInfo dir { get; set; }

        SelectedItem()
        {

        }

        public SelectedItem(FileInfo file)
        {
            filepath = file.FullName;
            name = file.Name;
            type = "File";
            this.file = file;
        }

        public SelectedItem(DirectoryInfo dir)
        {
            filepath = dir.FullName;
            name = dir.Name;
            type = "Directory";
            this.dir = dir;
        }

        public bool IsItDir()
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
                if (IsItDir())
                    this.dir.Delete();
                else
                    this.file.Delete();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }


    public class FileOperation
    {

        public void Compress(FileInfo fi)
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

        public void Compress(DirectoryInfo di)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                Compress(fi);
            }
        }

        public void Decompress(FileInfo fi)
        {
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length -
                        fi.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile,
                            CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fi.Name);

                    }
                }
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
            try {
                //File.Encrypt(fi.Name);
                fi.Encrypt();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
