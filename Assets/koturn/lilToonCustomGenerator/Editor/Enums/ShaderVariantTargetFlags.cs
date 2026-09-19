using System;


namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// Shader variant type enum.
    /// </summary>
    [Flags]
    [System.Runtime.InteropServices.Guid("74b0c316-e5eb-c8b4-982a-cf7fd78d1c68")]
    public enum ShaderVariantTargetFlags
    {
        /// <summary>
        /// Do not declare keywords for any shader stage.
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// Declare keywords for vertex shader stage.
        /// </summary>
        Vertex = 0x0001,
        /// <summary>
        /// Declare keywords for hull shader stage.
        /// </summary>
        Hull = 0x0002,
        /// <summary>
        /// Declare keywords for domain shader stage.
        /// </summary>
        Domain = 0x0004,
        /// <summary>
        /// Declare keywords for geometry shader stage.
        /// </summary>
        Geometry = 0x0008,
        /// <summary>
        /// Declare keywords for fragment shader stage.
        /// </summary>
        Fragment = 0x0010,
        /// <summary>
        /// Declare keywords for all shader stages.
        /// </summary>
        All = 0x001f
    }
}
