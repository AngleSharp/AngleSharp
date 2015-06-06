namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Css;
    using AngleSharp.Css.Conditions;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    sealed class CssSupportsState : CssParseState
    {
        public CssSupportsState(CssTokenizer tokenizer)
            : base(tokenizer)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssSupportsRule();
            rule.Condition = CreateCondition(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillRules(rule);
            return rule;
        }

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        public ICondition CreateCondition(ref CssToken token)
        {
            var condition = ExtractCondition(ref token);

            if (condition != null)
            {
                if (token.Data.Equals(Keywords.And, StringComparison.OrdinalIgnoreCase))
                {
                    token = _tokenizer.Get();
                    var conditions = MultipleConditions(condition, Keywords.And, ref token);
                    return new AndCondition(conditions);
                }
                else if (token.Data.Equals(Keywords.Or, StringComparison.OrdinalIgnoreCase))
                {
                    token = _tokenizer.Get();
                    var conditions = MultipleConditions(condition, Keywords.Or, ref token);
                    return new OrCondition(conditions);
                }
            }

            return condition;
        }

        ICondition ExtractCondition(ref CssToken token)
        {
            var condition = default(ICondition);

            if (token.Type == CssTokenType.RoundBracketOpen)
            {
                token = _tokenizer.Get();
                condition = CreateCondition(ref token);

                if (condition != null)
                    condition = new GroupCondition(condition);
                else if (token.Type == CssTokenType.Ident)
                    condition = DeclarationCondition(ref token);

                if (token.Type == CssTokenType.RoundBracketClose)
                    token = _tokenizer.Get();
            }
            else if (token.Data.Equals(Keywords.Not, StringComparison.OrdinalIgnoreCase))
            {
                token = _tokenizer.Get();
                condition = ExtractCondition(ref token);

                if (condition != null)
                    condition = new NotCondition(condition);
            }

            return condition;
        }

        ICondition DeclarationCondition(ref CssToken token)
        {
            var name = token.Data;
            var style = new CssStyleDeclaration();
            var property = Factory.Properties.Create(name, style);

            if (property == null)
                property = new CssUnknownProperty(name);

            token = _tokenizer.Get();

            if (token.Type == CssTokenType.Colon)
            {
                var important = false;
                var result = ReadValue(ref token, out important);
                property.IsImportant = important;

                if (result != null)
                    return new DeclarationCondition(property, result);
            }

            return null;
        }

        List<ICondition> MultipleConditions(ICondition condition, String connector, ref CssToken token)
        {
            var list = new List<ICondition>();
            list.Add(condition);

            while (token.Type != CssTokenType.Eof)
            {
                condition = ExtractCondition(ref token);

                if (condition == null)
                    break;

                list.Add(condition);

                if (!token.Data.Equals(connector, StringComparison.OrdinalIgnoreCase))
                    break;

                token = _tokenizer.Get();
            }

            return list;
        }
    }
}
