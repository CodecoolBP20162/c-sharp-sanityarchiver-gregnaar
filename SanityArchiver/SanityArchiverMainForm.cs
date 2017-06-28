using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanityArchiver
{
    public partial class SanityArchiver : Form
    {
        static string filePath;
        static ListViewItem selectedItem;

        public SanityArchiver()
        {
            InitializeComponent();
            PopulateTreeView(@"C:\");
            PopulateTreeView(@"E:\");
        }

        private void PopulateTreeView(string destination)
        {
            TreeNode rootNode;
            try
            {
                DirectoryInfo info = new DirectoryInfo(destination);
                if (info.Exists)
                {
                    rootNode = new TreeNode(info.Name);
                    rootNode.Tag = info;
                    GetDirectories(info.GetDirectories(), rootNode);
                    FilesTreeView.Nodes.Add(rootNode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            try
            {
                TreeNode aNode;
                DirectoryInfo[] subSubDirs;
                foreach (DirectoryInfo subDir in subDirs)
                {
                    aNode = new TreeNode(subDir.Name, 0, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode);
                    }
                    nodeToAddTo.Nodes.Add(aNode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void FilesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            refreshListView(nodeDirInfo);
        }



        private void refreshListView(DirectoryInfo newDir)
        {
            pathTextBox.Text = newDir.ToString();
            filesListView.Items.Clear();
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;
            foreach (DirectoryInfo dir in newDir.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item, "Directory"),
                          new ListViewItem.ListViewSubItem(item,
                          dir.LastAccessTime.ToShortDateString()),
                          new ListViewItem.ListViewSubItem(item, ""),
                          new ListViewItem.ListViewSubItem(item, dir.FullName) };
                item.SubItems.AddRange(subItems);
                filesListView.Items.Add(item);
            }
            foreach (FileInfo file in newDir.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, "File"),
                            new ListViewItem.ListViewSubItem(item,
                            file.LastAccessTime.ToShortDateString()),
                            new ListViewItem.ListViewSubItem(item, (file.Length/1024).ToString()+" MB"),
                            new ListViewItem.ListViewSubItem(item, file.FullName)};

                item.SubItems.AddRange(subItems);
                filesListView.Items.Add(item);
            }

            filesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void goToFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filesListView.Items.Clear();
            DirectoryInfo newDir = new DirectoryInfo(filePath);
            refreshListView(newDir);
        }

        private void filesListView_DoubleClick(object sender, EventArgs e)
        {
            if (selectedItem.SubItems[1].Text == "Directory")
            {
                DirectoryInfo newDir = new DirectoryInfo(filePath);
                refreshListView(newDir);
            }
            else
            {
                Process.Start(filePath);
            }
        }

        private void pathTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13))
            {
                try
                {
                    DirectoryInfo newDir = new DirectoryInfo(pathTextBox.Text);
                    refreshListView(newDir);
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
                refreshListView(newDir);
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
                    string[] dirs = Directory.GetFiles(pathTextBox.Text, searchTextBox.Text);
                    ListViewItem.ListViewSubItem[] subItems;
                    ListViewItem item = null;
                    filesListView.Items.Clear();

                    foreach (string dir in dirs)
                    {
                        Console.WriteLine(dir);
                        FileInfo file = new FileInfo(dir);
                        //refreshListView(new DirectoryInfo(file.Directory.ToString()));

                        item = new ListViewItem(file.Name, 1);
                        subItems = new ListViewItem.ListViewSubItem[]
                                    { new ListViewItem.ListViewSubItem(item, "File"),
                        new ListViewItem.ListViewSubItem(item,
                        file.LastAccessTime.ToShortDateString()),
                        new ListViewItem.ListViewSubItem(item, (file.Length/1024).ToString()+" MB"),
                        new ListViewItem.ListViewSubItem(item, file.FullName)};

                        item.SubItems.AddRange(subItems);
                        filesListView.Items.Add(item);



                    }
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
                string[] dirs = Directory.GetFiles(pathTextBox.Text, searchTextBox.Text);
                ListViewItem.ListViewSubItem[] subItems;
                ListViewItem item = null;
                filesListView.Items.Clear();

                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                    FileInfo file = new FileInfo(dir);
                    //refreshListView(new DirectoryInfo(file.Directory.ToString()));

                    item = new ListViewItem(file.Name, 1);
                    subItems = new ListViewItem.ListViewSubItem[]
                                { new ListViewItem.ListViewSubItem(item, "File"),
                        new ListViewItem.ListViewSubItem(item,
                        file.LastAccessTime.ToShortDateString()),
                        new ListViewItem.ListViewSubItem(item, (file.Length/1024).ToString()+" MB"),
                        new ListViewItem.ListViewSubItem(item, file.FullName)};

                    item.SubItems.AddRange(subItems);
                    filesListView.Items.Add(item);



                }
                filesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception f)
            {
                Console.WriteLine("The process failed: {0}", f.ToString());
            }
        }

        private void filesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            filePath = filesListView.FocusedItem.SubItems[4].Text;
            selectedItem = filesListView.FocusedItem;
        }
    }
}
