using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Backup_on_Server;

namespace Backup_on_Server
{
    class DirectoryCopyBackup
    {
        static void Main(String[] Dir)
        {

            if (Dir.Length == 3)
            {
                // Copy from the current directory, include subdirectories.
                DirectoryCopy(Dir[0], Dir[1], Convert.ToBoolean(Dir[2]));
            }else if(Dir.Length == 1)
            {
                //Cartelle App = CaricaImpostazioniDaXml("test");
                Cartelle App = CaricaImpostazioniDaXml(Dir[0]);
                DirectoryCopy(App.DirIniziale, App.DirDestinazione, Convert.ToBoolean(App.SyncSubDirs));
            }
            

        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                if (!File.Exists(temppath))
                    file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static Cartelle CaricaImpostazioniDaXml(String Profilo)
            {

                Cartelle Impostazioni = new Cartelle();

                #region Load File

                XmlDocument xmlProfili = new XmlDocument();
                xmlProfili.Load("Profili.xml");                     

                #endregion

                #region Search Id button father of the Page

                var nodeP = xmlProfili.SelectNodes(@"//Profile[@Name='" + Profilo + "']").Cast<XmlNode>().FirstOrDefault();
                Impostazioni.DirIniziale = (nodeP.ChildNodes.Cast<XmlLinkedNode>().Where(node => node.Name == "DirIniziale").FirstOrDefault().InnerText);
                Impostazioni.DirDestinazione = (nodeP.ChildNodes.Cast<XmlLinkedNode>().Where(node => node.Name == "DirDestinazione").FirstOrDefault().InnerText);
                Impostazioni.SyncSubDirs = (nodeP.ChildNodes.Cast<XmlLinkedNode>().Where(node => node.Name == "SyncSubDirs").FirstOrDefault().InnerText);

                #endregion

                return Impostazioni;
        }

    }
}
