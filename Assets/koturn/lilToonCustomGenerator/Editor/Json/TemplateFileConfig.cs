using System;
using UnityEngine;


namespace Koturn.LilToonCustomGenerator.Editor.Json
{
    /// <summary>
    /// One template config.
    /// </summary>
    [System.Runtime.InteropServices.Guid("1ddee1d9-995d-7964-7893-642219f67a3b")]
    [Serializable]
    internal sealed class TemplateFileConfig
    {
        /// <summary>
        /// Template file GUID.
        /// </summary>
        public string Guid => guid;
        /// <summary>
        /// Destination file path.
        /// </summary>
        public string Destination => destination;

        /// <summary>
        /// Backing field of <see cref="Guid"/>.
        /// </summary>
        [SerializeField]
        private string guid;
        /// <summary>
        /// Backing field of <see cref="Destination"/>.
        /// </summary>
        [SerializeField]
        private string destination;


        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private TemplateFileConfig()
        {
        }
    }
}
