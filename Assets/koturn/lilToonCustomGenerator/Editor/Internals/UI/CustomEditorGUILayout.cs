using System;
using UnityEditor;
using UnityEngine;


namespace Koturn.LilToonCustomGenerator.Editor.Internals.UI
{
    /// <summary>
    /// Custom GUI element like <see cref="EditorGUILayout"/>.
    /// </summary>
    [System.Runtime.InteropServices.Guid("74770b71-60fc-32b4-2a84-ca9da3f1e769")]
    internal static class CustomEditorGUILayout
    {
        /// <summary>
        /// The width of a single-level indent.
        /// </summary>
        private const float IndentSpaceUnit = 16.0f;

        public static string ToggleTextField(string label, string text, string disabledText, ref bool isChecked)
        {
            var rowRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            var toggleRect = new Rect(rowRect.x - (IndentSpaceUnit + 2.0f), rowRect.y, EditorGUIUtility.labelWidth + IndentSpaceUnit, rowRect.height);
            using (var ccScope = new EditorGUI.ChangeCheckScope())
            {
                isChecked = EditorGUI.ToggleLeft(toggleRect, label, isChecked);
                if (ccScope.changed && !isChecked)
                {
                    text = disabledText;
                }
            }
            using (new EditorGUI.DisabledScope(!isChecked))
            {
                text = EditorGUI.TextField(
                    new Rect(rowRect.x + toggleRect.width - (IndentSpaceUnit * 3.0f - 4.0f), rowRect.y, rowRect.width - toggleRect.width + (IndentSpaceUnit * 3.0f - 4.0f), rowRect.height),
                    text);
            }

            return text;
        }

        public static void ToggleMultiIntField(string label, GUIContent[] subLabels, int[] values, int[] disabledValues, ref bool isChecked)
        {
            var rowRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            var toggleRect = new Rect(rowRect.x - (IndentSpaceUnit + 2.0f), rowRect.y, EditorGUIUtility.labelWidth + IndentSpaceUnit, rowRect.height);
            using (var ccScope = new EditorGUI.ChangeCheckScope())
            {
                isChecked = EditorGUI.ToggleLeft(toggleRect, label, isChecked);
                if (ccScope.changed  && !isChecked)
                {
                    Buffer.BlockCopy(disabledValues, 0, values, 0, values.Length);
                }
            }
            using (new EditorGUI.DisabledScope(!isChecked))
            {
                EditorGUI.MultiIntField(
                    new Rect(rowRect.x + toggleRect.width - (IndentSpaceUnit - 2.0f), rowRect.y, rowRect.width - toggleRect.width + (IndentSpaceUnit - 2.0f), rowRect.height),
                    subLabels,
                    values);
            }
        }
    }
}
