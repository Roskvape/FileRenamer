using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace FileRenamer
{
    public class RegexMethods
    {

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

        public string NewFileName(Interface thisInterface, string sSource)
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

            if (thisInterface.IsNewFileName)
            {
                sFileNameOnly = thisInterface.NewFileName;
            }

            if (thisInterface.IsNewFileExt)
            {
                sFileExtOnly = thisInterface.NewFileExt;
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
    }
}
