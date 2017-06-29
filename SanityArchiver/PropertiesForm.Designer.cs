namespace SanityArchiver
{
    partial class PropertiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelHidden = new System.Windows.Forms.Label();
            this.labelReadOnly = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.checkBoxHidden = new System.Windows.Forms.CheckBox();
            this.checkBoxReadOnly = new System.Windows.Forms.CheckBox();
            this.buttonRename = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(22, 32);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(25, 81);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(30, 13);
            this.labelSize.TabIndex = 1;
            this.labelSize.Text = "Size:";
            // 
            // labelHidden
            // 
            this.labelHidden.AutoSize = true;
            this.labelHidden.Location = new System.Drawing.Point(25, 124);
            this.labelHidden.Name = "labelHidden";
            this.labelHidden.Size = new System.Drawing.Size(44, 13);
            this.labelHidden.TabIndex = 2;
            this.labelHidden.Text = "Hidden:";
            // 
            // labelReadOnly
            // 
            this.labelReadOnly.AutoSize = true;
            this.labelReadOnly.Location = new System.Drawing.Point(25, 165);
            this.labelReadOnly.Name = "labelReadOnly";
            this.labelReadOnly.Size = new System.Drawing.Size(60, 13);
            this.labelReadOnly.TabIndex = 3;
            this.labelReadOnly.Text = "Read-Only:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(84, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 4;
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(84, 73);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.ReadOnly = true;
            this.textBoxSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxSize.TabIndex = 5;
            // 
            // checkBoxHidden
            // 
            this.checkBoxHidden.AutoSize = true;
            this.checkBoxHidden.Location = new System.Drawing.Point(102, 124);
            this.checkBoxHidden.Name = "checkBoxHidden";
            this.checkBoxHidden.Size = new System.Drawing.Size(15, 14);
            this.checkBoxHidden.TabIndex = 6;
            this.checkBoxHidden.UseVisualStyleBackColor = true;
            // 
            // checkBoxReadOnly
            // 
            this.checkBoxReadOnly.AutoSize = true;
            this.checkBoxReadOnly.Location = new System.Drawing.Point(102, 160);
            this.checkBoxReadOnly.Name = "checkBoxReadOnly";
            this.checkBoxReadOnly.Size = new System.Drawing.Size(15, 14);
            this.checkBoxReadOnly.TabIndex = 7;
            this.checkBoxReadOnly.UseVisualStyleBackColor = true;
            this.checkBoxReadOnly.CheckedChanged += new System.EventHandler(this.checkBoxReadOnly_CheckedChanged);
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(225, 24);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(75, 23);
            this.buttonRename.TabIndex = 8;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 324);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.checkBoxReadOnly);
            this.Controls.Add(this.checkBoxHidden);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelReadOnly);
            this.Controls.Add(this.labelHidden);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.labelName);
            this.Name = "PropertiesForm";
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.PropertiesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelHidden;
        private System.Windows.Forms.Label labelReadOnly;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.CheckBox checkBoxHidden;
        private System.Windows.Forms.CheckBox checkBoxReadOnly;
        private System.Windows.Forms.Button buttonRename;
    }
}