using System;

namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// VRChat variable flags.
    /// </summary>
    [Flags]
    [System.Runtime.InteropServices.Guid("ea936386-653c-8ff4-bbcc-9c0a5f6e04fb")]
    public enum ProTVVariableFlags
    {
        /// <summary>
        /// Nothing.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Means declaring the uniform variable <c>_Udon_VideoTex</c>.
        /// </summary>
        UseUdonVideoTex = 0x01,
        /// <summary>
        /// Means declaring the uniform variable <c>_Udon_VideoTex_TexelSize</c>.
        /// </summary>
        UseUdonVideoTexTexelSize = 0x02,
        /// <summary>
        /// Means declaring the uniform variable <c>_Udon_VideoTex_ST</c>.
        /// </summary>
        UseUdonVideoTexST = 0x04,
        /// <summary>
        /// Means declaring all AudioLink variables.
        /// </summary>
        All = 0x07
    }
}
