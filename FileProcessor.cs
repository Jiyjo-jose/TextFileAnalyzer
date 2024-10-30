using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    public class FileProcessor
    {
        public string FilePath { get; private set; }
        public FileProcessor(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File does not exist.", filePath);
            }
            if (Path.GetExtension(filePath).ToLower() != ".txt")
            {
                throw new InvalidOperationException("Only .txt files are supported.");
            }
            FilePath = filePath;
        }
        public static FileProcessor FromDragAndDrop(string[] files)
        {
            if (files == null || files.Length == 0)
            {
                throw new ArgumentException("No files were provided for drag-and-drop.", nameof(files));
            }
            return new FileProcessor(files[0]);
        }
        public string ReadFile(Action<int> progressCallback = null)
        {
            StringBuilder content = new StringBuilder();
            const int bufferSize = 4096;
            char[] buffer = new char[bufferSize];
            int bytesRead;
            long totalBytesRead = 0;
            long fileLength = new FileInfo(FilePath).Length;
            using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
            {
                while ((bytesRead = reader.Read(buffer, 0, bufferSize)) > 0)
                {
                    content.Append(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;
                    progressCallback?.Invoke((int)((totalBytesRead * 100) / fileLength));
                }
            }
            return NormalizeLineEndings(content.ToString());
        }
        private string NormalizeLineEndings(string text)
        {
            return text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }
    }

}

