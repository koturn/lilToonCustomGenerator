using System;
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
        /// Default texture names.
        /// </summary>
        public static string[] DefaultTextureNames { get; } =
        {
            "black",
            "white",
            "gray",
            "bump"
        };

        /// <summary>
        /// Property name.
        /// </summary>
        public string name;
        /// <summary>
        /// Property description.
        /// </summary>
        public string description;
        /// <summary>
        /// Property type.
        /// </summary>
        public ShaderPropertyType propertyType;
        /// <summary>
        /// Variable type in HLSL.
        /// </summary>
        public ShaderVariableType uniformType;
        /// <summary>
        /// Minimum and Maximum value of range property.
        /// </summary>
        public Vector2 rangeMinMax;
        /// <summary>
        /// Default float value.
        /// </summary>
        public float defaultFloat;
        /// <summary>
        /// Default int value.
        /// </summary>
        public int defaultInt;
        /// <summary>
        /// Default vector value.
        /// </summary>
        public Vector4 defaultVector;
        /// <summary>
        /// Default color value.
        /// </summary>
        public Color defaultColor;
        /// <summary>
        /// Default texture index (0 ~ 3).
        /// </summary>
        public int defaultTextureIndex;

        /// <summary>
        /// Property type string.
        /// </summary>
        public string PropertyTypeText
        {
            get
            {
                var propTypeText = PropertyTypeSelections[(int)propertyType];
                if (propertyType == ShaderPropertyType.Range)
                {
                    propTypeText = $"{propTypeText} ({rangeMinMax.x}, {rangeMinMax.y})";
                }
                return propTypeText;
            }
        }
        /// <summary>
        /// Default texture name.
        /// </summary>
        public string DefaultTextureName => DefaultTextureNames[defaultTextureIndex];
        /// <summary>
        /// True if <see cref="propertyType"/> is <see cref="ShaderPropertyType.Texture2D"/>,
        /// <see cref="ShaderPropertyType.Texture3D"/> or <see cref="ShaderPropertyType.TextureCube"/>.
        /// </summary>
        public bool IsTexture => propertyType == ShaderPropertyType.Texture2D || propertyType == ShaderPropertyType.Texture3D || propertyType == ShaderPropertyType.TextureCube;
        /// <summary>
        /// Texture declaration macro.
        /// </summary>
        public string TextureDeclarationMacro
        {
            get
            {
                switch (uniformType)
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
                switch (propertyType)
                {
                    case ShaderPropertyType.Float:
                    case ShaderPropertyType.Range:
                        return defaultFloat.ToString();
                    case ShaderPropertyType.Int:
                        return defaultInt.ToString();
                    case ShaderPropertyType.Vector:
                        return $"({defaultVector.x}, {defaultVector.y}, {defaultVector.z}, {defaultVector.w})";
                    case ShaderPropertyType.Color:
                        return $"({defaultColor.r}, {defaultColor.g}, {defaultColor.b}, {defaultColor.a})";
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
        /// Create instance with shader property components.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="description">Property description.</param>
        /// <param name="propertyType">Property type.</param>
        /// <param name="uniformType">Variable type in HLSL.</param>
        public ShaderPropertyDefinition(string name, string description, ShaderPropertyType propertyType, ShaderVariableType uniformType)
        {
            this.name = name;
            this.description = description;
            this.propertyType = propertyType;
            this.uniformType = uniformType;
            this.rangeMinMax = new Vector2(0.0f, 1.0f);
            this.defaultFloat = 0.0f;
            this.defaultInt = 0;
            this.defaultVector = default(Vector4);
            this.defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            this.defaultTextureIndex = 0;
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
                case ShaderPropertyType.Range:
                    return FloatPropertyVariableTypes;
                case ShaderPropertyType.Int:
                    return IntPropertyVariableTypes;
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
    }
}
