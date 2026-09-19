using System;
using System.Runtime.CompilerServices;


namespace Koturn.LilToonCustomGenerator.Editor.Internals
{
    /// <summary>
    /// Utility methods for intrinsic bit-twiddling operations.
    /// </summary>
    [System.Runtime.InteropServices.Guid("e168e907-22bd-49b4-0a39-294535cf5d44")]
    internal static class BitOperations
    {
        /// <summary>
        /// Returns the population count (number of bits set) of a mask.
        /// Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <remarks>
        /// <seealso href="https://github.com/dotnet/runtime/blob/v10.0.12/src/libraries/System.Private.CoreLib/src/System/Numerics/BitOperations.cs#L448-L460"/>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PopCount(uint value)
        {
#if NETCOREAPP3_0_OR_GREATER
            return System.Numerics.BitOperations.PopCount(value);
#else
            const uint c1 = 0x55555555u;
            const uint c2 = 0x33333333u;
            const uint c3 = 0x0F0F0F0Fu;
            const uint c4 = 0x01010101u;

            value -= (value >> 1) & c1;
            value = (value & c2) + ((value >> 2) & c2);
            value = (((value + (value >> 4)) & c3) * c4) >> 24;

            return (int)value;
#endif  // NETCOREAPP3_0_OR_GREATER
        }
    }
}
