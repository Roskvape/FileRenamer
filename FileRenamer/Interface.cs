using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace FileRenamer
{
    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            if (folderBrowseSource.ShowDialog() == DialogResult.OK)
            {
                txtSourcePath.Text = folderBrowseSource.SelectedPath;
                sSourcePath = txtSourcePath.Text;
                ListDirectory(treeViewSource, sSourcePath);
            }
        }

        private void txtSourcePath_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(txtSourcePath.Text))
            {
                sSourcePath = txtSourcePath.Text;
                ListDirectory(treeViewSource, sSourcePath);
                lblSourcePath.Text = txtSourcePath.Text;
            }
            else
            {
                treeViewSource.Nodes.Clear();
                treeViewSource.Nodes.Add(new TreeNode("Directory not found."));
            }
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add((CreateDirectoryNode(rootDirectoryInfo)));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(CreateDirectoryNode((directory)));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            }

            return directoryNode;
        }

        private void treeViewSource_AfterCheck(object sender, TreeViewEventArgs e)
        {
            UpdatePreview(sender, e);
        }

        private void UpdatePreview(object sender, EventArgs e)
        {
            treeViewPreview.Nodes.Clear();
            GetCheckedNodes(treeViewSource.Nodes, treeViewPreview.Nodes);
        }

        private string CheckTokens(string input)
        {
            string output = input;
            if (input.Contains(":#"))
            {
                output = Regex.Replace(input, @":#", "001");
            }

            return output;
        }

        public void GetCheckedNodes(TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        {
            foreach (System.Windows.Forms.TreeNode aNode in nodesSource)
            {
                TreeNode bNode = new TreeNode(aNode.Text);
                nodesPreview.Add(bNode);

                if (aNode.Checked)
                {
                    bNode.Checked = true;
                    bNode.ForeColor = Color.Black;
                    bNode.BackColor = Color.PaleGreen;
                    if (rbFindAndReplace.Checked)
                    {
                        string newText = new RegexMethods().FindAndReplace(bNode.Text, txtReplace.Text, txtReplaceWith.Text);
                        bNode.Text = CheckTokens(newText);
                    }
                    else if (rbNewFileName.Checked)
                    {
                        string newText = new RegexMethods().NewFileName(this, bNode.Text);
                        bNode.Text = CheckTokens(newText);
                        //bNode.Text = new RegexMethods().NewFileName(this, bNode.Text);
                        //string newText = CheckTokens(txtNewFileName.Text));
                        //if (bNode.PrevNode != null && bNode.Text == bNode.PrevNode.Text)
                        //{
                        //    bNode.Text = bNode.Text + "x";
                        //}
                    }
                }
                else
                {
                    bNode.ForeColor = Color.Gray;
                }

                if (aNode.Nodes.Count != 0)
                {
                    GetCheckedNodes(aNode.Nodes, bNode.Nodes);
                    if (aNode.IsExpanded)
                    {
                        bNode.Expand();
                    }
                }

            }
        }

        #region Find and Replace
        private void rbFindAndReplace_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview(sender, e);

            if (rbFindAndReplace.Checked)
            {
                txtReplace.Enabled = true;
                txtReplaceWith.Enabled = true;
            }
            else
            {
                txtReplace.Enabled = false;
                txtReplaceWith.Enabled = false;
            }
        }

        private void txtReplace_TextChanged(object sender, EventArgs e)
        {
            if (rbFindAndReplace.Checked)
            {
                UpdatePreview(sender, e);
            }
        }

        private void txtReplaceWith_TextChanged(object sender, EventArgs e)
        {
            if (rbFindAndReplace.Checked)
            {
                UpdatePreview(sender, e);
            }
        }
        #endregion

        #region New File Name

        private void rbNewFileName_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview(sender, e);

            if (rbNewFileName.Checked)
            {
                cbNewName.Enabled = true;
                cbNewFileExtension.Enabled = true;
                txtNewFileName.Enabled = true;
                txtNewFileExtension.Enabled = true;
            }
            else
            {
                cbNewName.Enabled = false;
                cbNewFileExtension.Enabled = false;
                txtNewFileName.Enabled = false;
                txtNewFileExtension.Enabled = false;
            }
        }
        private void cbNewFileExtension_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview(sender, e);

            if (cbNewFileExtension.Checked)
            {
                txtNewFileExtension.Enabled = true;
            }
            else
            {
                txtNewFileExtension.Enabled = false;
            }
        }

        #endregion

        private void txtNewFileName_TextChanged(object sender, EventArgs e)
        {
            if (rbNewFileName.Checked)
            {
                UpdatePreview(sender, e);
            }
        }

        private void txtNewFileExtension_TextChanged(object sender, EventArgs e)
        {
            if (rbNewFileName.Checked && cbNewFileExtension.Checked)
            {
                UpdatePreview(sender, e);
            }
        }

        private void cbNewName_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview(sender, e);

            if (cbNewName.Checked)
            {
                txtNewFileName.Enabled = true;
            }
            else
            {
                txtNewFileName.Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
