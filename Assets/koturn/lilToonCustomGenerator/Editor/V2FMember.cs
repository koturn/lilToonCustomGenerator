using System;
using UnityEngine;
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
        /// Serialize name of backing field of <see cref="Name"/>.
        /// </summary>
        public const string NameOfName = nameof(_name);
        /// <summary>
        /// Serialize name of backing field of <see cref="VariableType"/>.
        /// </summary>
        public const string NameOfVariableType = nameof(_variableType);
        /// <summary>
        /// Serialize name of backing field of <see cref="InterpolationModifier"/>.
        /// </summary>
        public const string NameOfInterpolationModifier = nameof(_interpolationModifier);

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
        /// String representations of <see cref="Enums.InterpolationModifier"/>.
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
        /// String representation of <see cref="_interpolationModifier"/>.
        /// </summary>
        public string InterpolationModifierText => _interpolationModifierTexts[(int)_interpolationModifier];

        /// <summary>
        /// Member name.
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Member type.
        /// </summary>
        public ShaderVariableType VariableType => _variableType;
        /// <summary>
        /// Interpolation modifier.
        /// </summary>
        public InterpolationModifier InterpolationModifier => _interpolationModifier;
        /// <summary>
        /// Determine whether <see cref="_variableType"/> value is integer type or not.
        /// </summary>
        public bool IsInteger => IsIntegerType(_variableType);
        /// <summary>
        /// Text representation of <see cref="_variableType"/>.
        /// </summary>
        public string VariableTypeText => VariableTypeSelections[(int)_variableType];

        /// <summary>
        /// Backing field of <see cref="Name"/>.
        /// </summary>
        [SerializeField]
        private string _name;
        /// <summary>
        /// Backing field of <see cref="_variableType"/>.
        /// </summary>
        [SerializeField]
        private ShaderVariableType _variableType;
        /// <summary>
        /// Backing field of <see cref="InterpolationModifier"/>.
        /// </summary>
        [SerializeField]
        private InterpolationModifier _interpolationModifier;


        /// <summary>
        /// Create instance with specified member name and variable type.
        /// </summary>
        /// <param name="name">Member name.</param>
        /// <param name="variableType">Member type.</param>
        /// <param name="interpolationModifier">Interpolation modifier.</param>
        public V2FMember(string name, ShaderVariableType variableType, InterpolationModifier interpolationModifier = InterpolationModifier.Linear)
        {
            _name = name;
            _variableType = variableType;
            _interpolationModifier = interpolationModifier;
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
