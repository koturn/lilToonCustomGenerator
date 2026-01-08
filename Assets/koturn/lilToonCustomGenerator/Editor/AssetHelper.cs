using System;
using System.IO;
using System.Text;


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// Provides methods about unify assets.
    /// </summary>
    [System.Runtime.InteropServices.Guid("d4c4aabd-85da-a514-e9fc-5153f86e40a9")]
    public static class AssetHelper
    {
        /// <summary>
        /// Create meta file if not exists.
        /// </summary>
        /// <param name="metaFilePath">Meta file path.</param>
        /// <returns><see cref="Guid"/> of meta file.</returns>
        public static Guid CreateMetaFileIfNotExists(string metaFilePath)
        {
            if (File.Exists(metaFilePath))
            {
                return ReadMetaFileGuid(metaFilePath);
            }
            else
            {
                return CreateMetaFile(metaFilePath);
            }
        }

        /// <summary>
        /// Read GUID in metafile.
        /// </summary>
        /// <param name="metaFilePath">Meta file path.</param>
        /// <returns>If found, <see cref="Guid"/> in metafile. <see cref="Guid.Empty"/> if not found.</returns>
        public static Guid ReadMetaFileGuid(string metaFilePath)
        {
            using (var fs = new FileStream(metaFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 512, FileOptions.SequentialScan))
            using (var reader = new StreamReader(fs, Encoding.UTF8, false, 512))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("guid: "))
                    {
                        return new Guid(line.Substring(6));
                    }
                }
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Create meta file.
        /// </summary>
        /// <param name="metaFilePath">Meta file path to create.</param>
        /// <returns><see cref="Guid"/> of meta file.</returns>
        public static Guid CreateMetaFile(string metaFilePath)
        {
            var guid = Guid.NewGuid();
            CreateMetaFile(metaFilePath, guid);
            return guid;
        }

        /// <summary>
        /// Create meta file with specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="metaFilePath">Meta file path to create.</param>
        /// <param name="guid"><see cref="Guid"/> for meta file.</param>
        public static void CreateMetaFile(string metaFilePath, Guid guid)
        {
            using (var targetStream = new FileStream(metaFilePath, FileMode.Create, FileAccess.Write, FileShare.Read, 256, FileOptions.SequentialScan))
            using (var writer = new StreamWriter(targetStream, Encoding.ASCII, 256)
            {
                NewLine = "\n"
            })
            {
                writer.WriteLine("fileFormatVersion: 2");
                writer.WriteLine("guid: {0:N}", guid);
                if (Directory.Exists(Path.Combine(Path.GetDirectoryName(metaFilePath), Path.GetFileNameWithoutExtension(metaFilePath))))
                {
                    writer.WriteLine("folderAsset: yes");
                }
                writer.WriteLine("DefaultImporter:");
                writer.WriteLine("  externalObjects: {}");
                writer.WriteLine("  userData: ");
                writer.WriteLine("  assetBundleName: ");
                writer.WriteLine("  assetBundleVariant: ");
            }
        }
    }
}
