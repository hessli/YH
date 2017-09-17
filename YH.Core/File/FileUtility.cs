using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.File
{
    public class FileUtility
    {
        private string _filePath;
        public FileUtility(string filePath)
        {
            _filePath = filePath;

            if (!System.IO.File.Exists(_filePath))
            {
                System.IO.File.Create(_filePath);
            }
        }

        public void Write(string text)
        {
            using (StreamWriter w = System.IO.File.AppendText(_filePath))
            {
                w.Write(text);
            }
        }


        public void AppendLine(string text)
        {
            using (StreamWriter w = System.IO.File.AppendText(_filePath))
            {
                w.WriteLine(text);
            }
        }

        public async void AppendLineAsych(string text)
        {
            using (StreamWriter outputFile = new StreamWriter(_filePath))
            {
                await outputFile.WriteLineAsync(text);
            }

        }

        public async void WriteAsych(string text)
        {
            using (StreamWriter outputFile = new StreamWriter(_filePath))
            {
                await outputFile.WriteAsync(text);
            }
        }

    }
}
