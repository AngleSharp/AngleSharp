namespace AngleSharp.ReadOnly;

using System;
using Common;
using Text;

/// <summary>
/// Useful helpers for the XML parser.
/// </summary>
public static class XmlExtensions
{
    /// <summary>
    /// Determines if the given string is a valid (local or qualified) name.
    /// </summary>
    /// <param name="str">The string to examine.</param>
    /// <returns>The result of the test.</returns>
    public static Boolean IsXmlName(this StringOrMemory str)
    {
        if (str.Length > 0 && str[0].IsXmlNameStart())
        {
            for (var i = 1; i < str.Length; i++)
            {
                if (!str[i].IsXmlName())
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Determines if the given string is a valid qualified name.
    /// </summary>
    /// <param name="str">The string to examine.</param>
    /// <returns>The result of the test.</returns>
    public static Boolean IsQualifiedName(this StringOrMemory str)
    {
        var colon = str.Memory.Span.IndexOf(Symbols.Colon);

        if (colon == -1)
        {
            return str.IsXmlName();
        }

        if (colon > 0 && str[0].IsXmlNameStart())
        {
            for (var i = 1; i < colon; i++)
            {
                if (!str[i].IsXmlName())
                {
                    return false;
                }
            }

            colon++;
        }

        if (str.Length > colon && str[colon++].IsXmlNameStart())
        {
            for (var i = colon; i < str.Length; i++)
            {
                if (str[i] == Symbols.Colon || !str[i].IsXmlName())
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

}