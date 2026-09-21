using System;

namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// VRChat variable flags.
    /// </summary>
    [Flags]
    [System.Runtime.InteropServices.Guid("8de9551c-ba1a-5364-9a4f-2a0389cb82a5")]
    public enum AudioLinkVariableFlags
    {
        /// <summary>
        /// Nothing.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Means declaring the uniform variable <c>_AudioTexture</c>.
        /// </summary>
        UseAudioTexture = 0x01,
        /// <summary>
        /// Means declaring the uniform variable <c>_AudioTexture_TexelSize</c>.
        /// </summary>
        UseAudioTextureTexelSize = 0x02,
        /// <summary>
        /// Means declaring all AudioLink variables.
        /// </summary>
        All = 0x03
    }
}
