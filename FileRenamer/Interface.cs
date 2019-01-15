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
        #region Properties
        private Dictionary<string, int> dicNodes = new Dictionary<string, int>();

        #endregion
        
        // Initializer
        public Interface()
        {
            InitializeComponent();
        }

        #region Form Control Methods

        #region Path Controls
        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            if (folderBrowseSource.ShowDialog() == DialogResult.OK)
            {
                txtSourcePath.Text = folderBrowseSource.SelectedPath;
            }
        }

        private void txtSourcePath_TextChanged(object sender, EventArgs e)
        {
            RefreshSourceView();
            if (txtSourcePath.Text != null)
            {
                rbNewFileName.Enabled = true;
                rbFindAndReplace.Enabled = true;
                cbTargetIsSame.Enabled = true;
            }
            else
            {
                rbNewFileName.Enabled = false;
                rbFindAndReplace.Enabled = false;
                cbTargetIsSame.Enabled = false;
            }

            if (cbTargetIsSame.Checked)
            {
                txtTargetPath.Text = txtSourcePath.Text;
            }
        } 
        #endregion

        #region Find and Replace Controls
        private void rbFindAndReplace_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview();

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
                UpdatePreview();
            }
        }

        private void txtReplaceWith_TextChanged(object sender, EventArgs e)
        {
            if (rbFindAndReplace.Checked)
            {
                UpdatePreview();
            }
        }
        #endregion

        #region New File Name Controls
        private void rbNewFileName_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview();

            if (rbNewFileName.Checked)
            {
                cbNewName.Enabled = true;
                cbNewName.Checked = true;
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
            UpdatePreview();

            if (cbNewFileExtension.Checked)
            {
                txtNewFileExtension.Enabled = true;
            }
            else
            {
                txtNewFileExtension.Enabled = false;
            }
        }
        private void txtNewFileName_TextChanged(object sender, EventArgs e)
        {
            if (rbNewFileName.Checked)
            {
                UpdatePreview();
            }
        }

        private void txtNewFileExtension_TextChanged(object sender, EventArgs e)
        {
            if (rbNewFileName.Checked && cbNewFileExtension.Checked)
            {
                UpdatePreview();
            }
        }

        private void cbNewName_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreview();

            if (cbNewName.Checked)
            {
                txtNewFileName.Enabled = true;
            }
            else
            {
                txtNewFileName.Enabled = false;
            }
        }
        #endregion

        #region Source/Preview Controls
        private void btnUpdatePreview_Click(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void btnRefreshSource_Click(object sender, EventArgs e)
        {
            RefreshSourceView();
        }
        private void treeViewSource_AfterCheck(object sender, TreeViewEventArgs e)
        {
            UpdatePreview();
        }

        private void txtTargetPath_TextChanged(object sender, EventArgs e)
        {
            lblPreviewPath.Text = txtTargetPath.Text;
        }
        #endregion

        private void cbTargetIsSame_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTargetIsSame.Checked)
            {
                lblTargetPathSelection.Enabled = false;
                txtTargetPath.Enabled = false;
                btnBrowseTarget.Enabled = false;
                btnConfirm.Text = "Rename Files!";
            }
            else
            {
                lblTargetPathSelection.Enabled = true;
                txtTargetPath.Enabled = true;
                btnBrowseTarget.Enabled = true;
                btnConfirm.Text = "Copy Files!";
            }
            UpdatePreview();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbTargetIsSame.Checked)
            {
                RenameFiles();
            }
        }

        #endregion

        #region Source/Preview Methods
        private void RefreshSourceView()
        {
            if (IsPathValid(txtSourcePath.Text))
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

        private void UpdatePreview()
        {
            treeViewPreview.Nodes.Clear();
            dicNodes.Clear();
            if (cbTargetIsSame.Checked)
            {
                UpdatePreviewForRename(treeViewSource.Nodes, treeViewPreview.Nodes);
            }
            else
            {
                UpdatePreviewForCopy(treeViewSource.Nodes, treeViewPreview.Nodes);
                treeViewPreview.ExpandAll();
            }
        }

        private void SelectAllParents(TreeNode node, bool isChecked)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            if (!isChecked && HasCheckedNode(parent))
                return;

            parent.Checked = isChecked;
            SelectAllParents(parent, isChecked);
        }

        private bool HasCheckedNode(TreeNode node)
        {
            if (node.Nodes.Cast<TreeNode>().Any(n => n.Checked))
            {
                return true;
            }
            else if (node.Nodes.Count != 0)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    return HasCheckedNode(childNode);
                }

                return false;
            }
            else
            {
                return false;
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

        public void UpdatePreviewForRename(TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        {
            foreach (System.Windows.Forms.TreeNode aNode in nodesSource)
            {
                //TreeNode bNode = new TreeNode(aNode.Text);
                TreeNode bNode = CreateNodeInPreview(aNode, nodesPreview, aNode.Checked, false);
                //nodesPreview.Add(bNode);

                //if (aNode.Checked)
                //{
                //    FormatNode(bNode);
                //    RenameNode(bNode);
                //}
                //else
                //{
                //    bNode.ForeColor = Color.Gray;
                //}

                if (aNode.Nodes.Count != 0)
                {
                    UpdatePreviewForRename(aNode.Nodes, bNode.Nodes);
                    if (aNode.IsExpanded)
                    {
                        bNode.Expand();
                    }
                }
            }
        }

        public void UpdatePreviewForCopy(TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        {
            foreach (TreeNode aNode in nodesSource)
            {
                ProcessNode(aNode, nodesSource, nodesPreview);
            }
        }

        public void ProcessNode(TreeNode aNode, TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        {
            if (aNode.Checked || HasCheckedNode(aNode))
            {
                TreeNode bNode = CreateNodeInPreview(aNode, nodesPreview, aNode.Checked, true);
                if (aNode.Nodes.Count != 0)
                {
                    foreach (TreeNode childNode in aNode.Nodes)
                    {
                        ProcessNode(childNode, aNode.Nodes, bNode.Nodes);
                    }
                }
            }
        }

        public TreeNode CreateNodeInPreview(TreeNode aNode, TreeNodeCollection nodesPreview, bool bNameIsChanged, bool bCopiedToNewLocation)
        {
            TreeNode bNode = new TreeNode(aNode.Text);
            nodesPreview.Add(bNode);
            bool IsValidPreRename = IsNewFileNameValid(bNode);
            if (IsValidPreRename)
            {
                dicNodes.Add(bNode.Text, 0);
            }

            if (bNameIsChanged)
            {
                RenameNode(bNode);

                if (IsNewFileNameValid(bNode))
                {
                    FormatNode(bNode, "validRename");
                }
                FormatNode(bNode, "invalidRename");
            }
            else
            {
                if (bCopiedToNewLocation)
                {
                    FormatNode(bNode, "copiedWithoutRename");
                }
                else if (IsValidPreRename)
                {
                    FormatNode(bNode, "noChange");
                }
                else
                {
                    FormatNode(bNode, "conflict");
                }
            }
            return bNode;
        }

        public void FormatNode(TreeNode node, string status)
        {
            switch (status)
            {
                case "validRename":
                    node.Checked = true;
                    node.ForeColor = Color.Black;
                    node.BackColor = Color.PaleGreen;
                    break;
                case "invalidRename":
                    node.Checked = true;
                    node.ForeColor = Color.Red;
                    node.BackColor = Color.MistyRose;
                    break;
                case "conflict":
                    node.Checked = false;
                    node.ForeColor = Color.Red;
                    node.BackColor = Color.MistyRose;
                    break;
                case "noChange":
                    node.Checked = false;
                    node.ForeColor = Color.Gray;
                    break;
                case "copiedWithoutRename":
                    node.Checked = true;
                    node.ForeColor = Color.Black;
                    break;
                default:
                    break;
            }
        }

        public void RenameNode(TreeNode node)
        {
            if (rbFindAndReplace.Checked)
            {
                string newText = new RegexMethods().FindAndReplace(node.Text, txtReplace.Text, txtReplaceWith.Text);
                node.Text = CheckTokens(newText);
                if (IsNewFileNameValid(node))
                {
                    dicNodes.Add(node.Text, 0);
                }
                else
                {
                    btnConfirm.Enabled = false;
                }
            }
            else if (rbNewFileName.Checked)
            {
                string newText = new RegexMethods().NewFileName(this, node.Text);
                node.Text = CheckTokens(newText);
                dicNodes.Add(node.Text, 0);
            }
        }
        #endregion

        #region Validation
        public bool IsPathValid(string path)
        {
            return System.IO.Directory.Exists(path);
        }

        private bool IsNewFileNameValid(TreeNode node)
        {
            //bool bAlreadyExists = dicNodes.ContainsKey(node.Text);
            //bool bIsNameValid = true;
            //if (bAlreadyExists)
            //{
            //    FormatNode(node, "invalidRename");
            //    bIsNameValid = false;
            //}

            //return bIsNameValid;

            if (dicNodes.ContainsKey(node.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string CheckTokens(string input)
        {
            string output = input;
            if (input.Contains(txtCounterToken.Text))
            {
                output = Regex.Replace(input, Regex.Escape(txtCounterToken.Text), GetCounter());

                if (dicNodes.ContainsKey(output))
                {
                    dicNodes[output] += 1;
                    output = Regex.Replace(input, Regex.Escape(txtCounterToken.Text), GetCounter(dicNodes[output]));
                }
            }

            return output;
        }

        #endregion

        #region Counter Methods
        private string GetCounter(int iModifier = 0)
        {
            int iOutput = Int32.Parse(nupCounterStartAt.Text) + (iModifier * Int32.Parse(nupCounterIncrement.Text));
            string output = iOutput.ToString();

            while (Int32.Parse(nupCounterDigits.Text) > output.Length)
            {
                output = "0" + output;
            }

            return output;
        }
        #endregion

        private void RenameFiles()
        {
            CreateTempFolder();
            //Finish this
        }

        private void CreateTempFolder()
        {
            string sTempFolderName = "RenamerTempFolder";
            string sTempFolderFullPathOriginal = txtSourcePath.Text + "\\" + sTempFolderName;
            string sTempFolderFullPathNew = sTempFolderFullPathOriginal;
            int iInc = 0;
            while (Directory.Exists(sTempFolderFullPathNew))
            {
                iInc++;
                sTempFolderFullPathNew = sTempFolderFullPathOriginal + iInc;
            }

            Directory.CreateDirectory(sTempFolderFullPathNew);
        }
    }
}
