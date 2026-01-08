using System;


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
        public string guid = null;
        /// <summary>
        /// Destination file path.
        /// </summary>
        public string destination = null;
    }
}
