using System;
using System.Collections.Generic;
using System.Text;

namespace ModSync
{
    public class Functions
    {
        public static string SelectFolderWithOpenDialog()
        {
            using var dialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Выберите эту папку",
                InitialDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    ".minecraft"
                )
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return Path.GetDirectoryName(dialog.FileName);
            }

            return null;
        }
    }
}
