namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    static class CssParserExtensions
    {
        static readonly Dictionary<String, Func<String, DocumentFunction>> functionTypes = new Dictionary<String, Func<String, DocumentFunction>>(StringComparer.OrdinalIgnoreCase)
        {
            { FunctionNames.Url, str => new UrlFunction(str) },
            { FunctionNames.Domain, str => new DomainFunction(str) },
            { FunctionNames.UrlPrefix, str => new UrlPrefixFunction(str) },
        };

        static readonly Dictionary<String, Func<IEnumerable<IConditionFunction>, IConditionFunction>> groupCreators = new Dictionary<String, Func<IEnumerable<IConditionFunction>, IConditionFunction>>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.And, CreateAndCondition },
            { Keywords.Or, CreateOrCondition },
        };

        static IConditionFunction CreateAndCondition(IEnumerable<IConditionFunction> conditions)
        {
            var andCondition = new AndCondition();

            foreach (var condition in conditions)
            {
                andCondition.AppendChild(condition);
            }

            return andCondition;
        }

        static IConditionFunction CreateOrCondition(IEnumerable<IConditionFunction> conditions)
        {
            var orCondition = new OrCondition();

            foreach (var condition in conditions)
            {
                orCondition.AppendChild(condition);
            }

            return orCondition;
        }

        /// <summary>
        /// Gets the corresponding token type for the function name.
        /// </summary>
        /// <param name="functionName">The name to match.</param>
        /// <returns>The token type for the name.</returns>
        public static CssTokenType GetTypeFromName(this String functionName)
        {
            var creator = default(Func<String, DocumentFunction>);
            return functionTypes.TryGetValue(functionName, out creator) ? CssTokenType.Url : CssTokenType.Function;
        }

        /// <summary>
        /// Gets the corresponding conjunction creator, if there is any.
        /// </summary>
        /// <param name="conjunction">The conjunction to match.</param>
        /// <returns>The creator for the conjunction, if any.</returns>
        public static Func<IEnumerable<IConditionFunction>, IConditionFunction> GetCreator(this String conjunction)
        {
            var creator = default(Func<IEnumerable<IConditionFunction>, IConditionFunction>);
            groupCreators.TryGetValue(conjunction, out creator);
            return creator;
        }

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
        /// Checks if the provided token is either of the first or the second 
        /// type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to match.</param>
        /// <param name="b">The alternative match for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean Is(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type == a || type == b;
        }

        /// <summary>
        /// Checks if the provided token is neither of the first nor the second 
        /// type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to unmatch.</param>
        /// <param name="b">The alternative unmatch for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean IsNot(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type != a && type != b;
        }

        /// <summary>
        /// Checks if the provided token is neither of the first, nor the
        /// second nor the third type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to unmatch.</param>
        /// <param name="b">The alternative unmatch for the token.</param>
        /// <param name="c">The final unmatch for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean IsNot(this CssToken token, CssTokenType a, CssTokenType b, CssTokenType c)
        {
            var type = token.Type;
            return type != a && type != b && type != c;
        }

        /// <summary>
        /// Checks if the provided token is part of a declaration name.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean IsDeclarationName(this CssToken token)
        {
            return token.Type != CssTokenType.EndOfFile &&
                      token.Type != CssTokenType.Colon &&
                      token.Type != CssTokenType.Whitespace &&
                      token.Type != CssTokenType.Comment &&
                      token.Type != CssTokenType.CurlyBracketOpen &&
                      token.Type != CssTokenType.Semicolon;
        }

        /// <summary>
        /// Tries to create an IDocumentFunction from the provided token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>The created IDocumentFunction or null.</returns>
        public static DocumentFunction ToDocumentFunction(this CssToken token)
        {
            if (token.Type == CssTokenType.Url)
            {
                var creator = default(Func<String, DocumentFunction>);
                var functionName = ((CssUrlToken)token).FunctionName;
                functionTypes.TryGetValue(functionName, out creator);
                return creator(token.Data);
            }
            else if (token.Type == CssTokenType.Function && token.Data.Isi(FunctionNames.Regexp))
            {
                var str = ((CssFunctionToken)token).ArgumentTokens.ToCssString();

                if (str != null)
                {
                    return new RegexpFunction(str);
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a rule with the given type for the provided parser.
        /// </summary>
        /// <param name="parser">The underlying parser.</param>
        /// <param name="type">The type of the rule.</param>
        /// <returns>The created rule or null for invalid types.</returns>
        public static CssRule CreateRule(this CssParser parser, CssRuleType type)
        {
            switch (type)
            {
                case CssRuleType.Charset:
                    return new CssCharsetRule(parser);
                case CssRuleType.Document:
                    return new CssDocumentRule(parser);
                case CssRuleType.FontFace:
                    return new CssFontFaceRule(parser);
                case CssRuleType.Import:
                    return new CssImportRule(parser);
                case CssRuleType.Keyframe:
                    return new CssKeyframeRule(parser);
                case CssRuleType.Keyframes:
                    return new CssKeyframesRule(parser);
                case CssRuleType.Media:
                    return new CssMediaRule(parser);
                case CssRuleType.Namespace:
                    return new CssNamespaceRule(parser);
                case CssRuleType.Page:
                    return new CssPageRule(parser);
                case CssRuleType.Style:
                    return new CssStyleRule(parser);
                case CssRuleType.Supports:
                    return new CssSupportsRule(parser);
                case CssRuleType.Viewport:
                    return new CssViewportRule(parser);
                case CssRuleType.Unknown:
                case CssRuleType.RegionStyle:
                case CssRuleType.FontFeatureValues:
                case CssRuleType.CounterStyle:
                default:
                    return null;
            }
        }
    }
}
