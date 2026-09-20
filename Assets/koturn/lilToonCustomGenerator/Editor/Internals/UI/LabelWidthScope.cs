using System;
using UnityEditor;


namespace Koturn.LilToonCustomGenerator.Editor.Internals.UI
{
    /// <summary>
    /// Custom GUI element like <see cref="EditorGUILayout"/>.
    /// </summary>
    [System.Runtime.InteropServices.Guid("2b1794e4-c6c5-e654-a9d8-fca2d2e90a60")]
    internal struct LabelWidthScope : IDisposable
    {
        /// <summary>
        /// A flag property which indicates this instance is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Old label width.
        /// </summary>
        private readonly float _oldLabelWidth;


        /// <summary>
        /// Create <see cref="LabelWidthScope"/> with specified label width.
        /// </summary>
        /// <param name="labelWidth">Label width.</param>
        public LabelWidthScope(float labelWidth)
            : this()
        {
            _oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
        }


        /// <summary>
        /// Restore <see cref="EditorGUIUtility.labelWidth"/> to <see cref="_oldLabelWidth"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }


        /// <summary>
        /// Restore <see cref="EditorGUIUtility.labelWidth"/> to <see cref="_oldLabelWidth"/>.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                EditorGUIUtility.labelWidth = _oldLabelWidth;
            }

            IsDisposed = true;
        }
    }
}
