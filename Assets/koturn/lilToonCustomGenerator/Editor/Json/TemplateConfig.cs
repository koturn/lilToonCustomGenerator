using System;
using System.Collections.Generic;
using UnityEngine;


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
        public string Name => name;
        /// <summary>
        /// Parent config name.
        /// </summary>
        public string BasedOn => basedOn;
        /// <summary>
        /// Name for display.
        /// </summary>
        public List<TemplateFileConfig> Templates => templates;

        /// <summary>
        /// Backing field of <see cref="Name"/>.
        /// </summary>
        [SerializeField]
        private string name;
        /// <summary>
        /// Backing field of <see cref="BasedOn"/>.
        /// </summary>
        [SerializeField]
        private string basedOn;
        /// <summary>
        /// Backing field of <see cref="Templates"/>.
        /// </summary>
        [SerializeField]
        private List<TemplateFileConfig> templates;


        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private TemplateConfig()
        {
        }
    }
}
