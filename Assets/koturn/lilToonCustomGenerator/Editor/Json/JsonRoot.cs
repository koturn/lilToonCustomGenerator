using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Koturn.LilToonCustomGenerator.Editor.Json
{
    /// <summary>
    /// Json root object.
    /// </summary>
    [System.Runtime.InteropServices.Guid("a3ba9c95-6eea-9c94-3831-ce4225e0b506")]
    [Serializable]
    internal sealed class JsonRoot
    {
        /// <summary>
        /// <see cref="List{T}"/> of <see cref="TemplateConfig"/> instance.
        /// </summary>
        public List<TemplateConfig> ConfigList => configList;

        /// <summary>
        /// Backing field of <see cref="ConfigList"/>.
        /// </summary>
        [SerializeField]
        private List<TemplateConfig> configList;


        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private JsonRoot()
        {
        }


        /// <summary>
        /// Create instance from specified json file.
        /// </summary>
        /// <param name="filePath">File path to json file.</param>
        /// <returns>Created <see cref="JsonRoot"/> instance.</returns>
        /// <exception cref="FileNotFoundException">Thrown when specified file is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when instance is not created.</exception>
        public static JsonRoot LoadFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found: " + filePath, filePath);
            }
            return JsonUtility.FromJson<JsonRoot>(File.ReadAllText(filePath));
        }
    }
}
