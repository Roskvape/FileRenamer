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
        //private Dictionary<string, string> mapPreviewNodesToSource = new Dictionary<string, string>();
        //private List<Tuple<string, string>> mapPreviewNodesToSource = new List<Tuple<string, string>>();
        private List<string[]> mapPreviewNodesToSource = new List<string[]>();

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
            if (txtSourcePath.Text == null || !IsPathValid(txtSourcePath.Text))
            {
                rbNewFileName.Enabled = false;
                rbFindAndReplace.Enabled = false;
                cbTargetIsSame.Enabled = false;
            }
            else
            {
                rbNewFileName.Enabled = true;
                rbFindAndReplace.Enabled = true;
                cbTargetIsSame.Enabled = true;
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

        private void txtNewFileName_TextChanged(object sender, EventArgs e)
        {
            if (rbNewFileName.Checked)
            {
                UpdatePreview();
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

        private void txtNewFileExtension_TextChanged(object sender, EventArgs e)
        {
            if (rbNewFileName.Checked && cbNewFileExtension.Checked)
            {
                UpdatePreview();
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
            else
            {
                CopyFiles();
            }
        }

        #endregion

        #region Source/Preview Methods
        private void RefreshSourceView()
        {
            if (IsPathValid(txtSourcePath.Text))
            {
                GenerateSourceTree(treeViewSource, txtSourcePath.Text);
                lblSourcePath.Text = txtSourcePath.Text;
            }
            else
            {
                treeViewSource.Nodes.Clear();
                treeViewSource.Nodes.Add(new TreeNode("Directory not found."));
            }
        }

        private void GenerateSourceTree(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add((CreateSourceNode(rootDirectoryInfo)));
        }

        private static TreeNode CreateSourceNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(CreateSourceNode((directory)));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            }

            return directoryNode;
        }

        private void UpdatePreview()
        {
            treeViewPreview.Nodes.Clear();
            dicNodes.Clear();
            mapPreviewNodesToSource.Clear();

            if (cbTargetIsSame.Checked)
            {
                UpdatePreviewForRename(treeViewSource.Nodes, treeViewPreview.Nodes);
            }
            else
            {
                UpdatePreviewForCopy(treeViewSource.Nodes, treeViewPreview.Nodes);
                treeViewPreview.ExpandAll();
            }

            if (dicNodes.Any() && dicNodes.Max(x => x.Value) <= 1)
            {
                btnConfirm.Enabled = true;
            }
            else
            {
                btnConfirm.Enabled = false;
            }
        }

        public void UpdatePreviewForRename(TreeNodeCollection nodesSource, TreeNodeCollection nodesPreview)
        {
            foreach (TreeNode aNode in nodesSource)
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
                TreeNode bNode = CreateNodeInPreview(aNode, nodesPreview);
                if (aNode.Nodes.Count != 0)
                {
                    foreach (TreeNode childNode in aNode.Nodes)
                    {
                        ProcessNode(childNode, aNode.Nodes, bNode.Nodes);
                    }
                }
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

        public TreeNode CreateNodeInPreview(TreeNode aNode, TreeNodeCollection nodesPreview)
        {
            TreeNode bNode = new TreeNode(aNode.Text);
            bNode.Checked = aNode.Checked;

            //Add to Preview
            nodesPreview.Add(bNode);

            //Rename?
            if (bNode.Checked)
            {
                RenameNode(bNode);
                //bNode.Text = "Bob.txt";
            }

            //Add to uniqueness dictionary
            AddToDictionaryOrIncrement(bNode.FullPath);

            //Add to mapping dictionary
            mapPreviewNodesToSource.Add(new string[2] { aNode.FullPath, bNode.FullPath });

            //Add formatting
            FormatNode(bNode);

            return bNode;
        }

        public void FormatNode(TreeNode bNode)
        {
            if (bNode.Checked)
            {
                if (dicNodes[bNode.FullPath] > 1)
                {
                    //Duplicate Name
                    bNode.ForeColor = Color.Red;
                    bNode.BackColor = Color.MistyRose;
                }
                else
                {
                    //Valid Rename
                    bNode.ForeColor = Color.Black;
                    bNode.BackColor = Color.PaleGreen;
                }
            }

            if (cbTargetIsSame.Checked && !bNode.Checked)
            {
                //Not Changed
                bNode.ForeColor = Color.Gray;
            }
        }

        public void RenameNode(TreeNode node)
        {
            if (rbNewFileName.Checked)
            {
                node.Text = NewFileName(node.Text);
                CheckTokens(node);
            }
            else if (rbFindAndReplace.Checked)
            {
                node.Text = FindAndReplace(node.Text, txtReplace.Text, txtReplaceWith.Text);
                CheckTokens(node);
            }
        }

        public void RemoveFromDictionaryOrDecrement(string nodeFullPath)
        {
            if (dicNodes.ContainsKey(nodeFullPath))
            {
                if(dicNodes[nodeFullPath] > 1)
                {
                    dicNodes[nodeFullPath]--;
                }
                else
                {
                    dicNodes.Remove(nodeFullPath);
                }
            }
        }

        public void AddToDictionaryOrIncrement(string nodeFullPath)
        {
            if (dicNodes.ContainsKey(nodeFullPath))
            {
                dicNodes[nodeFullPath]++;
            }
            else
            {
                dicNodes.Add(nodeFullPath, 1);
            }
        }

        #endregion

        #region Validation
        public bool IsPathValid(string path)
        {
            return System.IO.Directory.Exists(path);
        }

        public bool IsNewFileNameValid(string nodeFullPath)
        {
            return !dicNodes.ContainsKey(nodeFullPath);
        }

        private void CheckTokens(TreeNode node)
        {
            if (node.Text.Contains(txtCounterToken.Text))
            {
                string oldNodeName = node.Text;
                node.Text = Regex.Replace(node.Text, Regex.Escape(txtCounterToken.Text), GetCounter());
                int iCount = 1;
                while (dicNodes.ContainsKey(node.FullPath))
                {
                    node.Text = Regex.Replace(oldNodeName, Regex.Escape(txtCounterToken.Text), GetCounter(iCount));
                    iCount++;
                }
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
            string tempFolder = CreateTempFolder();

            List<string[]> allFilesForCopy = new List<string[]>();
            GetAllSourceFilesForCopy(treeViewPreview.Nodes);
            void GetAllSourceFilesForCopy(TreeNodeCollection nodesPreview)
            {
                foreach (TreeNode bNode in nodesPreview)
                {
                    allFilesForCopy.Add(mapPreviewNodesToSource.Find(x => x[1] == bNode.FullPath));
                    //allFilesForCopy.Add(new string[2] { mapPreviewNodesToSource[bNode.FullPath], bNode.FullPath });
                    //MessageBox.Show(bNode.FullPath + " used to be " + mapPreviewNodesToSource[bNode.FullPath]);

                    if (bNode.Nodes.Count != 0)
                    {
                        GetAllSourceFilesForCopy(bNode.Nodes);
                    }
                }
            }

            //foreach (string[] x in allFilesForCopy)
            //{
            //    MessageBox.Show(x[1] + " used to be " + x[0]);
            //}

            foreach (string[] x in allFilesForCopy)
            {
                string sourceDirectory = Path.GetDirectoryName(x[0]); // RenamerTests/TextFile.txt becomes RenamerTests/
                string newDirectory = Path.GetDirectoryName(x[1]);
                string sourceRootDirectory = txtSourcePath.Text; // C:/Stuff/RenamerTests/
                string trimmedSourceRootDirectory = Path.GetDirectoryName(sourceRootDirectory); // C:/Stuff/ ??
                string newRootDirectory = txtTargetPath.Text;



                if (sourceDirectory == "" && newDirectory == "")
                {
                    Directory.CreateDirectory(Path.Combine(newRootDirectory, x[0]));
                }
                else
                {
                    Directory.CreateDirectory(newRootDirectory);

                    // Remove path from the file name.
                    string sourceFileName = x[0].Substring(sourceDirectory.Length + 1); // TextFile.txt
                    string newFileName = x[1].Substring(newDirectory.Length + 1);

                    // Will overwrite if the destination file already exists.
                    File.Move(Path.Combine(trimmedSourceRootDirectory, sourceDirectory, sourceFileName), Path.Combine(newRootDirectory, newDirectory, newFileName));
                }

            }
        }

        private void CopyFiles()
        {
            //CreateNewFolder();
            
            List<string[]> allFilesForCopy = new List<string[]>();
            GetAllSourceFilesForCopy(treeViewPreview.Nodes);
            void GetAllSourceFilesForCopy(TreeNodeCollection nodesPreview)
            {
                foreach (TreeNode bNode in nodesPreview)
                {
                    allFilesForCopy.Add(mapPreviewNodesToSource.Find(x => x[1] == bNode.FullPath));
                    //allFilesForCopy.Add(new string[2] { mapPreviewNodesToSource[bNode.FullPath], bNode.FullPath });
                    //MessageBox.Show(bNode.FullPath + " used to be " + mapPreviewNodesToSource[bNode.FullPath]);

                    if (bNode.Nodes.Count != 0)
                    {
                        GetAllSourceFilesForCopy(bNode.Nodes);
                    }
                }
            }

            foreach (string[] x in allFilesForCopy)
            {
                MessageBox.Show(x[1] + " used to be " + x[0]);
            }

            foreach (string[] x in allFilesForCopy)
            {
                string sourceDirectory = Path.GetDirectoryName(x[0]); // RenamerTests/TextFile.txt becomes RenamerTests/
                string newDirectory = Path.GetDirectoryName(x[1]);
                string sourceRootDirectory = txtSourcePath.Text; // C:/Stuff/RenamerTests/
                string trimmedSourceRootDirectory = Path.GetDirectoryName(sourceRootDirectory); // C:/Stuff/ ??
                string newRootDirectory = txtTargetPath.Text;
                


                if (sourceDirectory == "" && newDirectory == "")
                {
                    Directory.CreateDirectory(Path.Combine(newRootDirectory, x[0]));
                }
                else
                {
                    Directory.CreateDirectory(newRootDirectory);

                    // Remove path from the file name.
                    string sourceFileName = x[0].Substring(sourceDirectory.Length + 1); // TextFile.txt
                    string newFileName = x[1].Substring(newDirectory.Length + 1);

                    // Will overwrite if the destination file already exists.
                    File.Copy(Path.Combine(trimmedSourceRootDirectory, sourceDirectory, sourceFileName), Path.Combine(newRootDirectory, newDirectory, newFileName), true);
                }

            }
            
        }




        private string CreateTempFolder()
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

            return sTempFolderFullPathNew;
        }

        private void CreateNewFolder()
        {
            Directory.CreateDirectory(txtTargetPath.Text);
        }

        #region Regex
        public string FindAndReplace(string sSource, string sReplace, string sReplaceWith)
        {
            return Regex.Replace(sSource, Regex.Escape(sReplace), sReplaceWith, RegexOptions.None);
        }

        public string TestFileName(string sSource)
        {
            return Regex.Split(sSource, @"\.", RegexOptions.None).First();
        }

        public string TestFileExt(string sSource)
        {
            return Regex.Split(sSource, @"\.", RegexOptions.None).Last();
        }

        public string NewFileName(string sSource)
        {
            string sFileNameOnly = Regex.Split(sSource, @"\.").First();
            string sFileExtOnly = "";

            string[] sAllPieces = Regex.Split(sSource, @"\.");

            if (sAllPieces.Length > 1)
            {
                sFileExtOnly = Regex.Split(sSource, @"\.").Last();
            }

            if (sAllPieces.Length > 2)
            {
                for (int i = 1; i < sAllPieces.Length - 1; i++)
                {
                    sFileNameOnly = sFileNameOnly + "." + sAllPieces[i];
                }
            }

            if (cbNewName.Checked)
            {
                sFileNameOnly = txtNewFileName.Text;
            }

            if (cbNewFileExtension.Checked)
            {
                sFileExtOnly = txtNewFileExtension.Text;
            }

            if (sSource.Contains("."))
            {
                return sFileNameOnly + "." + sFileExtOnly;
            }
            return sFileNameOnly;
        }

        public string NewFileExtension(string sSource, string sNewFileExtension)
        {
            Regex rFileName = new Regex(@"[^\..+]*");
            Regex rExt = new Regex(@"([^\.]*)$");
            Match rMatchFileName = rFileName.Match(sSource);
            Match rMatchExt = rExt.Match(sSource);
            string sFileName = "";
            string sExt = "";
            if (rMatchFileName.Success)
            {
                sFileName = rMatchFileName.Value;
            }

            if (rMatchExt.Success)
            {
                sExt = rMatchExt.Value;
            }

            return sFileName + "*" + sNewFileExtension;
        }
        #endregion
    }
}
