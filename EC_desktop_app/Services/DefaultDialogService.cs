using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EC_desktop_app.Services
{
     public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }
        /// <summary>
        /// This method opens explorer for selecting a file to work with it
        /// </summary>
        /// <returns></returns>
        public bool OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML Files | *.xml|Text Files | *.txt|All files(*.*)| *.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        /// <summary>
        /// This method opens explorer for saving the data in file
        /// </summary>
        /// <returns></returns>
        public bool SaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = "XML Files | *.xml | All files(*.*)| *.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
        /// <summary>
        /// This method showing a message as resilt working with explorer
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
