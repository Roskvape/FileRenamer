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
        //private Dictionary<string, int> dicNodes = new Dictionary<string, int>();
        //private HashSet<string> hashPreviewNodes = new HashSet<string>();
        private Dictionary<string, string> dictSourceToPreviewMap = new Dictionary<string, string>();
        private Dictionary<string, string> dictPreviewToSourceMap = new Dictionary<string, string>();
        private List<string> filesForCopy = new List<string>();
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
            UpdatePreview();
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

        private void btnBrowseTarget_Click(object sender, EventArgs e)
        {
            if (folderBrowseTarget.ShowDialog() == DialogResult.OK)
            {
                txtTargetPath.Text = folderBrowseTarget.SelectedPath;
            }
        }

        private void txtTargetPath_TextChanged(object sender, EventArgs e)
        {
            lblPreviewPath.Text = txtTargetPath.Text;
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

        //Should be checkchanged?
        private void treeViewSource_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (ContainsNode(e.Node.FullPath, treeViewPreview.Nodes))
            {
                TreeNode foundNode = GetNode(e.Node.FullPath, treeViewPreview.Nodes);
                foundNode.Checked = e.Node.Checked;
                //dictSourceToPreviewMap.Remove(foundNode.FullPath);
                hashPreviewNodes.Remove(foundNode.FullPath);
                DetermineNodeStatus(foundNode);

                if (foundNode.Checked)
                {
                    ExpandAllParents(foundNode);
                    
                }
                else
                {
                    CollapseAllEmptyParents(foundNode);
                }
            }
            else
            {
                // TODO add node
            }
        }

        private bool ContainsNode(string fullPath, TreeNodeCollection collection)
        {
            foreach (TreeNode bNode in collection)
            {
                if (bNode.FullPath == fullPath)
                {
                    return true;
                }
                if (bNode.Nodes.Count != 0)
                {
                    if (ContainsNode(fullPath, bNode.Nodes))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private TreeNode GetNode(string fullPath, TreeNodeCollection collection)
        {
            TreeNode foundTreeNode = null;

            FindNode(collection);

            void FindNode(TreeNodeCollection innerCollection)
            {
                foreach (TreeNode bNode in innerCollection)
                {
                    if (bNode.FullPath == fullPath)
                    {
                        foundTreeNode = bNode;
                        break;
                    }

                    if (bNode.Nodes.Count != 0)
                    {
                        FindNode(bNode.Nodes);
                    }
                }
            }

            return foundTreeNode;
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
                PopulateSourceView(treeViewSource, sSourcePath);
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
            dictSourceToPreviewMap.Clear();
            dictPreviewToSourceMap.Clear();

            if (cbTargetIsSame.Checked)
            {
                UpdatePreviewForRename(treeViewSource.Nodes, treeViewPreview.Nodes);
            }
            else
            {
                //UpdatePreviewForCopy(treeViewSource.Nodes, treeViewPreview.Nodes);
                //treeViewPreview.ExpandAll();
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

        private void ExpandAllParents(TreeNode node)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            parent.Expand();
            ExpandAllParents(parent);
        }

        private void CollapseAllEmptyParents(TreeNode node)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            if (!HasCheckedNode(parent))
            {
                parent.Collapse();
                CollapseAllEmptyParents(parent);
            }
            else
            {
                return;
            }
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

        private void PopulateSourceView(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            //dictSourceToPreviewMap.Clear();
            //hashPreviewNodes.Clear();

            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add((CreateSourceNode(rootDirectoryInfo)));
        }

        private static TreeNode CreateSourceNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var newDir = CreateSourceNode(directory);
                //directoryNode.Name = Path.Combine(directoryInfo.FullName, newDir.Text);
                directoryNode.Nodes.Add(CreateSourceNode(directory));
                
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                var newNode = new TreeNode(file.Name);
                //newNode.Name = Path.Combine(directoryInfo.FullName, newNode.Text);
                directoryNode.Nodes.Add(newNode);
            }

            return directoryNode;
        }

        public void UpdatePreviewForRename(TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        {
            foreach (System.Windows.Forms.TreeNode aNode in nodesSource)
            {
                TreeNode bNode = CreateNodeInPreview(aNode, nodesPreview);

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

        //public void UpdatePreviewForCopy(TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        //{
        //    foreach (TreeNode aNode in nodesSource)
        //    {
        //        ProcessNode(aNode, nodesSource, nodesPreview);
        //    }
        //}

        //public void ProcessNode(TreeNode aNode, TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        //{
        //    if (aNode.Checked || HasCheckedNode(aNode))
        //    {
        //        TreeNode bNode = CreateNodeInPreview(aNode, nodesPreview, aNode.Checked, true);
        //        if (aNode.Nodes.Count != 0)
        //        {
        //            foreach (TreeNode childNode in aNode.Nodes)
        //            {
        //                ProcessNode(childNode, aNode.Nodes, bNode.Nodes);
        //            }
        //        }
        //    }
        //}

        public enum NodeStatus
        {
            ValidRename,
            RenameConflict,
            NoChange,
            CopyAsIs
        }

        public TreeNode CreateNodeInPreview(TreeNode aNode, TreeNodeCollection nodesPreview)
        {
            TreeNode bNode = new TreeNode(aNode.Text);
            nodesPreview.Add(bNode);

            ProcessNode(aNode, bNode);

            return bNode;
        }

        public void ProcessNode(TreeNode aNode, TreeNode bNode)
        {
            DetermineNodeStatus(bNode);
            AddToDictionaries(aNode, bNode);
        }

        public void DetermineNodeStatus(TreeNode bNode)
        {
            if (bNode.Checked)
            {
                FormatNode(bNode, NodeStatus.ValidRename);
                //Trigger Rename to update text
            }
            else if (!cbTargetIsSame.Checked)
            {
                FormatNode(bNode, NodeStatus.CopyAsIs);
                //Create New Node??
            }
            else
            {
                FormatNode(bNode, NodeStatus.NoChange);
            }
        }

        public void AddToDictionaries(TreeNode aNode, TreeNode bNode)
        {
            //ONLY population of dictionaries. Everything that adds a name must go through here.
            dictSourceToPreviewMap.Add(aNode.FullPath, bNode.FullPath);
            dictPreviewToSourceMap.Add(bNode.FullPath, aNode.FullPath);
        }

        public void RemoveFromDictionaries(TreeNode aNode, TreeNode bNode)
        {
            //Remove must be called before name is changed.
            dictSourceToPreviewMap.Remove(aNode.FullPath);
            dictPreviewToSourceMap.Remove(bNode.FullPath);
        }

        public void FormatNode(TreeNode bNode, NodeStatus status)
        {
            switch (status)
            {
                case NodeStatus.ValidRename:
                    bNode.ForeColor = Color.Black;
                    bNode.BackColor = Color.PaleGreen;
                    break;
                //case NodeStatus.RenameConflict:
                //    bNode.ForeColor = Color.Red;
                //    bNode.BackColor = Color.MistyRose;
                //    break;
                case NodeStatus.NoChange:
                    bNode.ForeColor = Color.Gray;
                    bNode.BackColor = Color.Empty;
                    break;
                case NodeStatus.CopyAsIs:
                    bNode.ForeColor = Color.Black;
                    bNode.BackColor = Color.Empty;
                    break; 
            }
        }

        public void RenameNode(TreeNode node)
        {
            hashPreviewNodes.Remove(node.FullPath);
            if (rbFindAndReplace.Checked)
            {
                string newText = new RegexMethods().FindAndReplace(node.Text, txtReplace.Text, txtReplaceWith.Text);
                node.Text = ProcessTokens(newText);
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
                string newText = new RegexMethods().NewFileName(node.Text, IsNewFileName, IsNewFileExt, txtNewFileName.Text, txtNewFileExtension.Text);
                if (newText.Contains(txtCounterToken.Text))
                {
                    node.Text = ProcessTokens(newText);
                    if (!IsNewFileNameValid(node))
                    {
                        IncrementTokens(node);
                    }
                }
                else
                {
                    node.Text = newText;
                }
                hashPreviewNodes.Add(node.FullPath);
                dictSourceToPreviewMap

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
            if (hashPreviewNodes.Contains(node.FullPath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string ProcessTokens(string input)
        {
            return Regex.Replace(input, Regex.Escape(txtCounterToken.Text), GetCounter());
        }

        private void IncrementTokens(TreeNode node)
        {
            int dupeCount = 1;
            while (!IsNewFileNameValid(node))
            {
                dupeCount++;
                node.Text = Regex.Replace(node.Text, Regex.Escape(txtCounterToken.Text), GetCounter(dupeCount));
            }
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
            string sTempFolderFullPathOriginal = Path.Combine(txtSourcePath.Text, sTempFolderName);
            string sTempFolderFullPathNew = sTempFolderFullPathOriginal;
            int iInc = 0;
            while (Directory.Exists(sTempFolderFullPathNew))
            {
                iInc++;
                sTempFolderFullPathNew = sTempFolderFullPathOriginal + iInc;
            }

            Directory.CreateDirectory(sTempFolderFullPathNew);
        }

        private void GetFilesForCopy()
        {
            foreach (TreeNode x in treeViewSource.Nodes)
            {
                filesForCopy.Add(x.Name);
            }
        }
    }
}
