using System;
using System.IO;
using UnityEngine;


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// Provides methods about asset path.
    /// </summary>
    [System.Runtime.InteropServices.Guid("acbb6e8b-26ac-5854-786b-d3bb42a35aad")]
    public static class AssetPathHelper
    {
        /// <summary>
        /// Normalize path separator.
        /// </summary>
        /// <param name="path">Target path.</param>
        /// <returns>Normalized path.</returns>
        public static string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            path = path.Replace('\\', '/');
            while (path.Contains("//"))
            {
                path = path.Replace("//", "/");
            }

            return path;
        }

        /// <summary>
        /// Convert absolute path to Unity asset path.
        /// </summary>
        /// <param name="absPath">Absolute path.</param>
        /// <returns>Unity asset path.</returns>
        public static string AbsPathToAssetPath(string absPath)
        {
            if (string.IsNullOrEmpty(absPath))
            {
                return null;
            }

            absPath = NormalizePath(absPath);

            if (absPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase)
                || absPath.StartsWith("Packages/", StringComparison.OrdinalIgnoreCase))
            {
                return absPath;
            }

            var assetAbsDirPath = NormalizePath(Application.dataPath);
            var projectAbsRootPath = assetAbsDirPath.Substring(0, assetAbsDirPath.Length - "Assets".Length);

            if (absPath.StartsWith(projectAbsRootPath, StringComparison.OrdinalIgnoreCase))
            {
                return absPath.Substring(projectAbsRootPath.Length);
            }

            return null;
        }

        /// <summary>
        /// Convert Unity asset path to absolute path.
        /// </summary>
        /// <param name="assetPath">Unity asset path.</param>
        /// <returns>Absolute path.</returns>
        public static string AssetPathToAbsPath(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                return null;
            }

            assetPath = NormalizePath(assetPath);

            var assetAbsDirPath = NormalizePath(Application.dataPath);
            var projectAbsRootPath = assetAbsDirPath.Substring(0, assetAbsDirPath.Length - "Assets".Length);

            if (assetPath.StartsWith(projectAbsRootPath, StringComparison.OrdinalIgnoreCase))
            {
                return assetPath;
            }

            if (!assetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase)
                && !assetPath.StartsWith("Packages/", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return NormalizePath(Path.Combine(projectAbsRootPath, assetPath));
        }
    }
}
