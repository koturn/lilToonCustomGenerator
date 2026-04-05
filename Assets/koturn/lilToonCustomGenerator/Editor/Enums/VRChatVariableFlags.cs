using System;

namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// VRChat variable flags.
    /// </summary>
    [Flags]
    [System.Runtime.InteropServices.Guid("7daae94c-5d06-e804-989a-13ec636eab74")]
    public enum VRChatVariableFlags
    {
        /// <summary>
        /// Nothing.
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatCameraMode`.
        /// </summary>
        UseVRChatCameraMode = 0x0001,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatCameraMask`.
        /// </summary>
        UseVRChatCameraMask = 0x0002,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatMirrorMode`.
        /// </summary>
        UseVRChatMirrorMode = 0x0004,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatFaceMirrorMode`.
        /// </summary>
        UseVRChatFaceMirrorMode = 0x0008,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatMirrorCameraPos`.
        /// </summary>
        UseVRChatMirrorCameraPos = 0x0010,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatScreenCameraPos`.
        /// </summary>
        UseVRChatScreenCameraPos = 0x0020,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatScreenCameraRot`.
        /// </summary>
        UseVRChatScreenCameraRot = 0x0040,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatPhotoCameraPos`.
        /// </summary>
        UseVRChatPhotoCameraPos = 0x0080,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatPhotoCameraRot`.
        /// </summary>
        UseVRChatPhotoCameraRot = 0x0100,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatTimeUTCUnixSeconds`.
        /// </summary>
        UseVRChatTimeUTCUnixSeconds = 0x0200,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatTimeNetworkMs`.
        /// </summary>
        UseVRChatTimeNetworkMs = 0x0400,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatTimeEncoded1`.
        /// </summary>
        UseVRChatTimeEncoded1 = 0x0800,
        /// <summary>
        /// Means declaring the uniform variable `_VRChatTimeEncoded2`.
        /// </summary>
        UseVRChatTimeEncoded2 = 0x1000,
        /// <summary>
        /// Means declaring all VRChat variables.
        /// </summary>
        All = 0x1fff,
    }
}
