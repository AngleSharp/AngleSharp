namespace AngleSharp.Text
{
    using System;

    internal static class DomNameExtensions
    {
        // https://dom.spec.whatwg.org/#valid-element-local-name
        public static Boolean IsValidElementLocalName(this String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            if (name[0].IsLetter())
            {
                for (var i = 1; i < name.Length; i++)
                {
                    if (IsForbiddenLocalNameCharacter(name[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (name[0] != Symbols.Colon && name[0] != Symbols.Underscore && name[0] < 0x80)
                {
                    return false;
                }

                // All non-ASCII code points are allowed, including surrogates, so
                // checking UTF-16 code units also handles supplementary characters.
                for (var i = 1; i < name.Length; i++)
                {
                    var c = name[i];

                    if (!c.IsAlphanumericAscii() && c != Symbols.Minus && c != Symbols.Dot &&
                        c != Symbols.Colon && c != Symbols.Underscore && c < 0x80)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // https://dom.spec.whatwg.org/#valid-attribute-local-name
        public static Boolean IsValidAttributeLocalName(this String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            for (var i = 0; i < name.Length; i++)
            {
                if (IsForbiddenLocalNameCharacter(name[i]) || name[i] == Symbols.Equality)
                {
                    return false;
                }
            }

            return true;
        }

        private static Boolean IsForbiddenLocalNameCharacter(Char c) =>
            c.IsSpaceCharacter() || c == '\0' || c == '/' || c == '>';
    }
}
