using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using IWshRuntimeLibrary;

namespace BindsManager.Classes
{
    public class FileManagerUtils
    {
        public static string StartMenu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs");

        public static void AddToStartmenu()
        {

            string shortcutLocation = Path.Combine(StartMenu, "BindsManager.lnk");

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "A shortcut to quickly use the Binds Manager!";
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.Save();
        }
    }
}
