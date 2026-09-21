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
        /// <see cref="GUIContent"/> for web link label.
        /// </summary>
        private static readonly GUIContent _webLinkLabel = new GUIContent();

        /// <summary>
        /// Draw web link button.
        /// </summary>
        /// <param name="text">Label text.</param>
        /// <param name="url">URL of the linked page.</param>
        /// <returns>True if button is pressed, otherwise false.</returns>
        public static bool WebButton(string text, string url)
        {
            var webLinkLabel = _webLinkLabel;
            webLinkLabel.text = text;
            webLinkLabel.tooltip = url;
            webLinkLabel.image = EditorGUIUtility.IconContent("BuildSettings.Web.Small").image;
            return WebButton(webLinkLabel, url);
        }

        /// <summary>
        /// Draw web link button.
        /// </summary>
        /// <param name="label"></param>
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
        /// Draws a toggle with a text field next to it.
        /// The text field is disabled when the toggle is unchecked.
        /// </summary>
        /// <param name="label">Label to display next to the toggle.</param>
        /// <param name="text">Text to display in the text field.</param>
        /// <param name="isChecked">Indicates whether the toggle is checked.</param>
        /// <returns>The text entered by the user.</returns>
        public static string ToggleTextField(string label, string text, ref bool isChecked)
        {
            var rowRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            var toggleRect = new Rect(rowRect.x - (IndentSpaceUnit + 2.0f), rowRect.y, EditorGUIUtility.labelWidth + IndentSpaceUnit, rowRect.height);
            isChecked = EditorGUI.ToggleLeft(toggleRect, label, isChecked);
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
    }
}
