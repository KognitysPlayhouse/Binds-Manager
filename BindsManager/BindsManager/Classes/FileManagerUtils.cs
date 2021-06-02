using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static BindsManager.Classes.InputBox;

namespace BindsManager.Classes
{
    public class FileManagerUtils
    {
        public static string StartMenu = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);

        public static void AddToStartmenu(string path)
        {
            using (StreamWriter writer = new StreamWriter(StartMenu + "\\" + path + ".url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
            }
        }
    }
}
