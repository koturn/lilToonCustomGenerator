using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Koturn.LilToonCustomGenerator.Editor.Internals.UI;


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// <see cref="ReorderableListContainer{T}"/> customized for <see cref="KVPair{TKey, TValue}"/>.
    /// </summary>
    [System.Runtime.InteropServices.Guid("9dc1f4b2-9b8e-d9e4-a9e1-e25c1876320b")]
    public sealed class AsmMetadataReorderableListContainer : ReorderableListContainer<KVPair<string, string>>
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
        /// Label for key.
        /// </summary>
        private static readonly GUIContent _labelKey = new GUIContent("Key");
        /// <summary>
        /// Label for value.
        /// </summary>
        private static readonly GUIContent _labelValue = new GUIContent("Value");


        /// <inheritdoc/>
        protected override ReorderableList CreateReorderableList(SerializedObject serializedObject, SerializedProperty serializedProperty)
        {
            return new ReorderableList(serializedObject, serializedProperty, true, true, true, true);
        }


        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly, Use ScriptableObject.CreateInstance()")]
        private AsmMetadataReorderableListContainer()
        {
        }


        /// <summary>
        /// Create and initialize <see cref="ReorderableList"/> instance.
        /// </summary>
        private void OnEnable()
        {
            var reorderableList = GetReorderableList();
            reorderableList.drawHeaderCallback = DrawHeader;
            reorderableList.elementHeightCallback = GetElementHeight;
            reorderableList.drawElementCallback = DrawElement;
            reorderableList.onAddCallback = OnAdd;
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.drawHeaderCallback"/>.</para>
        /// <para>Draw header of this <see cref="ReorderableList"/>.</para>
        /// </summary>
        /// <param name="rect">Header region.</param>
        private void DrawHeader(Rect rect)
        {
            rect.x -= IndentOffset * EditorGUI.indentLevel;
            rect.width -= IndentOffset * EditorGUI.indentLevel;
            EditorGUI.LabelField(rect, "Metadata");
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
            var col1LabelWidth = Labels.CalcLabelWidth(_labelKey) + WidthPadding;
            var col2LabelWidth = Labels.CalcLabelWidth(_labelValue) + WidthPadding;
            var col1Width = Math.Max(col1LabelWidth + 60.0f, rect.width * 0.25f);
            var col2Width = rect.width - col1Width;

            //
            // First line.
            //
            var leftRect = new Rect(rect.x, rect.y + HeightPadding, col1Width, rowHeight);

            using (new LabelWidthScope(Labels.CalcLabelWidth("Value") + WidthPadding))
            {
                EditorGUI.PropertyField(
                    leftRect,
                    element.FindPropertyRelative(KVPair.NameOfKey),
                    _labelKey);

                leftRect.x += leftRect.width + WidthPadding * 2.0f;
                leftRect.width = col2Width - WidthPadding * 2.0f;
                EditorGUI.PropertyField(
                    leftRect,
                    element.FindPropertyRelative(KVPair.NameOfValue),
                    _labelValue);
            }
        }

        /// <summary>
        /// <para>Callback method for <see cref="ReorderableList.onAddCallback"/>.</para>
        /// <para>Add new item to <see cref="_stringList"/>.</para>
        /// </summary>
        /// <param name="reorderableList">Source <see cref="ReorderableList"/>. (Unused)</param>
        private void OnAdd(ReorderableList reorderableList)
        {
            List.Add(KVPair.Create("Key", "Value"));
        }
    }
}
