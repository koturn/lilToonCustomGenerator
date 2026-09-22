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

        /// <summary>
        /// Text-only <see cref="GUIContent"/>.
        /// </summary>
        private static readonly GUIContent _tmpLabel1 = new GUIContent();
        /// <summary>
        /// <see cref="GUIContent"/> with text, tooltips, and icons.
        /// </summary>
        private static readonly GUIContent _tmpLabel3 = new GUIContent();


        /// <summary>
        /// Draw web link button.
        /// </summary>
        /// <param name="text">Label text.</param>
        /// <param name="url">URL of the linked page.</param>
        /// <returns>True if button is pressed, otherwise false.</returns>
        public static bool WebButton(string text, string url)
        {
            return WebButton(
                GetTempLabel(text, url, EditorGUIUtility.IconContent("BuildSettings.Web.Small").image),
                url);
        }

        /// <summary>
        /// Draw web link button.
        /// </summary>
        /// <param name="label">Label.</param>
        /// <param name="url">URL of the linked page.</param>
        /// <returns>True if button is pressed, otherwise false.</returns>
        public static bool WebButton(GUIContent label, string url)
        {
            var rect = GUILayoutUtility.GetRect(label, EditorStyles.linkLabel);

            bool isPressed;
            if (isPressed = GUI.Button(rect, label, EditorStyles.linkLabel))
            {
                Application.OpenURL(url);
            }
            EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

            return isPressed;
        }

        /// <summary>
        /// ToggleLeft, where the clickable area is limited to just the checkbox and label.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value">A boolean value indicating whether it is checked.</param>
        /// <returns>True if checked, false otherwise.</returns>
        public static bool ToggleLeftAdjusted(string text, bool value)
        {
            return ToggleLeftAdjusted(GetTempLabel(text), value);
        }

        /// <summary>
        /// ToggleLeft, where the clickable area is limited to just the checkbox and label.
        /// </summary>
        /// <param name="label">Label.</param>
        /// <param name="value">A boolean value indicating whether it is checked.</param>
        /// <returns>True if checked, false otherwise.</returns>
        public static bool ToggleLeftAdjusted(GUIContent label, bool value)
        {
            var rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
            rect.width = EditorStyles.toggle.CalcSize(label).x + EditorGUI.indentLevel * 15.0f + 2.0f;
            return EditorGUI.ToggleLeft(rect, label, value);
        }

        /// <summary>
        /// Draws a toggle with a text field next to it.
        /// The text field is disabled when the toggle is unchecked.
        /// </summary>
        /// <param name="label">Label to display next to the toggle.</param>
        /// <param name="text">Text to display in the text field.</param>
        /// <param name="isChecked">Indicates whether the toggle is checked.</param>
        /// <returns>The text entered by the user.</returns>
        public static string ToggleTextField(string label, string text, ref bool isChecked)
        {
            return ToggleTextField(GetTempLabel(label), text, ref isChecked);
        }

        /// <summary>
        /// Draws a toggle with a text field next to it.
        /// The text field is disabled when the toggle is unchecked.
        /// </summary>
        /// <param name="label">Label to display next to the toggle.</param>
        /// <param name="text">Text to display in the text field.</param>
        /// <param name="isChecked">Indicates whether the toggle is checked.</param>
        /// <returns>The text entered by the user.</returns>
        public static string ToggleTextField(GUIContent label, string text, ref bool isChecked)
        {
            return ToggleTextField(label, text, ref isChecked, false);
        }

        /// <summary>
        /// Draws a toggle with a text field next to it.
        /// The text field is disabled when the toggle is unchecked.
        /// </summary>
        /// <param name="label">Label to display next to the toggle.</param>
        /// <param name="text">Text to display in the text field.</param>
        /// <param name="isChecked">Indicates whether the toggle is checked.</param>
        /// <param name="isCheckBoxDisabled">True to disable checkbox.</param>
        /// <returns>The text entered by the user.</returns>
        public static string ToggleTextField(string label, string text, ref bool isChecked, bool isCheckBoxDisabled)
        {
            return ToggleTextField(GetTempLabel(label), text, ref isChecked, isCheckBoxDisabled);
        }

        /// <summary>
        /// Draws a toggle with a text field next to it.
        /// The text field is disabled when the toggle is unchecked.
        /// </summary>
        /// <param name="label">Label to display next to the toggle.</param>
        /// <param name="text">Text to display in the text field.</param>
        /// <param name="isChecked">Indicates whether the toggle is checked.</param>
        /// <param name="isCheckBoxDisabled">True to disable checkbox.</param>
        /// <returns>The text entered by the user.</returns>
        public static string ToggleTextField(GUIContent label, string text, ref bool isChecked, bool isCheckBoxDisabled)
        {
            var rowRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            var toggleRect = new Rect(rowRect.x - (IndentSpaceUnit + 2.0f), rowRect.y, EditorGUIUtility.labelWidth + IndentSpaceUnit, rowRect.height);
            using (new EditorGUI.DisabledScope(isCheckBoxDisabled))
            {
                isChecked = EditorGUI.ToggleLeft(toggleRect, label, isChecked);
            }
            using (new EditorGUI.DisabledScope(!isChecked))
            {
                var indentLevel = EditorGUI.indentLevel;
                var offset = IndentSpaceUnit + 14.0f * indentLevel;
                text = EditorGUI.TextField(
                    new Rect(rowRect.x + toggleRect.width - offset, rowRect.y, rowRect.width - toggleRect.width + offset, rowRect.height),
                    text);
            }

            return text;
        }

        /// <summary>
        /// Draws a toggle with multiple integer fields next to it.
        /// The fields are disabled when the toggle is unchecked.
        /// </summary>
        /// <param name="label">Label to display next to the toggle.</param>
        /// <param name="subLabels">Labels for each sub-field.</param>
        /// <param name="values">Values for each sub-field.</param>
        /// <param name="isChecked">Indicates whether the toggle is checked.</param>
        public static void ToggleMultiIntField(string label, GUIContent[] subLabels, int[] values, ref bool isChecked)
        {
            ToggleMultiIntField(GetTempLabel(label), subLabels, values, ref isChecked);
        }

        /// <summary>
        /// Draws a toggle with multiple integer fields next to it.
        /// The fields are disabled when the toggle is unchecked.
        /// </summary>
        /// <param name="label">Label to display next to the toggle.</param>
        /// <param name="subLabels">Labels for each sub-field.</param>
        /// <param name="values">Values for each sub-field.</param>
        /// <param name="isChecked">Indicates whether the toggle is checked.</param>
        public static void ToggleMultiIntField(GUIContent label, GUIContent[] subLabels, int[] values, ref bool isChecked)
        {
            var rowRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            var toggleRect = new Rect(rowRect.x - (IndentSpaceUnit + 2.0f), rowRect.y, EditorGUIUtility.labelWidth + IndentSpaceUnit, rowRect.height);
            isChecked = EditorGUI.ToggleLeft(toggleRect, label, isChecked);
            using (new EditorGUI.DisabledScope(!isChecked))
            {
                EditorGUI.MultiIntField(
                    new Rect(rowRect.x + toggleRect.width - (IndentSpaceUnit - 2.0f), rowRect.y, rowRect.width - toggleRect.width + (IndentSpaceUnit - 2.0f), rowRect.height),
                    subLabels,
                    values);
            }
        }

        /// <summary>
        /// Retrieve a cached label with the specified text.
        /// </summary>
        /// <param name="text">Label text.</param>
        /// <returns><see cref="_tmpLabel1"/> with the specified text.</returns>
        private static GUIContent GetTempLabel(string text)
        {
            var tmpLabel = _tmpLabel1;
            tmpLabel.text = text;
            return tmpLabel;
        }

        /// <summary>
        /// Retrieve a cached label with the specified text, tooltip, and icon.
        /// </summary>
        /// <param name="text">Label text.</param>
        /// <param name="tooltip">Tooltip text.</param>
        /// <param name="image">Icon image.</param>
        /// <returns><see cref="_tmpLabel3"/> with the specified text, tooltip, and icon.</returns>
        private static GUIContent GetTempLabel(string text, string tooltip, Texture image)
        {
            var tmpLabel = _tmpLabel3;
            tmpLabel.text = text;
            tmpLabel.tooltip = tooltip;
            tmpLabel.image = image;
            return tmpLabel;
        }
    }
}
