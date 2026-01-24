namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <see href="https://learn.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-struct"/>
    /// </remarks>
    [System.Runtime.InteropServices.Guid("ff27a877-eb6c-8a14-8914-39cae2c2b6e3")]
    public enum InterpolationModifier
    {
        /// <summary>
        /// Interpolate between shader inputs; linear is the default value if no interpolation modifier is specified.
        /// </summary>
        Linear,
        /// <summary>
        /// Interpolate between samples that are somewhere within the covered area of the pixel
        /// (this may require extrapolating end points from a pixel center).
        /// Centroid sampling may improve antialiasing if a pixel is partially covered (even if the pixel center is not covered).
        /// The centroid modifier must be combined with either the linear or noperspective modifier.
        /// </summary>
        Centroid,
        /// <summary>
        ///	Do not interpolate.
        /// </summary>
        Nointerpolation,
        /// <summary>
        /// Do not perform perspective-correction during interpolation.
        /// The noperspective modifier can be combined with the centroid modifier.
        /// </summary>
        Noperspective,
        /// <summary>
        /// Means <see cref="Centroid"/> and <see cref="Noperspective"/>.
        /// </summary>
        CentroidAndNoperspective,
        /// <summary>
        /// Available in shader model 4.1 and laterInterpolate at sample location rather than at the pixel center.
        /// This causes the pixel shader to execute per-sample rather than per-pixel.
        /// Another way to cause per-sample execution is to have an input with semantic SV_SampleIndex, which indicates the current sample.
        /// Only the inputs with sample specified (or inputting SV_SampleIndex) differ between shader invocations in the pixel,
        /// whereas other inputs that do not specify modifiers
        /// (for example, if you mix modifiers on different inputs) still interpolate at the pixel center.
        /// Both pixel shader invocation and depth/stencil testing occur for every covered sample in the pixel.
        /// This is sometimes known as supersampling.
        /// In contrast, in the absence of sample frequency invocation, known as multisampling,
        /// the pixel shader is invoked once per pixel regardless of how many samples are covered,
        /// while depth/stencil testing occurs at sample frequency.
        /// Both modes provide equivalent edge antialiasing.
        /// However, supersampling provides better shading quality by invoking the pixel shader more frequently.
        /// </summary>
        Sample
    }
}
