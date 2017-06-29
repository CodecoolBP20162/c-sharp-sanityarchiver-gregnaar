using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SanityArchiver
{
    public partial class SanityArchiverForm : Form
    {
        FileOperation fileOperator = new FileOperation();
        History history = History.Singleton;
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

            DirectoryInfo nodeDirInfo = new DirectoryInfo(newSelected.FullPath);
            updater.refreshListView(nodeDirInfo);
        }

        private void goToFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filesListView.Items.Clear();
            DirectoryInfo newDir = new DirectoryInfo(selected.filePath);
            updater.refreshListView(newDir);
        }

        private void filesListView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (selected.IsItDirectory())
                    updater.refreshListView(new DirectoryInfo(selected.filePath));
                else
                    Process.Start(selected.filePath);
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
                    Console.WriteLine(f.Message);
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
                    FileInfo targetfile = new FileInfo(filesListView.FocusedItem.SubItems[4].Text);
                    selected = new SelectedItem(targetfile);

                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
            }
        }

        private void compressToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (selected.GetDir() != null)
                fileOperator.Compress(selected.GetDir());
            else
                fileOperator.Compress(selected.GetFIle());

            updater.ReloadContent();
        }

        private void decompressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo targetFile = new FileInfo(selected.filePath);
            if (selected.IsItDirectory())
                fileOperator.Decompress(new DirectoryInfo(targetFile.FullName));
            else
                fileOperator.Decompress(targetFile);
            updater.ReloadContent();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected.Delete();
            updater.ReloadContent();
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo targetFile = new FileInfo(selected.filePath);
            fileOperator.Encrypt(targetFile);
        }

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo targetFile = new FileInfo(selected.filePath);
            fileOperator.Decrypt(targetFile);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Properties = new PropertiesForm(selected);
            Properties.ShowDialog();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            updater.refreshListView(new DirectoryInfo(history.GoBackInHistory()),true);
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileOperator.CreateNewFolder(SelectedItem.currentDir);
            updater.ReloadContent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileOperator.CreateNewFile(SelectedItem.currentDir);
            updater.ReloadContent();
        }
    }
}
