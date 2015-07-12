namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using System.Collections.Generic;

    sealed class CssDocumentState : CssParseState
    {
        public CssDocumentState(CssTokenizer tokenizer, CssParser parser)
            : base(tokenizer, parser)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssDocumentRule(_parser);
            var functions = CreateFunctions(ref token);
            rule.Conditions.AddRange(functions);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillRules(rule);
            return rule;
        }

        /// <summary>
        /// Called when the document functions have to been found.
        /// </summary>
        public List<IDocumentFunction> CreateFunctions(ref CssToken token)
        {
            var list = new List<IDocumentFunction>();

            do
            {
                var function = token.ToDocumentFunction();

                if (function == null)
                    break;

                list.Add(function);
                token = _tokenizer.Get();
            }
            while (token.Type == CssTokenType.Comma);

            return list;
        }
    }
}
