using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Koturn.LilToonCustomGenerator.Editor.Enums;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// Shader property definition.
    /// </summary>
    [System.Runtime.InteropServices.Guid("168e512a-09d3-8fa4-9a0f-0b253a1b19e3")]
    [Serializable]
    public sealed class ShaderPropertyDefinition
    {
        /// <summary>
        /// Serialize name of backing field of <see cref="Name"/>.
        /// </summary>
        public const string NameOfName = nameof(_name);
        /// <summary>
        /// Serialize name of backing field of <see cref="Description"/>.
        /// </summary>
        public const string NameOfDescription = nameof(_description);
        /// <summary>
        /// Serialize name of backing field of <see cref="PropertyType"/>.
        /// </summary>
        public const string NameOfPropertyType = nameof(_propertyType);
        /// <summary>
        /// Serialize name of backing field of <see cref="UniformType"/>.
        /// </summary>
        public const string NameOfUniformType = nameof(_uniformType);
        /// <summary>
        /// Serialize name of backing field of <see cref="RangeMinMax"/>.
        /// </summary>
        public const string NameOfRangeMinMax = nameof(_rangeMinMax);
        /// <summary>
        /// Serialize name of backing field of <see cref="DefaultFloat"/>.
        /// </summary>
        public const string NameOfDefaultFloat = nameof(_defaultFloat);
        /// <summary>
        /// Serialize name of backing field of <see cref="DefaultInt"/>.
        /// </summary>
        public const string NameOfDefaultInt = nameof(_defaultInt);
        /// <summary>
        /// Serialize name of backing field of <see cref="DefaultVector"/>.
        /// </summary>
        public const string NameOfDefaultVector = nameof(_defaultVector);
        /// <summary>
        /// Serialize name of backing field of <see cref="DefaultColor"/>.
        /// </summary>
        public const string NameOfDefaultColor = nameof(_defaultColor);
        /// <summary>
        /// Serialize name of backing field of <see cref="DefaultTextureIndex"/>.
        /// </summary>
        public const string NameOfDefaultTextureIndex = nameof(_defaultTextureIndex);
        /// <summary>
        /// Serialize name of backing field of <see cref="DrawerType"/>.
        /// </summary>
        public const string NameOfDrawerType = nameof(_drawerType);
        /// <summary>
        /// Serialize name of backing field of <see cref="NameOfDrawerArgument"/>.
        /// </summary>
        public const string NameOfDrawerArgument = nameof(_drawerArgument);

        /// <summary>
        /// Property types.
        /// </summary>
        public static string[] PropertyTypeSelections { get; } =
        {
            "Float",
            "Int",
            "Range",
            "Vector",
            "Color",
            "2D",
            "3D",
            "Cube"
        };
        /// <summary>
        /// Variable types in HLSL.
        /// </summary>
        public static string[] VariableTypeSelections { get; } =
        {
            "float",
            "float2",
            "float3",
            "float4",
            "half",
            "half2",
            "half3",
            "half4",
            "fixed",
            "fixed2",
            "fixed3",
            "fixed4",
            "bool",
            "lilBool",
            "int",
            "int2",
            "int3",
            "int4",
            "uint",
            "uint2",
            "uint3",
            "uint4",
            "Texture2D",
            "Texture2DArray",
            "Texture3D",
            "TextureCUBE"
        };
        /// <summary>
        /// Variable type names suitable for shader property <c>Float</c> or <c>Range</c>.
        /// </summary>
        public static string[] FloatPropertyVariableTypes { get; } =
        {
            "float",
            "half",
            "fixed"
        };
        /// <summary>
        /// Variable type names suitable for shader property <c>Int</c>.
        /// </summary>
        public static string[] IntPropertyVariableTypes { get; } =
        {
            "int",
            "uint",
            "bool",
            "lilBool"
        };
        public static string[] RangePropertyVariableTypes { get; } =
        {
            "float",
            "half",
            "fixed",
            "int",
            "uint"
        };
        /// <summary>
        /// Variable type names suitable for shader property <c>Vector</c>.
        /// </summary>
        public static string[] VectorPropertyVariableTypes { get; } =
        {
            "float2",
            "float3",
            "float4",
            "half2",
            "half3",
            "half4",
            "fixed2",
            "fixed3",
            "fixed4",
            "int2",
            "int3",
            "int4",
            "uint2",
            "uint3",
            "uint4"
        };
        /// <summary>
        /// Variable type names suitable for shader property <c>Color</c>.
        /// </summary>
        public static string[] ColorPropertyVariableTypes { get; } =
        {
            "float3",
            "float4",
            "half3",
            "half4",
            "fixed3",
            "fixed4"
        };
        /// <summary>
        /// Variable type names suitable for shader property <c>2D</c>.
        /// </summary>
        public static string[] Texture2DPropertyVariableTypes { get; } =
        {
            "Texture2D",
            "Texture2DArray"
        };
        /// <summary>
        /// Variable type names suitable for shader property <c>3D</c>.
        /// </summary>
        public static string[] Texture3DPropertyVariableTypes { get; } =
        {
            "Texture3D"
        };
        /// <summary>
        /// Variable type names suitable for shader property <c>Cube</c>.
        /// </summary>
        public static string[] TextureCubePropertyVariableTypes { get; } =
        {
            "TextureCUBE"
        };
        /// <summary>
        /// All drawer selections.
        /// </summary>
        public static string[] AllDrawerSelections { get; } =
        {
            "None",
            "Toggle",
            "ToggleOff",
            "PowerSlider",
            "IntRange",
            "KeywordEnum",
            "Enum",
            "Gamma",
            "HDR",
            "NoScaleOffset",
            "Normal",
            "lilHDR",
            "lilToggle",
            "lilToggleLeft",
            "lilAngle",
            "lilLOD",
            "lilBlink",
            "lilVec2R",
            "lilVec2",
            "lilVec3",
            "lilVec3Float",
            "lilHSVG",
            "lilUVAnim",
            "lilDecalAnim",
            "lilDecalSub",
            "lilEnum",
            "lilEnumLabel",
            "lilColorMask",
            "lil3Param",
            "lilFF",
            "lilFFFF",
            "lilFFFB",
            "lilFRFR",
            "lilVec3BDrawer",
            "lilALUVParams",
            "lilALLocal",
            "lilDissolve",
            "lilDissolveP",
            "lilOLWidth",
            "lilGlitParam1",
            "lilGlitParam2",
        };
        /// <summary>
        /// Default texture names.
        /// </summary>
        public static string[] DefaultTextureNames { get; } =
        {
            "black",
            "white",
            "gray",
            "red",
            "bump"
        };
        /// <summary>
        /// Suitable Drawers for Float property.
        /// </summary>
        public static string[] FloatDrawerSelections { get; } =
        {
            "None",
            "Gamma",
            "lilAngle",
            "lilLOD"
        };
        /// <summary>
        /// Suitable Drawers for Int property.
        /// </summary>
        public static string[] IntDrawerSelections { get; } =
        {
            "None",
            "Toggle",
            "ToggleOff",
            "KeywordEnum",
            "Enum",
            "lilToggle",
            "lilToggleLeft",
            "lilEnum",
            "lilEnumLabel",
            "lilColorMask"
        };
        /// <summary>
        /// Suitable Drawers for Range property.
        /// </summary>
        public static string[] RangeDrawerSelections { get; } =
        {
            "None",
            "PowerSlider",
            "IntRange",
            "lilOLWidth"
        };
        /// <summary>
        /// Suitable Drawers for Vector property.
        /// </summary>
        public static string[] VectorDrawerSelections { get; } =
        {
            "None",
            "Gamma",
            "lilBlink",
            "lilVec2R",
            "lilVec2",
            "lilVec3",
            "lilVec3Float",
            "lilHSVG",
            "lilUVAnim",
            "lilDecalAnim",
            "lilDecalSub",
            "lil3Param",
            "lilFF",
            "lilFFFF",
            "lilFFFB",
            "lilFRFR",
            "lilVec3BDrawer",
            "lilALUVParams",
            "lilALLocal",
            "lilDissolve",
            "lilDissolveP"
        };
        /// <summary>
        /// Suitable Drawers for Color property.
        /// </summary>
        public static string[] ColorDrawerSelections { get; } =
        {
            "None",
            "HDR",
            "lilHDR"
        };
        public static string[] TextureDrawerSelections { get; } =
        {
            "None",
            "NoScaleOffset",
            "Normal"
        };
        /// <summary>
        /// Property names used in lilToon.
        /// </summary>
        public static HashSet<string> LilToonPropertyNameSet { get; } = new HashSet<string>(new []
        {
            "_AlphaMask",
            "_AlphaMaskMode",
            "_AlphaMaskScale",
            "_AlphaMaskValue",
            "_AlphaToMask",
            "_Anisotropy2MatCap",
            "_Anisotropy2MatCap2nd",
            "_Anisotropy2Reflection",
            "_AnisotropyScaleMask",
            "_AnisotropyShiftNoiseMask",
            "_AnisotropyTangentMap",
            "_ApplyReflection",
            "_ApplySpecular",
            "_ApplySpecularFA",
            "_AsOverlay",
            "_AudioLink2Emission",
            "_AudioLink2Emission2nd",
            "_AudioLink2Emission2ndGrad",
            "_AudioLink2EmissionGrad",
            "_AudioLink2Main2nd",
            "_AudioLink2Main3rd",
            "_AudioLink2Vertex",
            "_AudioLinkAsLocal",
            "_AudioLinkDefaultValue",
            "_AudioLinkLocalMap",
            "_AudioLinkLocalMapParams",
            "_AudioLinkMask",
            "_AudioLinkMask_ScrollRotate",
            "_AudioLinkMask_UVMode",
            "_AudioLinkStart",
            "_AudioLinkUVMode",
            "_AudioLinkUVParams",
            "_AudioLinkVertexStart",
            "_AudioLinkVertexStrength",
            "_AudioLinkVertexUVMode",
            "_AudioLinkVertexUVParams",
            "_BackfaceColor",
            "_BacklightBackfaceMask",
            "_BacklightColor",
            "_BacklightColorTex",
            "_BacklightDirectivity",
            "_BacklightReceiveShadow",
            "_BaseColor",
            "_BaseColorMap",
            "_BaseMap",
            "_BeforeExposureLimit",
            "_BitKey0",
            "_BitKey1",
            "_BitKey10",
            "_BitKey11",
            "_BitKey12",
            "_BitKey13",
            "_BitKey14",
            "_BitKey15",
            "_BitKey16",
            "_BitKey17",
            "_BitKey18",
            "_BitKey19",
            "_BitKey2",
            "_BitKey20",
            "_BitKey21",
            "_BitKey22",
            "_BitKey23",
            "_BitKey24",
            "_BitKey25",
            "_BitKey26",
            "_BitKey27",
            "_BitKey28",
            "_BitKey29",
            "_BitKey3",
            "_BitKey30",
            "_BitKey31",
            "_BitKey4",
            "_BitKey5",
            "_BitKey6",
            "_BitKey7",
            "_BitKey8",
            "_BitKey9",
            "_BlendOp",
            "_BlendOpAlpha",
            "_BlendOpAlphaFA",
            "_BlendOpFA",
            "_Bump2ndMap",
            "_Bump2ndMap_UVMode",
            "_Bump2ndScaleMask",
            "_BumpMap",
            "_Color",
            "_Color2nd",
            "_Color3rd",
            "_ColorMask",
            "_Cull",
            "_DissolveColor",
            "_DissolveMask",
            "_DissolveNoiseMask",
            "_DissolveNoiseMask_ScrollRotate",
            "_DissolveParams",
            "_DissolvePos",
            "_DistanceFade",
            "_DistanceFadeColor",
            "_DistanceFadeMode",
            "_DistanceFadeRimColor",
            "_DitherMaxValue",
            "_DitherTex",
            "_DstBlend",
            "_DstBlendAlpha",
            "_DstBlendAlphaFA",
            "_DstBlendFA",
            "_DummyProperty",
            "_Emission2ndBlendMask",
            "_Emission2ndBlendMask_ScrollRotate",
            "_Emission2ndBlendMode",
            "_Emission2ndBlink",
            "_Emission2ndColor",
            "_Emission2ndGradSpeed",
            "_Emission2ndGradTex",
            "_Emission2ndMap",
            "_Emission2ndMap_ScrollRotate",
            "_Emission2ndMap_UVMode",
            "_Emission2ndUseGrad",
            "_EmissionBlendMask",
            "_EmissionBlendMask_ScrollRotate",
            "_EmissionBlendMode",
            "_EmissionBlink",
            "_EmissionColor",
            "_EmissionGradSpeed",
            "_EmissionGradTex",
            "_EmissionMap",
            "_EmissionMap_ScrollRotate",
            "_EmissionMap_UVMode",
            "_EmissionUseGrad",
            "_FakeShadowVector",
            "_FlipNormal",
            "_FurAlphaToMask",
            "_FurBlendOp",
            "_FurBlendOpAlpha",
            "_FurBlendOpAlphaFA",
            "_FurBlendOpFA",
            "_FurColorMask",
            "_FurCull",
            "_FurCutoutLength",
            "_FurDstBlend",
            "_FurDstBlendAlpha",
            "_FurDstBlendAlphaFA",
            "_FurDstBlendFA",
            "_FurLengthMask",
            "_FurMask",
            "_FurMeshType",
            "_FurNoiseMask",
            "_FurOffsetFactor",
            "_FurOffsetUnits",
            "_FurRandomize",
            "_FurRimColor",
            "_FurSrcBlend",
            "_FurSrcBlendAlpha",
            "_FurSrcBlendAlphaFA",
            "_FurSrcBlendFA",
            "_FurStencilComp",
            "_FurStencilFail",
            "_FurStencilPass",
            "_FurStencilZFail",
            "_FurVector",
            "_FurVectorTex",
            "_FurZClip",
            "_FurZTest",
            "_FurZWrite",
            "_GemEnvColor",
            "_GemEnvContrast",
            "_GemParticleColor",
            "_GemParticleLoop",
            "_GlitterAngleRandomize",
            "_GlitterApplyShape",
            "_GlitterApplyTransparency",
            "_GlitterAtras",
            "_GlitterBackfaceMask",
            "_GlitterColor",
            "_GlitterColorTex",
            "_GlitterColorTex_UVMode",
            "_GlitterParams1",
            "_GlitterParams2",
            "_GlitterPostContrast",
            "_GlitterSensitivity",
            "_GlitterShapeTex",
            "_GlitterUVMode",
            "_IDMask1",
            "_IDMask2",
            "_IDMask3",
            "_IDMask4",
            "_IDMask5",
            "_IDMask6",
            "_IDMask7",
            "_IDMask8",
            "_IDMaskCompile",
            "_IDMaskControlsDissolve",
            "_IDMaskFrom",
            "_IDMaskIndex1",
            "_IDMaskIndex2",
            "_IDMaskIndex3",
            "_IDMaskIndex4",
            "_IDMaskIndex5",
            "_IDMaskIndex6",
            "_IDMaskIndex7",
            "_IDMaskIndex8",
            "_IDMaskIsBitmap",
            "_IDMaskPrior1",
            "_IDMaskPrior2",
            "_IDMaskPrior3",
            "_IDMaskPrior4",
            "_IDMaskPrior5",
            "_IDMaskPrior6",
            "_IDMaskPrior7",
            "_IDMaskPrior8",
            "_IgnoreEncryption",
            "_Invisible",
            "_Keys",
            "_LightDirectionOverride",
            "_Main2ndBlendMask",
            "_Main2ndDissolveColor",
            "_Main2ndDissolveMask",
            "_Main2ndDissolveNoiseMask",
            "_Main2ndDissolveNoiseMask_ScrollRotate",
            "_Main2ndDissolveParams",
            "_Main2ndDissolvePos",
            "_Main2ndDistanceFade",
            "_Main2ndTex",
            "_Main2ndTexAlphaMode",
            "_Main2ndTexAngle",
            "_Main2ndTexBlendMode",
            "_Main2ndTexDecalAnimation",
            "_Main2ndTexDecalSubParam",
            "_Main2ndTexIsDecal",
            "_Main2ndTexIsLeftOnly",
            "_Main2ndTexIsMSDF",
            "_Main2ndTexIsRightOnly",
            "_Main2ndTexShouldCopy",
            "_Main2ndTexShouldFlipCopy",
            "_Main2ndTexShouldFlipMirror",
            "_Main2ndTex_Cull",
            "_Main2ndTex_ScrollRotate",
            "_Main2ndTex_UVMode",
            "_Main3rdBlendMask",
            "_Main3rdDissolveColor",
            "_Main3rdDissolveMask",
            "_Main3rdDissolveNoiseMask",
            "_Main3rdDissolveNoiseMask_ScrollRotate",
            "_Main3rdDissolveParams",
            "_Main3rdDissolvePos",
            "_Main3rdDistanceFade",
            "_Main3rdTex",
            "_Main3rdTexAlphaMode",
            "_Main3rdTexAngle",
            "_Main3rdTexBlendMode",
            "_Main3rdTexDecalAnimation",
            "_Main3rdTexDecalSubParam",
            "_Main3rdTexIsDecal",
            "_Main3rdTexIsLeftOnly",
            "_Main3rdTexIsMSDF",
            "_Main3rdTexIsRightOnly",
            "_Main3rdTexShouldCopy",
            "_Main3rdTexShouldFlipCopy",
            "_Main3rdTexShouldFlipMirror",
            "_Main3rdTex_Cull",
            "_Main3rdTex_ScrollRotate",
            "_Main3rdTex_UVMode",
            "_MainColorAdjustMask",
            "_MainGradationTex",
            "_MainTex",
            "_MainTexHSVG",
            "_MainTex_ScrollRotate",
            "_MatCap2ndApplyTransparency",
            "_MatCap2ndBackfaceMask",
            "_MatCap2ndBlendMask",
            "_MatCap2ndBlendMode",
            "_MatCap2ndBlendUV1",
            "_MatCap2ndBumpMap",
            "_MatCap2ndColor",
            "_MatCap2ndCustomNormal",
            "_MatCap2ndPerspective",
            "_MatCap2ndTex",
            "_MatCap2ndZRotCancel",
            "_MatCapApplyTransparency",
            "_MatCapBackfaceMask",
            "_MatCapBlendMask",
            "_MatCapBlendMode",
            "_MatCapBlendUV1",
            "_MatCapBumpMap",
            "_MatCapColor",
            "_MatCapCustomNormal",
            "_MatCapMul",
            "_MatCapPerspective",
            "_MatCapTex",
            "_MatCapZRotCancel",
            "_MetallicGlossMap",
            "_OffsetFactor",
            "_OffsetUnits",
            "_OutlineAlphaToMask",
            "_OutlineBlendOp",
            "_OutlineBlendOpAlpha",
            "_OutlineBlendOpAlphaFA",
            "_OutlineBlendOpFA",
            "_OutlineColor",
            "_OutlineColorMask",
            "_OutlineCull",
            "_OutlineDeleteMesh",
            "_OutlineDisableInVR",
            "_OutlineDstBlend",
            "_OutlineDstBlendAlpha",
            "_OutlineDstBlendAlphaFA",
            "_OutlineDstBlendFA",
            "_OutlineLitApplyTex",
            "_OutlineLitColor",
            "_OutlineLitOffset",
            "_OutlineLitScale",
            "_OutlineLitShadowReceive",
            "_OutlineOffsetFactor",
            "_OutlineOffsetUnits",
            "_OutlineSrcBlend",
            "_OutlineSrcBlendAlpha",
            "_OutlineSrcBlendAlphaFA",
            "_OutlineSrcBlendFA",
            "_OutlineStencilComp",
            "_OutlineStencilFail",
            "_OutlineStencilPass",
            "_OutlineStencilZFail",
            "_OutlineTex",
            "_OutlineTexHSVG",
            "_OutlineTex_ScrollRotate",
            "_OutlineVectorTex",
            "_OutlineVectorUVMode",
            "_OutlineVertexR2Width",
            "_OutlineWidthMask",
            "_OutlineZBias",
            "_OutlineZClip",
            "_OutlineZTest",
            "_OutlineZWrite",
            "_ParallaxMap",
            "_PreAlphaToMask",
            "_PreBlendOp",
            "_PreBlendOpAlpha",
            "_PreBlendOpAlphaFA",
            "_PreBlendOpFA",
            "_PreColor",
            "_PreColorMask",
            "_PreCull",
            "_PreDstBlend",
            "_PreDstBlendAlpha",
            "_PreDstBlendAlphaFA",
            "_PreDstBlendFA",
            "_PreOffsetFactor",
            "_PreOffsetUnits",
            "_PreOutType",
            "_PreSrcBlend",
            "_PreSrcBlendAlpha",
            "_PreSrcBlendAlphaFA",
            "_PreSrcBlendFA",
            "_PreStencilComp",
            "_PreStencilFail",
            "_PreStencilPass",
            "_PreStencilZFail",
            "_PreZClip",
            "_PreZTest",
            "_PreZWrite",
            "_Ramp",
            "_ReflectionApplyTransparency",
            "_ReflectionBlendMode",
            "_ReflectionColor",
            "_ReflectionColorTex",
            "_ReflectionCubeColor",
            "_ReflectionCubeOverride",
            "_ReflectionCubeTex",
            "_RefractionColor",
            "_RefractionColorFromMain",
            "_RimApplyTransparency",
            "_RimBackfaceMask",
            "_RimBlendMode",
            "_RimColor",
            "_RimColorTex",
            "_RimIndirColor",
            "_RimShadeColor",
            "_RimShadeMask",
            "_Shadow2ndColor",
            "_Shadow2ndColorTex",
            "_Shadow3rdColor",
            "_Shadow3rdColorTex",
            "_ShadowAOShift",
            "_ShadowAOShift2",
            "_ShadowBlurMask",
            "_ShadowBorderColor",
            "_ShadowBorderMask",
            "_ShadowColor",
            "_ShadowColorTex",
            "_ShadowColorType",
            "_ShadowMaskType",
            "_ShadowPostAO",
            "_ShadowStrengthMask",
            "_ShiftBackfaceUV",
            "_SmoothnessTex",
            "_SpecularToon",
            "_SrcBlend",
            "_SrcBlendAlpha",
            "_SrcBlendAlphaFA",
            "_SrcBlendFA",
            "_StencilComp",
            "_StencilFail",
            "_StencilPass",
            "_StencilZFail",
            "_TransparentMode",
            "_TriMask",
            "_UDIMDiscardCompile",
            "_UDIMDiscardMode",
            "_UDIMDiscardRow0_0",
            "_UDIMDiscardRow0_1",
            "_UDIMDiscardRow0_2",
            "_UDIMDiscardRow0_3",
            "_UDIMDiscardRow1_0",
            "_UDIMDiscardRow1_1",
            "_UDIMDiscardRow1_2",
            "_UDIMDiscardRow1_3",
            "_UDIMDiscardRow2_0",
            "_UDIMDiscardRow2_1",
            "_UDIMDiscardRow2_2",
            "_UDIMDiscardRow2_3",
            "_UDIMDiscardRow3_0",
            "_UDIMDiscardRow3_1",
            "_UDIMDiscardRow3_2",
            "_UDIMDiscardRow3_3",
            "_UDIMDiscardUV",
            "_UseAnisotropy",
            "_UseAudioLink",
            "_UseBacklight",
            "_UseBump2ndMap",
            "_UseBumpMap",
            "_UseClippingCanceller",
            "_UseDither",
            "_UseEmission",
            "_UseEmission2nd",
            "_UseGlitter",
            "_UseMain2ndTex",
            "_UseMain3rdTex",
            "_UseMatCap",
            "_UseMatCap2nd",
            "_UseOutline",
            "_UsePOM",
            "_UseParallax",
            "_UseReflection",
            "_UseRim",
            "_UseRimShade",
            "_UseShadow",
            "_VertexColor2FurVector",
            "_ZClip",
            "_ZTest",
            "_ZWrite",
            "_e2ga0",
            "_e2ga1",
            "_e2ga2",
            "_e2ga3",
            "_e2ga4",
            "_e2ga5",
            "_e2ga6",
            "_e2ga7",
            "_e2gai",
            "_e2gc0",
            "_e2gc1",
            "_e2gc2",
            "_e2gc3",
            "_e2gc4",
            "_e2gc5",
            "_e2gc6",
            "_e2gc7",
            "_e2gci",
            "_ega0",
            "_ega1",
            "_ega2",
            "_ega3",
            "_ega4",
            "_ega5",
            "_ega6",
            "_ega7",
            "_egai",
            "_egc0",
            "_egc1",
            "_egc2",
            "_egc3",
            "_egc4",
            "_egc5",
            "_egc6",
            "_egc7",
            "_egci",
            "_lilShadowCasterBias",
            "_lilToonVersion"
        });


        /// <summary>
        /// Property name.
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Property description.
        /// </summary>
        public string Description => _description;
        /// <summary>
        /// Property type.
        /// </summary>
        public ShaderPropertyType PropertyType => _propertyType;
        /// <summary>
        /// Variable type in HLSL.
        /// </summary>
        public ShaderVariableType UniformType => _uniformType;
        /// <summary>
        /// Minimum and Maximum value of range property.
        /// </summary>
        public Vector2 RangeMinMax => _rangeMinMax;
        /// <summary>
        /// Default float value.
        /// </summary>
        public float DefaultFloat => _defaultFloat;
        /// <summary>
        /// Default int value.
        /// </summary>
        public int DefaultInt => _defaultInt;
        /// <summary>
        /// Default vector value.
        /// </summary>
        public Vector4 DefaultVector => _defaultVector;
        /// <summary>
        /// Default color value.
        /// </summary>
        public Color DefaultColor => _defaultColor;
        /// <summary>
        /// Default texture index (0 ~ 3).
        /// </summary>
        public int DefaultTextureIndex => _defaultTextureIndex;
        /// <summary>
        /// Drawer type.
        /// </summary>
        public DrawerType DrawerType => _drawerType;
        /// <summary>
        /// Drawer argument.
        /// </summary>
        public string DrawerArgument => _drawerArgument;
        /// <summary>
        /// Property type string.
        /// </summary>
        public string PropertyTypeText
        {
            get
            {
                var propTypeText = PropertyTypeSelections[(int)_propertyType];
                if (_propertyType == ShaderPropertyType.Range)
                {
                    propTypeText = $"{propTypeText}({_rangeMinMax.x}, {_rangeMinMax.y})";
                }
                return propTypeText;
            }
        }
        /// <summary>
        /// Default texture name.
        /// </summary>
        public string DefaultTextureName => DefaultTextureNames[_defaultTextureIndex];
        /// <summary>
        /// True if <see cref="PropertyType"/> is <see cref="ShaderPropertyType.Texture2D"/>,
        /// <see cref="ShaderPropertyType.Texture3D"/> or <see cref="ShaderPropertyType.TextureCube"/>.
        /// </summary>
        public bool IsTexture => _propertyType == ShaderPropertyType.Texture2D || _propertyType == ShaderPropertyType.Texture3D || _propertyType == ShaderPropertyType.TextureCube;
        /// <summary>
        /// Texture declaration macro.
        /// </summary>
        public string TextureDeclarationMacro
        {
            get
            {
                switch (_uniformType)
                {
                    case ShaderVariableType.Texture2D:
                        return "TEXTURE2D";
                    case ShaderVariableType.Texture2DArray:
                        return "TEXTURE2D_ARRAY";
                    case ShaderVariableType.Texture3D:
                        return "TEXTURE3D";
                    case ShaderVariableType.TextureCube:
                        return "TEXTURECUBE";
                    default:
                        return null;
                }
            }
        }
        /// <summary>
        /// String representation of the default value.
        /// </summary>
        public string DefaultValueString
        {
            get
            {
                switch (_propertyType)
                {
                    case ShaderPropertyType.Float:
                    case ShaderPropertyType.Range:
                        return _defaultFloat.ToString();
                    case ShaderPropertyType.Int:
                        return _defaultInt.ToString();
                    case ShaderPropertyType.Vector:
                        return $"({_defaultVector.x}, {_defaultVector.y}, {_defaultVector.z}, {_defaultVector.w})";
                    case ShaderPropertyType.Color:
                        return $"({_defaultColor.r}, {_defaultColor.g}, {_defaultColor.b}, {_defaultColor.a})";
                    case ShaderPropertyType.Texture2D:
                    case ShaderPropertyType.Texture3D:
                    case ShaderPropertyType.TextureCube:
                        return $"\"{DefaultTextureName}\" {{}}";
                    default:
                        return null;
                }
            }
        }
        /// <summary>
        /// Drawer part.
        /// </summary>
        public string Drawer
        {
            get
            {
                var drawerType = _drawerType;
                if (drawerType == DrawerType.None)
                {
                    return "";
                }

                var sb = new StringBuilder();
                sb.Append('[').Append(AllDrawerSelections[(int)drawerType]);

                var arg = _drawerArgument;
                if (GetDrawerArgumentType(drawerType) != ArgumentType.NotRequired && string.IsNullOrEmpty(arg))
                {
                    sb.Append('(').Append(arg).Append(')');
                }

                return sb.Append(']').ToString();
            }
        }
        /// <summary>
        /// Drawer argument type.
        /// </summary>
        public ArgumentType DrawerArgumentType => GetDrawerArgumentType(_drawerType);

        /// <summary>
        /// Backing field of <see cref="Name"/>.
        /// </summary>
        [SerializeField]
        private string _name;
        /// <summary>
        /// Backing field of <see cref="Description"/>.
        /// </summary>
        [SerializeField]
        private string _description;
        /// <summary>
        /// Backing field of <see cref="PropertyType"/>.
        /// </summary>
        [SerializeField]
        private ShaderPropertyType _propertyType;
        /// <summary>
        /// Backing field of <see cref="UniformType"/>.
        /// </summary>
        [SerializeField]
        private ShaderVariableType _uniformType;
        /// <summary>
        /// Backing field of <see cref="RangeMinMax"/>.
        /// </summary>
        [SerializeField]
        private Vector2 _rangeMinMax = new Vector2(0.0f, 1.0f);
        /// <summary>
        /// Backing field of <see cref="DefaultFloat"/>.
        /// </summary>
        [SerializeField]
        private float _defaultFloat = default;
        /// <summary>
        /// Backing field of <see cref="DefaultInt"/>.
        /// </summary>
        [SerializeField]
        private int _defaultInt = default;
        /// <summary>
        /// Backing field of <see cref="DefaultVector"/>.
        /// </summary>
        [SerializeField]
        private Vector4 _defaultVector = default;
        /// <summary>
        /// Backing field of <see cref="DefaultColor"/>.
        /// </summary>
        [SerializeField]
        public Color _defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        /// <summary>
        /// Backing field of <see cref="DefaultTextureIndex"/>.
        /// </summary>
        [SerializeField]
        public int _defaultTextureIndex = 0;
        /// <summary>
        /// Backing field of <see cref="DrawerType"/>.
        /// </summary>
        [SerializeField]
        public DrawerType _drawerType = DrawerType.None;
        /// <summary>
        /// Backing field of <see cref="DrawerArgument"/>.
        /// </summary>
        [SerializeField]
        public string _drawerArgument = "";


        /// <summary>
        /// Create instance with shader property components.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="description">Property description.</param>
        /// <param name="propertyType">Property type.</param>
        /// <param name="uniformType">Variable type in HLSL.</param>
        public ShaderPropertyDefinition(string name, string description, ShaderPropertyType propertyType, ShaderVariableType uniformType)
        {
            _name = name;
            _description = description;
            _propertyType = propertyType;
            _uniformType = uniformType;
        }


        /// <summary>
        /// Get suitable type names for <paramref name="propertyType"/>.
        /// </summary>
        /// <param name="propertyType">Property type value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="propertyType"/> is out of <see cref="ShaderPropertyType"/> values.</exception>
        public static string[] GetSuitableVariableTypeNames(ShaderPropertyType propertyType)
        {
            switch (propertyType)
            {
                case ShaderPropertyType.Float:
                    return FloatPropertyVariableTypes;
                case ShaderPropertyType.Int:
                    return IntPropertyVariableTypes;
                case ShaderPropertyType.Range:
                    return RangePropertyVariableTypes;
                case ShaderPropertyType.Vector:
                    return VectorPropertyVariableTypes;
                case ShaderPropertyType.Color:
                    return ColorPropertyVariableTypes;
                case ShaderPropertyType.Texture2D:
                    return Texture2DPropertyVariableTypes;
                case ShaderPropertyType.Texture3D:
                    return Texture3DPropertyVariableTypes;
                case ShaderPropertyType.TextureCube:
                    return TextureCubePropertyVariableTypes;
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType));
            }
        }

        /// <summary>
        /// Get suitable drawer selections.
        /// </summary>
        /// <param name="propertyType">Property type value.</param>
        /// <returns>Selection string array for specified <see cref="PropertyType"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="propertyType"/> is out of <see cref="ShaderPropertyType"/> values.</exception>
        public static string[] GetSuitableDrawerSelections(ShaderPropertyType propertyType)
        {
            switch (propertyType)
            {
                case ShaderPropertyType.Float:
                    return FloatDrawerSelections;
                case ShaderPropertyType.Int:
                    return IntDrawerSelections;
                case ShaderPropertyType.Range:
                    return RangeDrawerSelections;
                case ShaderPropertyType.Vector:
                    return VectorDrawerSelections;
                case ShaderPropertyType.Color:
                    return ColorDrawerSelections;
                case ShaderPropertyType.Texture2D:
                case ShaderPropertyType.Texture3D:
                case ShaderPropertyType.TextureCube:
                    return TextureDrawerSelections;
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType));
            }
        }

        /// <summary>
        /// Get <see cref="ArgumentType"/> of specified drawer.
        /// </summary>
        /// <param name="drawerType">Drawer type value.</param>
        /// <returns><see cref="ArgumentType"/> of specified drawer.</returns>
        public static ArgumentType GetDrawerArgumentType(DrawerType drawerType)
        {
            switch (drawerType)
            {
                case DrawerType.Toggle:
                case DrawerType.ToggleOff:
                    return ArgumentType.Optional;
                case DrawerType.PowerSlider:
                case DrawerType.KeywordEnum:
                case DrawerType.Enum:
                    return ArgumentType.Required;
                default:
                    return ArgumentType.NotRequired;
            }
        }

        /// <summary>
        /// Get default drawer argument.
        /// </summary>
        /// <param name="drawerType">Drawer type value.</param>
        /// <returns>Default drawer argument.</returns>
        public static string GetDefaultDescription(DrawerType drawerType)
        {
            switch (drawerType)
            {
                case DrawerType.LilHSVG:
                    return "sHSVGs";
                case DrawerType.LilUVAnim:
                    return "sScrollRotates";
                case DrawerType.LilDecalAnim:
                    return "sDecalAnimations";
                case DrawerType.LilDecalSub:
                    return "sDecalSubParams";
                case DrawerType.LilEnum:
                    return "UV Mode|UV0|UV1|UV2|UV3|MatCap";
                case DrawerType.LilEnumLabel:
                    return "sAlphaMaskModes";
                case DrawerType.LilFF:
                    return "3rd Scale|3rd Offset";
                case DrawerType.LilFFFF:
                    return "1st Scale|1st Offset|2nd Scale|2nd Offset";
                case DrawerType.LilFFFB:
                    return "sDistanceFadeSettings";
                case DrawerType.LilFRFR:
                    return "Strength|Blink Strength|Blink Speed|Blink Threshold";
                case DrawerType.LilVec3BDrawer:
                    return "sLightDirectionOverrides";
                case DrawerType.LilALUVParams:
                    return "Scale|Offset|sAngle|Band|Bass|Low Mid|High Mid|Treble";
                case DrawerType.LilALLocal:
                    return "sAudioLinkLocalMapParams";
                case DrawerType.LilDissolve:
                    return "sDissolveParams";
                case DrawerType.LilDissolveP:
                    return "Dissolve Position";
                case DrawerType.LilOLWidth:
                    return "Width";
                case DrawerType.LilGlitParam1:
                    return "Tiling|Particle Size|Contrast";
                case DrawerType.LilGlitParam2:
                    return "sGlitterParams2";
                default:
                    return string.Empty;
            }
        }
    }
}
