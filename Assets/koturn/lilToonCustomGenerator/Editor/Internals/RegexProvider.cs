#if NET9_0_OR_GREATER
#    define SUPPORT_GENERATED_REGEX_PROPERTY
#endif  // NET9_0_OR_GREATER
#if NET7_0_OR_GREATER
#    define SUPPORT_GENERATED_REGEX
#endif  // NET7_0_OR_GREATER

using System.Text.RegularExpressions;


namespace Koturn.LilToonCustomGenerator.Editor.Internals
{
    /// <summary>
    /// Provides some <see cref="Regex"/> instances.
    /// </summary>
    [System.Runtime.InteropServices.Guid("c6962062-6b55-ed24-7b4a-3a2c8649a659")]
#if SUPPORT_GENERATED_REGEX
    internal static partial class RegexProvider
#else
    internal static class RegexProvider
#endif  // SUPPORT_GENERATED_REGEX
    {
        /// <summary>
        /// Options for <see cref="Regex"/> instances.
        /// </summary>
        private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.CultureInvariant;

        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching identifier.
        /// </summary>
        internal const string IdentifierPattern = @"^([a-zA-Z_])(\w*)$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching identifier.
        /// </summary>
        internal const string NamespacePattern = @"^([a-zA-Z_])(\w*)(\.([a-zA-Z_])(\w*))*$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching package name.
        /// </summary>
        internal const string PackageNamePattern = @"^[a-z0-9][a-z0-9_]*(?:\.[a-z0-9][a-z0-9_]*)*$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching property name.
        /// </summary>
        internal const string PropertyNamePattern = @"^_*(\w)(\w*)$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching version number.
        /// </summary>
        internal const string VersionNumberPattern = @"^(0|[1-9]\d*)(?:\.(0|[1-9]\d*)(?:\.(0|[1-9]\d*)(?:\.((0|[1-9]\d*)))?)?)?$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching version number of semantic versioning 2.0.0.
        /// </summary>
        /// <remarks>
        /// <see href="https://semver.org/#is-there-a-suggested-regular-expression-regex-to-check-a-semver-string"/>
        /// </remarks>
        internal const string SemVerPattern = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching ifempty tags.
        /// </summary>
        internal const string TagIfemptyPattern = @"^!!\s*(el)?if(not)?empty:\s*(\w+)\s*!!\s*$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching else tag.
        /// </summary>
        internal const string TagElsePattern = @"^!!\s*else!!\s*$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching endif tag.
        /// </summary>
        internal const string TagEndIfPattern = @"^!!\s*endif\s*!!\s*$";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching replacement tags.
        /// </summary>
        internal const string TagPattern = @"%%(\w+)\s*(?::\s*(.+))?%%";


        /// <summary>
        /// <see cref="Regex"/> instance matching identifier.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(IdentifierRegexPattern, Options)]
        public static partial Regex IdentifierRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex IdentifierRegex => IdentifierRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching identifier.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching identifier.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex IdentifierRegex();
#else
        public static Regex IdentifierRegex => _identifierRegex ?? (_identifierRegex = new Regex(IdentifierPattern, Options));
        /// <summary>
        /// Cache field of <see cref="IdentifierRegex"/>.
        /// </summary>
        private static Regex _identifierRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching namespace.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(NamespaceRegexPattern, Options)]
        public static partial Regex NamespaceRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex NamespaceRegex => NamespaceRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching identifier.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching identifier.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex NamespaceRegex();
#else
        public static Regex NamespaceRegex => _namespaceRegex ?? (_namespaceRegex = new Regex(NamespacePattern, Options));
        /// <summary>
        /// Cache field of <see cref="NamespaceRegex"/>.
        /// </summary>
        private static Regex _namespaceRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching package name.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(PackageNameRegexPattern, Options)]
        public static partial Regex PackageNameRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex PackageNameRegex => PackageNameRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching package name.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching package name.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex PackageNameRegex();
#else
        public static Regex PackageNameRegex => _packageName ?? (_packageName = new Regex(PackageNamePattern, Options));
        /// <summary>
        /// Cache field of <see cref="PackageNameRegex"/>.
        /// </summary>
        private static Regex _packageName;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching property name.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(PropertyNameRegexPattern, Options)]
        public static partial Regex PropertyNameRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex PropertyNameRegex => PropertyNameRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching property name.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching property name.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex PropertyNameRegex();
#else
        public static Regex PropertyNameRegex => _propertyNameRegex ?? (_propertyNameRegex = new Regex(PropertyNamePattern, Options));
        /// <summary>
        /// Cache field of <see cref="PropertyNameRegex"/>.
        /// </summary>
        private static Regex _propertyNameRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching version number.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(VersionNumberRegexPattern, Options)]
        public static partial Regex VersionNumberRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex VersionNumberRegex => VersionNumberRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching version number.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching version number.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex VersionNumberRegex();
#else
        public static Regex VersionNumberRegex => _versionNumberRegex ?? (_versionNumberRegex = new Regex(VersionNumberPattern, Options));
        /// <summary>
        /// Cache field of <see cref="VersionNumberRegex"/>.
        /// </summary>
        private static Regex _versionNumberRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching version number of semantic versioning 2.0.0.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(SemVerRegexPattern, Options)]
        public static partial Regex SemVerRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex SemVerRegex => SemVerRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching version number of semantic versioning 2.0.0.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching version number of semantic versioning 2.0.0.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex SemVerRegex();
#else
        public static Regex SemVerRegex => _semVerRegex ?? (_semVerRegex = new Regex(SemVerPattern, Options));
        /// <summary>
        /// Cache field of <see cref="SemVerRegex"/>.
        /// </summary>
        private static Regex _semVerRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching ifempty tags.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(TagIfemptyRegexPattern, Options)]
        public static partial Regex TagIfemptyRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex TagIfemptyRegex => TagIfemptyRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching ifempty tag.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching ifempty tags.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex TagIfemptyRegex();
#else
        public static Regex TagIfemptyRegex => _tagIfemptyRegex ?? (_tagIfemptyRegex = new Regex(TagIfemptyPattern, Options));
        /// <summary>
        /// Cache field of <see cref="TagIfemptyRegex"/>.
        /// </summary>
        private static Regex _tagIfemptyRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching else tags.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(TagElseRegexPattern, Options)]
        public static partial Regex TagElseRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex TagElseRegex => TagElseRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching else tag.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching else tags.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex TagElseRegex();
#else
        public static Regex TagElseRegex => _tagElseRegex ?? (_tagElseRegex = new Regex(TagElsePattern, Options));
        /// <summary>
        /// Cache field of <see cref="TagElseRegex"/>.
        /// </summary>
        private static Regex _tagElseRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching endif tag.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(TagEndIfRegexPattern, Options)]
        public static partial Regex TagEndIfRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex TagEndIfRegex => TagEndIfRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching endif tag.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching endif tag.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex TagEndIfRegex();
#else
        public static Regex TagEndIfRegex => _tagEndIfRegex ?? (_tagEndIfRegex = new Regex(TagEndIfPattern, Options));
        /// <summary>
        /// Cache field of <see cref="TagEndIfRegex"/>.
        /// </summary>
        private static Regex _tagEndIfRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching replacement tags.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(TagRegexPattern, Options)]
        public static partial Regex TagRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex TagRegex => TagRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching replacement tags.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching replacement tags.</returns>
        [GeneratedRegex(InstanceResetNotificationPattern, Options)]
        private static partial Regex TagRegex();
#else
        public static Regex TagRegex => _tagRegex ?? (_tagRegex = new Regex(TagPattern, Options));
        /// <summary>
        /// Cache field of <see cref="TagRegex"/>.
        /// </summary>
        private static Regex _tagRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY
    }
}
