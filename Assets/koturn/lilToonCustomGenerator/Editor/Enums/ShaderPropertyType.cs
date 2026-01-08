namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// Shader property type enum.
    /// </summary>
    [System.Runtime.InteropServices.Guid("ade8290e-9e75-f304-183f-ede5314ebc49")]
    public enum ShaderPropertyType
    {
        /// <summary>
        /// Value correspond to <c>Float</c>.
        /// </summary>
        Float,
        /// <summary>
        /// Value correspond to <c>Int</c>.
        /// </summary>
        Int,
        /// <summary>
        /// Value correspond to <c>Range</c>.
        /// </summary>
        Range,
        /// <summary>
        /// Value correspond to <c>Vector</c>.
        /// </summary>
        Vector,
        /// <summary>
        /// Value correspond to <c>Color</c>.
        /// </summary>
        Color,
        /// <summary>
        /// Value correspond to <c>2D</c>.
        /// </summary>
        Texture2D,
        /// <summary>
        /// Value correspond to <c>3D</c>.
        /// </summary>
        Texture3D,
        /// <summary>
        /// Value correspond to <c>Cube</c>.
        /// </summary>
        TextureCube,
    }
}
