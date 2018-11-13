using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultytreadingDirectotyFinder
{
    public partial class Form1 : Form
    {
        XMLloader xMLloader = new XMLloader();
        public Form1()
        {


            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DriveTreeInit(folderBrowserDialog1.SelectedPath);
            }

           


        }
        public void GetDirs(TreeNode node)// thread
        {
            object syncronize= new object();
            object fileobject = new object();
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
                //lock (fileobject)
                //{
                   /* Task.Run(() =>*/// xMLloader.AddTofile(dirinfo);
              //  }
                TreeNode dir = new TreeNode(dirinfo.Name, 1, 2);


                 xMLloader.AddTofile(dirinfo);
                
                node.Nodes.Add(dir);
                var x = Directory.GetDirectories(dir.FullPath + "\\");
                var fils= Directory.GetFiles(dir.FullPath + "\\");
                if (x.Length != 0||fils.Length != 0)
                {
                    GetDirs(dir);
                }

            }
            foreach (FileInfo fileinfo in fiArray)
            {
                TreeNode file = new TreeNode(fileinfo.Name, 1, 1);
                node.Nodes.Add(file);
                xMLloader.AddTofile(fileinfo);
            }
            var xx = node.FullPath;
           
           
          

        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.BeginUpdate();

            foreach (TreeNode node in e.Node.Nodes)
            {

                GetDirs(node);
            }



            treeView1.EndUpdate();

        }
        void DriveTreeInit(string path)
        {
            string[] drivesArray = Directory.GetDirectories(path);

            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            foreach (string s in drivesArray)
            {
                
                TreeNode drive = new TreeNode(s, 0, 0);
               
                treeView1.Nodes.Add(drive);
                //var x = Directory.GetDirectories(drive.FullPath + "\\");
                //if (x.Length != 0)
                //{
                //    DriveTreeInit(drive.FullPath);
                //}

                GetDirs(drive);
            }    
          


            treeView1.EndUpdate();
        }

      
    }
}
