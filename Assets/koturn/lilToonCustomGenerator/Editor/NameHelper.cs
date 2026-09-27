using System;
using System.Text;
using Koturn.LilToonCustomGenerator.Editor.Internals;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// Provides some name conversion methods and checking methods.
    /// </summary>
    [System.Runtime.InteropServices.Guid("5a941b80-97c9-dbd4-dbe3-7741818211a6")]
    public static class NameHelper
    {
        /// <summary>
        /// Substrings of shader names that can lead to incorrect determinations.
        /// </summary>
        private static readonly string[] _specificShaderNameSubstrings =
        {
            "Blur",
            "Cutout",
            "Fur",
            "Gem",
            "Lite",
            "Multi",
            "OnePass",
            "Outline",
            "Overlay",
            "Refraction",
            "Tessellation",
            "Transparent",
            "TwoPass"
        };

        /// <summary>
        /// Convert shader name to C# namespace.
        /// </summary>
        /// <param name="shaderName">Shader name.</param>
        /// <returns>C# namespace.</returns>
        public static string ConvertShaderNameToCSharpNamespace(string shaderName)
        {
            var sb = new StringBuilder();
            foreach (var part in shaderName.Replace('/', '.').Split('.'))
            {
                var part2 = RegexProvider.NonIdentifierCharRegex.Replace(part, "");
                if (part2.Length == 0)
                {
                    continue;
                }
                if (sb.Length > 0)
                {
                    sb.Append('.');
                }
                sb.Append(char.ToUpper(part2[0]));
                if (part2.Length > 1)
                {
                    sb.Append(part2, 1, part2.Length - 1);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert shader name to package name.
        /// </summary>
        /// <param name="shaderName">Shader name.</param>
        /// <returns>Package name.</returns>
        public static string ConvertShaderNameToPackageName(string shaderName)
        {
            return ConvertShaderNameToPackageName(shaderName, "com");
        }

        /// <summary>
        /// Convert shader name to package name.
        /// </summary>
        /// <param name="shaderName">Shader name.</param>
        /// <param name="domainName">Domain name.</param>
        /// <returns>Package name.</returns>
        public static string ConvertShaderNameToPackageName(string shaderName, string domainName)
        {
            var packageName = domainName + ".";
            shaderName = shaderName.Replace('.', '-').Replace('/', '.').ToLower();

            var isContainsDot = false;
            foreach (var c in shaderName)
            {
                if (c == '.')
                {
                    isContainsDot = true;
                    break;
                }
            }
            if (!isContainsDot)
            {
                packageName += Environment.UserName.Replace('.', '-').Replace('/', '.') + ".";
            }

            packageName += shaderName;
            return RegexProvider.NonPackageNameCharRegex.Replace(packageName, "");
        }

        /// <summary>
        /// Check whether the shader name contains any substrings that could lead to incorrect determinations.
        /// </summary>
        /// <returns>True if shader name NOT contains any substrings that could lead to incorrect determinations, false otherwise.</returns>
        public static bool CheckShaderName(string shaderName)
        {
            foreach (var substr in _specificShaderNameSubstrings)
            {
                if (shaderName.Contains(substr))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
