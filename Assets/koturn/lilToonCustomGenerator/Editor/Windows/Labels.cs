using UnityEngine;
using UnityEditor;

#if LILTOON
using lilToon;
#endif  // LILTOON


namespace Koturn.LilToonCustomGenerator.Editor.Windows
{
    /// <summary>
    /// Provides label utility methods.
    /// </summary>
    [System.Runtime.InteropServices.Guid("a57f7f95-497f-1344-b97d-a02606e0e82a")]
    internal static class Labels
    {
        /// <summary>
        /// Temporary label.
        /// </summary>
        private static readonly GUIContent _tmpLabel = new GUIContent();

        /// <summary>
        /// Calc label width.
        /// </summary>
        /// <param name="labelText">Label text.</param>
        /// <returns>Label width.</returns>
        public static float CalcLabelWidth(string labelText)
        {
            _tmpLabel.text = labelText;
            return CalcWidth(_tmpLabel, EditorStyles.label);
        }

        /// <summary>
        /// Calc label width.
        /// </summary>
        /// <param name="label">Label.</param>
        /// <returns>Label width.</returns>
        public static float CalcLabelWidth(GUIContent label)
        {
            return CalcWidth(label, EditorStyles.label);
        }

        /// <summary>
        /// Calc toggle width.
        /// </summary>
        /// <param name="labelText">Label text of the toggle.</param>
        /// <returns>Toggle width.</returns>
        public static float CalcToggleWidth(string labelText)
        {
            _tmpLabel.text = labelText;
            return CalcWidth(_tmpLabel, EditorStyles.toggle);
        }

        /// <summary>
        /// Calc toggle width.
        /// </summary>
        /// <param name="label">Label of the toggle.</param>
        /// <returns>Toggle width.</returns>
        public static float CalcToggleWidth(GUIContent label)
        {
            return CalcWidth(label, EditorStyles.toggle);
        }

        /// <summary>
        /// Calc width of the specified <see cref="GUIStyle"/>.
        /// </summary>
        /// <param name="label">Label.</param>
        /// <param name="style"><see cref="GUIStyle"/> to calculate width.</param>
        /// <returns></returns>
        public static float CalcWidth(GUIContent label, GUIStyle style)
        {
            return style.CalcSize(label).x;
        }
    }
}
