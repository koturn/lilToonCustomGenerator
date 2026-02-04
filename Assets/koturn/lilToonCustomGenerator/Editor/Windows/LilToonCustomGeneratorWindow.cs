using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using Koturn.LilToonCustomGenerator.Editor.Enums;
using Koturn.LilToonCustomGenerator.Editor.Json;
using Koturn.LilToonCustomGenerator.Editor.Internals;
using System.Linq;
#if LILTOON
using lilToon;
#endif  // LILTOON


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// lilToon custom shader generator window.
    /// </summary>
    [System.Runtime.InteropServices.Guid("86433750-bf75-6434-fad1-5cc9da8420b0")]
    public sealed class LilToonCustomGeneratorWindow : EditorWindow
    {
        /// <summary>
        /// New line string selections.
        /// </summary>
        private static readonly string[] _newLineSelections =
        {
            "LF",
            "CR",
            "CR + LF"
        };
        /// <summary>
        /// Invalid characters for shader name.
        /// </summary>
        private static readonly char[] _invalidShaderNameChars = { '\n', '\r', '"' };
        /// <summary>
        /// <see cref="ReorderableListContainer{T}"/> for <see cref="ShaderPropertyDefinition"/>.
        /// </summary>
        private PropertyReorderableListContainer _propertyReorderableListContainer;
        /// <summary>
        /// <see cref="ReorderableListContainer{T}"/> for <see cref="V2FMember"/>.
        /// </summary>
        private V2FMemberReorderbleListContainer _v2fMemberReorderableListContainer;
        /// <summary>
        /// <see cref="ReorderableListContainer{T}"/> for <see cref="string"/>.
        /// </summary>
        private TextReorderableListContainer _packageKeywordReorderableListContaner;
        /// <summary>
        /// Error message list of <see cref="_propertyReorderableListContainer"/>.
        /// </summary>
        private readonly List<string> _propertyReorderableListErrorMessages = new List<string>();
        /// <summary>
        /// Error message list of <see cref="_v2fMemberReorderableListContainer"/>.
        /// </summary>
        private readonly List<string> _v2fMemberReorderableListErrorMessages = new List<string>();
        /// <summary>
        /// Current scroll position.
        /// </summary>
        private Vector2 _scrollPosition;
        /// <summary>
        /// Json root instance.
        /// </summary>
        private JsonRoot _jsonRoot;
        /// <summary>
        /// Template name array.
        /// </summary>
        private string[] _templateNames;
        /// <summary>
        /// Template index.
        /// </summary>
        private int _templateIndex;
        /// <summary>
        /// Custom shader name.
        /// </summary>
        private string _shaderName = "MyCustomShader";
        /// <summary>
        /// Custom shader title displaying on the inspector.
        /// </summary>
        private string _shaderTitle = "My Custom Shader";
        /// <summary>
        /// Namespace and assembly name.
        /// </summary>
        private string _namespace = "LilToonCustom.Editor";
        /// <summary>
        /// Inspector class name.
        /// </summary>
        private string _inspectorName = "CustomInspector";
        /// <summary>
        /// New line type.
        /// </summary>
        private NewLineType _newLineType;
        /// <summary>
        /// True to consider bug in the <c>LIL_CUSTOM_V2F_MEMBER</c> macro in lilToon 1.4.0.
        /// </summary>
        private bool _shouldEmitVer140Workaround = true;
        /// <summary>
        /// True to generate <c>Editor/Startup.cs</c>.
        /// </summary>
        private bool _shouldGenerateVersionDetectionHeader = false;
        /// <summary>
        /// True to allow unsafe code.
        /// </summary>
        private bool _allowUnsafeCode = false;
        /// <summary>
        /// True to generate <c>Editor/lang_custom.tsv</c>.
        /// </summary>
        private bool _shouldGenerateLangTsv = true;
        /// <summary>
        /// True to emit shader conversion menu.
        /// </summary>
        private bool _shouldGenerateConvertMenu = true;
        /// <summary>
        /// True to emit cache clear menu.
        /// </summary>
        private bool _shouldGenerateCacheClearMenu = false;
        /// <summary>
        /// True to emit geometry shader template to <c>Shaders/custom_insert_post.hlsl</c>.
        /// </summary>
        private bool _shouldEmitGeometryShader = false;
        /// <summary>
        /// True to override geometry shader of fur shaders.
        /// </summary>
        private bool _shouldOverrideFurGeometry = false;
        /// <summary>
        /// True to override geometry shader of one pass outline shaders.
        /// </summary>
        private bool _shouldOverrideOnePassOutlineGeometry = false;
        /// <summary>
        /// True to generate InsertPost tag and <c>Shaders/custom_insert_post.hlsl</c>.
        /// </summary>
        private bool _shouldGenerateInsertPost = false;
        /// <summary>
        /// <para>True to declare following variables.</para>
        /// <para>
        /// <list type="bullet">
        ///   <item>
        ///     <term><c>_VRChatCameraMode</c></term>
        ///     <description>0: Rendering normally, 1: Rendering in VR handheld camera, 2: Rendering in Desktop handheld camera, 3: Rendering for a screenshot</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatCameraMask</c></term>
        ///     <description>The cullingMask property of the active camera, available if _VRChatCameraMode != 0.</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatMirrorMode</c></term>
        ///     <description>0: Rendering normally, not in a mirror, 1: Rendering in a mirror viewed in VR, 2: Rendering in a mirror viewed in desktop mode</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatFaceMirrorMode</c></term>
        ///     <description>1 when rendering the face mirror (VR and Desktop use different camera types!), 0 otherwise.</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatMirrorCameraPos</c></term>
        ///     <description>World space position of mirror camera (eye independent, "centered" in VR). (0,0,0) when not rendering in a mirror.</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatScreenCameraPos</c></term>
        ///     <description>World space position of main screen camera.</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatScreenCameraRot</c></term>
        ///     <description>World space rotation (quaternion) of main screen camera.</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatPhotoCameraPos</c></term>
        ///     <description>World space position of handheld photo camera (first instance when using Dolly Multicam), (0,0,0) when camera is not active.</description>
        ///   </item>
        ///   <item>
        ///     <term><c>_VRChatPhotoCameraRot</c></term>
        ///     <description>World space rotation (quaternion) of photo camera.</description>
        ///   </item>
        /// </list>
        /// </para>
        /// </summary>
        /// <remarks>
        /// <seealso href="https://creators.vrchat.com/worlds/udon/vrc-graphics/vrchat-shader-globals/"/>
        /// </remarks>
        private bool _shouldDeclareVRChatVariables = false;
        /// <summary>
        /// <para>True to declare following two variables in <c>Shaders/custom_insert.hlsl</c>.</para>
        /// <para>
        /// <list type="bullet">
        ///   <item><c>_AudioTexture</c></item>
        ///   <item><c>_AudioTexture_TexelSize</c></item>
        /// </list>
        /// </para>
        /// </summary>
        private bool _shouldDeclareAudioLinkVariables = false;
        /// <summary>
        /// <para>True to declare following three variables in <c>Shaders/custom_insert.hlsl</c>.</para>
        /// <para>
        /// <list type="bullet">
        ///   <item><c>_Udon_VideoTex</c></item>
        ///   <item><c>_Udon_VideoTex_TexelSize</c></item>
        ///   <item><c>_Udon_VideoTex_ST</c></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <remarks>
        /// <seealso href="https://protv.dev/avatars"/>
        /// </remarks>
        private bool _shouldDeclareProTVVariables = false;
        /// <summary>
        /// True to generate <c>Editor/AssemblyInfo.cs</c>.
        /// </summary>
        private bool _shouldGenerateAssemblyInfo = true;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyTitleAttribute"/>.
        /// </summary>
        private string _assemblyTitle;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyDescriptionAttribute"/>.
        /// </summary>
        private string _assemblyDescription;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyCompanyAttribute"/>.
        /// </summary>
        private string _assemblyCompany;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyProductAttribute"/>.
        /// </summary>
        private string _assemblyProduct;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyCopyrightAttribute"/>.
        /// </summary>
        private string _assemblyCopyright;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyTrademarkAttribute"/>.
        /// </summary>
        private string _assemblyTrademark;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyCultureAttribute"/>.
        /// </summary>
        private string _assemblyCulture;
        /// <summary>
        /// Text for <see cref="System.Reflection.AssemblyVersionAttribute"/>.
        /// </summary>
        private string _assemblyVersion;
        /// <summary>
        /// True to generate package.json.
        /// </summary>
        private bool _shouldGeneratePackageJson = true;
        /// <summary>
        /// Value of "name" in package.json.
        /// </summary>
        private string _packageName;
        /// <summary>
        /// Value of "version" in package.json.
        /// </summary>
        private string _packageVersion;
        /// <summary>
        /// Value of "displayName" in package.json.
        /// </summary>
        private string _packageDisplayName;
        /// <summary>
        /// Value of "description" in package.json.
        /// </summary>
        private string _packageDescription;
        /// <summary>
        /// Value of "unity" in package.json.
        /// </summary>
        private string _packageUnityVersion;
        /// <summary>
        /// Value of "changelogUrl" in package.json.
        /// </summary>
        private string _packageChangeLogUrl;
        /// <summary>
        /// Value of "documentationUrl" in package.json.
        /// </summary>
        private string _packageDocumentationUrl;
        /// <summary>
        /// Value of "licenseUrl" in package.json.
        /// </summary>
        private string _packageLicenseUrl;
        /// <summary>
        /// Value of "license" in package.json.
        /// </summary>
        private string _packageLicense;
        /// <summary>
        /// Values of "author.name" in package.json.
        /// </summary>
        private string _packageAuthorName;
        /// <summary>
        /// Values of "author.email" in package.json.
        /// </summary>
        private string _packageAuthorEmail;
        /// <summary>
        /// Values of "author.url" in package.json.
        /// </summary>
        private string _packageAuthorUrl;
        /// <summary>
        /// Last export directory.
        /// </summary>
        private string _lastExportDirectoryPath;


        /// <summary>
        /// <para>Called when this window is created.</para>
        /// <para>Initialize <see cref="_serializedObject"/> and <see cref="_propertyReorderableListContainer"/>.</para>
        /// </summary>
        private void Awake()
        {
            _jsonRoot = DeserializeJson(AssetDatabase.GUIDToAssetPath("407d2dc27f05f774d9ca8d53fdef2047"));
            _templateNames = _jsonRoot.ConfigList.Select(config => config.Name).ToArray();

            _propertyReorderableListContainer = CreateInstance<PropertyReorderableListContainer>();
            _v2fMemberReorderableListContainer = CreateInstance<V2FMemberReorderbleListContainer>();
            _packageKeywordReorderableListContaner = CreateInstance<TextReorderableListContainer>();

            var userName = Environment.UserName;
            var m = RegexProvider.IdentifierRegex.Match(userName);
            if (m.Success)
            {
                var g = m.Groups;
                _namespace = g[1].Value.ToUpperInvariant() + g[2].Value + ".LilToonCustom.Editor";
                _shaderName = g[0].Value + "/MyCustomShader";
                _packageName = "com." + g[0].Value.ToLowerInvariant() + ".mycustomshader";
            }

            _assemblyTitle = _namespace;
            _assemblyDescription = $"Material inspector of {_shaderName}.";
            _assemblyCompany = userName;
            _assemblyProduct = _assemblyTitle;
            _assemblyCopyright = $"Copyright (C) {DateTime.Now.Year} {userName} All Rights Reserverd.";
            _assemblyTrademark = "";
            _assemblyCulture = "";
            _assemblyVersion = "1.0.0.0";

            _packageVersion = "1.0.0";
            _packageDisplayName = "MyCustomShader";
            _packageDescription = "My custom shader";
            _packageUnityVersion = "2019.4";
            _packageChangeLogUrl = "";
            _packageDocumentationUrl = "";
            _packageLicenseUrl = "";
            _packageLicense = "";

            var packageKeywordList = _packageKeywordReorderableListContaner.List;
            if (packageKeywordList.Count == 0)
            {
                packageKeywordList.Add("lilToon");
                packageKeywordList.Add("Material");
                packageKeywordList.Add("Custom shader");
            }

            _packageAuthorName = Environment.UserName;
            _packageAuthorEmail = "";
            _packageAuthorUrl = "";
        }


        /// <summary>
        /// Draw GUI components.
        /// </summary>
        private void OnGUI()
        {
            if (_jsonRoot == null)
            {
                EditorGUILayout.HelpBox("template.json is not loaded.", MessageType.Error);
                return;
            }

            var errorCount = 0;

            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Basic configuration", EditorStyles.boldLabel);
                _templateIndex = EditorGUILayout.Popup("Template", _templateIndex, _templateNames);
                _shaderName = EditorGUILayout.TextField("Shader name", _shaderName);
                if (string.IsNullOrEmpty(_shaderName))
                {
                    errorCount++;
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.HelpBox("Shader name must not be null.", MessageType.Error);
                    }
                }
                else if (_shaderName.IndexOfAny(_invalidShaderNameChars) > 0)
                {
                    errorCount++;
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.HelpBox("Invalid shader name.", MessageType.Error);
                    }
                }
                _shaderTitle = EditorGUILayout.TextField("Shader title", _shaderTitle);
                _namespace = EditorGUILayout.TextField("Inspector Namespace", _namespace);
                if (!RegexProvider.NamespaceRegex.IsMatch(_namespace))
                {
                    errorCount++;
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.HelpBox(
                            "Invalid namespace. Namespace names must be identifiers separated by periods.\n"
                                + "The first character of an identifier must be an alphabetical character or an underscore.\n"
                                + "Subsequent characters must be alphabetical characters, underscores, or digits.",
                            MessageType.Error);
                    }
                }
                _inspectorName = EditorGUILayout.TextField("Inspector class name", _inspectorName);
                if (!RegexProvider.IdentifierRegex.IsMatch(_inspectorName))
                {
                    errorCount++;
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.HelpBox(
                            "Invalid inspector name.\n"
                                + "The first character of an identifier must be an alphabetical character or an underscore.\n"
                                + "Subsequent characters must be alphabetical characters, underscores, or digits.",
                            MessageType.Error);
                    }
                }
                _newLineType = (NewLineType)EditorGUILayout.Popup("New Line Code", (int)_newLineType, _newLineSelections);
            }

            using (var svScope = new EditorGUILayout.ScrollViewScope(_scrollPosition))
            {
                _scrollPosition = svScope.scrollPosition;

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    using (var ccScope = new EditorGUI.ChangeCheckScope())
                    {
                        _propertyReorderableListContainer.Draw();
                        if (ccScope.changed)
                        {
                            _propertyReorderableListErrorMessages.Clear();
                            var invalidNameList = _propertyReorderableListContainer.GetInvalidPropertyNames();
                            if (invalidNameList.Count > 0)
                            {
                                _propertyReorderableListErrorMessages.Add("Following property names are invalid.\n" + string.Join("\n", invalidNameList));
                            }
                            var dupNameList = _propertyReorderableListContainer.GetDuplicatePropertyNames();
                            if (dupNameList.Count > 0)
                            {
                                _propertyReorderableListErrorMessages.Add("Duplicate property names are detected.\n" + string.Join("\n", dupNameList));
                            }
                        }
                    }
                    using (new EditorGUI.IndentLevelScope())
                    {
                        foreach (var errmsg in _propertyReorderableListErrorMessages)
                        {
                            EditorGUILayout.HelpBox(errmsg, MessageType.Error);
                        }
                    }
                }
                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    using (var ccScope = new EditorGUI.ChangeCheckScope())
                    {
                        _v2fMemberReorderableListContainer.Draw();
                        if (ccScope.changed)
                        {
                            _v2fMemberReorderableListErrorMessages.Clear();
                            var invalidNameList = _v2fMemberReorderableListContainer.GetInvalidMemberNames();
                            if (invalidNameList.Count > 0)
                            {
                                _v2fMemberReorderableListErrorMessages.Add("Following member names are invalid.\n" + string.Join("\n", invalidNameList));
                            }
                            var dupNameList = _v2fMemberReorderableListContainer.GetDuplicateMemberNames();
                            if (dupNameList.Count > 0)
                            {
                                _v2fMemberReorderableListErrorMessages.Add("Duplicate property names are detected.\n" + string.Join("\n", dupNameList));
                            }
                        }
                    }
                    if (_v2fMemberReorderableListContainer.List.Count > 0)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        {
                            _shouldEmitVer140Workaround = EditorGUILayout.ToggleLeft("Consider bug in the LIL_CUSTOM_V2F_MEMBER macro in lilToon 1.4.0", _shouldEmitVer140Workaround);
                        }
                    }
                    using (new EditorGUI.IndentLevelScope())
                    {
                        foreach (var errmsg in _v2fMemberReorderableListErrorMessages)
                        {
                            EditorGUILayout.HelpBox(errmsg, MessageType.Error);
                        }
                    }
                    var memberLimit = _shouldEmitVer140Workaround ? 7 : 8;
                    if (_v2fMemberReorderableListContainer.List.Count > memberLimit)
                    {
                        errorCount++;
                        using (new EditorGUI.IndentLevelScope())
                        {
                            EditorGUILayout.HelpBox(
                                "Number of member must be less than or euqal to " + memberLimit,
                                MessageType.Error);
                        }
                    }
                }

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.LabelField("Shader options", EditorStyles.boldLabel);
                    _shouldEmitGeometryShader = EditorGUILayout.ToggleLeft("Edit geometry shader", _shouldEmitGeometryShader);
                    if (_shouldEmitGeometryShader)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                        {
                            _shouldOverrideFurGeometry = EditorGUILayout.ToggleLeft("Override fur geometry shaders", _shouldOverrideFurGeometry);
                            _shouldOverrideOnePassOutlineGeometry = EditorGUILayout.ToggleLeft("Override one pass outline geometry shaders (HDRP only)", _shouldOverrideOnePassOutlineGeometry);
                        }
                        using (new EditorGUI.DisabledScope(_shouldEmitGeometryShader))
                        {
                            EditorGUILayout.ToggleLeft("Emit lilSubShaderInsertPost and generate lilCustomShaderInsertPost.lilblock and custom_insert_post.hlsl", _shouldEmitGeometryShader);
                        }
                    }
                    else
                    {
                        _shouldGenerateInsertPost = EditorGUILayout.ToggleLeft("Emit lilSubShaderInsertPost and generate lilCustomShaderInsertPost.lilblock and custom_insert_post.hlsl", _shouldGenerateInsertPost);
                    }
                    _shouldDeclareVRChatVariables = EditorGUILayout.ToggleLeft("Use VRChat variables", _shouldDeclareVRChatVariables);
                    _shouldDeclareAudioLinkVariables = EditorGUILayout.ToggleLeft("Use AudioLink variables", _shouldDeclareAudioLinkVariables);
                    _shouldDeclareProTVVariables = EditorGUILayout.ToggleLeft("Use ProTV variables", _shouldDeclareProTVVariables);
                }

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.LabelField("Inspector options", EditorStyles.boldLabel);
                    _shouldGenerateVersionDetectionHeader = EditorGUILayout.ToggleLeft("Generate Version Detection Header", _shouldGenerateVersionDetectionHeader);
                    if (_shouldGenerateVersionDetectionHeader)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        {
                            _allowUnsafeCode = EditorGUILayout.ToggleLeft("Allow unsafe code", _allowUnsafeCode);
                        }
                    }
                    _shouldGenerateLangTsv = EditorGUILayout.ToggleLeft("Generate Language File", _shouldGenerateLangTsv);
                    _shouldGenerateConvertMenu = EditorGUILayout.ToggleLeft("Generate Convert Menu", _shouldGenerateConvertMenu);
                    _shouldGenerateCacheClearMenu = EditorGUILayout.ToggleLeft("Generate Cache Clear Menu", _shouldGenerateCacheClearMenu);
                    _shouldGenerateAssemblyInfo = EditorGUILayout.ToggleLeft("Generate AssemblyInfo.cs", _shouldGenerateAssemblyInfo);
                    if (_shouldGenerateAssemblyInfo)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                        {
                            _assemblyTitle = EditorGUILayout.TextField("AssemblyTitle", _assemblyTitle);
                            _assemblyDescription = EditorGUILayout.TextField("AssemblyDescription", _assemblyDescription);
                            _assemblyCompany = EditorGUILayout.TextField("AssemblyCompany", _assemblyCompany);
                            _assemblyProduct = EditorGUILayout.TextField("AssemblyProduct", _assemblyProduct);
                            _assemblyCopyright = EditorGUILayout.TextField("AssemblyCopyright", _assemblyCopyright);
                            _assemblyTrademark = EditorGUILayout.TextField("AssemblyTrademark", _assemblyTrademark);
                            _assemblyCulture = EditorGUILayout.TextField("AssemblyCulture", _assemblyCulture);
                            _assemblyVersion = EditorGUILayout.TextField("AssemblyVersion", _assemblyVersion);
                            if (!RegexProvider.VersionNumberRegex.IsMatch(_assemblyVersion))
                            {
                                errorCount++;
                                using (new EditorGUI.IndentLevelScope())
                                {
                                    EditorGUILayout.HelpBox(
                                        "Version numbers must consist of one to four numeric parts separated by periods.",
                                        MessageType.Error);
                                }
                            }
                        }
                    }
                }

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.LabelField("Others", EditorStyles.boldLabel);
                    _shouldGeneratePackageJson = EditorGUILayout.ToggleLeft("Generate package.json", _shouldGeneratePackageJson);
                    if (_shouldGeneratePackageJson)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                        {
                            _packageName = EditorGUILayout.TextField("Name", _packageName);
                            if (string.IsNullOrEmpty(_packageName))
                            {
                                errorCount++;
                                using (new EditorGUI.IndentLevelScope())
                                {
                                    EditorGUILayout.HelpBox(
                                        "Package name must not be empty.",
                                        MessageType.Error);
                                }
                            }
                            else if (!RegexProvider.PackageNameRegex.IsMatch(_packageName))
                            {
                                errorCount++;
                                using (new EditorGUI.IndentLevelScope())
                                {
                                    EditorGUILayout.HelpBox(
                                        "Package name must consist of the following names separated by periods.\n"
                                            + "The first character of the name must be a lowercase letter or a digit.\n"
                                            + "Subsequent characters must be lowercase letters, digits, hyphens, or underscores.",
                                        MessageType.Error);
                                }
                            }
                            _packageVersion = EditorGUILayout.TextField("Version", _packageVersion);
                            if (string.IsNullOrEmpty(_packageVersion))
                            {
                                errorCount++;
                                using (new EditorGUI.IndentLevelScope())
                                {
                                    EditorGUILayout.HelpBox(
                                        "Package version must not be empty.",
                                        MessageType.Error);
                                }
                            }
                            else if (!RegexProvider.SemVerRegex.IsMatch(_packageVersion))
                            {
                                errorCount++;
                                using (new EditorGUI.IndentLevelScope())
                                {
                                    EditorGUILayout.HelpBox(
                                        "Package version numbers must follow semantic versioning.",
                                        MessageType.Error);
                                }
                            }
                            _packageDisplayName = EditorGUILayout.TextField("Display name", _packageDisplayName);
                            _packageDescription = EditorGUILayout.TextField("Description", _packageDescription);
                            _packageUnityVersion = EditorGUILayout.TextField("Minimal Unity version", _packageUnityVersion);
                            _packageChangeLogUrl = EditorGUILayout.TextField("Change log URL", _packageChangeLogUrl);
                            _packageDocumentationUrl = EditorGUILayout.TextField("Documentation URL", _packageDocumentationUrl);
                            _packageLicenseUrl = EditorGUILayout.TextField("License URL", _packageLicenseUrl);
                            _packageLicense = EditorGUILayout.TextField("License type", _packageLicense);
                            _packageKeywordReorderableListContaner.Draw();
                            _packageAuthorName = EditorGUILayout.TextField("Author name", _packageAuthorName);
                            _packageAuthorEmail = EditorGUILayout.TextField("Author E-mail", _packageAuthorEmail);
                            _packageAuthorUrl = EditorGUILayout.TextField("Author URL", _packageAuthorUrl);
                        }
                    }
                }
            }

            using (new EditorGUI.DisabledScope(errorCount > 0))
            {
                if (GUILayout.Button("Generate Custom Shader"))
                {
                    var exportDirPath = EditorUtility.SaveFolderPanel(
                        "Select export directory",
                        Directory.Exists(_lastExportDirectoryPath) ? _lastExportDirectoryPath : Application.dataPath,
                        string.Empty);
                    if (string.IsNullOrEmpty(exportDirPath))
                    {
                        return;
                    }

                    _lastExportDirectoryPath = exportDirPath;

                    Debug.LogFormat("Export dir: {0}", exportDirPath);

                    var assetPath = AssetPathHelper.AbsPathToAssetPath(exportDirPath);
                    Generate(assetPath == null ? exportDirPath : assetPath);
                }
            }
        }

        /// <summary>
        /// Generate custom shader files.
        /// </summary>
        /// <param name="dstDirAssetPath">Destination directory path.</param>
        private void Generate(string dstDirAssetPath)
        {
            var templateEngine = CreateTemplateEngine();
            var tagDict = templateEngine.TagDictionary;

            var isInProject = dstDirAssetPath.StartsWith("Assets") || dstDirAssetPath.StartsWith("Packages");
            if (isInProject)
            {
                tagDict.Add("BASE_DIRECTORY", dstDirAssetPath + "/");
            }

            // Generate `Shaders` directory and obtain its GUID.
            var shaderDirAssetPath = dstDirAssetPath + "/" + "Shaders";
            Directory.CreateDirectory(shaderDirAssetPath);

            var guidShaderDir = ReadOrGenerateGuid(shaderDirAssetPath, isInProject);
            if (guidShaderDir.Length != 0)
            {
                tagDict.Add("GUID_SHADER_DIR", guidShaderDir);
            }

            var config = _jsonRoot.ConfigList[_templateIndex];
            Debug.LogFormat("Generate files from {0}", config.Name);

            // Clone template list.
            var templates = new List<TemplateFileConfig>(config.Templates);

            // Try to find `Editor/lang_custom.tsv`.
            var langCustomIndex = IndexOfDestination(templates, "Editor/lang_custom.tsv");

            // Generate `Editor/lang_custom.tsv` and obtain its GUID.
            if (langCustomIndex != -1)
            {
                if (_shouldGenerateLangTsv)
                {
                    var tfcLangCustom = templates[langCustomIndex];

                    var dstFilePath = dstDirAssetPath + "/" + templateEngine.Replace(tfcLangCustom.Destination);
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFilePath));

                    var path = AssetDatabase.GUIDToAssetPath(tfcLangCustom.Guid);

                    Debug.LogFormat("  {0} -> {1}", path, dstFilePath);
                    templateEngine.ExpandTemplate(path, dstFilePath);

                    var guidLangCustom = ReadOrGenerateGuid(dstFilePath, isInProject);
                    if (guidLangCustom.Length != 0)
                    {
                        tagDict.Add("GUID_LANG_CUSTOM", guidLangCustom);
                    }
                }
                templates.RemoveAt(langCustomIndex);
            }

            if (!_shouldEmitGeometryShader && !_shouldGenerateInsertPost)
            {
                var index = IndexOfDestination(templates, "Shaders/custom_insert_post.hlsl");
                if (index != -1)
                {
                    templates.RemoveAt(index);
                }

                index = IndexOfDestination(templates, "Shaders/lilCustomShaderInsertPost.lilblock");
                if (index != -1)
                {
                    templates.RemoveAt(index);
                }
            }

            if (!_shouldGenerateVersionDetectionHeader)
            {
                var index = IndexOfDestination(templates, "Editor/Startup.cs");
                if (index != -1)
                {
                    templates.RemoveAt(index);
                }

                index = IndexOfDestination(templates, "Shaders/lil_current_version.hlsl");
                if (index != -1)
                {
                    templates.RemoveAt(index);
                }
            }

            var asmInfoIndex = IndexOfDestination(templates, "Editor/AssemblyInfo.cs");
            if (asmInfoIndex != -1)
            {
                if (_shouldGenerateAssemblyInfo)
                {
                    int asmdefIndex = 0;
                    foreach (var tfc in templates)
                    {
                        if (tfc.Destination.EndsWith(".asmdef"))
                        {
                            var asmdefPath = dstDirAssetPath + "/" + templateEngine.Replace(tfc.Destination);
                            Directory.CreateDirectory(Path.GetDirectoryName(asmdefPath));

                            var asmdefTemplatePath = AssetDatabase.GUIDToAssetPath(tfc.Guid);

                            Debug.LogFormat("  {0} -> {1}", asmdefTemplatePath, asmdefPath);
                            templateEngine.ExpandTemplate(asmdefTemplatePath, asmdefPath);

                            var guidAsmdef = ReadOrGenerateGuid(asmdefPath, isInProject);
                            if (guidAsmdef.Length != 0)
                            {
                                tagDict.Add("GUID_D_ASMDEF", new Guid(guidAsmdef).ToString("D"));
                            }

                            break;
                        }
                        asmdefIndex++;
                    }
                    if (asmdefIndex < templates.Count)
                    {
                        templates.RemoveAt(asmdefIndex);
                        if (asmInfoIndex > asmdefIndex)
                        {
                            asmInfoIndex--;
                        }
                    }

                    var asmInfoPath = dstDirAssetPath + "/Editor/AssemblyInfo.cs";

                    if (!tagDict.ContainsKey("GUID_D_ASMDEF"))
                    {
                        // Create empty file.
                        Directory.CreateDirectory(Path.GetDirectoryName(asmInfoPath));
                        File.Create(asmInfoPath).Dispose();

                        // Generate GUID.
                        var guidAsmInfo = ReadOrGenerateGuid(asmInfoPath, isInProject);
                        if (guidAsmInfo.Length != 0)
                        {
                            tagDict.Add("GUID_D_ASSEMBLY_INFO", new Guid(guidAsmInfo).ToString("D"));
                        }
                    }

                    // Overwrite empty file.
                    var asmInfoTemplatePath = AssetDatabase.GUIDToAssetPath(templates[asmInfoIndex].Guid);

                    Debug.LogFormat("  {0} -> {1}", asmInfoTemplatePath, asmInfoPath);
                    templateEngine.ExpandTemplate(asmInfoTemplatePath, asmInfoPath);
                }

                templates.RemoveAt(asmInfoIndex);
            }

            foreach (var tfc in templates)
            {
                var dstFilePath = dstDirAssetPath + "/" + templateEngine.Replace(tfc.Destination);
                Directory.CreateDirectory(Path.GetDirectoryName(dstFilePath));

                var templateFilePath = AssetDatabase.GUIDToAssetPath(tfc.Guid);
                if (string.IsNullOrEmpty(templateFilePath))
                {
                    throw new InvalidOperationException(tfc.Guid);
                }

                Debug.LogFormat("  {0} -> {1}", templateFilePath, dstFilePath);
                templateEngine.ExpandTemplate(templateFilePath, dstFilePath);
            }

            if (isInProject)
            {
                // Import created files.
                AssetDatabase.ImportAsset(dstDirAssetPath, ImportAssetOptions.ImportRecursive);
                // AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// Create <see cref="TemplateEngine"/> instance.
        /// </summary>
        /// <returns><see cref="TemplateEngine"/> instance.</returns>
        private TemplateEngine CreateTemplateEngine()
        {
            var shaderPropDefList = _propertyReorderableListContainer.List;
            var materialPropNames = new string[shaderPropDefList.Count];
            var langTags = new string[shaderPropDefList.Count];

            var index = 0;
            foreach (var shaderProp in shaderPropDefList)
            {
                var m = RegexProvider.PropertyNameRegex.Match(shaderProp.name);
                if (m.Success)
                {
                    var g = m.Groups;
                    materialPropNames[index] = "_" + g[1].Value.ToLower() + g[2].Value;
                    langTags[index] = "s" + g[1].Value + g[2].Value;
                }
                index++;
            }

            var tagDict = new Dictionary<string, string>
            {
                { "SHADER_NAME", _shaderName },
                { "NAMESPACE", _namespace },
                { "SHADER_TITLE", _shaderTitle },
                { "INSPECTOR_NAME", _inspectorName },
            };

            if (_shouldGenerateAssemblyInfo)
            {
                tagDict.Add("ASSEMBLY_TITLE", EscapeString(_assemblyTitle));
                tagDict.Add("ASSEMBLY_DESCRIPTION", EscapeString(_assemblyDescription));
                tagDict.Add("ASSEMBLY_COMPANY", EscapeString(_assemblyCompany));
                tagDict.Add("ASSEMBLY_PRODUCT", EscapeString(_assemblyProduct));
                tagDict.Add("ASSEMBLY_COPYRIGHT", EscapeString(_assemblyCopyright));
                tagDict.Add("ASSEMBLY_TRADEMARK", EscapeString(_assemblyTrademark));
                tagDict.Add("ASSEMBLY_CULTURE", EscapeString(_assemblyCulture));
                tagDict.Add("ASSEMBLY_VERSION", EscapeString(_assemblyVersion));
            }

            var sb = new StringBuilder();

            index = 0;
            foreach (var shaderProp in shaderPropDefList)
            {
                sb.AppendLine("/// <summary>")
                    .AppendFormat("/// <see cref=\"MaterialProperty\" of \"{0}\".", shaderProp.name).AppendLine()
                    .AppendLine("/// </summary>")
                    .AppendFormat("private MaterialProperty {0};", materialPropNames[index])
                    .AppendLine();
                index++;
            }
            tagDict.Add("DECLARE_MATERIAL_PROPERTIES", sb.ToString());

            sb.Clear();
            index = 0;
            foreach (var shaderProp in shaderPropDefList)
            {
                sb.AppendFormat("{0} = FindProperty(\"{1}\", props);", materialPropNames[index], shaderProp.name)
                    .AppendLine();
                index++;
            }
            tagDict.Add("INITIALIZE_MATERIAL_PROPERTIES", sb.ToString());

            sb.Clear();
            index = 0;
            foreach (var shaderProp in shaderPropDefList)
            {
                sb.AppendFormat("propertyList.Add({0});", materialPropNames[index])
                    .AppendLine();
                index++;
            }
            tagDict.Add("INITIALIZE_MATERIAL_PROPERTY_LIST", sb.ToString());

            if (_shouldGenerateLangTsv)
            {
                sb.Clear();
                index = 0;
                foreach (var shaderProp in shaderPropDefList)
                {
                    sb.AppendFormat(
                        "m_MaterialEditor.ShaderProperty({0}, GetLoc({0}.displayName));",
                        materialPropNames[index]).AppendLine();
                    index++;
                }
                tagDict.Add("DRAW_MATERIAL_PROPERTIES", sb.ToString());

                sb.Clear();
                index = 0;
                foreach (var shaderProp in shaderPropDefList)
                {
                    sb.AppendFormat("{0}\t{1}\t{1}\t{1}\t{1}\t{1}", langTags[index], shaderProp.description.Length == 0 ? langTags[index] : shaderProp.description)
                        .AppendLine();
                    index++;
                }
                tagDict.Add("LANGUAGE_FILE_CONTENT", sb.ToString());
            }
            else
            {
                sb.Clear();
                index = 0;
                foreach (var shaderProp in shaderPropDefList)
                {
                    sb.AppendFormat(
                        "m_MaterialEditor.ShaderProperty({0}, {0}.displayName);",
                        materialPropNames[index]).AppendLine();
                    index++;
                }
                tagDict.Add("DRAW_MATERIAL_PROPERTIES", sb.ToString());
            }

            sb.Clear();
            index = 0;
            foreach (var shaderProp in shaderPropDefList)
            {
                if (shaderProp.IsTexture)
                {
                    sb.AppendFormat(
                        "lilEditorGUI.LocalizedPropertyTexture(m_MaterialEditor, new GUIContent(GetLoc({0}.displayName), GetLoc(\"sTextureRGBA\")), {0});",
                        materialPropNames[index]);
                }
                else
                {
                    sb.AppendFormat(
                        "lilEditorGUI.LocalizedProperty(m_MaterialEditor, {0});",
                        materialPropNames[index]);
                }
                sb.AppendLine();
                index++;
            }
            tagDict.Add("DRAW_LOCALIZED_MATERIAL_PROPERTIES", sb.ToString());

            sb.Clear();
            if (_shouldGenerateLangTsv)
            {
                index = 0;
                foreach (var shaderProp in shaderPropDefList)
                {
                    sb.AppendFormat(
                        "{0} (\"{1}\", {2}) = {3}",
                        shaderProp.name,
                        langTags[index],
                        shaderProp.PropertyTypeText,
                        shaderProp.DefaultValueString).AppendLine();
                    index++;
                }
            }
            else
            {
                foreach (var shaderProp in shaderPropDefList)
                {
                    sb.AppendFormat(
                        "{0} (\"{1}\", {2}) = {3}",
                        shaderProp.name,
                        shaderProp.description,
                        shaderProp.PropertyTypeText,
                        shaderProp.DefaultValueString).AppendLine();
                }
            }
            tagDict.Add("DECLARE_CUSTOM_PROPERTIES", sb.ToString());

            sb.Clear();
            foreach (var shaderProp in shaderPropDefList)
            {
                if (shaderProp.IsTexture)
                {
                    continue;
                }
                if (sb.Length > 0)
                {
                    sb.Append(@" \").AppendLine();
                }
                sb.AppendFormat("{0} {1};", ShaderPropertyDefinition.VariableTypeSelections[(int)shaderProp.uniformType], shaderProp.name);
            }
            if (sb.Length > 0)
            {
                sb.AppendLine();
                tagDict.Add("DECLARE_UNIFORM_VARIABLES", sb.ToString());
            }

            sb.Clear();
            foreach (var shaderProp in shaderPropDefList)
            {
                var textureDeclarationMacro = shaderProp.TextureDeclarationMacro;
                if (textureDeclarationMacro == null)
                {
                    continue;
                }
                if (sb.Length > 0)
                {
                    sb.Append(@" \").AppendLine();
                }
                sb.AppendFormat("{0}({1});", textureDeclarationMacro, shaderProp.name);
            }
            if (sb.Length > 0)
            {
                sb.AppendLine();
                tagDict.Add("DECLARE_TEXTURE_VARIABLES", sb.ToString());
            }

            var v2fMemberList = _v2fMemberReorderableListContainer.List;

            sb.Clear();
            index = 0;
            foreach (var v2fMember in v2fMemberList)
            {
                if (sb.Length > 0)
                {
                    sb.Append(@" \").AppendLine();
                }
                if (v2fMember.interpolationModifier != InterpolationModifier.Linear && !v2fMember.IsInteger)
                {
                    sb.Append(v2fMember.InterpolationModifierText).Append(' ');
                }
                sb.AppendFormat("{0} {1} : TEXCOORD ## id{2};", v2fMember.VariableTypeText, v2fMember.name, index);
                index++;
            }
            if (sb.Length > 0)
            {
                sb.AppendLine();
                tagDict.Add("V2F_MEMBERS", sb.ToString());
            }

            if (_shouldEmitVer140Workaround)
            {
                sb.Clear();
                index = 1;  // Avoid to use id0.
                foreach (var v2fMember in v2fMemberList)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(@" \").AppendLine();
                    }
                    if (v2fMember.interpolationModifier != InterpolationModifier.Linear && !v2fMember.IsInteger)
                    {
                        sb.Append(v2fMember.InterpolationModifierText).Append(' ');
                    }
                    sb.AppendFormat("{0} {1} : TEXCOORD ## id{2};", v2fMember.VariableTypeText, v2fMember.name, index);
                    index++;
                }
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                    tagDict.Add("V2F_MEMBERS_VER140_SHADOWCASTER", sb.ToString());
                }
            }

            sb.Clear();
            index = 0;
            foreach (var v2fMember in v2fMemberList)
            {
                if (sb.Length > 0)
                {
                    sb.Append(@" \").AppendLine();
                }
                sb.AppendFormat("output.{0} = ({1})0;  /* TODO: Initialize with the appropriate value. */", v2fMember.name, v2fMember.VariableTypeText);
                index++;
            }
            if (sb.Length > 0)
            {
                sb.AppendLine();
                tagDict.Add("INITIALIZE_V2F_MEMBERS", sb.ToString());
            }

            if (_shouldGeneratePackageJson)
            {
                tagDict.Add("PACKAGE_NAME", EscapeString(_packageName));
                tagDict.Add("PACKAGE_VERSION", EscapeString(_packageVersion));
                tagDict.Add("PACKAGE_DISPLAY_NAME", EscapeString(_packageDisplayName));
                tagDict.Add("PACKAGE_DESCRIPTION", EscapeString(_packageDescription));
                tagDict.Add("PACKAGE_UNITY_VERSION", EscapeString(_packageUnityVersion));
                tagDict.Add("PACKAGE_CHANGELOG_URL", EscapeString(_packageChangeLogUrl));
                tagDict.Add("PACKAGE_DOCUMENTATION_URL", EscapeString(_packageDocumentationUrl));
                tagDict.Add("PACKAGE_LICENSE_URL", EscapeString(_packageLicenseUrl));
                tagDict.Add("PACKAGE_LICENSE", EscapeString(_packageLicense));

                sb.Clear();
                index = 0;
                foreach (var keyword in _packageKeywordReorderableListContaner.List)
                {
                    if (string.IsNullOrEmpty(keyword))
                    {
                        continue;
                    }
                    if (index > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.AppendFormat("\"{0}\"", EscapeString(keyword));
                    index++;
                }
                if (sb.Length == 0)
                {
                    sb.Append("\"lilToon\"");
                }
                tagDict.Add("PACKAGE_KEYWORDS", sb.ToString());

                index = 0;
                sb.Clear();
                foreach (var kv in new[] {("name", _packageAuthorName), ("email", _packageAuthorEmail), ("url", _packageAuthorUrl)})
                {
                    if (string.IsNullOrEmpty(kv.Item2))
                    {
                        continue;
                    }
                    if (index > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.AppendFormat("\"{0}\": \"{1}\"", kv.Item1, kv.Item2);
                    index++;
                }
                if (sb.Length != 0)
                {
                    tagDict.Add("PACKAGE_AUTHOR_KEYVALUES", sb.ToString());
                }
            }

            if (!_namespace.StartsWith("lilToon."))
            {
                tagDict.Add("SHOULD_EMIT_USING_LILTOON", "true");
            }
            if (_shouldEmitGeometryShader)
            {
                tagDict.Add("SHOULD_EMIT_GEOMETRY_SHADER", "true");
            }
            if (_shouldOverrideFurGeometry)
            {
                tagDict.Add("OVERRIDE_FUR_GEOMETRY", "true");
            }
            if (_shouldOverrideOnePassOutlineGeometry)
            {
                tagDict.Add("OVERRIDE_ONEPASS_GEOMETRY", "true");
            }
            if (_shouldEmitGeometryShader || _shouldGenerateInsertPost)
            {
                tagDict.Add("SHOULD_GENERATE_INSERT_POST", "true");
            }
            if (_shouldGenerateConvertMenu)
            {
                tagDict.Add("SHOULD_GENERATE_CONVERT_MENU", "true");
            }
            if (_shouldGenerateCacheClearMenu)
            {
                tagDict.Add("SHOULD_GENERATE_REFRESH_MENU", "true");
            }
            if (_shouldGenerateVersionDetectionHeader)
            {
                tagDict.Add("SHOULD_GENERATE_VERSION_DEF_FILE", "true");
                if (_allowUnsafeCode)
                {
                    tagDict.Add("ALLOW_UNSAFE_CODE", "true");
                }
#if LILTOON
                tagDict.Add("LIL_CURRENT_VERSION_VALUE", lilConstants.currentVersionValue.ToString());
                var match = RegexProvider.VersionNumberRegex.Match(lilConstants.currentVersionName);
                if (match.Success)
                {
                    var g = match.Groups;
                    tagDict.Add("LIL_CURRENT_VERSION_MAJOR", g[1].Value);
                    tagDict.Add("LIL_CURRENT_VERSION_MINOR", g[2].Value);
                    tagDict.Add("LIL_CURRENT_VERSION_PATCH", g[3].Value);
                }
                else
                {
                    tagDict.Add("LIL_CURRENT_VERSION_MAJOR", "2");
                    tagDict.Add("LIL_CURRENT_VERSION_MINOR", "3");
                    tagDict.Add("LIL_CURRENT_VERSION_PATCH", "2");
                }
#else
                tagDict.Add("LIL_CURRENT_VERSION_VALUE", "45");
                tagDict.Add("LIL_CURRENT_VERSION_MAJOR", "2");
                tagDict.Add("LIL_CURRENT_VERSION_MINOR", "3");
                tagDict.Add("LIL_CURRENT_VERSION_PATCH", "2");
#endif  // LILTOON
            }
            if (_shouldDeclareVRChatVariables)
            {
                tagDict.Add("SHOULD_DECLARE_VRCHAT_VARIABLES", "true");
            }
            if (_shouldDeclareAudioLinkVariables)
            {
                tagDict.Add("SHOULD_DECLARE_AUDIOLINK_VARIABLES", "true");
            }
            if (_shouldDeclareProTVVariables)
            {
                tagDict.Add("SHOULD_DECLARE_PROTV_VARIABLES", "true");
            }

            var newLine = _newLineType == NewLineType.CrLf ? "\n\r" : _newLineType == NewLineType.Cr ? "\r" : "\n";
            return new TemplateEngine(tagDict, newLine);
        }


        /// <summary>
        /// Open window.
        /// </summary>
        [MenuItem("Window/koturn/lilToon Custom Generator")]
        private static void OpenWindow()
        {
            GetWindow<LilToonCustomGeneratorWindow>("lilToon Custom Generator");
        }

        /// <summary>
        /// Deserialize specified json file.
        /// </summary>
        /// <param name="filePath">Json file path.</param>
        /// <returns>Deserialize result, <see cref="JsonRoot"/> instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when circular definition is detected.</exception>
        private static JsonRoot DeserializeJson(string filePath)
        {
            var jsonRoot = JsonRoot.LoadFromJsonFile(filePath);
            var nameConfigDict = new Dictionary<string, TemplateConfig>();

            // Create list to resolve their parents.
            var inheritList = new List<TemplateConfig>();
            foreach (var config in jsonRoot.ConfigList)
            {
                nameConfigDict.Add(config.Name, config);
                if (config.BasedOn != null)
                {
                    inheritList.Add(config);
                }
            }

            // Resolve "basedOn".
            var visitSet = new HashSet<string>();
            var dstSet = new HashSet<string>();
            foreach (var config in inheritList)
            {
                dstSet.Clear();
                foreach (var tfc in config.Templates)
                {
                    dstSet.Add(tfc.Destination);
                }

                visitSet.Clear();
                visitSet.Add(config.Name);

                var parentConfig = config;
                while (parentConfig.BasedOn != null)
                {
                    if (visitSet.Contains(parentConfig.BasedOn))
                    {
                        throw new InvalidOperationException("Circular definition detected: " + config.Name);
                    }

                    parentConfig = nameConfigDict[parentConfig.BasedOn];
                    foreach (var tfc in parentConfig.Templates)
                    {
                        if (!dstSet.Contains(tfc.Destination))
                        {
                            config.Templates.Add(tfc);
                            dstSet.Add(tfc.Destination);
                        }
                    }
                }
            }

            return jsonRoot;
        }

        /// <summary>
        /// Escape specified string for json.
        /// </summary>
        /// <param name="s">Target string.</param>
        /// <returns>Escaped string.</returns>
        private static string EscapeString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var sb = new StringBuilder(s.Length + 4);
            foreach (var c in s)
            {
                switch (c)
                {
                    case '\\':
                    case '"':
                        sb.Append('\\').Append(c);
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    default:
                        if (c < ' ')
                        {
                            var t = $"000{c:X}";
                            sb.Append("\\u" + t.Substring(t.Length - 4));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Search index of <see cref="TemplateFileConfig"/> instance in the list which has specified destination.
        /// </summary>
        /// <param name="tfcList"><see cref="List{T}"/> of <see cref="TemplateFileConfig"/>.</param>
        /// <param name="destination">Target destination.</param>
        /// <returns>Index of <see cref="TemplateFileConfig"/> instance if found, otherwise -1.</returns>
        private static int IndexOfDestination(List<TemplateFileConfig> tfcList, string destination)
        {
            int index = 0;
            foreach (var tfc in tfcList)
            {
                if (tfc.Destination == destination)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Read or generate GUID string of specified asset.
        /// </summary>
        /// <param name="path">Target path.</param>
        /// <param name="isInProject">True to import specified path with <see cref="AssetDatabase.ImportAsset(string)"/>.</param>
        /// <returns>GUID string of specified path.</returns>
        private static string ReadOrGenerateGuid(string path, bool isInProject)
        {
            if (isInProject)
            {
                AssetDatabase.ImportAsset(path);
                return AssetDatabase.AssetPathToGUID(path);
            }
            else
            {
                return AssetHelper.CreateMetaFileIfNotExists(path + ".meta").ToString("N");
            }
        }
    }
}
