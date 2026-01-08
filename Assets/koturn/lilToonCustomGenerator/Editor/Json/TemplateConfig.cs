using System;
using System.Collections.Generic;


namespace Koturn.LilToonCustomGenerator.Editor.Json
{
    /// <summary>
    /// One template set.
    /// </summary>
    [System.Runtime.InteropServices.Guid("99b12216-81db-a774-59bf-f7c3ff1dc21e")]
    [Serializable]
    internal sealed class TemplateConfig
    {
        /// <summary>
        /// Name of config.
        /// </summary>
        public string name = null;
        /// <summary>
        /// Parent config name.
        /// </summary>
        public string basedOn = null;
        /// <summary>
        /// Name for display.
        /// </summary>
        public List<TemplateFileConfig> templates = null;
    }
}
