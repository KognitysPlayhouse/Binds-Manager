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
        public static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string ResourcePath = "BindsManager.Main.Resources";

        public static void AddShortcut(string path)
        {
            string shortcutLocation = Path.Combine(path, "BindsManager.lnk");

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "A shortcut to quickly use the Binds Manager!";
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.Save();
        }

        public static bool IsFileExtensionExists(string extension)
        {
            return Registry.CurrentUser.OpenSubKey("Software\\Classes\\.kog", false) == null;
        }

        public static void SetDefaultFileExtension(string extension)
        {
            RegistryKey extensionKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\.kog");
            RegistryKey appKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Applications\\BindsManager.exe");
            RegistryKey associateKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.kog");



            extensionKey.CreateSubKey($"{ResourcePath}.icong.ico"); //brb


        }
    }


}
