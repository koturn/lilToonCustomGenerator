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
        /// Means declaring the uniform variable <c>_VRChatCameraMode</c>.
        /// </summary>
        UseVRChatCameraMode = 0x0001,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatCameraMask</c>.
        /// </summary>
        UseVRChatCameraMask = 0x0002,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatMirrorMode</c>.
        /// </summary>
        UseVRChatMirrorMode = 0x0004,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatFaceMirrorMode</c>.
        /// </summary>
        UseVRChatFaceMirrorMode = 0x0008,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatMirrorCameraPos</c>.
        /// </summary>
        UseVRChatMirrorCameraPos = 0x0010,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatScreenCameraPos</c>.
        /// </summary>
        UseVRChatScreenCameraPos = 0x0020,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatScreenCameraRot</c>.
        /// </summary>
        UseVRChatScreenCameraRot = 0x0040,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatPhotoCameraPos</c>.
        /// </summary>
        UseVRChatPhotoCameraPos = 0x0080,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatPhotoCameraRot</c>.
        /// </summary>
        UseVRChatPhotoCameraRot = 0x0100,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatTimeUTCUnixSeconds</c>.
        /// </summary>
        UseVRChatTimeUTCUnixSeconds = 0x0200,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatTimeNetworkMs</c>.
        /// </summary>
        UseVRChatTimeNetworkMs = 0x0400,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatTimeEncoded1</c>.
        /// </summary>
        UseVRChatTimeEncoded1 = 0x0800,
        /// <summary>
        /// Means declaring the uniform variable <c>_VRChatTimeEncoded2</c>.
        /// </summary>
        UseVRChatTimeEncoded2 = 0x1000,
        /// <summary>
        /// Means declaring all VRChat variables.
        /// </summary>
        All = 0x1fff,
    }
}
