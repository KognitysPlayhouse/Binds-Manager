using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using IWshRuntimeLibrary;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace BindsManager.Classes
{
    public class FileManagerUtils
    {
        public static string StartMenu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs");
        public static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);

        public static void AddShortcut(string path)
        {
            string shortcutLocation = Path.Combine(path, "BindsManager.lnk");

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "A shortcut to quickly use the Binds Manager!";
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.Save();
        }
    }
}
