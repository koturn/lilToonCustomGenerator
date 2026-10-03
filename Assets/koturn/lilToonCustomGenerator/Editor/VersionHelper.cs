using System;
using System.Collections.Generic;


namespace Koturn.LilToonCustomGenerator.Editor
{
    /// <summary>
    /// Povides some version array, version dictionaries.
    /// </summary>
    [System.Runtime.InteropServices.Guid("ffd778a6-7d89-8414-aa05-86fb77b5f902")]
    public static class VersionHelper
    {
        /// <summary>
        /// Default minimal unity version array for package.json.
        /// </summary>
        public static int[] DefaultMinimalUnityVersion { get; } = { 2019, 4 };
        /// <summary>
        /// Default minimal lilToon version array.
        /// </summary>
        public static int[] DefaultMinimalLilToonVersion { get; } = { 1, 2, 11 };
        /// <summary>
        /// Default minimal lilToon version array for VPM.
        /// </summary>
        public static int[] DefaultVpmMinimalLilToonVersion { get; } = { 1, 3, 7 };
        /// <summary>
        /// lilToon 1.2.11 version array.
        /// </summary>
        public static int[] LilToonVersion010211 { get; } = { 1, 2, 11 };
        /// <summary>
        /// lilToon 1.3.0 version array.
        /// </summary>
        public static int[] LilToonVersion010300 { get; } = { 1, 3, 0 };
        /// <summary>
        /// lilToon 1.4.0 version array.
        /// </summary>
        public static int[] LilToonVersion010400 { get; } = { 1, 4, 0 };
        /// <summary>
        /// lilToon 1.4.1 version array.
        /// </summary>
        public static int[] LilToonVersion010401 { get; } = { 1, 4, 1 };
        /// <summary>
        /// Released lilToon version names.
        /// </summary>
        public static string[] ReleasedLilToonVersionNames { get; } = {
            "1.2.11",
            "1.2.12",
            "1.3.0",
            "1.3.1",
            "1.3.2",
            "1.3.3",
            "1.3.4",
            "1.3.5",
            "1.3.6",
            "1.3.7",
            "1.4.0",
            "1.4.1",
            "1.5.0",
            "1.5.1",
            "1.6.0",
            "1.6.1",
            "1.7.0",
            "1.7.1",
            "1.7.2",
            "1.7.3",
            "1.8.0",
            "1.8.1",
            "1.8.2",
            "1.8.3",
            "1.8.4",
            "1.8.5",
            "1.9.0",
            "1.10.0",
            "1.10.1",
            "1.10.2",
            "1.10.3",
            "2.0.0",
            "2.1.0",
            "2.1.1",
            "2.1.2",
            "2.1.3",
            "2.1.4",
            "2.1.5",
            "2.1.6",
            "2.1.7",
            "2.1.8",
            "2.1.9",
            "2.1.10",
            "2.2.0",
            "2.2.1",
            "2.3.0",
            "2.3.1",
            "2.3.2",
            "2.3.3",
            "2.3.4"
        };
        /// <summary>
        /// <para>Maximum minor version number dict of lilToon.</para>
        /// <para>key: Major version number.</para>
        /// <para>Value: Maximum minor version number.</para>
        /// </summary>
        public static Dictionary<int, int> UnityMaxMinorNumberDict { get; } = new Dictionary<int, int>()
        {
            { 2023, 2 },
            { 2022, 3 },
            { 2021, 2 },
            { 2020, 3 },
            { 2019, 4 }
        };
        /// <summary>
        /// Array of the tuple; Unity version, lilToon version and message.
        /// </summary>
        public static Tuple<int[], int[], string>[] LilToonUnityVersionCheckTuples { get; } = new[]
        {
            Tuple.Create(
                new[] { 2, 0, 0 },
                new[] { 2022, 1 },
                "Starting with lilToon 2.0.0, it is compatible only with Unity 2022 and later.\n"
                    + "Please consider fixing either the \"Minimal lilToon version\" or the \"Minimal Unity Version\"."),
            Tuple.Create(
                new[] { 1, 10, 0 },
                new[] { 2021, 2 },
                "The C# scripts of lilToon 1.10.0 cannot be compiled unless using Unity 2021.2 or later.\n"
                    + "Please consider fixing either the \"Minimal lilToon version\" or the \"Minimal Unity Version\"."),
            Tuple.Create(
                new[] { 1, 9, 0 },
                new[] { 2020, 2 },
                "The C# scripts of lilToon 1.9.0 cannot be compiled unless using Unity 2020.2 or later.\n"
                    + "Please consider fixing either the \"Minimal lilToon version\" or the \"Minimal Unity Version\"."),
        };
        /// <summary>
        /// <para>Maximum patch number dict of lilToon.</para>
        /// <para>key: The upper 16 bits represent the major version, and the lower 16 bits represent the minor version.</para>
        /// <para>Value: Maximum patch number.</para>
        /// </summary>
        public static Dictionary<uint, int> LilToonMaxPatchNumberDict { get; }  = new Dictionary<uint, int>()
        {
            { (uint)1 << 16 | (uint)2, 12 },
            { (uint)1 << 16 | (uint)3, 7 },
            { (uint)1 << 16 | (uint)4, 1 },
            { (uint)1 << 16 | (uint)5, 1 },
            { (uint)1 << 16 | (uint)6, 1 },
            { (uint)1 << 16 | (uint)7, 3 },
            { (uint)1 << 16 | (uint)8, 5 },
            { (uint)1 << 16 | (uint)9, 0 },
            { (uint)1 << 16 | (uint)10, 3 },
            { (uint)2 << 16 | (uint)0, 0 },
            { (uint)2 << 16 | (uint)1, 10 },
            { (uint)2 << 16 | (uint)2, 1 },
        };

        /// <summary>
        /// Compare two version array.
        /// </summary>
        /// <param name="versions1">First version array.</param>
        /// <param name="versions2">Second version array.</param>
        /// <returns>
        /// 1: <paramref name="versions1"/> is greater than <paramref name="versions2"/>.
        /// 0: <paramref name="versions1"/> is equals to <paramref name="versions2"/>.
        /// -1: <paramref name="versions1"/> is less than <paramref name="versions2"/>.
        /// </returns>
        public static int CompareVersionArray(int[] versions1, int[] versions2)
        {
            int count = Math.Min(versions1.Length, versions2.Length);
            for (int i = 0; i < count; i++)
            {
                if (versions1[i] > versions2[i])
                {
                    return 1;
                }

                if (versions1[i] < versions2[i])
                {
                    return -1;
                }
            }

            var versions = versions1.Length > versions2.Length ? versions1 : versions2;
            for (int i = count; i < versions.Length; i++)
            {
                if (versions[i] > 0)
                {
                    return versions == versions1 ? 1 : -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Check version consistency between minimal Unity and lilToon versions.
        /// </summary>
        /// <param name="unityVersion">Unity version array.</param>
        /// <param name="lilToonVersion">lilToon version array.</param>
        /// <returns></returns>
        public static bool CheckMinimalUnityAndLilToonVersion(int[] unityVersion, int[] lilToonVersion, out int[] recommendedUnityVersion, out string message)
        {
            recommendedUnityVersion = null;
            message = null;

            foreach (var checkTuple in LilToonUnityVersionCheckTuples)
            {
                if (CompareVersionArray(lilToonVersion, checkTuple.Item1) >= 0
                    && CompareVersionArray(unityVersion, checkTuple.Item2) < 0)
                {
                    recommendedUnityVersion = checkTuple.Item2;
                    message = checkTuple.Item3;
                    return false;
                }
            }

            return true;
        }
    }
}
