using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Koturn.LilToonCustomGenerator.Editor.Enums;
using Koturn.LilToonCustomGenerator.Editor.Internals;
using Koturn.LilToonCustomGenerator.Editor.Internals.UI;


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// <see cref="ReorderableListContainer{T}"/> customized for <see cref="ShaderPropertyDefinition"/>.
    /// </summary>
    [System.Runtime.InteropServices.Guid("686b2be4-289e-ebd4-29f1-5694f379dcbb")]
    public sealed class PropertyReorderableListContainer : ReorderableListContainer<ShaderPropertyDefinition>
    {
        /// <summary>
        /// Width padding.
        /// </summary>
        private const float WidthPadding = 2.0f;
        /// <summary>
        /// Height padding.
        /// </summary>
        private const float HeightPadding = 2.0f;
        /// <summary>
        /// Labels for min/max values of Range property.
        /// </summary>
        private static readonly GUIContent[] _rangeMinMaxLabel =
        {
            new GUIContent("Min", "Minimum value of the range"),
            new GUIContent("Max", "Maximum value of the range")
        };
        /// <summary>
        /// Labels for <see cref="Vector4"/> value input filed.
        /// </summary>
        private static readonly GUIContent[] _defaultVectorLabel =
        {
            new GUIContent("X"),
            new GUIContent("Y"),
            new GUIContent("Z"),
            new GUIContent("W"),
        };
        /// <summary>
        /// Label for Property Name.
        /// </summary>
        private static readonly GUIContent _labelPropertyName = new GUIContent("Property name");
        /// <summary>
        /// "Default" labels.
        /// </summary>
        private static readonly GUIContent _labelDefaultValue = new GUIContent("Default");
        /// <summary>
        /// Label for drawer argument.
        /// </summary>
        private static readonly GUIContent _labelDrawerArgument = new GUIContent("Argument");
        /// <summary>
        /// Duplicate property name list.
        /// </summary>
        private readonly List<string> _duplicatePropertyNameList = new List<string>();
        /// <summary>
        /// Invalid property name list.
        /// </summary>
        private readonly List<string> _invalidPropertyNameList = new List<string>();
        /// <summary>
        /// List of property names with missing arguments.
        /// </summary>
        private readonly List<string> _missingDrawerArgumentPropertyNameList = new List<string>();
        /// <summary>
        /// List of property names with invalid arguments.
        /// </summary>
        private readonly List<string> _invalidDrawerArgumentPropertyNameList = new List<string>();
        /// <summary>
        /// List of property names which is used in lilToon.
        /// </summary>
        private readonly List<string> _usedInlilToonPropertyNameList = new List<string>();
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_duplicatePropertyNameList"/>.
        /// </summary>
        private readonly ReadOnlyCollection<string> _duplicatePropertyNameCollection;
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_invalidPropertyNameList"/>.
        /// </summary>
        private readonly ReadOnlyCollection<string> _invalidPropertyNameCollection;
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_missingDrawerArgumentPropertyNameList"/>.
        /// </summary>
        private readonly ReadOnlyCollection<string> _missingDrawerArgumentPropertyCollection;
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_invalidDrawerArgumentPropertyNameList"/>.
        /// </summary>
        private readonly ReadOnlyCollection<string> _invalidDrawerArgumentPropertyNameCollection;
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_usedInlilToonPropertyNameList"/>.
        /// </summary>
        private readonly ReadOnlyCollection<string> _usedInlilToonPropertyNameCollection;
        /// <summary>
        /// Width of the label of the property type.
        /// </summary>
        private float _propertyTypePopupWidth;
        /// <summary>
        /// Width of the label of the variable type.
        /// </summary>
        private float _variableTypePopupWidth;
        /// <summary>
        /// Width of the popup of the interpolation mode.
        /// </summary>
        private float _drawerPopupWidth;
        /// <summary>
        /// Shader stage toggle width.
        /// </summary>
        private float _shaderStageToggleWidth;


        /// <summary>
        /// Cached array of min/max values of the range property.
        /// </summary>
        private readonly float[] _rangeMinMaxArray = new float[2];
        /// <summary>
        /// Cached array of min/max values of the int range property.
        /// </summary>
        private readonly int[] _rangeIntMinMaxArray = new int[2];
        /// <summary>
        /// A cached temporary array corresponding to each component of <see cref="Vector2"/>.
        /// </summary>
        private readonly float[] _tmpVectorArray2 = new float[2];
        /// <summary>
        /// A cached temporary array corresponding to each component of <see cref="Vector3"/>.
        /// </summary>
        private readonly float[] _tmpVectorArray3 = new float[3];
        /// <summary>
        /// A cached temporary array corresponding to each component of <see cref="Vector4"/>.
        /// </summary>
        private readonly float[] _tmpVectorArray4 = new float[4];
        /// <summary>
        /// A cached temporary array corresponding to each component of <see cref="Vector2Int"/>.
        /// </summary>
        private readonly int[] _tmpVectorIntArray2 = new int[2];
        /// <summary>
        /// A cached temporary array corresponding to each component of <see cref="Vector3Int"/>.
        /// </summary>
        private readonly int[] _tmpVectorIntArray3 = new int[3];
        /// <summary>
        /// A cached temporary array containing four `int` elements.
        /// </summary>
        private readonly int[] _tmpVectorIntArray4 = new int[4];


        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly, Use ScriptableObject.CreateInstance()")]
        private PropertyReorderableListContainer()
        {
            _duplicatePropertyNameCollection = _duplicatePropertyNameList.AsReadOnly();
            _invalidPropertyNameCollection = _invalidPropertyNameList.AsReadOnly();
            _missingDrawerArgumentPropertyCollection = _missingDrawerArgumentPropertyNameList.AsReadOnly();
            _invalidDrawerArgumentPropertyNameCollection = _invalidDrawerArgumentPropertyNameList.AsReadOnly();
            _usedInlilToonPropertyNameCollection = _usedInlilToonPropertyNameList.AsReadOnly();
        }


        /// <summary>
        /// Get duplicate property names.
        /// </summary>
        /// <returns><see cref="ReadOnlyCollection{T}"/> of duplicate property names.</returns>
        public ReadOnlyCollection<string> GetDuplicatePropertyNames()
        {
            var dupNameList = _duplicatePropertyNameList;
            dupNameList.Clear();

            var set = new HashSet<string>();
            foreach (var item in List)
            {
                if (set.Contains(item.Name))
                {
                    dupNameList.Add(item.Name);
                }
                else
                {
                    set.Add(item.Name);
                }
            }

            return _duplicatePropertyNameCollection;
        }

        /// <summary>
        /// Get invalid property names.
        /// </summary>
        /// <returns><see cref="ReadOnlyCollection{T}"/> of invalid property names.</returns>
        public ReadOnlyCollection<string> GetInvalidPropertyNames()
        {
            var invalidNameList = _invalidPropertyNameList;
            invalidNameList.Clear();

            foreach (var item in List)
            {
                if (!RegexProvider.IdentifierRegex.IsMatch(item.Name))
                {
                    invalidNameList.Add(item.Name);
                }
            }

            return _invalidPropertyNameCollection;
        }

        /// <summary>
        /// Get list of property names with missing arguments.
        /// </summary>
        /// <returns><see cref="ReadOnlyCollection{T}"/> of property names with invalid arguments.</returns>
        public ReadOnlyCollection<string> GetMissingDrawerArgumentPropertyNames()
        {
            var missingDrawerArgumentPropertyNameList = _missingDrawerArgumentPropertyNameList;
            missingDrawerArgumentPropertyNameList.Clear();

            foreach (var item in List)
            {
                if (item.DrawerArgumentType == ArgumentType.Required && string.IsNullOrEmpty(item.DrawerArgument))
                {
                    missingDrawerArgumentPropertyNameList.Add(item.Name);
                }
            }

            return _missingDrawerArgumentPropertyCollection;
        }

        /// <summary>
        /// Get list of property names with invalid arguments.
        /// </summary>
        /// <returns><see cref="ReadOnlyCollection{T}"/> of property names with invalid arguments.</returns>
        public ReadOnlyCollection<string> GetInvalidDrawerArgumentPropertyNames()
        {
            var invalidDrawerArgumentPropertyNameList = _invalidDrawerArgumentPropertyNameList;
            invalidDrawerArgumentPropertyNameList.Clear();

            foreach (var item in List)
            {
                var argText = item.DrawerArgument;
                if (argText.Length == 0)
                {
                    continue;
                }

                var argType = item.DrawerArgumentType;
                if (argType == ArgumentType.NotRequired)
                {
                    continue;
                }

                var isValid = true;
                var args = item.GetDrawerArguments();
                switch (item.DrawerType)
                {
                    case DrawerType.Toggle:
                    case DrawerType.ToggleOff:
                        // MaterialToggleDrawer and MaterialToggleOffDrawer accepts only one argument.
                        if (args.Length > 1
                            || (args.Length == 1 && !RegexProvider.IdentifierRegex.IsMatch(args[0])))
                        {
                            isValid = false;
                        }
                        break;
                    case DrawerType.KeywordEnum:
                        if (args.Length > 9)
                        {
                            isValid = false;
                            break;
                        }
                        foreach (var arg in args)
                        {
                            if (!RegexProvider.KeywordEnumArgumentRegex.IsMatch(arg))
                            {
                                isValid = false;
                                break;
                            }
                        }
                        break;
                    case DrawerType.Enum:
                        if (args.Length % 2 == 1 || args.Length > 14)
                        {
                            isValid = false;
                            break;
                        }
                        for (int i = 0; i < args.Length; i += 2)
                        {
                            if (!RegexProvider.DrawerArgumentRegex.IsMatch(args[i])
                                || !RegexProvider.DrawerArgumentRegex.IsMatch(args[i + 1])
                                || !int.TryParse(args[i + 1], out _))
                            {
                                isValid = false;
                                break;
                            }
                        }
                        break;
                    case DrawerType.PowerSlider:
                        if (args.Length != 1 || !float.TryParse(args[0], out _))
                        {
                            isValid = false;
                        }
                        break;
                    default:
                        break;
                }

                if (!isValid)
                {
                    invalidDrawerArgumentPropertyNameList.Add(item.Name);
                }
            }

            return _invalidDrawerArgumentPropertyNameCollection;
        }

        /// <summary>
        /// Get list of property names which is used in lilToon.
        /// </summary>
        /// <returns><see cref="ReadOnlyCollection{T}"/> of property names with invalid arguments.</returns>
        public ReadOnlyCollection<string> GetNamesUsedInLilToon()
        {
            var usedInlilToonPropertyNameList = _usedInlilToonPropertyNameList;
            usedInlilToonPropertyNameList.Clear();

            var nameSet = ShaderPropertyDefinition.LilToonPropertyNameSet;
            foreach (var item in List)
            {
                if (nameSet.Contains(item.Name))
                {
                    usedInlilToonPropertyNameList.Add(item.Name);
                }
            }

            return _usedInlilToonPropertyNameCollection;
        }


        /// <inheritdoc/>
        protected override ReorderableList CreateReorderableList(SerializedObject serializedObject, SerializedProperty serializedProperty)
        {
            return new ReorderableList(serializedObject, serializedProperty, true, true, true, true);
        }


        /// <summary>
        /// Initialize <see cref="ReorderableList"/> instance.
        /// </summary>
        private void OnEnable()
        {
            var reorderableList = GetReorderableList();
            reorderableList.drawHeaderCallback = DrawHeader;
            reorderableList.elementHeightCallback = GetElementHeight;
            reorderableList.drawElementCallback = DrawElement;
            reorderableList.onAddCallback = OnAdd;

            var tmpLabel = new GUIContent();
            try
            {
                var style = EditorStyles.popup;

                var maxWidth = 0.0f;
                foreach (var text in ShaderPropertyDefinition.PropertyTypeSelections)
                {
                    tmpLabel.text = text;
                    maxWidth = Math.Max(maxWidth, style.CalcSize(tmpLabel).x);
                }
                _propertyTypePopupWidth = maxWidth + 4.0f;

                maxWidth = 0.0f;
                foreach (var text in ShaderPropertyDefinition.VariableTypeSelections)
                {
                    tmpLabel.text = text;
                    maxWidth = Math.Max(maxWidth, style.CalcSize(tmpLabel).x);
                }
                _variableTypePopupWidth = maxWidth + 4.0f;

                maxWidth = 0.0f;
                foreach (var text in ShaderPropertyDefinition.AllDrawerSelections)
                {
                    tmpLabel.text = text;
                    maxWidth = Math.Max(maxWidth, style.CalcSize(tmpLabel).x);
                }
                _drawerPopupWidth = maxWidth + 4.0f;
            }
            catch (NullReferenceException)
            {
                // NullReferenceException will occur when assembly is recompiled.
            }

            try
            {
                var style = EditorStyles.toggle;

                var maxWidth = 0.0f;
                foreach (var text in new[]
                {
                    nameof(ShaderVariantTargetFlags.Vertex),
                    nameof(ShaderVariantTargetFlags.Domain),
                    nameof(ShaderVariantTargetFlags.Hull),
                    nameof(ShaderVariantTargetFlags.Geometry),
                    nameof(ShaderVariantTargetFlags.Fragment)
                })
                {
                    tmpLabel.text = text;
                    maxWidth = Math.Max(maxWidth, style.CalcSize(tmpLabel).x);
                }
                _shaderStageToggleWidth = maxWidth + 6.0f;
            }
            catch (NullReferenceException)
            {
                // NullReferenceException will occur when assembly is recompiled.
            }
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.drawHeaderCallback"/>.</para>
        /// <para>Draw header of this <see cref="ReorderableList"/>.</para>
        /// </summary>
        /// <param name="rect"></param>
        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Shader Property Definitions");
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.elementHeight"/>.</para>
        /// <para>Returns height of the element of the specified index.</para>
        /// </summary>
        /// <param name="index">Element index. (unused)</param>
        /// <returns>Height of the element of the specified index.</returns>
        private float GetElementHeight(int index)
        {
            var element = GetReorderableList().serializedProperty.GetArrayElementAtIndex(index);
            var propDrawerType = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDrawerType);
            if ((DrawerType)propDrawerType.intValue == DrawerType.Toggle
                || (DrawerType)propDrawerType.intValue == DrawerType.ToggleOff
                || (DrawerType)propDrawerType.intValue == DrawerType.KeywordEnum)
            {
                return (EditorGUIUtility.singleLineHeight + HeightPadding) * 4.0f;
            }
            else
            {
                return (EditorGUIUtility.singleLineHeight + HeightPadding) * 3.0f;
            }
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.drawElementCallback"/>.</para>
        /// <para>Draw single element.</para>
        /// </summary>
        /// <param name="rect">Draw target <see cref="Rect"/>.</param>
        /// <param name="index">Element index.</param>
        /// <param name="isActive">True if the element is active, otherwise false.</param>
        /// <param name="isFocused">True if the element is focused, otherwise false.</param>
        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = GetReorderableList().serializedProperty.GetArrayElementAtIndex(index);
            var rowHeight = EditorGUIUtility.singleLineHeight;
            var col1LabelWidth = Labels.CalcLabelWidth(_labelPropertyName) + WidthPadding;
            var col2LabelWidth = Labels.CalcLabelWidth("Variable type") + WidthPadding;
            var col1Width = Math.Max(col1LabelWidth + 184.0f, rect.width * 0.3f);
            var col2Width = rect.width - col1Width;

            //
            // First line.
            //
            var leftRect = new Rect(rect.x, rect.y + HeightPadding, rect.width, rowHeight);

            using (new LabelWidthScope(col1LabelWidth))
            {
                leftRect.width = col1Width - WidthPadding * 2.0f;
                EditorGUI.PropertyField(
                    leftRect,
                    element.FindPropertyRelative(ShaderPropertyDefinition.NameOfName),
                    _labelPropertyName);
            }

            var propDescription = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDescription);
            using (new LabelWidthScope(col2LabelWidth))
            {
                leftRect.x += leftRect.width + WidthPadding * 2.0f;
                leftRect.width = col2Width;
                EditorGUI.PropertyField(
                    new Rect(leftRect.x, leftRect.y, col2Width, rowHeight),
                    propDescription);
            }

            //
            // Second line.
            //
            leftRect.x = rect.x;
            leftRect.y += rowHeight + HeightPadding;

            var propPropertyType = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfPropertyType);
            var propUniformType = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfUniformType);
            var propDrawerType = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDrawerType);
            using (var ccScope = new EditorGUI.ChangeCheckScope())
            {
                using (new LabelWidthScope(col1LabelWidth))
                {
                    leftRect.width = EditorGUIUtility.labelWidth + _propertyTypePopupWidth;
                    if ((ShaderPropertyType)propPropertyType.intValue == ShaderPropertyType.Range)
                    {
                        propPropertyType.intValue = EditorGUI.Popup(
                            leftRect,
                            "Property type",
                            propPropertyType.intValue,
                            ShaderPropertyDefinition.PropertyTypeSelections);
                    }
                    else
                    {
                        propPropertyType.intValue = EditorGUI.Popup(
                            leftRect,
                            "Property type",
                            propPropertyType.intValue,
                            ShaderPropertyDefinition.PropertyTypeSelections);
                    }
                }

                if (ccScope.changed)
                {
                    switch ((ShaderPropertyType)propPropertyType.intValue)
                    {
                        case ShaderPropertyType.Float:
                        case ShaderPropertyType.Range:
                            propUniformType.intValue = (int)ShaderVariableType.Float;
                            break;
                        case ShaderPropertyType.Int:
                            propUniformType.intValue = (int)ShaderVariableType.Int;
                            break;
                        case ShaderPropertyType.Vector:
                        case ShaderPropertyType.Color:
                            propUniformType.intValue = (int)ShaderVariableType.Float4;
                            break;
                        case ShaderPropertyType.Texture2D:
                            propUniformType.intValue = (int)ShaderVariableType.Texture2D;
                            break;
                        case ShaderPropertyType.Texture3D:
                            propUniformType.intValue = (int)ShaderVariableType.Texture3D;
                            break;
                        case ShaderPropertyType.TextureCube:
                            propUniformType.intValue = (int)ShaderVariableType.TextureCube;
                            break;
                    }
                    propDrawerType.intValue = (int)DrawerType.None;
                }
            }

            if ((ShaderPropertyType)propPropertyType.intValue == ShaderPropertyType.Range)
            {
                leftRect.x += leftRect.width + WidthPadding;
                leftRect.width = col1Width - leftRect.width - WidthPadding * 2.5f;

                var propRangeMinMax = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfRangeMinMax);
                var rangeMinMax = propRangeMinMax.vector2Value;

                if ((DrawerType)propDrawerType.intValue == DrawerType.IntRange)
                {
                    var rangeIntMinMaxArray = _rangeIntMinMaxArray;
                    rangeIntMinMaxArray[0] = (int)rangeMinMax.x;
                    rangeIntMinMaxArray[1] = (int)rangeMinMax.y;
                    EditorGUI.MultiIntField(
                        leftRect,
                        _rangeMinMaxLabel,
                        rangeIntMinMaxArray);
                    propRangeMinMax.vector2Value = new Vector2(rangeIntMinMaxArray[0], rangeIntMinMaxArray[1]);
                }
                else
                {
                    var rangeMinMaxArray = _rangeMinMaxArray;
                    rangeMinMaxArray[0] = rangeMinMax.x;
                    rangeMinMaxArray[1] = rangeMinMax.y;
                    EditorGUI.MultiFloatField(
                        leftRect,
                        _rangeMinMaxLabel,
                        rangeMinMaxArray);
                    propRangeMinMax.vector2Value = new Vector2(rangeMinMaxArray[0], rangeMinMaxArray[1]);
                }
            }

            using (new LabelWidthScope(col2LabelWidth))
            {
                leftRect.x = rect.x + col1Width;
                leftRect.width = EditorGUIUtility.labelWidth + _variableTypePopupWidth;
                var availableTypeNames = ShaderPropertyDefinition.GetSuitableVariableTypeNames((ShaderPropertyType)propPropertyType.intValue);
                var availableTypeIndex = EditorGUI.Popup(
                    leftRect,
                    "Variable type",
                    Array.IndexOf(availableTypeNames, ShaderPropertyDefinition.VariableTypeSelections[propUniformType.intValue]),
                    availableTypeNames);
                propUniformType.intValue = Array.IndexOf(ShaderPropertyDefinition.VariableTypeSelections, availableTypeNames[availableTypeIndex]);
            }

            leftRect.x += leftRect.width + WidthPadding * 2.0f;
            leftRect.width = rect.x + rect.width - leftRect.x;
            switch ((ShaderPropertyType)propPropertyType.intValue)
            {
                case ShaderPropertyType.Float:
                case ShaderPropertyType.Range:
                    using (new LabelWidthScope(Labels.CalcLabelWidth(_labelDefaultValue) + WidthPadding))
                    {
                        EditorGUI.PropertyField(
                            leftRect,
                            element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDefaultFloat),
                            _labelDefaultValue);
                    }
                    break;
                case ShaderPropertyType.Int:
                    using (new LabelWidthScope(Labels.CalcLabelWidth(_labelDefaultValue) + WidthPadding))
                    {
                        EditorGUI.PropertyField(
                            leftRect,
                            element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDefaultInt),
                            _labelDefaultValue);
                    }
                    break;
                case ShaderPropertyType.Vector:
                    var propDefaultVector = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDefaultVector);
                    var vectorValue = propDefaultVector.vector4Value;

                    float[] tmpVectorArray = null;
                    int[] tmpVectorIntArray = null;
                    var isUnsigned = false;
                    switch ((ShaderVariableType)propUniformType.intValue)
                    {
                        case ShaderVariableType.Fixed2:
                        case ShaderVariableType.Half2:
                        case ShaderVariableType.Float2:
                            tmpVectorArray = _tmpVectorArray2;
                            break;
                        case ShaderVariableType.Fixed3:
                        case ShaderVariableType.Half3:
                        case ShaderVariableType.Float3:
                            tmpVectorArray = _tmpVectorArray3;
                            break;
                        case ShaderVariableType.Fixed4:
                        case ShaderVariableType.Half4:
                        case ShaderVariableType.Float4:
                            tmpVectorArray = _tmpVectorArray4;
                            break;
                        case ShaderVariableType.Int2:
                            tmpVectorIntArray = _tmpVectorIntArray2;
                            break;
                        case ShaderVariableType.Int3:
                            tmpVectorIntArray = _tmpVectorIntArray3;
                            break;
                        case ShaderVariableType.Int4:
                            tmpVectorIntArray = _tmpVectorIntArray4;
                            break;
                        case ShaderVariableType.UInt2:
                            tmpVectorIntArray = _tmpVectorIntArray2;
                            isUnsigned = true;
                            break;
                        case ShaderVariableType.UInt3:
                            tmpVectorIntArray = _tmpVectorIntArray3;
                            isUnsigned = true;
                            break;
                        case ShaderVariableType.UInt4:
                            tmpVectorIntArray = _tmpVectorIntArray4;
                            isUnsigned = true;
                            break;
                        default:
                            tmpVectorArray = _tmpVectorArray4;
                            break;
                    }

                    var vector4 = propDefaultVector.vector4Value;
                    unsafe
                    {
                        float* pFloat = &vector4.x;
                        if (tmpVectorIntArray != null)
                        {
                            if (isUnsigned)
                            {
                                for (int i = 0; i < tmpVectorIntArray.Length; i++)
                                {
                                    tmpVectorIntArray[i] = (int)Math.Max(0.0f, pFloat[i]);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < tmpVectorIntArray.Length; i++)
                                {
                                    tmpVectorIntArray[i] = (int)pFloat[i];
                                }
                            }
                            EditorGUI.MultiIntField(
                                leftRect,
                                _defaultVectorLabel,
                                tmpVectorIntArray);
                            if (isUnsigned)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    pFloat[i] = i < tmpVectorIntArray.Length ? (float)Math.Max(0, tmpVectorIntArray[i]) : 0.0f;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    pFloat[i] = i < tmpVectorIntArray.Length ? (float)tmpVectorIntArray[i] : 0.0f;
                                }
                            }
                            propDefaultVector.vector4Value = vector4;
                        }
                        else
                        {
                            for (int i = 0; i < tmpVectorArray.Length; i++)
                            {
                                tmpVectorArray[i] = pFloat[i];
                            }
                            EditorGUI.MultiFloatField(
                                leftRect,
                                _defaultVectorLabel,
                                tmpVectorArray);
                            for (int i = 0; i < 4; i++)
                            {
                                pFloat[i] = i < tmpVectorArray.Length ? tmpVectorArray[i] : 0.0f;
                            }
                            propDefaultVector.vector4Value = vector4;
                        }
                    }
                    break;
                case ShaderPropertyType.Color:
                    using (new LabelWidthScope(Labels.CalcLabelWidth(_labelDefaultValue) + WidthPadding))
                    {
                        EditorGUI.PropertyField(
                            leftRect,
                            element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDefaultColor));
                    }
                    break;
                case ShaderPropertyType.Texture2D:
                case ShaderPropertyType.Texture3D:
                case ShaderPropertyType.TextureCube:
                    var propDefaultTextureIndex = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDefaultTextureIndex);
                    using (new LabelWidthScope(Labels.CalcLabelWidth(_labelDefaultValue) + WidthPadding))
                    {
                        propDefaultTextureIndex.intValue = EditorGUI.Popup(
                            leftRect,
                            "Default",
                            propDefaultTextureIndex.intValue,
                            ShaderPropertyDefinition.DefaultTextureNames);
                    }
                    break;
            }

            //
            // Third line.
            //
            leftRect.x = rect.x;
            leftRect.y += rowHeight + HeightPadding;

            using (var ccScope = new EditorGUI.ChangeCheckScope())
            {
                var drawerSelections = ShaderPropertyDefinition.GetSuitableDrawerSelections((ShaderPropertyType)propPropertyType.intValue);
                using (new LabelWidthScope(col1LabelWidth))
                {
                    leftRect.width = col1Width - WidthPadding;
                    var drawerIndex = EditorGUI.Popup(
                        leftRect,
                        "Drawer",
                        Array.IndexOf(drawerSelections, ShaderPropertyDefinition.AllDrawerSelections[propDrawerType.intValue]),
                        drawerSelections);
                    propDrawerType.intValue = Array.IndexOf(ShaderPropertyDefinition.AllDrawerSelections, drawerSelections[drawerIndex]);
                }

                if (ccScope.changed && propDescription.stringValue.Length == 0)
                {
                    propDescription.stringValue = ShaderPropertyDefinition.GetDefaultDescription((DrawerType)propDrawerType.intValue);
                }
            }

            if (ShaderPropertyDefinition.GetDrawerArgumentType((DrawerType)propDrawerType.intValue) != ArgumentType.NotRequired)
            {
                leftRect.x += leftRect.width + WidthPadding * 2.0f;
                leftRect.width = rect.width - leftRect.width - WidthPadding * 2.0f;
                using (new LabelWidthScope(col2LabelWidth))
                {
                    EditorGUI.PropertyField(
                        leftRect,
                        element.FindPropertyRelative(ShaderPropertyDefinition.NameOfDrawerArgument),
                        _labelDrawerArgument);
                }
            }

            //
            // Fourth line.
            //
            if ((DrawerType)propDrawerType.intValue == DrawerType.Toggle
                || (DrawerType)propDrawerType.intValue == DrawerType.ToggleOff
                || (DrawerType)propDrawerType.intValue == DrawerType.KeywordEnum)
            {
                leftRect.y += rowHeight + HeightPadding;
                leftRect.x = rect.x;
                leftRect.width = col1Width - WidthPadding * 2.0f;

                var propShaderVariantType = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfShaderVariantType);
                using (new LabelWidthScope(col1LabelWidth))
                {
                    propShaderVariantType.intValue = EditorGUI.Popup(
                        leftRect,
                        "Variant",
                        propShaderVariantType.intValue,
                        ShaderPropertyDefinition.ShaderVariantTypeSelections);
                }

                leftRect.x += leftRect.width + WidthPadding * 2.0f;

                var propVariantTargetFlags = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfShaderVariantTargetFlags);
                var oldFlags = (ShaderVariantTargetFlags)propVariantTargetFlags.intValue;
                var newFlags = ShaderVariantTargetFlags.None;

                leftRect.width = Labels.CalcToggleWidth(nameof(ShaderVariantTargetFlags.Vertex));
                newFlags |= EditorGUI.ToggleLeft(
                    leftRect,
                    nameof(ShaderVariantTargetFlags.Vertex),
                    (oldFlags & ShaderVariantTargetFlags.Vertex) != 0) ? ShaderVariantTargetFlags.Vertex : ShaderVariantTargetFlags.None;
                leftRect.x += _shaderStageToggleWidth;

                leftRect.width = Labels.CalcToggleWidth(nameof(ShaderVariantTargetFlags.Fragment));
                newFlags |= EditorGUI.ToggleLeft(
                    leftRect,
                    nameof(ShaderVariantTargetFlags.Fragment),
                    (oldFlags & ShaderVariantTargetFlags.Fragment) != 0) ? ShaderVariantTargetFlags.Fragment : ShaderVariantTargetFlags.None;
                leftRect.x += _shaderStageToggleWidth;

                leftRect.width = Labels.CalcToggleWidth(nameof(ShaderVariantTargetFlags.Geometry));
                newFlags |= EditorGUI.ToggleLeft(
                    leftRect,
                    nameof(ShaderVariantTargetFlags.Geometry),
                    (oldFlags & ShaderVariantTargetFlags.Geometry) != 0) ? ShaderVariantTargetFlags.Geometry : ShaderVariantTargetFlags.None;
                leftRect.x += _shaderStageToggleWidth;

                leftRect.width = Labels.CalcToggleWidth(nameof(ShaderVariantTargetFlags.Domain));
                newFlags |= EditorGUI.ToggleLeft(
                    leftRect,
                    nameof(ShaderVariantTargetFlags.Domain),
                    (oldFlags & ShaderVariantTargetFlags.Domain) != 0) ? ShaderVariantTargetFlags.Domain : ShaderVariantTargetFlags.None;
                leftRect.x += _shaderStageToggleWidth;

                leftRect.width = Labels.CalcToggleWidth(nameof(ShaderVariantTargetFlags.Hull));
                newFlags |= EditorGUI.ToggleLeft(
                    leftRect,
                    nameof(ShaderVariantTargetFlags.Hull),
                    (oldFlags & ShaderVariantTargetFlags.Hull) != 0) ? ShaderVariantTargetFlags.Hull : ShaderVariantTargetFlags.None;
                leftRect.x += _shaderStageToggleWidth;

                propVariantTargetFlags.intValue = (int)newFlags;

                var propAllowKeywordEvenOnNonMultiShader = element.FindPropertyRelative(ShaderPropertyDefinition.NameOfAllowKeywordEvenOnNonMultiShader);
                var toggleWidth = Labels.CalcToggleWidth("Allow on non-multi shaders");
                propAllowKeywordEvenOnNonMultiShader.boolValue = EditorGUI.ToggleLeft(
                    new Rect(rect.x + rect.width - toggleWidth - WidthPadding, leftRect.y, toggleWidth, rowHeight),
                    "Allow on non-multi shaders",
                    propAllowKeywordEvenOnNonMultiShader.boolValue);
            }
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.onAddCallback"/>.</para>
        /// <para>Add new item to <see cref="_shaderPropDefList"/>.</para>
        /// </summary>
        /// <param name="reorderableList">Source <see cref="ReorderableList"/>. (Unused)</param>
        private void OnAdd(ReorderableList reorderableList)
        {
            var propName = "_CustomProperty";
            for (int i = 1; i < 256; i++)
            {
                var isFound = false;
                foreach (var shaderProp in List)
                {
                    if (shaderProp.Name == propName)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    break;
                }
                propName = "_CustomProperty" + i;
            }
            List.Add(new ShaderPropertyDefinition(
                propName,
                string.Empty,
                ShaderPropertyType.Float,
                ShaderVariableType.Float));
        }
    }
}
