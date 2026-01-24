using System;
using Koturn.LilToonCustomGenerator.Editor.Enums;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// v2f struct member definition.
    /// </summary>
    [System.Runtime.InteropServices.Guid("730f3377-8816-cf54-f8d3-cdcdfb2783cf")]
    [Serializable]
    public sealed class V2FMember
    {
        /// <summary>
        /// Available type names for v2f struct.
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
            "uint4"
        };
        /// <summary>
        /// Interpolation modifier names.
        /// </summary>
        public static string[] InterpolationModifierSelections { get; } =
        {
            "linear (default)",  // Default
            "centroid",
            "nointerpolation",
            "noperspective",
            "centroid noperspective",
            "sample"
        };
        /// <summary>
        /// String representations of <see cref="InterpolationModifier"/>.
        /// </summary>
        public static readonly string[] _interpolationModifierTexts =
        {
            "",  // Default
            "centroid",
            "nointerpolation",
            "noperspective",
            "centroid noperspective",
            "sample"
        };

        /// <summary>
        /// String representation of <see cref="interpolationModifier"/>.
        /// </summary>
        public string InterpolationModifierText => _interpolationModifierTexts[(int)interpolationModifier];

        /// <summary>
        /// Member name.
        /// </summary>
        public string name;
        /// <summary>
        /// Member type.
        /// </summary>
        public ShaderVariableType variableType;
        /// <summary>
        /// Interpolation modifier.
        /// </summary>
        public InterpolationModifier interpolationModifier;

        /// <summary>
        /// Determine whether <see cref="variableType"/> value is integer type or not.
        /// </summary>
        public bool IsInteger => IsIntegerType(variableType);
        /// <summary>
        /// Text representation of <see cref="variableType"/>.
        /// </summary>
        public string VariableTypeText => VariableTypeSelections[(int)variableType];

        /// <summary>
        /// Create instance with specified member name and variable type.
        /// </summary>
        /// <param name="name">Member name.</param>
        /// <param name="variableType">Member type.</param>
        /// <param name="interpolationModifier">Interpolation modifier.</param>
        public V2FMember(string name, ShaderVariableType variableType, InterpolationModifier interpolationModifier = InterpolationModifier.Linear)
        {
            this.name = name;
            this.variableType = variableType;
            this.interpolationModifier = interpolationModifier;
        }


        /// <summary>
        /// Determine whether specified <see cref="ShaderVariableType"/> value is integer type or not.
        /// </summary>
        /// <param name="shaderVarType">Shader variable type.</param>
        /// <returns>True if <paramref name="shaderVarType"/> is integer type, otherwise false.</returns>
        public static bool IsIntegerType(ShaderVariableType shaderVarType)
        {
            switch (shaderVarType)
            {
                case ShaderVariableType.Bool:
                case ShaderVariableType.LilBool:
                case ShaderVariableType.Int:
                case ShaderVariableType.Int2:
                case ShaderVariableType.Int3:
                case ShaderVariableType.Int4:
                case ShaderVariableType.UInt:
                case ShaderVariableType.UInt2:
                case ShaderVariableType.UInt3:
                case ShaderVariableType.UInt4:
                    return true;
                default:
                    return false;
            }
        }
    }
}
