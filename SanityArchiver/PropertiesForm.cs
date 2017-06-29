using SanityArchiverLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanityArchiver
{
    public partial class PropertiesForm : Form
    {
        public SelectedItem selected { get; set; }
        public FileOperation fileOperator = new FileOperation();
        public PropertiesForm(SelectedItem sel)
        {
            InitializeComponent();
            selected = sel;
        }

        private void PropertiesForm_Load(object sender, EventArgs e)
        {
            if (selected.IsItDirectory())
            {
                this.textBoxName.Text = selected.dir.Name;
                this.textBoxSize.Text = fileOperator.GetFolderSize(selected.dir)+" MB";
            }
            else
            {
                this.textBoxName.Text = selected.file.Name;
                this.textBoxSize.Text = (selected.file.Length / 1024).ToString() + " MB";
                this.checkBoxReadOnly.Checked = selected.file.IsReadOnly;
                //selected.file.GetAccessControl.Hide();
            }
        }

        private void checkBoxReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxReadOnly.Checked == true)
                {
                    checkBoxReadOnly.Checked = false;
                    selected.file.IsReadOnly = false;
                }
                else
                {
                    checkBoxReadOnly.Checked = true;
                    selected.file.IsReadOnly = true;
                }
            }
            catch(Exception f)
            {
                Console.WriteLine(f.Message);
            }
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            string newName = textBoxName.Text;
            if (selected.IsItDirectory())
                fileOperator.Rename(selected.dir, newName);
            else
                fileOperator.Rename(selected.file, newName);
        }
    }
}
