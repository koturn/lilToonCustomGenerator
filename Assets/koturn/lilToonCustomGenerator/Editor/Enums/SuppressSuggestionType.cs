namespace Koturn.LilToonCustomGenerator.Editor.Enums
{
    /// <summary>
    /// Suppress suggestion type enum.
    /// </summary>
    [System.Runtime.InteropServices.Guid("73fcebbd-776f-7d34-2b77-fc6a75189b4d")]
    public enum SuppressSuggestionType
    {
        /// <summary>
        /// Do not suppress any suggestion.
        /// </summary>
        None,
        /// <summary>
        /// Write all <see cref="System.Diagnostics.CodeAnalysis.SuppressMessageAttribute"/> in <c>GlobalSuppressions.cs</c>.
        /// </summary>
        GlobalSuppressionFile,
        /// <summary>
        /// Use <c>.editorconfig</c> to suppress suggestions.
        /// </summary>
        EditorConfig,
        /// <summary>
        /// Use <c>#pragma warning disable</c> at the top of the file.
        /// </summary>
        PragmaOnTop,
        /// <summary>
        /// Use <c>#pragma warning disable</c> at each location.
        /// </summary>
        PragmaEachLocation,
        /// <summary>
        /// Use <see cref="System.Diagnostics.CodeAnalysis.SuppressMessageAttribute"/> at each location.
        /// </summary>
        AttributeEachLocation
    };
}
