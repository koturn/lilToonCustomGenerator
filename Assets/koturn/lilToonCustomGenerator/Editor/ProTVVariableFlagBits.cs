using Koturn.LilToonCustomGenerator.Editor.Enums;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// v2f struct member definition.
    /// </summary>
    [System.Runtime.InteropServices.Guid("0e338459-f47b-10c4-79c2-1c473c75dbb8")]
    public struct ProTVVariableFlagBits
    {
        /// <summary>
        /// Flag value.
        /// </summary>
        public ProTVVariableFlags Value { get; set; }

        /// <summary>
        /// True to declare the uniform variable <c>_Udon_VideoTex</c>.
        /// </summary>
        public bool UseUdonVideoTex
        {
            readonly get => (Value & ProTVVariableFlags.UseUdonVideoTex) != 0;
            set => Value = value ? (Value | ProTVVariableFlags.UseUdonVideoTex) : (Value & ~ProTVVariableFlags.UseUdonVideoTex);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_Udon_VideoTex_TexelSize</c>.
        /// </summary>
        public bool UseUdonVideoTexTexelSize
        {
            readonly get => (Value & ProTVVariableFlags.UseUdonVideoTexTexelSize) != 0;
            set => Value = value ? (Value | ProTVVariableFlags.UseUdonVideoTexTexelSize) : (Value & ~ProTVVariableFlags.UseUdonVideoTexTexelSize);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_Udon_VideoTex_ST</c>.
        /// </summary>
        public bool UseUdonVideoTexST
        {
            readonly get => (Value & ProTVVariableFlags.UseUdonVideoTexST) != 0;
            set => Value = value ? (Value | ProTVVariableFlags.UseUdonVideoTexST) : (Value & ~ProTVVariableFlags.UseUdonVideoTexST);
        }

        /// <summary>
        /// Set initial value.
        /// </summary>
        /// <param name="val">Initial value.</param>
        public ProTVVariableFlagBits(ProTVVariableFlags val)
        {
            Value = val;
        }
    }
}
