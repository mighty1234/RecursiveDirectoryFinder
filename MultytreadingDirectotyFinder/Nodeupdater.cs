using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultytreadingDirectotyFinder
{
    public static  class Nodeupdater
    {
        public static void GetDirs(TreeNode node)// thread
        {
            DirectoryInfo[] diArray;
            FileInfo[] fiArray;
            node.Nodes.Clear();
            string fullPath = node.FullPath;
            DirectoryInfo di = new DirectoryInfo(fullPath);
            try
            {
                diArray = di.GetDirectories();
                fiArray = di.GetFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            foreach (DirectoryInfo dirinfo in diArray)
            {

                TreeNode dir = new TreeNode(dirinfo.Name, 1, 2);

                node.Nodes.Add(dir);
                var x = Directory.GetDirectories(dir.FullPath + "\\");
                var fils = Directory.GetFiles(dir.FullPath + "\\");
                if (x.Length != 0 || fils.Length != 0)
                {
                    GetDirs(dir);
                }

            }
            foreach (FileInfo fileinfo in fiArray)
            {
                TreeNode file = new TreeNode(fileinfo.Name, 1, 1);
                node.Nodes.Add(file);
            }
            var xx = node.FullPath;




        }
    }
}
