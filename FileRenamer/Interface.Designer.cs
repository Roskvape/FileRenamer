using System;

namespace FileRenamer
{
    partial class Interface
    {
        public bool IsNewFileName
        {
            get { return this.cbNewName.Checked; }
        }

        public string NewFileName
        {
            get { return this.txtNewFileName.Text; }
        }

        public bool IsNewFileExt
        {
            get { return this.cbNewFileExtension.Checked; }
        }

        public string NewFileExt
        {
            get { return this.txtNewFileExtension.Text; }
        }

        public string sSourcePath = "I'm not yet initialized. :P";

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlOptions1 = new System.Windows.Forms.Panel();
            this.gBoxTypes = new System.Windows.Forms.GroupBox();
            this.cbNewName = new System.Windows.Forms.CheckBox();
            this.lblReplace = new System.Windows.Forms.Label();
            this.rbFindAndReplace = new System.Windows.Forms.RadioButton();
            this.rbNewFileName = new System.Windows.Forms.RadioButton();
            this.lblReplaceWith = new System.Windows.Forms.Label();
            this.txtReplaceWith = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.txtNewFileName = new System.Windows.Forms.TextBox();
            this.cbNewFileExtension = new System.Windows.Forms.CheckBox();
            this.txtNewFileExtension = new System.Windows.Forms.TextBox();
            this.lblSourcePathSelection = new System.Windows.Forms.Label();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.pnlOptions2 = new System.Windows.Forms.Panel();
            this.gBoxCounter = new System.Windows.Forms.GroupBox();
            this.txtCounterToken = new System.Windows.Forms.TextBox();
            this.lblCounterToken = new System.Windows.Forms.Label();
            this.lblCounterDigits = new System.Windows.Forms.Label();
            this.nupCounterDigits = new System.Windows.Forms.NumericUpDown();
            this.lblCounterIncrement = new System.Windows.Forms.Label();
            this.lblCounterStartAt = new System.Windows.Forms.Label();
            this.nupCounterIncrement = new System.Windows.Forms.NumericUpDown();
            this.nupCounterStartAt = new System.Windows.Forms.NumericUpDown();
            this.lblOptions = new System.Windows.Forms.Label();
            this.pnlSource = new System.Windows.Forms.Panel();
            this.lblSourcePath = new System.Windows.Forms.Label();
            this.lblSourceTreeView = new System.Windows.Forms.Label();
            this.treeViewSource = new System.Windows.Forms.TreeView();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.lblPreviewPath = new System.Windows.Forms.Label();
            this.lblPreviewTreeView = new System.Windows.Forms.Label();
            this.treeViewPreview = new System.Windows.Forms.TreeView();
            this.folderBrowseSource = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlOptions1.SuspendLayout();
            this.gBoxTypes.SuspendLayout();
            this.pnlOptions2.SuspendLayout();
            this.gBoxCounter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupCounterDigits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCounterIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCounterStartAt)).BeginInit();
            this.pnlSource.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pnlOptions1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlOptions2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlSource, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlPreview, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.8568F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.1432F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlOptions1
            // 
            this.pnlOptions1.Controls.Add(this.gBoxTypes);
            this.pnlOptions1.Controls.Add(this.lblSourcePathSelection);
            this.pnlOptions1.Controls.Add(this.txtSourcePath);
            this.pnlOptions1.Controls.Add(this.btnBrowseSource);
            this.pnlOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOptions1.Location = new System.Drawing.Point(3, 3);
            this.pnlOptions1.Name = "pnlOptions1";
            this.pnlOptions1.Size = new System.Drawing.Size(394, 161);
            this.pnlOptions1.TabIndex = 3;
            // 
            // gBoxTypes
            // 
            this.gBoxTypes.Controls.Add(this.cbNewName);
            this.gBoxTypes.Controls.Add(this.lblReplace);
            this.gBoxTypes.Controls.Add(this.rbFindAndReplace);
            this.gBoxTypes.Controls.Add(this.rbNewFileName);
            this.gBoxTypes.Controls.Add(this.lblReplaceWith);
            this.gBoxTypes.Controls.Add(this.txtReplaceWith);
            this.gBoxTypes.Controls.Add(this.txtReplace);
            this.gBoxTypes.Controls.Add(this.txtNewFileName);
            this.gBoxTypes.Controls.Add(this.cbNewFileExtension);
            this.gBoxTypes.Controls.Add(this.txtNewFileExtension);
            this.gBoxTypes.Location = new System.Drawing.Point(3, 45);
            this.gBoxTypes.Name = "gBoxTypes";
            this.gBoxTypes.Size = new System.Drawing.Size(388, 113);
            this.gBoxTypes.TabIndex = 9;
            this.gBoxTypes.TabStop = false;
            this.gBoxTypes.Text = "Rename Type";
            // 
            // cbNewName
            // 
            this.cbNewName.AutoSize = true;
            this.cbNewName.Enabled = false;
            this.cbNewName.Location = new System.Drawing.Point(3, 38);
            this.cbNewName.Name = "cbNewName";
            this.cbNewName.Size = new System.Drawing.Size(54, 17);
            this.cbNewName.TabIndex = 15;
            this.cbNewName.Text = "Name";
            this.cbNewName.UseVisualStyleBackColor = true;
            this.cbNewName.CheckedChanged += new System.EventHandler(this.cbNewName_CheckedChanged);
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(187, 39);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(50, 13);
            this.lblReplace.TabIndex = 14;
            this.lblReplace.Text = "Replace:";
            // 
            // rbFindAndReplace
            // 
            this.rbFindAndReplace.AutoSize = true;
            this.rbFindAndReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFindAndReplace.Location = new System.Drawing.Point(190, 16);
            this.rbFindAndReplace.Name = "rbFindAndReplace";
            this.rbFindAndReplace.Size = new System.Drawing.Size(125, 17);
            this.rbFindAndReplace.TabIndex = 13;
            this.rbFindAndReplace.Text = "Find and Replace";
            this.rbFindAndReplace.UseVisualStyleBackColor = true;
            this.rbFindAndReplace.CheckedChanged += new System.EventHandler(this.rbFindAndReplace_CheckedChanged);
            // 
            // rbNewFileName
            // 
            this.rbNewFileName.AutoSize = true;
            this.rbNewFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNewFileName.Location = new System.Drawing.Point(3, 15);
            this.rbNewFileName.Name = "rbNewFileName";
            this.rbNewFileName.Size = new System.Drawing.Size(110, 17);
            this.rbNewFileName.TabIndex = 0;
            this.rbNewFileName.Text = "New File Name";
            this.rbNewFileName.UseVisualStyleBackColor = true;
            this.rbNewFileName.CheckedChanged += new System.EventHandler(this.rbNewFileName_CheckedChanged);
            // 
            // lblReplaceWith
            // 
            this.lblReplaceWith.AutoSize = true;
            this.lblReplaceWith.Location = new System.Drawing.Point(187, 65);
            this.lblReplaceWith.Name = "lblReplaceWith";
            this.lblReplaceWith.Size = new System.Drawing.Size(32, 13);
            this.lblReplaceWith.TabIndex = 12;
            this.lblReplaceWith.Text = "With:";
            // 
            // txtReplaceWith
            // 
            this.txtReplaceWith.Enabled = false;
            this.txtReplaceWith.Location = new System.Drawing.Point(225, 62);
            this.txtReplaceWith.Name = "txtReplaceWith";
            this.txtReplaceWith.Size = new System.Drawing.Size(154, 20);
            this.txtReplaceWith.TabIndex = 11;
            this.txtReplaceWith.TextChanged += new System.EventHandler(this.txtReplaceWith_TextChanged);
            // 
            // txtReplace
            // 
            this.txtReplace.Enabled = false;
            this.txtReplace.Location = new System.Drawing.Point(243, 36);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(136, 20);
            this.txtReplace.TabIndex = 9;
            this.txtReplace.TextChanged += new System.EventHandler(this.txtReplace_TextChanged);
            // 
            // txtNewFileName
            // 
            this.txtNewFileName.Enabled = false;
            this.txtNewFileName.Location = new System.Drawing.Point(3, 58);
            this.txtNewFileName.Name = "txtNewFileName";
            this.txtNewFileName.Size = new System.Drawing.Size(106, 20);
            this.txtNewFileName.TabIndex = 1;
            this.txtNewFileName.Text = "NewFileName_:#";
            this.txtNewFileName.TextChanged += new System.EventHandler(this.txtNewFileName_TextChanged);
            // 
            // cbNewFileExtension
            // 
            this.cbNewFileExtension.AutoSize = true;
            this.cbNewFileExtension.Enabled = false;
            this.cbNewFileExtension.Location = new System.Drawing.Point(115, 38);
            this.cbNewFileExtension.Name = "cbNewFileExtension";
            this.cbNewFileExtension.Size = new System.Drawing.Size(44, 17);
            this.cbNewFileExtension.TabIndex = 8;
            this.cbNewFileExtension.Text = "Ext.";
            this.cbNewFileExtension.UseVisualStyleBackColor = true;
            this.cbNewFileExtension.CheckedChanged += new System.EventHandler(this.cbNewFileExtension_CheckedChanged);
            // 
            // txtNewFileExtension
            // 
            this.txtNewFileExtension.Enabled = false;
            this.txtNewFileExtension.Location = new System.Drawing.Point(115, 58);
            this.txtNewFileExtension.Name = "txtNewFileExtension";
            this.txtNewFileExtension.Size = new System.Drawing.Size(37, 20);
            this.txtNewFileExtension.TabIndex = 7;
            this.txtNewFileExtension.Text = "jpg";
            this.txtNewFileExtension.TextChanged += new System.EventHandler(this.txtNewFileExtension_TextChanged);
            // 
            // lblSourcePathSelection
            // 
            this.lblSourcePathSelection.AutoSize = true;
            this.lblSourcePathSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourcePathSelection.Location = new System.Drawing.Point(0, 0);
            this.lblSourcePathSelection.Name = "lblSourcePathSelection";
            this.lblSourcePathSelection.Size = new System.Drawing.Size(161, 15);
            this.lblSourcePathSelection.TabIndex = 4;
            this.lblSourcePathSelection.Text = "Select Source Directory:";
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Location = new System.Drawing.Point(3, 18);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(307, 20);
            this.txtSourcePath.TabIndex = 1;
            this.txtSourcePath.TextChanged += new System.EventHandler(this.txtSourcePath_TextChanged);
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Location = new System.Drawing.Point(316, 16);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSource.TabIndex = 1;
            this.btnBrowseSource.Text = "Browse";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // pnlOptions2
            // 
            this.pnlOptions2.Controls.Add(this.gBoxCounter);
            this.pnlOptions2.Controls.Add(this.lblOptions);
            this.pnlOptions2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOptions2.Location = new System.Drawing.Point(403, 3);
            this.pnlOptions2.Name = "pnlOptions2";
            this.pnlOptions2.Size = new System.Drawing.Size(394, 161);
            this.pnlOptions2.TabIndex = 4;
            // 
            // gBoxCounter
            // 
            this.gBoxCounter.Controls.Add(this.txtCounterToken);
            this.gBoxCounter.Controls.Add(this.lblCounterToken);
            this.gBoxCounter.Controls.Add(this.lblCounterDigits);
            this.gBoxCounter.Controls.Add(this.nupCounterDigits);
            this.gBoxCounter.Controls.Add(this.lblCounterIncrement);
            this.gBoxCounter.Controls.Add(this.lblCounterStartAt);
            this.gBoxCounter.Controls.Add(this.nupCounterIncrement);
            this.gBoxCounter.Controls.Add(this.nupCounterStartAt);
            this.gBoxCounter.Location = new System.Drawing.Point(0, 23);
            this.gBoxCounter.Name = "gBoxCounter";
            this.gBoxCounter.Size = new System.Drawing.Size(124, 135);
            this.gBoxCounter.TabIndex = 17;
            this.gBoxCounter.TabStop = false;
            this.gBoxCounter.Text = "Counter";
            // 
            // txtCounterToken
            // 
            this.txtCounterToken.Enabled = false;
            this.txtCounterToken.Location = new System.Drawing.Point(66, 20);
            this.txtCounterToken.Name = "txtCounterToken";
            this.txtCounterToken.Size = new System.Drawing.Size(47, 20);
            this.txtCounterToken.TabIndex = 18;
            this.txtCounterToken.Text = ":#";
            // 
            // lblCounterToken
            // 
            this.lblCounterToken.AutoSize = true;
            this.lblCounterToken.Location = new System.Drawing.Point(6, 23);
            this.lblCounterToken.Name = "lblCounterToken";
            this.lblCounterToken.Size = new System.Drawing.Size(41, 13);
            this.lblCounterToken.TabIndex = 17;
            this.lblCounterToken.Text = "Token:";
            // 
            // lblCounterDigits
            // 
            this.lblCounterDigits.AutoSize = true;
            this.lblCounterDigits.Location = new System.Drawing.Point(6, 46);
            this.lblCounterDigits.Name = "lblCounterDigits";
            this.lblCounterDigits.Size = new System.Drawing.Size(36, 13);
            this.lblCounterDigits.TabIndex = 12;
            this.lblCounterDigits.Text = "Digits:";
            this.lblCounterDigits.Click += new System.EventHandler(this.label1_Click);
            // 
            // nupCounterDigits
            // 
            this.nupCounterDigits.Location = new System.Drawing.Point(66, 44);
            this.nupCounterDigits.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nupCounterDigits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCounterDigits.Name = "nupCounterDigits";
            this.nupCounterDigits.Size = new System.Drawing.Size(47, 20);
            this.nupCounterDigits.TabIndex = 11;
            this.nupCounterDigits.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCounterDigits.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // lblCounterIncrement
            // 
            this.lblCounterIncrement.AutoSize = true;
            this.lblCounterIncrement.Location = new System.Drawing.Point(6, 72);
            this.lblCounterIncrement.Name = "lblCounterIncrement";
            this.lblCounterIncrement.Size = new System.Drawing.Size(57, 13);
            this.lblCounterIncrement.TabIndex = 14;
            this.lblCounterIncrement.Text = "Increment:";
            // 
            // lblCounterStartAt
            // 
            this.lblCounterStartAt.AutoSize = true;
            this.lblCounterStartAt.Location = new System.Drawing.Point(6, 98);
            this.lblCounterStartAt.Name = "lblCounterStartAt";
            this.lblCounterStartAt.Size = new System.Drawing.Size(45, 13);
            this.lblCounterStartAt.TabIndex = 16;
            this.lblCounterStartAt.Text = "Start At:";
            // 
            // nupCounterIncrement
            // 
            this.nupCounterIncrement.Location = new System.Drawing.Point(66, 70);
            this.nupCounterIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCounterIncrement.Name = "nupCounterIncrement";
            this.nupCounterIncrement.Size = new System.Drawing.Size(47, 20);
            this.nupCounterIncrement.TabIndex = 13;
            this.nupCounterIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nupCounterStartAt
            // 
            this.nupCounterStartAt.Location = new System.Drawing.Point(66, 96);
            this.nupCounterStartAt.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.nupCounterStartAt.Name = "nupCounterStartAt";
            this.nupCounterStartAt.Size = new System.Drawing.Size(47, 20);
            this.nupCounterStartAt.TabIndex = 15;
            this.nupCounterStartAt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblOptions
            // 
            this.lblOptions.AutoSize = true;
            this.lblOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptions.Location = new System.Drawing.Point(0, 0);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(60, 15);
            this.lblOptions.TabIndex = 10;
            this.lblOptions.Text = "Options:";
            // 
            // pnlSource
            // 
            this.pnlSource.Controls.Add(this.lblSourcePath);
            this.pnlSource.Controls.Add(this.lblSourceTreeView);
            this.pnlSource.Controls.Add(this.treeViewSource);
            this.pnlSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSource.Location = new System.Drawing.Point(3, 170);
            this.pnlSource.Name = "pnlSource";
            this.pnlSource.Size = new System.Drawing.Size(394, 246);
            this.pnlSource.TabIndex = 5;
            // 
            // lblSourcePath
            // 
            this.lblSourcePath.AutoSize = true;
            this.lblSourcePath.Location = new System.Drawing.Point(3, 15);
            this.lblSourcePath.Name = "lblSourcePath";
            this.lblSourcePath.Size = new System.Drawing.Size(0, 13);
            this.lblSourcePath.TabIndex = 7;
            // 
            // lblSourceTreeView
            // 
            this.lblSourceTreeView.AutoSize = true;
            this.lblSourceTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSourceTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceTreeView.Location = new System.Drawing.Point(0, 0);
            this.lblSourceTreeView.Name = "lblSourceTreeView";
            this.lblSourceTreeView.Size = new System.Drawing.Size(56, 15);
            this.lblSourceTreeView.TabIndex = 5;
            this.lblSourceTreeView.Text = "Source:";
            // 
            // treeViewSource
            // 
            this.treeViewSource.CheckBoxes = true;
            this.treeViewSource.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.treeViewSource.Location = new System.Drawing.Point(0, 31);
            this.treeViewSource.Name = "treeViewSource";
            this.treeViewSource.Size = new System.Drawing.Size(394, 215);
            this.treeViewSource.TabIndex = 2;
            this.treeViewSource.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSource_AfterCheck);
            // 
            // pnlPreview
            // 
            this.pnlPreview.Controls.Add(this.lblPreviewPath);
            this.pnlPreview.Controls.Add(this.lblPreviewTreeView);
            this.pnlPreview.Controls.Add(this.treeViewPreview);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(403, 170);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(394, 246);
            this.pnlPreview.TabIndex = 6;
            // 
            // lblPreviewPath
            // 
            this.lblPreviewPath.AutoSize = true;
            this.lblPreviewPath.Location = new System.Drawing.Point(3, 15);
            this.lblPreviewPath.Name = "lblPreviewPath";
            this.lblPreviewPath.Size = new System.Drawing.Size(35, 13);
            this.lblPreviewPath.TabIndex = 7;
            this.lblPreviewPath.Text = "label1";
            // 
            // lblPreviewTreeView
            // 
            this.lblPreviewTreeView.AutoSize = true;
            this.lblPreviewTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPreviewTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewTreeView.Location = new System.Drawing.Point(0, 0);
            this.lblPreviewTreeView.Name = "lblPreviewTreeView";
            this.lblPreviewTreeView.Size = new System.Drawing.Size(57, 15);
            this.lblPreviewTreeView.TabIndex = 6;
            this.lblPreviewTreeView.Text = "Preview";
            // 
            // treeViewPreview
            // 
            this.treeViewPreview.CheckBoxes = true;
            this.treeViewPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.treeViewPreview.Location = new System.Drawing.Point(0, 31);
            this.treeViewPreview.Name = "treeViewPreview";
            this.treeViewPreview.Size = new System.Drawing.Size(394, 215);
            this.treeViewPreview.TabIndex = 3;
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Interface";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlOptions1.ResumeLayout(false);
            this.pnlOptions1.PerformLayout();
            this.gBoxTypes.ResumeLayout(false);
            this.gBoxTypes.PerformLayout();
            this.pnlOptions2.ResumeLayout(false);
            this.pnlOptions2.PerformLayout();
            this.gBoxCounter.ResumeLayout(false);
            this.gBoxCounter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupCounterDigits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCounterIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCounterStartAt)).EndInit();
            this.pnlSource.ResumeLayout(false);
            this.pnlSource.PerformLayout();
            this.pnlPreview.ResumeLayout(false);
            this.pnlPreview.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowseSource;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.TreeView treeViewSource;
        private System.Windows.Forms.Panel pnlOptions1;
        private System.Windows.Forms.Panel pnlOptions2;
        private System.Windows.Forms.Panel pnlSource;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.TreeView treeViewPreview;
        private System.Windows.Forms.Label lblSourcePathSelection;
        private System.Windows.Forms.Label lblSourcePath;
        private System.Windows.Forms.Label lblSourceTreeView;
        private System.Windows.Forms.Label lblPreviewPath;
        private System.Windows.Forms.Label lblPreviewTreeView;
        private System.Windows.Forms.TextBox txtNewFileName;
        private System.Windows.Forms.CheckBox cbNewFileExtension;
        private System.Windows.Forms.TextBox txtNewFileExtension;
        private System.Windows.Forms.GroupBox gBoxTypes;
        private System.Windows.Forms.Label lblReplaceWith;
        private System.Windows.Forms.TextBox txtReplaceWith;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.RadioButton rbFindAndReplace;
        private System.Windows.Forms.RadioButton rbNewFileName;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.CheckBox cbNewName;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.NumericUpDown nupCounterDigits;
        private System.Windows.Forms.Label lblCounterDigits;
        private System.Windows.Forms.Label lblCounterStartAt;
        private System.Windows.Forms.NumericUpDown nupCounterStartAt;
        private System.Windows.Forms.Label lblCounterIncrement;
        private System.Windows.Forms.NumericUpDown nupCounterIncrement;
        private System.Windows.Forms.GroupBox gBoxCounter;
        private System.Windows.Forms.TextBox txtCounterToken;
        private System.Windows.Forms.Label lblCounterToken;
    }
}

