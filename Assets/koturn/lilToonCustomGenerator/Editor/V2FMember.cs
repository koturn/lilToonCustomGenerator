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
        /// Member name.
        /// </summary>
        public string name;
        /// <summary>
        /// Member type.
        /// </summary>
        public ShaderVariableType variableType;

        /// <summary>
        /// Text representation of <see cref="variableType"/>.
        /// </summary>
        public string VariableTypeText => VariableTypeSelections[(int)variableType];

        /// <summary>
        /// Create instance with specified member name and variable type.
        /// </summary>
        /// <param name="name">Member name.</param>
        /// <param name="variableType">Member type.</param>
        public V2FMember(string name, ShaderVariableType variableType)
        {
            this.name = name;
            this.variableType = variableType;
        }
    }
}
