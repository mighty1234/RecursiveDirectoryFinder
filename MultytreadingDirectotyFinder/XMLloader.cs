using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MultytreadingDirectotyFinder
{
     public class XMLloader 
    {
        private string path;
        private FileDirectoryModel model;

        //private XMLloader(FileDirectoryModel model, string path)
        //{
        //    this.path = path;
        //    this.model = model;
        //}

        public void AddTofile(DirectoryInfo info)
        {

            List<FileDirectoryModel> list = new List<FileDirectoryModel>();
           FileDirectoryModel model = new FileDirectoryModel();
           model.Attributes = info.Attributes.ToString();
           model.DateOfCreation = info.CreationTime;
           model.DateOfLastAccess = info.LastAccessTime;
           model.Name = info.Name;
           model.Extention= info.Extension;
           model.DateOfLastModification = info.LastWriteTime;
           model.Size = DirectorySize(info, Directory.GetDirectories(info.FullName).Length == 0 ? false : true);

            XmlSerializer formatter = new XmlSerializer(typeof(List<FileDirectoryModel>));          

            list.Add(model);
           
          
            using (FileStream fs = new FileStream("Directories.xml", FileMode.Append))
            {
                formatter.Serialize(fs, list);

                Console.WriteLine("Объект добавлен");
                fs.Close();
            }
            


        }
        public void AddTofile(FileInfo info)
        {

            List<FileDirectoryModel> list = new List<FileDirectoryModel>();
            FileDirectoryModel model = new FileDirectoryModel();
            model.Attributes = info.ToString();
            model.DateOfCreation = info.CreationTime;
            model.DateOfLastAccess = info.LastAccessTime;
            model.Name = info.Name;
            model.Extention = info.Extension;
            model.DateOfLastModification = info.LastWriteTime;
            model.Size = info.Length;
            model.Directiory = info.Directory.Name;
            XmlSerializer formatter = new XmlSerializer(typeof(List<FileDirectoryModel>));



            list.Add(model);


            using (FileStream fs = new FileStream("Directories.xml", FileMode.Append))
            {
                formatter.Serialize(fs, list);

                Console.WriteLine("Объект добавлен");
                fs.Close();
            }
            //using (FileStream fs = new FileStream("Directories.xml", FileMode.Open))
            //{
            //    list = (List<FileDirectoryModel>)formatter.Deserialize(fs);
            //}

            // десериализация


        }
        public long DirectorySize(DirectoryInfo dInfo, bool includeSubDir)
        {
            // Enumerate all the files
            long totalSize = dInfo.EnumerateFiles()
                         .Sum(file => file.Length);

            // If Subdirectories are to be included
            if (includeSubDir)
            {
                // Enumerate all sub-directories
                totalSize += dInfo.EnumerateDirectories()
                         .Sum(dir => DirectorySize(dir, true));
            }
            return totalSize;
        }


    }
}
