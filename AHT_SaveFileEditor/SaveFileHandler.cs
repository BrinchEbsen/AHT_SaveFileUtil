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

        private static string BackupsDir => Path.Join(Path.GetDirectoryName(Application.ExecutablePath), "backups");

        public bool IsBigEndian
        {
            get
            {
                if (saveFile == null)
                    return false;

                return saveFile.Platform == GamePlatform.GameCube;
            }
        }

        private SaveFileHandler() { }

        public static SaveFileHandler Instance => instance ??= new SaveFileHandler();

        public SaveFile? SaveFile => saveFile;

        public void OpenFile(string filename, GamePlatform platform)
        {
            workingFile = filename;
            stream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            saveFile = SaveFile.FromFileStream(stream, platform);
        }

        /// <summary>
        /// Copy the current working file to a "backups" folder
        /// </summary>
        public void CreateBackup()
        {
            if (workingFile == null) return;

            string backupsDir = BackupsDir;

            if (!Directory.Exists(backupsDir))
                Directory.CreateDirectory(backupsDir);

            File.Copy(workingFile, Path.Join(backupsDir, Path.GetFileName(workingFile)), true);
        }

        public void CloseStream()
        {
            stream?.Dispose();
            stream?.Close();
            stream = null;
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
                    stream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    workingFile = filename;
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
