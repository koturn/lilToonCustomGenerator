using UnityEngine;
using UnityEditor;

#if LILTOON
using lilToon;
#endif  // LILTOON


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// Provides label utility methods.
    /// </summary>
    [System.Runtime.InteropServices.Guid("a57f7f95-497f-1344-b97d-a02606e0e82a")]
    internal static class Labels
    {
        /// <summary>
        /// Temporary label.
        /// </summary>
        private static readonly GUIContent _tmpLabel = new GUIContent();

        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Shader title".
        /// </summary>
        public static GUIContent ShaderTitle { get; } = new GUIContent(
            "Shader title",
            "The shader title is used as the text displayed in the collapsible section of the Inspector.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Inspector namespace".
        /// </summary>
        public static GUIContent InspectorNamespace { get; } = new GUIContent(
            "Inspector namespace",
            "Inspector namespace is used as namespace of C# script and name of the .asmdef file.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of workaround for lilToon 1.4.0.
        /// </summary>
        public static GUIContent WorkaroundForLilToon140 { get; } = new GUIContent(
            "Consider bug in the LIL_CUSTOM_V2F_MEMBER macro in lilToon 1.4.0",
            "In lilToon 1.4.0, in the ShadowCaster Pass, the `id0` passed to the `LIL_CUSTOM_V2F_MEMBER` macro overlaps with the TEXCOORD numbers used by the lilToon.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Use GrabPass even with shaders that are neither Gem nor Refraction".
        /// </summary>
        public static GUIContent UseGrabPass { get; } = new GUIContent(
            "Use GrabPass even with shaders that are neither Gem nor Refraction",
            "Enable the use of `_lilBackgroundTexture` in BRP shaders other than the Gem and Refraction families.\n"
                + "`_lilBackgroundTexture` should be sampled using the `LIL_GET_BG_TEX` macro.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Use VRChat variables".
        /// </summary>
        public static GUIContent VRChatVariables { get; } = new GUIContent(
            "Use VRChat variables",
            "Declare following uniform variables\n"
                + "  - float _VRChatCameraMode\n"
                + "  - uint _VRChatCameraMask\n"
                + "  - float _VRChatMirrorMode\n"
                + "  - float _VRChatFaceMirrorMode\n"
                + "  - float _VRChatMirrorCameraPos\n"
                + "  - float3 _VRChatScreenCameraPos\n"
                + "  - float4 _VRChatScreenCameraRot\n"
                + "  - float3 _VRChatPhotoCameraPos\n"
                + "  - float4 _VRChatPhotoCameraRot\n"
                + "  - uint _VRChatTimeUTCUnixSeconds\n"
                + "  - uint _VRChatTimeNetworkMs\n"
                + "  - uint _VRChatTimeEncoded1\n"
                + "  - uint _VRChatTimeEncoded2");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatCameraMode</c>.
        /// </summary>
        public static GUIContent UseVRChatCameraMode { get; } = new GUIContent(
            "_VRChatCameraMask",
            "float _VRChatCameraMask\n"
                + "  0: Rendering normally\n"
                + "  1: Rendering in VR handheld camera\n"
                + "  2: Rendering in Desktop handheld camera\n"
                + "  3: Rendering for a screenshot");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatCameraMask</c>.
        /// </summary>
        public static GUIContent UseVRChatCameraMask { get; } = new GUIContent(
            "_VRChatCameraMode",
            "uint _VRChatCameraMode\n"
                + "The cullingMask property of the active camera, available if _VRChatCameraMode != 0.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatMirrorMode</c>.
        /// </summary>
        public static GUIContent UseVRChatMirrorMode { get; } = new GUIContent(
            "_VRChatMirrorMode",
            "uint _VRChatMirrorMode\n"
                + "  0: Rendering normally, not in a mirror\n"
                + "  1: Rendering in a mirror viewed in VR\n"
                + "  2: Rendering in a mirror viewed in desktop mode");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatFaceMirrorMode</c>.
        /// </summary>
        public static GUIContent UseVRChatFaceMirrorMode { get; } = new GUIContent(
            "_VRChatFaceMirrorMode",
            "uint _VRChatFaceMirrorMode\n"
                + "  1: when rendering the face mirror (VR and Desktop use different camera types!)\n"
                + "  0: otherwise.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatMirrorCameraPos</c>.
        /// </summary>
        public static GUIContent UseVRChatMirrorCameraPos { get; } = new GUIContent(
            "_VRChatMirrorCameraPos",
            "float3 _VRChatMirrorCameraPos\n"
                + "  World space position of mirror camera (eye independent, \"centered\" in VR)\n"
                + "  (0,0,0) when not rendering in a mirror.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatScreenCameraPos</c>.
        /// </summary>
        public static GUIContent UseVRChatScreenCameraPos { get; } = new GUIContent(
            "_VRChatScreenCameraPos",
            "float3 _VRChatScreenCameraPos\n"
                + "  World space position of main screen camera.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatScreenCameraRot</c>.
        /// </summary>
        public static GUIContent UseVRChatScreenCameraRot { get; } = new GUIContent(
            "_VRChatScreenCameraRot",
            "float4 _VRChatScreenCameraRot\n"
                + "  World space rotation (quaternion) of main screen camera.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatPhotoCameraPos</c>.
        /// </summary>
        public static GUIContent UseVRChatPhotoCameraPos { get; } = new GUIContent(
            "_VRChatPhotoCameraPos",
            "float3 _VRChatPhotoCameraPos\n"
                + "  World space position of handheld photo camera (first instance when using Dolly Multicam), (0,0,0) when camera is not active.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatPhotoCameraRot</c>.
        /// </summary>
        public static GUIContent UseVRChatPhotoCameraRot { get; } = new GUIContent(
            "_VRChatPhotoCameraRot",
            "float4 _VRChatPhotoCameraRot\n"
                + "  World space rotation (quaternion) of photo camera.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatTimeUTCUnixSeconds</c>.
        /// </summary>
        public static GUIContent UseVRChatTimeUTCUnixSeconds { get; } = new GUIContent(
            "_VRChatTimeUTCUnixSeconds",
            "uint _VRChatTimeUTCUnixSeconds\n"
                + "  The lower 32 bits of the current UTC time in seconds since the Unix epoch.\n"
                + "  Note that this should be treated as an unsigned number and will thus not (yet) overflow in 2038.\n"
                + "  If system time is set to pre-1970 this value is undefined.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatTimeNetworkMs</c>.
        /// </summary>
        public static GUIContent UseVRChatTimeNetworkMs { get; } = new GUIContent(
            "_VRChatTimeNetworkMs",
            "uint _VRChatTimeNetworkMs\n"
                + "  Synchronized network time in milliseconds."
                + "  This is the same value as returned by `Networking.GetServerTimeInMilliseconds` in Udon."
                + "  This is technically a signed value, but may be treated as unsigned."
                + "  It should only be used for synchronization and offsets, as the absolute value does not represent any meaningful quantity."
                + "  This value can wrap.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatTimeEncoded1</c>.
        /// </summary>
        public static GUIContent UseVRChatTimeEncoded1 { get; } = new GUIContent(
            "_VRChatTimeEncoded1",
            "uint _VRChatTimeEncoded1\n"
                + "  bit 0-4: Hour component of the current time of day (UTC).\n"
                + "  bit 5-10: Minute component of the current time of day (UTC).\n"
                + "  bit 11-16: Second component of the current time of day (UTC & Local, shared).\n"
                + "  bit 17-21: Hour component of the current time of day (Local).\n"
                + "  bit 22-27: Minute component of the current time of day (Local).\n"
                + "  bit 28-31: Reserved.\n");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_VRChatTimeEncoded2</c>.
        /// </summary>
        public static GUIContent UseVRChatTimeEncoded2 { get; } = new GUIContent(
            "_VRChatTimeEncoded2",
            "uint _VRChatTimeEncoded2\n"
                + "  bit 0-9: Millisecond component of the current time of day (UTC & Local, shared).\n"
                + "  bit 10: Sign bit of timezone offset. 1 if offset is negative.\n"
                + "  bit 11-26: Timezone offset from UTC to Local time in seconds.\n"
                + "  bit 27-31: Reserved.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Use AudioLink variables".
        /// </summary>
        public static GUIContent AudioLinkVariables { get; } = new GUIContent(
            "Use AudioLink variables",
            "Declare following uniform variables\n"
                + "  - Texture2D<float> _AudioTexture\n"
                + "  - float4 _AudioTexture_TexelSize");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_AudioTexture</c>.
        /// </summary>
        public static GUIContent UseAudioTexture { get; } = new GUIContent(
            "_AudioTexture",
            "Texture2D<float> _AudioTexture\n"
                + "AudioLink data texture.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_AudioTexture_TexelSize</c>.
        /// </summary>
        public static GUIContent UseAudioTextureTexelSize { get; } = new GUIContent(
            "_AudioTexture_TexelSize",
            "float4 _AudioTexture_TexelSize\n"
                + "Texture size information of _AudioTexture.\n"
                + "  - x: 1.0 / width\n"
                + "  - y: 1.0 / height\n"
                + "  - z: width\n"
                + "  - w: height");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Use ProTV variables".
        /// </summary>
        public static GUIContent ProTVVariables { get; } = new GUIContent(
            "Use ProTV variables",
            "Declare following uniform variables.\n"
                + "  - Texture2D _Udon_VideoTex\n"
                + "  - float4 _Udon_VideoTex_TexelSize\n"
                + "  - float4 _Udon_VideoTex_ST");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_Udon_VideoTex</c>.
        /// </summary>
        public static GUIContent UseUdonVideoTex { get; } = new GUIContent(
            "_Udon_VideoTex",
            "Texture2D<float> _AudioTexture\n"
                + "ProTV video data texture.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_Udon_VideoTex_TexelSize</c>.
        /// </summary>
        public static GUIContent UseUdonVideoTexTexelSize { get; } = new GUIContent(
            "_Udon_VideoTex_TexelSize",
            "float4 _Udon_VideoTex_TexelSize\n"
                + "Texture size information of _Udon_VideoTex.\n"
                + "  - x: 1.0 / width\n"
                + "  - y: 1.0 / height\n"
                + "  - z: width\n"
                + "  - w: height");
        /// <summary>
        /// <see cref="GUIContent"/> for description of <c>_Udon_VideoTex_ST</c>.
        /// </summary>
        public static GUIContent UseUdonVideoTexST { get; } = new GUIContent(
            "_Udon_VideoTex_ST",
            "float4 _Udon_VideoTex_ST\n"
                + "Tiling and offset information of _Udon_VideoTex.\n"
                + "  - x: X tiling value\n"
                + "  - y: Y tiling value\n"
                + "  - z: X offset value\n"
                + "  - w: Y offset value");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Support property search".
        /// </summary>
        public static GUIContent SupportPropertySearch { get; } = new GUIContent(
            "Support property search",
            "Property search was implemented in lilToon 1.4.0.\n"
                + "Use methods in lilEditorGUI class instead of ShaderProperty().");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Get version from package.json...".
        /// </summary>
        public static GUIContent GetVersionFromPackageJson { get; } = new GUIContent(
            "Get version from package.json of lilToon (Support lilToon 1.2.11 and 1.2.12)",
            "Retrieve the version string from lilToon’s `package.json` rather than from the `lilConstans` class.\n"
                + "The `lilConstans` class was introduced in lilToon 1.3.0.\n"
                + "Therefore, if you need to support lilToon 1.2.11 or lilToon 1.2.12, it is recommended that you retrieve the version from `package.json`.\n"
                + "We do not support code generation that uses reflection to determine which class to retrieve the version from.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Generate version detection script".
        /// </summary>
        public static GUIContent GenerateVersionDetectionScript { get; } = new GUIContent(
            "Generate version detection script",
            "Create a C# script to detect the version of lilToon and generate a file named `lil_current_version.hlsl` that contains a macro definition for the lilToon version information.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Generate language file".
        /// </summary>
        public static GUIContent GenerateLanguageFile { get; } = new GUIContent(
            "Generate language file",
            "Create a TSV file named \"lang_custom.tsv\", which is required for multilingual support.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Generate convert menu".
        /// </summary>
        public static GUIContent GenerateConvertMenu { get; } = new GUIContent(
            "Generate convert menu",
            "Add a menu item to the \"Assets\" menu that converts the shader of the selected materials from the original lilToon to a corresponded custom shader.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Generate cache clear menu".
        /// </summary>
        public static GUIContent GenerateCacheClearMenu { get; } = new GUIContent(
            "Generate cache clear menu",
            "Add a menu item to the \"Assets\" menu to resolve an issue where compilation errors for lilToon custom shaders sometimes persist.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Generate AssemblyInfo.cs".
        /// </summary>
        public static GUIContent GenerateAssemblyInfo { get; } = new GUIContent(
            "Generate AssemblyInfo.cs",
            "By including AssemblyInfo.cs, you can add information to the DLLs located in the Library/ScriptAssemblies folder.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Mininal lilToon version".
        /// </summary>
        public static GUIContent MinimalLilToonVersion { get; } = new GUIContent(
            "Minimal lilToon version",
            "The custom shader feature is now available starting with lilToon 1.2.11.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of "url" entry in package.json.
        /// </summary>
        public static GUIContent VpmUrl { get; } = new GUIContent(
            "URL",
            "A direct-download link to a zip file of your package.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of ToggleLeft of "Mininal lilToon version (VPM)".
        /// </summary>
        public static GUIContent MinimalVpmLilToonVersion { get; } = new GUIContent(
            "Minimal lilToon version (VPM)",
            "lilToon began supporting VPM starting with version 1.3.7.");
        /// <summary>
        /// <see cref="GUIContent"/> for description of "legacyFolders" entry in package.json.
        /// </summary>
        public static GUIContent LegacyFolders { get; } = new GUIContent(
            "Set the destination directory to \"legacyFolders\" when files are generated under the \"Assets/\"",
            "legacyFolders property can be used to detect and migrate from the old .unitypackage version of your project to this version.\n"
                + "Any folders found with a matching path will be removed.");

        /// <summary>
        /// Calc label width.
        /// </summary>
        /// <param name="labelText">Label text.</param>
        /// <returns>Label width.</returns>
        public static float CalcLabelWidth(string labelText)
        {
            _tmpLabel.text = labelText;
            return CalcWidth(_tmpLabel, EditorStyles.label);
        }

        /// <summary>
        /// Calc label width.
        /// </summary>
        /// <param name="label">Label.</param>
        /// <returns>Label width.</returns>
        public static float CalcLabelWidth(GUIContent label)
        {
            return CalcWidth(label, EditorStyles.label);
        }

        /// <summary>
        /// Calc toggle width.
        /// </summary>
        /// <param name="labelText">Label text of the toggle.</param>
        /// <returns>Toggle width.</returns>
        public static float CalcToggleWidth(string labelText)
        {
            _tmpLabel.text = labelText;
            return CalcWidth(_tmpLabel, EditorStyles.toggle);
        }

        /// <summary>
        /// Calc toggle width.
        /// </summary>
        /// <param name="label">Label of the toggle.</param>
        /// <returns>Toggle width.</returns>
        public static float CalcToggleWidth(GUIContent label)
        {
            return CalcWidth(label, EditorStyles.toggle);
        }

        /// <summary>
        /// Calc width of the specified <see cref="GUIStyle"/>.
        /// </summary>
        /// <param name="label">Label.</param>
        /// <param name="style"><see cref="GUIStyle"/> to calculate width.</param>
        /// <returns></returns>
        public static float CalcWidth(GUIContent label, GUIStyle style)
        {
            return style.CalcSize(label).x;
        }
    }
}
