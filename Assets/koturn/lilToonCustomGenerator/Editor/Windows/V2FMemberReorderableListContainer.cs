using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Koturn.LilToonCustomGenerator.Editor.Enums;
using Koturn.LilToonCustomGenerator.Editor.Internals;


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// <see cref="ReorderableListContainer{T}"/> customized for <see cref="V2FMember"/>
    /// </summary>
    [System.Runtime.InteropServices.Guid("00d57aa2-cf72-4234-69f9-7c27ed86aa57")]
    public sealed class V2FMemberReorderableListContainer : ReorderableListContainer<V2FMember>
    {
        /// <summary>
        /// Width margin.
        /// </summary>
        private const float WidthPadding = 2.0f;
        /// <summary>
        /// Height padding.
        /// </summary>
        private const float HeightPadding = 2.0f;

        /// <summary>
        /// Label <see cref="GUIContent"/> for member name.
        /// </summary>
        private readonly GUIContent _labelMemberName = new GUIContent("Member name");

        /// <summary>
        /// Duplicate property name list.
        /// </summary>
        private readonly List<string> _duplicatePropertyNameList = new List<string>();
        /// <summary>
        /// Invalid property name list.
        /// </summary>
        private readonly List<string> _invalidPropertyNameList = new List<string>();
        /// <summary>
        /// List of v2f member names which is used in lilToon.
        /// </summary>
        private readonly List<string> _usedInlilToonV2FMemberNameList = new List<string>();
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_duplicatePropertyNameList"/>.
        /// </summary>
        private readonly ReadOnlyCollection<string> _duplicatePropertyNameCollection;
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_invalidPropertyNameList"/>
        /// </summary>
        private readonly ReadOnlyCollection<string> _invalidPropertyNameCollection;
        /// <summary>
        /// <see cref="ReadOnlyCollection{T}"/> of <see cref="_usedInlilToonV2FMemberNameList"/>
        /// </summary>
        private readonly ReadOnlyCollection<string> _usedInlilToonV2FMemberNameCollection;
        /// <summary>
        /// Width of the label of the variable type.
        /// </summary>
        private float _variableTypePopupWidth;
        /// <summary>
        /// Width of the popup of the interpolation mode.
        /// </summary>
        private float _interpolationModifierPopupWidth;


        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly, Use ScriptableObject.CreateInstance()")]
        private V2FMemberReorderableListContainer()
        {
            _duplicatePropertyNameCollection = _duplicatePropertyNameList.AsReadOnly();
            _invalidPropertyNameCollection = _invalidPropertyNameList.AsReadOnly();
            _usedInlilToonV2FMemberNameCollection = _usedInlilToonV2FMemberNameList.AsReadOnly();
        }


        /// <summary>
        /// Get duplicate property names.
        /// </summary>
        /// <returns><see cref="ReadOnlyCollection{T}"/> of duplicate property names.</returns>
        public ReadOnlyCollection<string> GetDuplicateMemberNames()
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
        public ReadOnlyCollection<string> GetInvalidMemberNames()
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
        /// Get list of property names which is used in lilToon.
        /// </summary>
        /// <returns><see cref="ReadOnlyCollection{T}"/> of property names with invalid arguments.</returns>
        public ReadOnlyCollection<string> GetNamesUsedInLilToon()
        {
            var usedInlilToonV2FMemberNameList = _usedInlilToonV2FMemberNameList;
            usedInlilToonV2FMemberNameList.Clear();

            var nameSet = V2FMember.LilToonV2FMemberNameSet;
            foreach (var item in List)
            {
                if (nameSet.Contains(item.Name))
                {
                    usedInlilToonV2FMemberNameList.Add(item.Name);
                }
            }

            return _usedInlilToonV2FMemberNameCollection;
        }


        /// <inheritdoc/>
        protected override ReorderableList CreateReorderableList(SerializedObject serializedObject, SerializedProperty serializedProperty)
        {
            return new ReorderableList(serializedObject, serializedProperty, true, true, true, true);
        }


        /// <summary>
        /// Create <see cref="V2FMemberReorderableListContainer"/> with specified  <see cref="SerializedObject"/> and <see cref="SerializedProperty"/>.
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
                foreach (var text in V2FMember.VariableTypeSelections)
                {
                    tmpLabel.text = text;
                    maxWidth = Math.Max(maxWidth, style.CalcSize(tmpLabel).x);
                }
                _variableTypePopupWidth = maxWidth + 4.0f;

                maxWidth = 0.0f;
                foreach (var text in V2FMember.InterpolationModifierSelections)
                {
                    tmpLabel.text = text;
                    maxWidth = Math.Max(maxWidth, style.CalcSize(tmpLabel).x);
                }
                _interpolationModifierPopupWidth = maxWidth + 4.0f;
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
            EditorGUI.LabelField(rect, "v2f members");
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.elementHeight"/>.</para>
        /// <para>Returns height of the element of the specified index.</para>
        /// </summary>
        /// <param name="index">Element index. (unused)</param>
        /// <returns>Height of the element of the specified index.</returns>
        private float GetElementHeight(int index)
        {
            return EditorGUIUtility.singleLineHeight + HeightPadding;
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

            //
            // First line.
            //
            rect.y += HeightPadding;

            var memberNameLabelWidth = Labels.CalcLabelWidth(_labelMemberName);
            var variableTypeLabelWidth = Labels.CalcLabelWidth("Variable type");
            var interpolationModifierLabelWidth = Labels.CalcLabelWidth("Interpolation modifier");

            var rightWidth = variableTypeLabelWidth + interpolationModifierLabelWidth + _variableTypePopupWidth + _interpolationModifierPopupWidth + WidthPadding * 2.0f;

            var rightRect = new Rect(
                rect.x + rect.width - rightWidth,
                rect.y + HeightPadding,
                rect.width - WidthPadding * 2.0f,
                rowHeight);
            var leftRect = new Rect(
                rect.x,
                rect.y + HeightPadding,
                rect.width - rightWidth - WidthPadding * 2.0f,
                rowHeight);

            var oldWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = memberNameLabelWidth + WidthPadding;
            EditorGUI.PropertyField(
                leftRect,
                element.FindPropertyRelative(V2FMember.NameOfName),
                _labelMemberName);

            var propVariableType = element.FindPropertyRelative(V2FMember.NameOfVariableType);
            EditorGUIUtility.labelWidth = variableTypeLabelWidth + WidthPadding;
            rightRect.width = EditorGUIUtility.labelWidth + _variableTypePopupWidth - WidthPadding * 2.0f;
            propVariableType.intValue = EditorGUI.Popup(
                rightRect,
                "Variable type",
                propVariableType.intValue,
                V2FMember.VariableTypeSelections);

            if (!V2FMember.IsIntegerType((ShaderVariableType)propVariableType.intValue))
            {
                EditorGUIUtility.labelWidth = interpolationModifierLabelWidth + WidthPadding;
                var propInterpolationModifier = element.FindPropertyRelative(V2FMember.NameOfInterpolationModifier);
                rightRect.x += rightRect.width + WidthPadding * 2.0f;
                rightRect.width = Math.Max(0.0f, EditorGUIUtility.labelWidth + _interpolationModifierPopupWidth);
                propInterpolationModifier.intValue = EditorGUI.Popup(
                    rightRect,
                    "Interpolation Modifier",
                    propInterpolationModifier.intValue,
                    V2FMember.InterpolationModifierSelections);
            }

            EditorGUIUtility.labelWidth = oldWidth;
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.onAddCallback"/>.</para>
        /// <para>Add new item to <see cref="_shaderPropDefList"/>.</para>
        /// </summary>
        /// <param name="reorderableList">Source <see cref="ReorderableList"/>. (Unused)</param>
        private void OnAdd(ReorderableList reorderableList)
        {
            var memberName = "member";
            for (int i = 1; i < 256; i++)
            {
                var isFound = false;
                foreach (var member in List)
                {
                    if (member.Name == memberName)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    break;
                }
                memberName = "member" + i;
            }
            List.Add(new V2FMember(memberName, ShaderVariableType.Float));
        }
    }
}
