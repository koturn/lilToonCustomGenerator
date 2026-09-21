#if UNITY_2021_3_OR_NEWER
#    define SUPPORT_READONLY_INSTANCE_MEMBER
#endif

using Koturn.LilToonCustomGenerator.Editor.Enums;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// v2f struct member definition.
    /// </summary>
    [System.Runtime.InteropServices.Guid("b3765b01-ece0-a244-3989-ca279a4a47dd")]
    public struct VRChatVariableFlagBits
    {
        /// <summary>
        /// Flag value.
        /// </summary>
        public VRChatVariableFlags Value { get; set; }

        /// <summary>
        /// True to declare the uniform variable <c>_VRChatCameraMode</c>.
        /// </summary>
        public bool UseVRChatCameraMode
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatCameraMode) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatCameraMode) : (Value & ~VRChatVariableFlags.UseVRChatCameraMode);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatCameraMask</c>.
        /// </summary>
        public bool UseVRChatCameraMask
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatCameraMask) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatCameraMask) : (Value & ~VRChatVariableFlags.UseVRChatCameraMask);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatMirrorMode</c>.
        /// </summary>
        public bool UseVRChatMirrorMode
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatMirrorMode) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatMirrorMode) : (Value & ~VRChatVariableFlags.UseVRChatMirrorMode);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatFaceMirrorMode</c>.
        /// </summary>
        public bool UseVRChatFaceMirrorMode
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatFaceMirrorMode) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatFaceMirrorMode) : (Value & ~VRChatVariableFlags.UseVRChatFaceMirrorMode);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatMirrorCameraPos</c>.
        /// </summary>
        public bool UseVRChatMirrorCameraPos
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatMirrorCameraPos) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatMirrorCameraPos) : (Value & ~VRChatVariableFlags.UseVRChatMirrorCameraPos);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatScreenCameraPos</c>.
        /// </summary>
        public bool UseVRChatScreenCameraPos
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatScreenCameraPos) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatScreenCameraPos) : (Value & ~VRChatVariableFlags.UseVRChatScreenCameraPos);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatScreenCameraRot</c>.
        /// </summary>
        public bool UseVRChatScreenCameraRot
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatScreenCameraRot) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatScreenCameraRot) : (Value & ~VRChatVariableFlags.UseVRChatScreenCameraRot);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatPhotoCameraPos</c>.
        /// </summary>
        public bool UseVRChatPhotoCameraPos
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatPhotoCameraPos) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatPhotoCameraPos) : (Value & ~VRChatVariableFlags.UseVRChatPhotoCameraPos);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatPhotoCameraRot</c>.
        /// </summary>
        public bool UseVRChatPhotoCameraRot
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatPhotoCameraRot) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatPhotoCameraRot) : (Value & ~VRChatVariableFlags.UseVRChatPhotoCameraRot);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatTimeUTCUnixSeconds</c>.
        /// </summary>
        public bool UseVRChatTimeUTCUnixSeconds
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatTimeUTCUnixSeconds) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatTimeUTCUnixSeconds) : (Value & ~VRChatVariableFlags.UseVRChatTimeUTCUnixSeconds);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatTimeNetworkMs</c>.
        /// </summary>
        public bool UseVRChatTimeNetworkMs
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatTimeNetworkMs) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatTimeNetworkMs) : (Value & ~VRChatVariableFlags.UseVRChatTimeNetworkMs);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatTimeEncoded1</c>.
        /// </summary>
        public bool UseVRChatTimeEncoded1
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatTimeEncoded1) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatTimeEncoded1) : (Value & ~VRChatVariableFlags.UseVRChatTimeEncoded1);
        }
        /// <summary>
        /// True to declare the uniform variable <c>_VRChatTimeEncoded2</c>.
        /// </summary>
        public bool UseVRChatTimeEncoded2
        {
#if SUPPORT_READONLY_INSTANCE_MEMBER
            readonly
#endif  // SUPPORT_READONLY_INSTANCE_MEMBER
            get => (Value & VRChatVariableFlags.UseVRChatTimeEncoded2) != 0;
            set => Value = value ? (Value | VRChatVariableFlags.UseVRChatTimeEncoded2) : (Value & ~VRChatVariableFlags.UseVRChatTimeEncoded2);
        }

        /// <summary>
        /// Set initial value.
        /// </summary>
        /// <param name="val">Initial value.</param>
        public VRChatVariableFlagBits(VRChatVariableFlags val)
        {
            Value = val;
        }
    }
}
