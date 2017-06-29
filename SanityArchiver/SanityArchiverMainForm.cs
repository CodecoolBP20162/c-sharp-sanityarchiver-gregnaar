using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SanityArchiverLogic
{
    public partial class SanityArchiverForm : Form
    {
        FileOperation fileOperator = new FileOperation();
        Updates updater;
        SelectedItem selected;
        
        public SanityArchiverForm()
        {
            InitializeComponent();
            updater = new Updates(filesListView, FilesTreeView, pathTextBox, searchTextBox);
            //updater.PopulateTreeView(@"C:\");
            updater.PopulateTreeView(@"E:\");
            
        }

        private void FilesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            updater.refreshListView(nodeDirInfo);
        }

        private void goToFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filesListView.Items.Clear();
            DirectoryInfo newDir = new DirectoryInfo(selected.filepath);
            updater.refreshListView(newDir);
        }

        private void filesListView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (selected.IsItDir())
                    updater.refreshListView(new DirectoryInfo(selected.filepath));
                else
                    Process.Start(selected.filepath);
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
            }
        }

        private void pathTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13))
            {
                try
                {
                    DirectoryInfo newDir = new DirectoryInfo(pathTextBox.Text);
                    updater.refreshListView(newDir);
                }
                catch (Exception f)
                {
                    Console.WriteLine(f.Message);
                }

            }
        }

        private void goToPathButton_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo newDir = new DirectoryInfo(pathTextBox.Text);
                updater.refreshListView(newDir);
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
            }
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13))
            {
                try
                {
                    updater.Search();
                    filesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                catch (Exception f)
                {
                    Console.WriteLine("The process failed: {0}", f.ToString());
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                updater.Search();
                filesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception f)
            {
                Console.WriteLine("The process failed: {0}", f.ToString());
            }
        }

        private void filesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (filesListView.FocusedItem.SubItems[1].Text == "Directory")
                {
                    DirectoryInfo targetdir = new DirectoryInfo(filesListView.FocusedItem.SubItems[4].Text);
                    selected = new SelectedItem(targetdir);

                }
                else
                {
                    FileInfo targetfile = new FileInfo(filesListView.FocusedItem.SubItems[5].Text);
                    selected = new SelectedItem(targetfile);

                }
                pathTextBox.Text = selected.filepath;
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
            }
        }

        private void compressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo targetFile = new FileInfo(selected.filepath);
            if (selected.IsItDir())
                fileOperator.Compress(new DirectoryInfo(targetFile.FullName));
            else
                fileOperator.Compress(targetFile);

            updater.refreshListView(new DirectoryInfo(targetFile.DirectoryName));
        }

        private void decompressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo targetFile = new FileInfo(selected.filepath);
            if (selected.IsItDir())
                fileOperator.Decompress(new DirectoryInfo(targetFile.FullName));
            else
                fileOperator.Decompress(targetFile);
            updater.refreshListView(new DirectoryInfo(targetFile.DirectoryName));
        }

        private DirectoryInfo GetCurrentDirectory()
        {
            return new DirectoryInfo(selected.filepath);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected.Delete();
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo targetFile = new FileInfo(selected.filepath);
            fileOperator.Encrypt(targetFile);
        }
    }
}
