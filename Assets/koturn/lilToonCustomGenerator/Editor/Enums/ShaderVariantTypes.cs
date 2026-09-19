namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// Shader variant type enum.
    /// </summary>
    [System.Runtime.InteropServices.Guid("2da75c6a-58bd-3f84-aaac-3d6fc566644f")]
    public enum ShaderVariantType
    {
        /// <summary>
        /// Declare keywords with <c>#shader_feature</c>.
        /// </summary>
        ShaderFeature,
        /// <summary>
        /// Declare keywords with <c>#shader_feature_local</c>.
        /// </summary>
        ShaderFeatureLocal,
        /// <summary>
        /// Declare keywords with <c>#multi_compile</c>.
        /// </summary>
        MultiCompile,
        /// <summary>
        /// Declare keywords with <c>#multi_compile_local</c>.
        /// </summary>
        MultiCompileLocal
    }
}
