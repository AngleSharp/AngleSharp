namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Conditions;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    [DebuggerStepThrough]
    static class CssParserExtensions
    {
        static readonly Dictionary<String, Func<String, DocumentFunction>> functionTypes = new Dictionary<String, Func<String, DocumentFunction>>(StringComparer.OrdinalIgnoreCase)
        {
            { FunctionNames.Url, str => new UrlFunction(str) },
            { FunctionNames.Domain, str => new DomainFunction(str) },
            { FunctionNames.UrlPrefix, str => new UrlPrefixFunction(str) },
        };

        static readonly Dictionary<String, Func<IEnumerable<CssCondition>, CssCondition>> groupCreators = new Dictionary<String, Func<IEnumerable<CssCondition>, CssCondition>>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.And, conditions => new AndCondition(conditions) },
            { Keywords.Or, conditions => new OrCondition(conditions) },
        };

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
        public static Func<IEnumerable<CssCondition>, CssCondition> GetCreator(this String conjunction)
        {
            var creator = default(Func<IEnumerable<CssCondition>, CssCondition>);
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
                var str = ((CssFunctionToken)token).ToCssString();

                if (str != null)
                    return new RegexpFunction(str);
            }

            return null;
        }
    }
}
