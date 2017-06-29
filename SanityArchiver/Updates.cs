using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanityArchiverLogic
{
    class Updates
    {
        ListView filesListView;
        TreeView filesTreeView;
        TextBox pathTextBox;
        TextBox searchTextBox;

        Updates()
        {
        }

        public Updates(ListView lv, TreeView tv, TextBox tb, TextBox ts)
        {
            filesListView = lv;
            filesTreeView = tv;
            pathTextBox = tb;
            searchTextBox = ts;
        }
        public void refreshListView(DirectoryInfo newDir)
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
                          new ListViewItem.ListViewSubItem(item, dir.FullName),
                          new ListViewItem.ListViewSubItem(item, dir.Parent.ToString())};
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
                            new ListViewItem.ListViewSubItem(item, file.FullName),
                            new ListViewItem.ListViewSubItem(item, file.Directory.FullName)};


                item.SubItems.AddRange(subItems);
                filesListView.Items.Add(item);
            }

            filesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        public void PopulateTreeView(string destination)
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
                    filesTreeView.Nodes.Add(rootNode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            
                TreeNode aNode;
                DirectoryInfo[] subSubDirs;
                foreach (DirectoryInfo subDir in subDirs)
                {
                try
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Search()
        {
            string[] dirs = Directory.GetFiles(pathTextBox.Text, searchTextBox.Text);
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;
            filesListView.Items.Clear();

            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                FileInfo file = new FileInfo(dir);

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
        }

    }
}
