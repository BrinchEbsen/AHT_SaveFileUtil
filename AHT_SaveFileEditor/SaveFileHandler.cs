using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileEditor
{
    internal class SaveFileHandler : IDisposable
    {
        private FileStream? stream;

        private string? workingFile;

        private SaveFile? saveFile = null;

        private static SaveFileHandler? instance;

        private SaveFileHandler() { }

        public static SaveFileHandler Instance => instance ??= new SaveFileHandler();

        public SaveFile? SaveFile => saveFile;

        public void OpenFile(string filename, GamePlatform platform)
        {
            workingFile = filename;
            stream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            saveFile = SaveFile.FromFileStream(stream, platform);
        }

        public void CloseStream()
        {
            stream?.Dispose();
            stream?.Close();
            stream = null;
            workingFile = null;
        }

        public void WriteFile()
        {
            if (workingFile != null)
                WriteFile(workingFile);
        }

        public void WriteFile(string filename)
        {
            if (saveFile == null)
                return;

            //If we're overwriting the original file, don't bother re-opening the stream.
            if (workingFile != null)
                if (workingFile != filename)
                {
                    CloseStream();
                    File.Copy(workingFile, filename, true);
                    stream = File.Open(filename, FileMode.Open, FileAccess.Write, FileShare.None);
                }

            saveFile.ToFileStream(stream);
        }

        public void Dispose()
        {
            CloseStream();
            saveFile = null;
        }
    }
}
