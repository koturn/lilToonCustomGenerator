namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// Drawer types.
    /// </summary>
    [System.Runtime.InteropServices.Guid("334c31e6-886d-7f64-aade-14974ce0cd78")]
    public enum DrawerType
    {
        /// <summary>
        /// Means the property does not have a Drawer.
        /// </summary>
        None,
        /// <summary>
        /// Means the property have <c>[Toggle]</c> (<see cref="UnityEditor.MaterialToggleDrawer"/>).
        /// </summary>
        Toggle,
        /// <summary>
        /// Means the property have <c>[ToggleOff]</c> (<see cref="UnityEditor.MaterialToggleOffDrawer"/>).
        /// </summary>
        ToggleOff,
        /// <summary>
        /// Means the property have <c>[PowerSlider]</c> (<see cref="UnityEditor.MaterialPowerSliderDrawer"/>).
        /// </summary>
        PowerSlider,
        /// <summary>
        /// Means the property have <c>[IntRange]</c> (<see cref="UnityEditor.MaterialIntRangeDrawer"/>).
        /// </summary>
        IntRange,
        /// <summary>
        /// Means the property have <c>[KeywordEnum]</c> (<see cref="UnityEditor.MaterialKeywordEnumDrawer"/>).
        /// </summary>
        KeywordEnum,
        /// <summary>
        /// Means the property have <c>[Enum]</c> (<see cref="UnityEditor.MaterialEnumDrawer"/>).
        /// </summary>
        Enum,
        /// <summary>
        /// Means the property have <c>[Gamma]</c> (<see cref="UnityEditor.MaterialProperty.PropFlags.Gamma"/>).
        /// </summary>
        Gamma,
        /// <summary>
        /// Means the property have <c>[HDR]</c> (<see cref="UnityEditor.MaterialProperty.PropFlags.HDR"/>).
        /// </summary>
        HDR,
        /// <summary>
        /// Means the property have <c>[NoScaleOffset]</c> (<see cref="UnityEditor.MaterialProperty.PropFlags.NoScaleOffset"/>).
        /// </summary>
        NoScaleOffset,
        /// <summary>
        /// Means the property have <c>[Normal]</c> (<see cref="UnityEditor.MaterialProperty.PropFlags.Normal"/>).
        /// </summary>
        Normal,
        /// <summary>
        /// Means the property have <c>[lilHDR]</c> (<see cref="lilToon.lilHDRDrawer"/>).
        /// </summary>
        LilHDR,
        /// <summary>
        /// Means the property have <c>[lilToggle]</c> (<see cref="lilToon.lilToggleDrawer"/>).
        /// </summary>
        LilToggle,
        /// <summary>
        /// Means the property have <c>[lilToggleLeft]</c> (<see cref="lilToon.lilToggleLeftDrawer"/>).
        /// </summary>
        LilToggleLeft,
        /// <summary>
        /// Means the property have <c>[lilAngle]</c> (<see cref="lilToon.lilAngleDrawer"/>).
        /// </summary>
        LilAngle,
        /// <summary>
        /// Means the property have <c>[lilLOD]</c> (<see cref="lilToon.lilLODDrawer"/>).
        /// </summary>
        LilLOD,
        /// <summary>
        /// Means the property have <c>[lilBlink]</c> (<see cref="lilToon.lilBlinkDrawer"/>).
        /// </summary>
        LilBlink,
        /// <summary>
        /// Means the property have <c>[lilVec2R]</c> (<see cref="lilToon.lilVec2RDrawer"/>).
        /// </summary>
        LilVec2R,
        /// <summary>
        /// Means the property have <c>[lilVec2]</c> (<see cref="lilToon.lilVec2Drawer"/>).
        /// </summary>
        LilVec2,
        /// <summary>
        /// Means the property have <c>[lilVec3]</c> (<see cref="lilToon.lilVec3Drawer"/>).
        /// </summary>
        LilVec3,
        /// <summary>
        /// Means the property have <c>[lilVec3Float]</c> (<see cref="lilToon.lilVec3FloatDrawer"/>).
        /// </summary>
        LilVec3Float,
        /// <summary>
        /// Means the property have <c>[lilHSVG]</c> (<see cref="lilToon.lilHSVGDrawer"/>).
        /// </summary>
        LilHSVG,
        /// <summary>
        /// Means the property have <c>[lilUVAnim]</c> (<see cref="lilToon.lilUVAnim"/>).
        /// </summary>
        LilUVAnim,
        /// <summary>
        /// Means the property have <c>[lilDecalAnim]</c> (<see cref="lilToon.lilDecalAnim"/>).
        /// </summary>
        LilDecalAnim,
        /// <summary>
        /// Means the property have <c>[lilDecalSub]</c> (<see cref="lilToon.lilDecalSub"/>).
        /// </summary>
        LilDecalSub,
        /// <summary>
        /// Means the property have <c>[lilEnum]</c> (<see cref="lilToon.lilEnum"/>).
        /// </summary>
        LilEnum,
        /// <summary>
        /// Means the property have <c>[lilEnumLabel]</c> (<see cref="lilToon.lilEnumLabel"/>).
        /// </summary>
        LilEnumLabel,
        /// <summary>
        /// Means the property have <c>[lilColorMask]</c> (<see cref="lilToon.lilColorMask"/>).
        /// </summary>
        LilColorMask,
        /// <summary>
        /// Means the property have <c>[lil3Param]</c> (<see cref="lilToon.lil3Param"/>).
        /// </summary>
        Lil3Param,
        /// <summary>
        /// Means the property have <c>[lilFF]</c> (<see cref="lilToon.lilFF"/>).
        /// </summary>
        LilFF,
        /// <summary>
        /// Means the property have <c>[lilFFFF]</c> (<see cref="lilToon.lilFFFF"/>).
        /// </summary>
        LilFFFF,
        /// <summary>
        /// Means the property have <c>[lilFFFB]</c> (<see cref="lilToon.lilFFFB"/>).
        /// </summary>
        LilFFFB,
        /// <summary>
        /// Means the property have <c>[lilFRFR]</c> (<see cref="lilToon.lilFRFR"/>).
        /// </summary>
        LilFRFR,
        /// <summary>
        /// Means the property have <c>[lilVec3BD]</c> (<see cref="lilToon.lilVec3BDrawer"/>).
        /// </summary>
        LilVec3BDrawer,
        /// <summary>
        /// Means the property have <c>[lilALUVParams]</c> (<see cref="lilToon.lilALUVParams"/>).
        /// </summary>
        LilALUVParams,
        /// <summary>
        /// Means the property have <c>[lilALLocal]</c> (<see cref="lilToon.lilALLocal"/>).
        /// </summary>
        LilALLocal,
        /// <summary>
        /// Means the property have <c>[lilDissolve]</c> (<see cref="lilToon.lilDissolve"/>).
        /// </summary>
        LilDissolve,
        /// <summary>
        /// Means the property have <c>[lilDissolveP]</c> (<see cref="lilToon.lilDissolveP"/>).
        /// </summary>
        LilDissolveP,
        /// <summary>
        /// Means the property have <c>[lilOLWidth]</c> (<see cref="lilToon.lilOLWidth"/>).
        /// </summary>
        LilOLWidth,
        /// <summary>
        /// Means the property have <c>[lilGlitParam1]</c> (<see cref="lilToon.lilGlitParam1"/>).
        /// </summary>
        LilGlitParam1,
        /// <summary>
        /// Means the property have <c>[lilGlitParam2]</c> (<see cref="lilToon.lilGlitParam2"/>).
        /// </summary>
        LilGlitParam2
    }
}
