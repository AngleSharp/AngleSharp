namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Conditions;
    using AngleSharp.Css.MediaFeatures;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for details.
    /// </summary>
    [DebuggerStepThrough]
    sealed class CssBuilder
    {
        #region Fields

        readonly CssTokenizer _tokenizer;
        readonly CssParser _parser;

        #endregion

        #region ctor

        public CssBuilder(CssTokenizer tokenizer, CssParser parser)
        {
            _tokenizer = tokenizer;
            _parser = parser;
        }

        #endregion

        #region Create Rules

        /// <summary>
        /// Parses an @-rule with the given name, if there is any.
        /// </summary>
        public CssRule CreateAtRule(CssToken token)
        {
            if (token.Data.Is(RuleNames.Media))
                return CreateMedia(token);
            else if (token.Data.Is(RuleNames.FontFace))
                return CreateFontFace(token);
            else if (token.Data.Is(RuleNames.Keyframes))
                return CreateKeyframes(token);
            else if (token.Data.Is(RuleNames.Import))
                return CreateImport(token);
            else if (token.Data.Is(RuleNames.Charset))
                return CreateCharset(token);
            else if (token.Data.Is(RuleNames.Namespace))
                return CreateNamespace(token);
            else if (token.Data.Is(RuleNames.Page))
                return CreatePage(token);
            else if (token.Data.Is(RuleNames.Supports))
                return CreateSupports(token);
            else if (token.Data.Is(RuleNames.ViewPort))
                return CreateViewport(token);
            else if (token.Data.Is(RuleNames.Document))
                return CreateDocument(token);

            return CreateUnknown(token);
        }

        /// <summary>
        /// Creates a rule with the enumeration of tokens.
        /// </summary>
        public CssRule CreateRule(CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                    return CreateAtRule(token);

                case CssTokenType.CurlyBracketOpen:
                    _tokenizer.RaiseErrorOccurred(CssParseError.InvalidBlockStart, token.Position);
                    _tokenizer.SkipUnknownRule();
                    return null;

                case CssTokenType.String:
                case CssTokenType.Url:
                case CssTokenType.CurlyBracketClose:
                case CssTokenType.RoundBracketClose:
                case CssTokenType.SquareBracketClose:
                    _tokenizer.RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
                    _tokenizer.SkipUnknownRule();
                    return null;

                default:
                    return CreateStyle(token);
            }
        }
        
        public CssRule CreateCharset(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssCharsetRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);

            if (token.Type == CssTokenType.String)
                rule.CharacterSet = token.Data;

            _tokenizer.JumpToNextSemicolon();
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateDocument(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssDocumentRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);
            FillFunctions(rule, ref token);
            CollectTrivia(rule, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillRules(rule);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateViewport(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssViewportRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillDeclarations(rule, Factory.Properties.CreateViewport);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateFontFace(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssFontFaceRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillDeclarations(rule, Factory.Properties.CreateFont);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateImport(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssImportRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = _tokenizer.Get();
                CollectTrivia(rule, ref token);
                FillMediaList(rule.Media, CssTokenType.Semicolon, ref token);
            }

            CollectTrivia(rule, ref token);
            _tokenizer.JumpToNextSemicolon();
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateKeyframes(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssKeyframesRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);
            rule.Name = GetRuleName(ref token);
            CollectTrivia(rule, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillKeyframeRules(rule);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateMedia(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssMediaRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);
            FillMediaList(rule.Media, CssTokenType.CurlyBracketOpen, ref token);
            CollectTrivia(rule, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                while (token.Type != CssTokenType.Eof)
                {
                    if (token.Type == CssTokenType.Semicolon)
                        return null;
                    else if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;

                    token = _tokenizer.Get();
                }
            }

            FillRules(rule);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateNamespace(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssNamespaceRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);
            rule.Prefix = GetRuleName(ref token);
            CollectTrivia(rule, ref token);

            if (token.Type == CssTokenType.Url)
                rule.NamespaceUri = token.Data;

            CollectTrivia(rule, ref token);
            _tokenizer.JumpToNextSemicolon();
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreatePage(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssPageRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);
            rule.Selector = CreateSelector(ref token);
            CollectTrivia(rule, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillDeclarations(rule.Style);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateSupports(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssSupportsRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref token);
            rule.Condition = AggregateCondition(ref token);
            CollectTrivia(rule, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillRules(rule);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateStyle(CssToken current)
        {
            var rule = new CssStyleRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref current);
            rule.Selector = CreateSelector(ref current);
            CollectTrivia(rule, ref current);
            FillDeclarations(rule.Style);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule.Selector != null ? rule : null;
        }

        public CssKeyframeRule CreateKeyframeRule(CssToken current)
        {
            var rule = new CssKeyframeRule(_parser);
            rule.Start = current.Position;
            CollectTrivia(rule, ref current);
            rule.Key = CreateKeyframeSelector(ref current);
            CollectTrivia(rule, ref current);
            FillDeclarations(rule.Style);
            return rule;
        }

        public CssRule CreateUnknown(CssToken current)
        {
            if (_parser.Options.IsIncludingUnknownRules)
            {
                var rule = new CssUnknownRule(current.Data, _parser);
                rule.Start = current.Position;
                var token = _tokenizer.Get();

                while (token.IsNot(CssTokenType.CurlyBracketOpen, CssTokenType.Semicolon, CssTokenType.Eof))
                {
                    rule.Prelude.Add(token);
                    token = _tokenizer.Get();
                }

                if (token.Type != CssTokenType.Eof)
                {
                    rule.Content.Add(token);

                    if (token.Type == CssTokenType.CurlyBracketOpen)
                    {
                        var curly = 1;

                        do
                        {
                            token = _tokenizer.Get();
                            rule.Content.Add(token);

                            switch (token.Type)
                            {
                                case CssTokenType.CurlyBracketOpen:
                                    curly++;
                                    break;
                                case CssTokenType.CurlyBracketClose:
                                    curly--;
                                    break;
                                case CssTokenType.Eof:
                                    curly = 0;
                                    break;
                            }
                        }
                        while (curly != 0);
                    }
                }

                rule.End = _tokenizer.GetCurrentPosition();
                return rule;
            }
            else
            {
                RaiseErrorOccurred(CssParseError.UnknownAtRule, current);
                _tokenizer.SkipUnknownRule();
                return null;
            }
        }

        #endregion

        #region API

        /// <summary>
        /// Creates a single value. Does not care about the !important flag.
        /// </summary>
        public CssValue CreateValue(ref CssToken token)
        {
            var important = false;
            return CreateValue(CssTokenType.CurlyBracketClose, ref token, out important);
        }

        /// <summary>
        /// Creates a list of CssMedium objects.
        /// </summary>
        public List<CssMedium> CreateMedia(ref CssToken token)
        {
            var list = new List<CssMedium>();
            RemoveTrivia(ref token);

            while (token.Type != CssTokenType.Eof)
            {
                var medium = CreateMedium(ref token);

                if (medium == null || token.IsNot(CssTokenType.Comma, CssTokenType.Eof))
                    throw new DomException(DomError.Syntax);

                list.Add(medium);
                token = _tokenizer.Get();
                CollectTrivia(medium, ref token);
            }

            return list;
        }

        /// <summary>
        /// Creates as many rules as possible.
        /// </summary>
        /// <returns>The found rules.</returns>
        public void CreateRules(CssStyleSheet sheet)
        {
            var token = _tokenizer.Get();
            CollectTrivia(sheet, ref token);

            do
            {
                var rule = CreateRule(token);
                token = _tokenizer.Get();
                CollectTrivia(rule, ref token);
                sheet.AddRule(rule);
            }
            while (token.Type != CssTokenType.Eof);
        }

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        public CssCondition CreateCondition(ref CssToken token)
        {
            RemoveTrivia(ref token);
            return AggregateCondition(ref token);
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        public KeyframeSelector CreateKeyframeSelector(ref CssToken token)
        {
            var keys = new List<Percent>();
            var valid = true;
            var start = token;
            var selector = new KeyframeSelector(keys);
            CollectTrivia(selector, ref token);

            while (token.Type != CssTokenType.Eof)
            {
                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;
                    else if (token.Type != CssTokenType.Comma)
                        valid = false;
                    else
                        token = _tokenizer.Get();

                    CollectTrivia(selector, ref token);
                }

                if (token.Type == CssTokenType.Percentage)
                    keys.Add(new Percent(((CssUnitToken)token).Value));
                else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.From))
                    keys.Add(Percent.Zero);
                else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.To))
                    keys.Add(Percent.Hundred);
                else
                    valid = false;

                token = _tokenizer.Get();
                CollectTrivia(selector, ref token);
            }

            if (!valid)
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);

            return selector;
        }

        /// <summary>
        /// Called when the document functions have to been found.
        /// </summary>
        public List<CssDocumentFunction> CreateFunctions(ref CssToken token)
        {
            var rule = new CssDocumentRule(_parser);
            RemoveTrivia(ref token);
            FillFunctions(rule, ref token);
            return rule.Conditions;
        }

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        public void FillDeclarations(CssStyleDeclaration style)
        {
            var token = _tokenizer.Get();
            style.Start = token.Position;
            CollectTrivia(style, ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(Factory.Properties.Create, ref token);

                if (property != null && property.HasValue)
                    style.SetProperty(property);

                CollectTrivia(style, ref token);
            }

            style.End = _tokenizer.GetCurrentPosition();
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public CssProperty CreateDeclarationWith(Func<String, CssProperty> createProperty, ref CssToken token)
        {
            var property = default(CssProperty);

            if (token.Type == CssTokenType.Ident)
            {
                property = _parser.Options.IsIncludingUnknownDeclarations || _parser.Options.IsToleratingInvalidValues ?
                    new CssUnknownProperty(token.Data) : 
                    createProperty(token.Data);

                if (property == null)
                    RaiseErrorOccurred(CssParseError.UnknownDeclarationName, token);
                else
                    property.Start = token.Position;

                token = _tokenizer.Get();
                CollectTrivia(property, ref token);

                if (token.Type == CssTokenType.Colon)
                {
                    var important = false;
                    var val = CreateValue(CssTokenType.CurlyBracketClose, ref token, out important);

                    if (val == null)
                        RaiseErrorOccurred(CssParseError.ValueMissing, token);
                    else if (property != null && property.TrySetValue(val))
                        property.IsImportant = important;

                    CollectTrivia(property, ref token);
                }
                else
                    RaiseErrorOccurred(CssParseError.ColonMissing, token);

                _tokenizer.JumpToEndOfDeclaration();

                if (property != null)
                    property.End = _tokenizer.GetCurrentPosition();

                token = _tokenizer.Get();
            }
            else if (token.Type != CssTokenType.Eof)
            {
                RaiseErrorOccurred(CssParseError.IdentExpected, token);
                _tokenizer.JumpToEndOfDeclaration();
                token = _tokenizer.Get();
            }

            if (token.Type == CssTokenType.Semicolon)
                token = _tokenizer.Get();

            return property;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public CssProperty CreateDeclaration(ref CssToken token)
        {
            RemoveTrivia(ref token);
            return CreateDeclarationWith(Factory.Properties.Create, ref token);
        }

        /// <summary>
        /// Scans the current medium for the @media or @import rule.
        /// </summary>
        public CssMedium CreateMedium(ref CssToken token)
        {
            var medium = new CssMedium();
            CollectTrivia(medium, ref token);
            medium.Start = token.Position;

            if (token.Type == CssTokenType.Ident)
            {
                var identifier = token.Data;

                if (identifier.Isi(Keywords.Not))
                {
                    medium.IsInverse = true;
                    token = _tokenizer.Get();
                    CollectTrivia(medium, ref token);
                }
                else if (identifier.Isi(Keywords.Only))
                {
                    medium.IsExclusive = true;
                    token = _tokenizer.Get();
                    CollectTrivia(medium, ref token);
                }
            }

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = token.Data;
                medium.End = _tokenizer.GetCurrentPosition();
                token = _tokenizer.Get();
                CollectTrivia(medium, ref token);

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(Keywords.And))
                    return medium;

                token = _tokenizer.Get();
                CollectTrivia(medium, ref token);
            }

            do
            {
                if (token.Type != CssTokenType.RoundBracketOpen)
                    return null;

                token = _tokenizer.Get();
                CollectTrivia(medium, ref token);
                var constraint = TrySetConstraint(medium, ref token);

                if (token.Type != CssTokenType.RoundBracketClose)
                    return null;

                medium.End = token.Position;
                token = _tokenizer.Get();
                CollectTrivia(medium, ref token);

                if (constraint == false)
                    return null;

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(Keywords.And))
                    break;

                token = _tokenizer.Get();
                CollectTrivia(medium, ref token);
            }
            while (token.Type != CssTokenType.Eof);

            return medium;
        }

        #endregion

        #region Helpers

        void CollectTrivia(CssNode node, ref CssToken token)
        {
            if (_parser.Options.IsStoringTrivia)
            {
                while (token.Type == CssTokenType.Whitespace || token.Type == CssTokenType.Comment)
                {
                    (node.Trivia ?? (node.Trivia = new List<CssToken>())).Add(token);
                    token = _tokenizer.Get();
                }
            }
            else
            {
                RemoveTrivia(ref token);
            }
        }

        void RemoveTrivia(ref CssToken token)
        {
            while (token.Type == CssTokenType.Whitespace || token.Type == CssTokenType.Comment)
            {
                token = _tokenizer.Get();
            }
        }

        CssCondition AggregateCondition(ref CssToken token)
        {
            var condition = ExtractCondition(ref token);

            if (condition != null)
            {
                CollectTrivia(condition, ref token);
                var conjunction = token.Data;
                var creator = conjunction.GetCreator();

                if (creator != null)
                {
                    token = _tokenizer.Get();
                    CollectTrivia(condition, ref token);
                    var conditions = MultipleConditions(condition, conjunction, ref token);
                    var group = creator(conditions);
                    group.Start = condition.Start;
                    group.End = token.Position;
                    condition = group;
                }
            }

            return condition;
        }

        CssCondition ExtractCondition(ref CssToken token)
        {
            var condition = default(CssCondition);

            if (token.Type == CssTokenType.RoundBracketOpen)
            {
                var group = new GroupCondition();
                group.Start = token.Position;
                token = _tokenizer.Get();
                CollectTrivia(null, ref token);
                condition = AggregateCondition(ref token);

                if (condition != null)
                {
                    group.Value = condition;
                    condition = group;
                }
                else if (token.Type == CssTokenType.Ident)
                    condition = DeclarationCondition(ref token);

                if (token.Type == CssTokenType.RoundBracketClose)
                {
                    if (condition != null)
                        condition.End = token.Position;

                    token = _tokenizer.Get();
                    CollectTrivia(condition, ref token);
                }
            }
            else if (token.Data.Isi(Keywords.Not))
            {
                var negate = new NotCondition();
                negate.Start = token.Position;
                token = _tokenizer.Get();
                CollectTrivia(negate, ref token);
                condition = ExtractCondition(ref token);

                if (condition != null)
                {
                    negate.Value = condition;
                    negate.End = condition.End;
                    condition = negate;
                }
            }

            return condition;
        }

        CssCondition DeclarationCondition(ref CssToken token)
        {
            var property = Factory.Properties.Create(token.Data) ?? new CssUnknownProperty(token.Data);
            var start = token.Position;

            token = _tokenizer.Get();
            CollectTrivia(property, ref token);

            if (token.Type == CssTokenType.Colon)
            {
                var important = false;
                var result = CreateValue(CssTokenType.RoundBracketClose, ref token, out important);
                property.IsImportant = important;

                if (result != null)
                {
                    var condition = new DeclarationCondition(property, result);
                    condition.Start = start;
                    condition.End = token.Position;
                    return condition;
                }
            }

            return null;
        }

        List<CssCondition> MultipleConditions(CssCondition condition, String connector, ref CssToken token)
        {
            var list = new List<CssCondition>();
            CollectTrivia(condition, ref token);
            list.Add(condition);

            while (token.Type != CssTokenType.Eof)
            {
                condition = ExtractCondition(ref token);

                if (condition == null)
                    break;

                list.Add(condition);

                if (!token.Data.Isi(connector))
                    break;

                token = _tokenizer.Get();
                CollectTrivia(condition, ref token);
            }

            return list;
        }

        void FillFunctions(CssDocumentRule rule, ref CssToken token)
        {
            do
            {
                var function = token.ToDocumentFunction();

                if (function == null)
                    break;

                function.Start = token.Position;
                function.End = _tokenizer.GetCurrentPosition();
                rule.Conditions.Add(function);
                token = _tokenizer.Get();
                CollectTrivia(rule, ref token);

                if (token.Type != CssTokenType.Comma)
                    break;

                token = _tokenizer.Get();
                CollectTrivia(rule, ref token);
            }
            while (token.Type == CssTokenType.Eof);
        }

        void FillKeyframeRules(CssKeyframesRule parentRule)
        {
            var token = _tokenizer.Get();
            CollectTrivia(parentRule, ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateKeyframeRule(token);

                if (rule != null)
                    parentRule.Rules.Add(rule, parentRule.Owner, parentRule);

                token = _tokenizer.Get();
                CollectTrivia(parentRule, ref token);
            }
        }

        void FillDeclarations(CssDeclarationRule rule, Func<String, CssProperty> createProperty)
        {
            var token = _tokenizer.Get();
            CollectTrivia(rule, ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(createProperty, ref token);

                if (property != null && property.HasValue)
                    rule.SetProperty(property);

                CollectTrivia(rule, ref token);
            }
        }

        CssRule SkipDeclarations(CssToken token)
        {
            RaiseErrorOccurred(CssParseError.InvalidToken, token);
            _tokenizer.SkipUnknownRule();
            return null;
        }

        void FillRules(CssGroupingRule group)
        {
            var token = _tokenizer.Get();
            CollectTrivia(group, ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateRule(token);
                group.AddRule(rule);
                token = _tokenizer.Get();
                CollectTrivia(rule, ref token);
            }
        }

        ISelector CreateSelector(ref CssToken token)
        {
            var selector = Pool.NewSelectorConstructor();
            var start = token;

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketOpen, CssTokenType.CurlyBracketClose))
            {
                selector.Apply(token);
                token = _tokenizer.Get();
            }

            if (!selector.IsValid)
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);

            return selector.ToPool();
        }

        CssValue CreateValue(CssTokenType closing, ref CssToken token, out Boolean important)
        {
            var value = Pool.NewValueBuilder();
            _tokenizer.IsInValue = true;
            token = _tokenizer.Get();

            while (token.Type != CssTokenType.Eof)
            {
                if (token.Is(CssTokenType.Semicolon, closing))
                    break;

                value.Apply(token);
                token = _tokenizer.Get();
            }

            important = value.IsImportant;
            _tokenizer.IsInValue = false;

            if (value.IsValid || _parser.Options.IsToleratingInvalidValues)
                return value.ToPool();

            value.ToPool();
            return null;
        }

        String GetRuleName(ref CssToken token)
        {
            var name = String.Empty;

            if (token.Type == CssTokenType.Ident)
            {
                name = token.Data;
                token = _tokenizer.Get();
            }

            return name;
        }

        void FillMediaList(MediaList list, CssTokenType end, ref CssToken token)
        {
            if (token.Type == end)
                return;

            while (token.Type != CssTokenType.Eof)
            {
                var medium = CreateMedium(ref token);

                if (medium != null)
                    list.Add(medium);

                if (token.Type != CssTokenType.Comma)
                    break;

                token = _tokenizer.Get();
                CollectTrivia(list, ref token);
            }

            if (token.Type == end && list.Length > 0)
                return;

            list.Clear();
            list.Add(new CssMedium
            {
                IsInverse = true,
                Type = Keywords.All
            });
        }

        void RaiseErrorOccurred(CssParseError code, CssToken token)
        {
            _tokenizer.RaiseErrorOccurred(code, token.Position);
        }

        Boolean TrySetConstraint(CssMedium medium, ref CssToken token)
        {
            if (token.Type != CssTokenType.Ident)
            {
                _tokenizer.JumpToClosedArguments();
                token = _tokenizer.Get();
                return false;
            }

            var val = CssValue.Empty;
            var feature = _parser.Options.IsToleratingInvalidConstraints ?
                new UnknownMediaFeature(token.Data) : Factory.MediaFeatures.Create(token.Data);

            if (feature != null)
                feature.Start = token.Position;

            token = _tokenizer.Get();

            if (token.Type == CssTokenType.Colon)
            {
                var value = Pool.NewValueBuilder();
                token = _tokenizer.Get();

                while (token.Type != CssTokenType.RoundBracketClose || value.IsReady == false)
                {
                    if (token.Type == CssTokenType.Eof)
                        break;

                    value.Apply(token);
                    token = _tokenizer.Get();
                }

                val = value.ToPool();
            }
            else if (token.Type == CssTokenType.Eof)
                return false;

            if (feature != null && feature.TrySetValue(val))
            {
                feature.End = token.Position;
                medium.AddConstraint(feature);
                return true;
            }

            return false;
        }

        #endregion
    }
}
