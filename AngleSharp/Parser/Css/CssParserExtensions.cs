namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Dom.Css;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    [DebuggerStepThrough]
    static class CssParserExtensions
    {
        /// <summary>
        /// Retrieves a number describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The code of the error.</returns>
        public static Int32 GetCode(this CssParseError code)
        {
            return (Int32)code;
        }

        /// <summary>
        /// Before the name of an @keyframes rule has been detected.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The name of the keyframes.</returns>
        public static String InKeyframesName(this IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            if (token.Type == CssTokenType.Ident)
            {
                tokens.MoveNext();
                return token.Data;
            }

            return String.Empty;
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The text of the keyframe.</returns>
        public static KeyframeSelector InKeyframeText(this IEnumerator<CssToken> tokens)
        {
            var keys = new List<Percent>();

            do
            {
                var token = tokens.Current;

                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;
                    else if (token.Type != CssTokenType.Comma || !tokens.MoveNext())
                        return null;

                    token = tokens.Current;
                }

                if (token.Type == CssTokenType.Percentage)
                    keys.Add(new Percent(((CssUnitToken)token).Value));
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.From))
                    keys.Add(Percent.Zero);
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.To))
                    keys.Add(Percent.Hundred);
                else
                    return null;
            } while (tokens.MoveNext());

            return new KeyframeSelector(keys);
        }

        public static void JumpToEndOfDeclaration(this IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.Semicolon:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        public static void JumpToNextSemicolon(this IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.Semicolon:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        public static void JumpToClosedArguments(this IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        round--;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        public static CssMedium GetMedium(this IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;
            var medium = new CssMedium();

            if (token.Type == CssTokenType.Ident)
            {
                var ident = token.Data;

                if (String.Compare(ident, Keywords.Not, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    tokens.MoveNext();
                    medium.IsInverse = true;
                }
                else if (String.Compare(ident, Keywords.Only, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    tokens.MoveNext();
                    medium.IsExclusive = true;
                }
            }

            return medium;
        }

        /// <summary>
        /// State that is called once in the head of an unknown @ rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        public static void SkipUnknownRule(this IEnumerator<CssToken> tokens)
        {
            var curly = 0;
            var round = 0;
            var square = 0;
            var cont = true;

            do
            {
                var token = tokens.Current;

                switch (token.Type)
                {
                    case CssTokenType.Semicolon:
                        cont = curly > 0 || round > 0 || square > 0;
                        break;
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        cont = curly > 0 || round > 0 || square > 0;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                }
            }
            while (cont && tokens.MoveNext());
        }

        /// <summary>
        /// Checks if the provided token is actually a match token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if the type is matching, otherwise false.</returns>
        public static Boolean IsMatchToken(this CssToken token)
        {
            var type = token.Type;
            return type == CssTokenType.IncludeMatch ||
                type == CssTokenType.DashMatch ||
                type == CssTokenType.PrefixMatch ||
                type == CssTokenType.SubstringMatch ||
                type == CssTokenType.SuffixMatch ||
                type == CssTokenType.NotMatch;
        }

        /// <summary>
        /// Converts the data to an identifier value. Uses inherit for inherit.
        /// </summary>
        /// <returns>The created value.</returns>
        public static ICssValue ToIdentifier(this CssToken token)
        {
            var data = token.Data;

            if (data.Equals(Keywords.Inherit, StringComparison.OrdinalIgnoreCase))
                return CssValue.Inherit;
            else if (data.Equals(Keywords.Initial, StringComparison.OrdinalIgnoreCase))
                return CssValue.Initial;

            return new CssIdentifier(data);
        }

        /// <summary>
        /// Converts the given unit to a value. Uses number for 0.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>The created value.</returns>
        public static ICssValue ToUnit(this CssUnitToken token)
        {
            if (token.Type == CssTokenType.Percentage)
                return new Percent(token.Value);

            return Factory.Units.Create(token.Value, token.Unit.ToLowerInvariant());
        }
    }
}
