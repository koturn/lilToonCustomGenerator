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
        /// File ID of lilShaderContainerImporter.
        /// </summary>
        private const int CsScriptFileId = 11500000;
        /// <summary>
        /// <see cref="Guid"/> of lilShaderContainerImporter.
        /// </summary>
        private static readonly Guid LilShaderContainerImporterGuid = Guid.ParseExact("3089979ac9fdd004ba564a7e5418ee8d", "N");

        /// <summary>
        /// Create meta file if not exists.
        /// </summary>
        /// <param name="path">File path or directory path for which meta file is to be created.</param>
        /// <returns><see cref="Guid"/> of meta file.</returns>
        public static Guid CreateMetaFileIfNotExists(string path)
        {
            var metaFilePath = path + ".meta";
            if (File.Exists(metaFilePath))
            {
                return ReadMetaFileGuid(metaFilePath);
            }
            else
            {
                return CreateMetaFile(path);
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
                        return Guid.ParseExact(line.Substring(6), "N");
                    }
                }
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Create meta file.
        /// </summary>
        /// <param name="path">File path or directory path for which meta file is to be created.</param>
        /// <returns><see cref="Guid"/> of meta file.</returns>
        public static Guid CreateMetaFile(string path)
        {
            var guid = Guid.NewGuid();
            CreateMetaFile(path, guid);
            return guid;
        }

        /// <summary>
        /// Create meta file with specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="path">File path or directory path for which meta file is to be created.</param>
        /// <param name="guid"><see cref="Guid"/> for meta file.</param>
        public static void CreateMetaFile(string path, Guid guid)
        {
            if (Directory.Exists(path))
            {
                CreateDefaultMetaFile(path, guid);
                return;
            }

            switch (Path.GetExtension(path))
            {
                case ".cs":
                    CreateMonoMetaFile(path, guid);
                    break;
                case ".hlsl":
                    CreateDefaultMetaFile(path, guid, "ShaderIncludeImporter");
                    break;
                case ".json":
                    CreateDefaultMetaFile(path, guid, "TextScriptImporter");
                    break;
                case ".lilcontainer":
                    CreateScriptedMetaFile(path, guid, CsScriptFileId, LilShaderContainerImporterGuid);
                    break;
                case ".asmdef":
                    CreateDefaultMetaFile(path, guid, "AssemblyDefinitionImporter");
                    break;
                default:
                    CreateDefaultMetaFile(path, guid);
                    break;
            }
        }

        /// <summary>
        /// Create default meta file with specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="path">File path or directory path for which meta file is to be created.</param>
        /// <param name="guid"><see cref="Guid"/> for meta file.</param>
        private static void CreateDefaultMetaFile(string path, Guid guid)
        {
            CreateDefaultMetaFile(path, guid, "DefaultImporter");
        }

        /// <summary>
        /// Create default meta file with specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="path">File path or directory path for which meta file is to be created.</param>
        /// <param name="guid"><see cref="Guid"/> for meta file.</param>
        /// <param name="importerName">Importer name.</param>
        private static void CreateDefaultMetaFile(string path, Guid guid, string importerName)
        {
            using (var targetStream = new FileStream(path + ".meta", FileMode.Create, FileAccess.Write, FileShare.Read, 256, FileOptions.SequentialScan))
            using (var writer = new StreamWriter(targetStream, Encoding.ASCII, 256)
            {
                NewLine = "\n"
            })
            {
                writer.WriteLine("fileFormatVersion: 2");
                writer.WriteLine("guid: {0:N}", guid);
                if (Directory.Exists(path))
                {
                    writer.WriteLine("folderAsset: yes");
                }
                writer.WriteLine("{0}:", importerName);
                writer.WriteLine("  externalObjects: {}");
                writer.WriteLine("  userData: ");
                writer.WriteLine("  assetBundleName: ");
                writer.WriteLine("  assetBundleVariant: ");
            }
        }

        /// <summary>
        /// Create meta file for C# script with specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="filePath">File path for which meta file is to be created.</param>
        /// <param name="guid"><see cref="Guid"/> for meta file.</param>
        private static void CreateMonoMetaFile(string filePath, Guid guid)
        {
            using (var targetStream = new FileStream(filePath + ".meta", FileMode.Create, FileAccess.Write, FileShare.Read, 256, FileOptions.SequentialScan))
            using (var writer = new StreamWriter(targetStream, Encoding.ASCII, 256)
            {
                NewLine = "\n"
            })
            {
                writer.WriteLine("fileFormatVersion: 2");
                writer.WriteLine("guid: {0:N}", guid);
                writer.WriteLine("MonoImporter:");
                writer.WriteLine("  externalObjects: {}");
                writer.WriteLine("  serializedVersion: 2");
                writer.WriteLine("  defaultReferences: []");
                writer.WriteLine("  executionOrder: 0");
                writer.WriteLine("  icon: {instanceID: 0}");
                writer.WriteLine("  userData: ");
                writer.WriteLine("  assetBundleName: ");
                writer.WriteLine("  assetBundleVariant: ");
            }
        }

        /// <summary>
        /// Create meta file for file using ScriptedImporter with specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="filePath">File path for which meta file is to be created.</param>
        /// <param name="guid"><see cref="Guid"/> for meta file.</param>
        private static void CreateScriptedMetaFile(string filePath, Guid guid, int fileId, Guid importerGuid)
        {
            using (var targetStream = new FileStream(filePath + ".meta", FileMode.Create, FileAccess.Write, FileShare.Read, 256, FileOptions.SequentialScan))
            using (var writer = new StreamWriter(targetStream, Encoding.ASCII, 256)
            {
                NewLine = "\n"
            })
            {
                writer.WriteLine("fileFormatVersion: 2");
                writer.WriteLine("guid: {0:N}", guid);
                writer.WriteLine("ScriptedImporter:");
                writer.WriteLine("  internalIDToNameTable: []");
                writer.WriteLine("  externalObjects: {}");
                writer.WriteLine("  serializedVersion: 2");
                writer.WriteLine("  userData: ");
                writer.WriteLine("  assetBundleName: ");
                writer.WriteLine("  assetBundleVariant: ");
                writer.WriteLine("  script: {fileID: {0}, guid: {1:N}, type: 3}", fileId, importerGuid);
            }
        }
    }
}
