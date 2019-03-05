using System;
using Microsoft.Win32;
using System.IO;

namespace C.I.M.S_WPF.Utils
{
    public static class DbConnector
    {
        public static bool SaveFile<T>(T source, string title = null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = title ?? "";
            saveFileDialog.Filter = "CIMS files (*.cims)|*.cims";
            saveFileDialog.InitialDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString() + @"\SaveFiles";

            if (saveFileDialog.ShowDialog() == true)
            {
                BinarySerialization.WriteToBinaryFile(saveFileDialog.FileName, source);
                return true;
            }

            return false;
        }

        public static T LoadFile<T>()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CIMS files (*.cims)|*.cims";
            openFileDialog.InitialDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString() + @"\SaveFiles";

            if (openFileDialog.ShowDialog() == true)
            {
                return BinarySerialization.ReadFromBinaryFile<T>(openFileDialog.FileName);
            }
            else
            {
                return default(T);
            }
        }
    }
}
