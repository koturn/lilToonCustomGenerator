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
        /// Property type string.
        /// </summary>
        public string PropertyTypeText
        {
            get
            {
                var propTypeText = PropertyTypeSelections[(int)_propertyType];
                if (_propertyType == ShaderPropertyType.Range)
                {
                    propTypeText = $"{propTypeText} ({_rangeMinMax.x}, {_rangeMinMax.y})";
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
        private Vector2 _rangeMinMax;
        /// <summary>
        /// Backing field of <see cref="DefaultFloat"/>.
        /// </summary>
        [SerializeField]
        private float _defaultFloat;
        /// <summary>
        /// Backing field of <see cref="DefaultInt"/>.
        /// </summary>
        [SerializeField]
        private int _defaultInt;
        /// <summary>
        /// Backing field of <see cref="DefaultVector"/>.
        /// </summary>
        [SerializeField]
        private Vector4 _defaultVector;
        /// <summary>
        /// Backing field of <see cref="DefaultColor"/>.
        /// </summary>
        [SerializeField]
        public Color _defaultColor;
        /// <summary>
        /// Backing field of <see cref="DefaultTextureIndex"/>.
        /// </summary>
        [SerializeField]
        public int _defaultTextureIndex;


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
            _rangeMinMax = new Vector2(0.0f, 1.0f);
            _defaultFloat = 0.0f;
            _defaultInt = 0;
            _defaultVector = default(Vector4);
            _defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            _defaultTextureIndex = 0;
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
