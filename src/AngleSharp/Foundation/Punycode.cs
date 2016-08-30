namespace AngleSharp.Foundation
{
    using AngleSharp.Extensions;
    using System;
    using System.Text;

    /// <summary>
    /// Represents a Punycode encoding helper class.
    /// </summary>
    static class Punycode
    {
        #region Constants

        private const Int32 PunycodeBase = 36;
        private const Int32 Tmin = 1;
        private const Int32 Tmax = 26;

        private static readonly String acePrefix = "xn--";
        private static readonly Char[] possibleDots = { '.', '\u3002', '\uFF0E', '\uFF61' };

        #endregion

        #region Methods

        public static String Encode(String text)
        {
            const Int32 InitialBias = 72;
            const Int32 InitialNumber = 0x80;
            const Int32 MaxIntValue = 0x7ffffff;
            const Int32 LabelLimit = 63;
            const Int32 DefaultNameLimit = 255;

            // 0 length strings aren't allowed
            if (text.Length == 0)
            {
                return text;
            }

            var output = new StringBuilder(text.Length);
            var iNextDot = 0;
            var iAfterLastDot = 0;
            var iOutputAfterLastDot = 0;

            // Find the next dot
            while (iNextDot < text.Length)
            {
                // Find end of this segment
                iNextDot = text.IndexOfAny(possibleDots, iAfterLastDot);

                if (iNextDot < 0)
                {
                    iNextDot = text.Length;
                }

                // Only allowed to have empty . section at end (www.microsoft.com.)
                if (iNextDot == iAfterLastDot)
                {
                    break;
                }

                // We'll need an Ace prefix
                output.Append(acePrefix);

                var basicCount = 0;
                var numProcessed = 0;

                for (basicCount = iAfterLastDot; basicCount < iNextDot; basicCount++)
                {
                    if (text[basicCount] < 0x80)
                    {
                        output.Append(EncodeBasic(text[basicCount]));
                        numProcessed++;
                    }
                    else if (Char.IsSurrogatePair(text, basicCount))
                    {
                        basicCount++;
                    }
                }

                var numBasicCodePoints = numProcessed;

                if (numBasicCodePoints == iNextDot - iAfterLastDot)
                {
                    output.Remove(iOutputAfterLastDot, acePrefix.Length);
                }
                else
                {
                    // If it has some non-basic code points the input cannot start with xn--
                    if (text.Length - iAfterLastDot >= acePrefix.Length && text.Substring(iAfterLastDot, acePrefix.Length).Equals(acePrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    // Need to do ACE encoding
                    var numSurrogatePairs = 0;

                    // Add a delimiter (-) if we had any basic code points (between basic and encoded pieces)
                    if (numBasicCodePoints > 0)
                    {
                        output.Append(Symbols.Minus);
                    }

                    // Initialize the state
                    var n = InitialNumber;
                    var delta = 0;
                    var bias = InitialBias;

                    // Main loop
                    while (numProcessed < (iNextDot - iAfterLastDot))
                    {
                        var j = 0;
                        var m = 0;
                        var test = 0;

                        for (m = MaxIntValue, j = iAfterLastDot; j < iNextDot; j += IsSupplementary(test) ? 2 : 1)
                        {
                            test = text.ConvertToUtf32(j);

                            if (test >= n && test < m)
                            {
                                m = test;
                            }
                        }

                        // Increase delta enough to advance the decoder's 
                        // <n,i> state to <m,0>, but guard against overflow:
                        delta += (m - n) * ((numProcessed - numSurrogatePairs) + 1);
                        n = m;

                        for (j = iAfterLastDot; j < iNextDot; j += IsSupplementary(test) ? 2 : 1)
                        {
                            // Make sure we're aware of surrogates
                            test = text.ConvertToUtf32(j);

                            // Adjust for character position (only the chars in our string already, some
                            // haven't been processed.

                            if (test < n)
                            {
                                delta++;
                            }
                            else if (test == n)
                            {
                                // Represent delta as a generalized variable-length integer:
                                int q, k;

                                for (q = delta, k = PunycodeBase; ; k += PunycodeBase)
                                {
                                    var t = k <= bias ? Tmin : k >= bias + Tmax ? Tmax : k - bias;

                                    if (q < t)
                                    {
                                        break;
                                    }

                                    output.Append(EncodeDigit(t + (q - t) % (PunycodeBase - t)));
                                    q = (q - t) / (PunycodeBase - t);
                                }

                                output.Append(EncodeDigit(q));
                                bias = AdaptChar(delta, (numProcessed - numSurrogatePairs) + 1, numProcessed == numBasicCodePoints);
                                delta = 0;
                                numProcessed++;

                                if (IsSupplementary(m))
                                {
                                    numProcessed++;
                                    numSurrogatePairs++;
                                }
                            }
                        }

                        ++delta;
                        ++n;
                    }
                }

                // Make sure its not too big
                if (output.Length - iOutputAfterLastDot > LabelLimit)
                    throw new ArgumentException();

                // Done with this segment, add dot if necessary
                if (iNextDot != text.Length)
                {
                    output.Append(possibleDots[0]);
                }

                iAfterLastDot = iNextDot + 1;
                iOutputAfterLastDot = output.Length;
            }

            var rest = IsDot(text[text.Length - 1]) ? 0 : 1;
            var maxlength = DefaultNameLimit - rest;

            // Throw if we're too long
            if (output.Length > maxlength)
            {
                output.Remove(maxlength, output.Length - maxlength);
            }

            return output.ToString();
        }

        #endregion

        #region Helpers

        private static Boolean IsSupplementary(Int32 test)
        {
            return test >= 0x10000;
        }

        private static Boolean IsDot(Char c)
        {
            for (var i = 0; i < possibleDots.Length; i++)
            {
                if (possibleDots[i] == c)
                {
                    return true;
                }
            }

            return false;
        }

        private static Char EncodeDigit(Int32 digit)
        {
            const Char NumberOffset = (Char)('0' - 26);
            const Char LetterOffset = 'a';

            if (digit > 25)
            {
                // 26-35 map to ASCII 0-9
                return (Char)(digit + NumberOffset);
            }

            // 0-25 map to a-z or A-Z
            return (Char)(digit + LetterOffset);
        }

        private static Char EncodeBasic(Char character)
        {
            const Char CaseDifference = (Char)('a' - 'A');

            if (Char.IsUpper(character))
            {
                character += CaseDifference;
            }

            return character;
        }

        private static Int32 AdaptChar(Int32 delta, Int32 numPoints, Boolean firstTime)
        {
            const Int32 Skew = 38;
            const Int32 Damp = 700;

            var k = 0u;

            delta = firstTime ? delta / Damp : delta / 2;
            delta += delta / numPoints;

            for (k = 0; delta > ((PunycodeBase - Tmin) * Tmax) / 2; k += PunycodeBase)
            {
                delta /= PunycodeBase - Tmin;
            }

            return (Int32)(k + PunycodeBase * delta / (delta + Skew));
        }

        #endregion
    }
}
