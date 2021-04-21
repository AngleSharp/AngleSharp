using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AngleSharp
{
    internal static class NumberHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ParseInt32(ReadOnlySpan<char> text)
        {
#if NETSTANDARD2_1 || NETCOREAPP3_1_OR_GREATER
            return int.Parse(text, provider: CultureInfo.InvariantCulture);
#else
            return int.Parse(text.ToString(), CultureInfo.InvariantCulture);
#endif
        }
    }
}
