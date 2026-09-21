#if UNITY_2021_3_OR_NEWER
#    define SUPPORT_READONLY_INSTANCE_MEMBER
#endif

using Koturn.LilToonCustomGenerator.Editor.Enums;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// v2f struct member definition.
    /// </summary>
    [System.Runtime.InteropServices.Guid("1b146e33-1f1b-4894-fa0b-7658f481f7f6")]
    public struct AudioLinkVariableFlagBits
    {
        /// <summary>
        /// Flag value.
        /// </summary>
        public AudioLinkVariableFlags Value { get; set; }

        /// <summary>
        /// True to declare the uniform variable <c>_AudioTexture</c>.
        /// </summary>
        public bool UseAudioTexture
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & AudioLinkVariableFlags.UseAudioTexture) != 0;
            set => Value = value ? (Value | AudioLinkVariableFlags.UseAudioTexture) : (Value & ~AudioLinkVariableFlags.UseAudioTexture);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_AudioTexture_TexelSize</c>.
        /// </summary>
        public bool UseAudioTextureTexelSize
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & AudioLinkVariableFlags.UseAudioTextureTexelSize) != 0;
            set => Value = value ? (Value | AudioLinkVariableFlags.UseAudioTextureTexelSize) : (Value & ~AudioLinkVariableFlags.UseAudioTextureTexelSize);
        }

        /// <summary>
        /// Set initial value.
        /// </summary>
        /// <param name="val">Initial value.</param>
        public AudioLinkVariableFlagBits(AudioLinkVariableFlags val)
        {
            Value = val;
        }
    }
}
