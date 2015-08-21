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
            var trivia = GetTrivia(ref token);

            if (token.Type == CssTokenType.String)
                rule.CharacterSet = token.Data;

            trivia = GetTrivia(ref token);
            _tokenizer.JumpToNextSemicolon();
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateDocument(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssDocumentRule(_parser);
            rule.Start = current.Position;
            var trivia = GetTrivia(ref token);
            var functions = CreateFunctions(ref token);
            rule.Conditions.AddRange(functions);
            trivia = GetTrivia(ref token);

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
            var trivia = GetTrivia(ref token);

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
            var trivia = GetTrivia(ref token);

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
            var trivia = GetTrivia(ref token);

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
                FillMediaList(rule.Media, CssTokenType.Semicolon, ref token);
            }

            trivia = GetTrivia(ref token);
            _tokenizer.JumpToNextSemicolon();
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateKeyframes(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssKeyframesRule(_parser);
            rule.Start = current.Position;
            var trivia = GetTrivia(ref token);
            rule.Name = GetRuleName(ref token);
            trivia = GetTrivia(ref token);

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
            var trivia = GetTrivia(ref token);
            FillMediaList(rule.Media, CssTokenType.CurlyBracketOpen, ref token);
            trivia = GetTrivia(ref token);

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
            var trivia = GetTrivia(ref token);
            rule.Prefix = GetRuleName(ref token);
            trivia = GetTrivia(ref token);

            if (token.Type == CssTokenType.Url)
                rule.NamespaceUri = token.Data;

            trivia = GetTrivia(ref token);
            _tokenizer.JumpToNextSemicolon();
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreatePage(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssPageRule(_parser);
            rule.Start = current.Position;
            var trivia = GetTrivia(ref token);
            rule.Selector = CreateSelector(ref token);
            trivia = GetTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillDeclarations(rule.Style);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule;
        }

        public CssRule CreateStyle(CssToken current)
        {
            var rule = new CssStyleRule(_parser);
            rule.Start = current.Position;
            var trivia = GetTrivia(ref current);
            rule.Selector = CreateSelector(ref current);
            trivia = GetTrivia(ref current);
            FillDeclarations(rule.Style);
            rule.End = _tokenizer.GetCurrentPosition();
            return rule.Selector != null ? rule : null;
        }

        public CssRule CreateSupports(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssSupportsRule(_parser);
            rule.Start = current.Position;
            var trivia = GetTrivia(ref token);
            rule.Condition = CreateCondition(ref token);
            trivia = GetTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillRules(rule);
            rule.End = _tokenizer.GetCurrentPosition();
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
            var trivia = GetTrivia(ref token);

            while (token.Type != CssTokenType.Eof)
            {
                var medium = CreateMedium(ref token);

                if (medium == null || token.IsNot(CssTokenType.Comma, CssTokenType.Eof))
                    throw new DomException(DomError.Syntax);

                list.Add(medium);
                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }

            return list;
        }

        /// <summary>
        /// Creates as many rules as possible.
        /// </summary>
        /// <returns>The found rules.</returns>
        public IEnumerable<CssRule> CreateRules()
        {
            var token = _tokenizer.Get();
            var trivia = GetTrivia(ref token);

            do
            {
                yield return CreateRule(token);
                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }
            while (token.Type != CssTokenType.Eof);
        }

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        public ICondition CreateCondition(ref CssToken token)
        {
            var trivia = GetTrivia(ref token);
            var condition = ExtractCondition(ref token);

            if (condition != null)
            {
                trivia = GetTrivia(ref token);

                if (token.Data.Isi(Keywords.And))
                {
                    token = _tokenizer.Get();
                    trivia = GetTrivia(ref token);
                    var conditions = MultipleConditions(condition, Keywords.And, ref token);
                    return new AndCondition(conditions);
                }
                else if (token.Data.Isi(Keywords.Or))
                {
                    token = _tokenizer.Get();
                    trivia = GetTrivia(ref token);
                    var conditions = MultipleConditions(condition, Keywords.Or, ref token);
                    return new OrCondition(conditions);
                }
            }

            return condition;
        }

        /// <summary>
        /// Before the curly bracket of an @keyframes rule has been seen.
        /// </summary>
        public CssKeyframeRule CreateKeyframeRule(CssToken token)
        {
            var rule = new CssKeyframeRule(_parser);
            var trivia = GetTrivia(ref token);
            rule.Key = CreateKeyframeSelector(ref token);
            trivia = GetTrivia(ref token);

            if (rule.Key == null)
            {
                _tokenizer.JumpToEndOfDeclaration();
                return null;
            }

            FillDeclarations(rule.Style);
            return rule;
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        public KeyframeSelector CreateKeyframeSelector(ref CssToken token)
        {
            var keys = new List<Percent>();
            var trivia = GetTrivia(ref token);

            while (token.Type != CssTokenType.Eof)
            {
                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;
                    else if (token.Type != CssTokenType.Comma)
                        return null;

                    token = _tokenizer.Get();
                    trivia = GetTrivia(ref token);
                }

                if (token.Type == CssTokenType.Percentage)
                    keys.Add(new Percent(((CssUnitToken)token).Value));
                else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.From))
                    keys.Add(Percent.Zero);
                else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.To))
                    keys.Add(Percent.Hundred);
                else
                    return null;

                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }

            return new KeyframeSelector(keys);
        }

        /// <summary>
        /// Called when the document functions have to been found.
        /// </summary>
        public List<IDocumentFunction> CreateFunctions(ref CssToken token)
        {
            var list = new List<IDocumentFunction>();
            var trivia = GetTrivia(ref token);

            do
            {
                var function = token.ToDocumentFunction();

                if (function == null)
                    break;

                list.Add(function);
                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }
            while (token.Type == CssTokenType.Comma);

            return list;
        }

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        public void FillDeclarations(CssStyleDeclaration style)
        {
            var token = _tokenizer.Get();
            var trivia = GetTrivia(ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclaration(ref token);

                if (property != null && property.HasValue)
                    style.SetProperty(property);

                trivia = GetTrivia(ref token);
            }
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public CssProperty CreateDeclarationWith(Func<String, CssProperty> createProperty, ref CssToken token)
        {
            var property = default(CssProperty);

            if (token.Type == CssTokenType.Ident)
            {
                var propertyName = token.Data;
                token = _tokenizer.Get();
                var trivia = GetTrivia(ref token);

                if (token.Type == CssTokenType.Colon)
                {
                    property = _parser.Options.IsIncludingUnknownDeclarations || _parser.Options.IsToleratingInvalidValues ?
                        new CssUnknownProperty(propertyName) : createProperty(propertyName);

                    if (property == null)
                        RaiseErrorOccurred(CssParseError.UnknownDeclarationName, token);

                    var important = false;
                    var val = CreateValue(CssTokenType.CurlyBracketClose, ref token, out important);

                    if (val == null)
                        RaiseErrorOccurred(CssParseError.ValueMissing, token);
                    else if (property != null && property.TrySetValue(val))
                        property.IsImportant = important;

                    trivia = GetTrivia(ref token);
                }
                else
                    RaiseErrorOccurred(CssParseError.ColonMissing, token);

                _tokenizer.JumpToEndOfDeclaration();
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
            var trivia = GetTrivia(ref token);
            return CreateDeclarationWith(Factory.Properties.Create, ref token);
        }

        /// <summary>
        /// Scans the current medium for the @media or @import rule.
        /// </summary>
        public CssMedium CreateMedium(ref CssToken token)
        {
            var medium = new CssMedium();
            var trivia = GetTrivia(ref token);

            if (token.Type == CssTokenType.Ident)
            {
                var identifier = token.Data;

                if (identifier.Isi(Keywords.Not))
                {
                    medium.IsInverse = true;
                    token = _tokenizer.Get();
                    trivia = GetTrivia(ref token);
                }
                else if (identifier.Isi(Keywords.Only))
                {
                    medium.IsExclusive = true;
                    token = _tokenizer.Get();
                    trivia = GetTrivia(ref token);
                }
            }

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = token.Data;
                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(Keywords.And))
                    return medium;

                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }

            do
            {
                if (token.Type != CssTokenType.RoundBracketOpen)
                    return null;

                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
                var couldSetConstraint = TrySetConstraint(medium, ref token);

                if (token.Type != CssTokenType.RoundBracketClose)
                    return null;

                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);

                if (couldSetConstraint == false)
                    return null;

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(Keywords.And))
                    break;

                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }
            while (token.Type != CssTokenType.Eof);

            return medium;
        }

        #endregion

        #region Helpers

        List<CssToken> GetTrivia(ref CssToken token)
        {
            var store = _parser.Options.IsStoringTrivia;
            var list = default(List<CssToken>);

            while (token.Type == CssTokenType.Whitespace || token.Type == CssTokenType.Comment)
            {
                if (store)
                    (list ?? (list = new List<CssToken>())).Add(token);

                token = _tokenizer.Get();
            }

            return list;
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
                {
                    token = _tokenizer.Get();
                    var trivia = GetTrivia(ref token);
                }
            }
            else if (token.Data.Isi(Keywords.Not))
            {
                token = _tokenizer.Get();
                var trivia = GetTrivia(ref token);
                condition = ExtractCondition(ref token);

                if (condition != null)
                    condition = new NotCondition(condition);
            }

            return condition;
        }

        ICondition DeclarationCondition(ref CssToken token)
        {
            var trivia = GetTrivia(ref token);
            var name = token.Data;
            var property = Factory.Properties.Create(name);

            if (property == null)
                property = new CssUnknownProperty(name);

            token = _tokenizer.Get();
            trivia = GetTrivia(ref token);

            if (token.Type == CssTokenType.Colon)
            {
                var important = false;
                var result = CreateValue(CssTokenType.RoundBracketClose, ref token, out important);
                property.IsImportant = important;

                if (result != null)
                    return new DeclarationCondition(property, result);
            }

            return null;
        }

        List<ICondition> MultipleConditions(ICondition condition, String connector, ref CssToken token)
        {
            var list = new List<ICondition>();
            var trivia = GetTrivia(ref token);
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
                trivia = GetTrivia(ref token);
            }

            return list;
        }

        /// <summary>
        /// Fills the given keyframe rule with rules given by the tokens.
        /// </summary>
        void FillKeyframeRules(CssKeyframesRule parentRule)
        {
            var token = _tokenizer.Get();
            var trivia = GetTrivia(ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateKeyframeRule(token);

                if (rule != null)
                    parentRule.Rules.Add(rule, parentRule.Owner, parentRule);

                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }
        }

        void FillDeclarations(CssDeclarationRule rule, Func<String, CssProperty> createProperty)
        {
            var token = _tokenizer.Get();
            var trivia = GetTrivia(ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(createProperty, ref token);

                if (property != null && property.HasValue)
                    rule.SetProperty(property);

                trivia = GetTrivia(ref token);
            }
        }

        /// <summary>
        /// Skips the current declaration.
        /// </summary>
        CssRule SkipDeclarations(CssToken token)
        {
            RaiseErrorOccurred(CssParseError.InvalidToken, token);
            _tokenizer.SkipUnknownRule();
            return null;
        }

        /// <summary>
        /// Fills the given parent rule with rules given by the tokens.
        /// </summary>
        void FillRules(CssGroupingRule group)
        {
            var token = _tokenizer.Get();
            var trivia = GetTrivia(ref token);

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateRule(token);
                group.AddRule(rule);
                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
            }
        }

        /// <summary>
        /// State that is called once we are in a CSS selector.
        /// </summary>
        ISelector CreateSelector(ref CssToken token)
        {
            var selector = Pool.NewSelectorConstructor();
            var start = token;

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketOpen, CssTokenType.CurlyBracketClose))
            {
                selector.Apply(token);
                token = _tokenizer.Get();
            }

            if (selector.IsValid == false)
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);

            return selector.ToPool();
        }

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        CssValue CreateValue(CssTokenType closing, ref CssToken token, out Boolean important)
        {
            var value = Pool.NewValueBuilder();
            _tokenizer.IsInValue = true;
            token = _tokenizer.Get();
            var trivia = GetTrivia(ref token);

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

        /// <summary>
        /// Before the name of a rule has been detected.
        /// </summary>
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

        /// <summary>
        /// Before any medium has been found for the @media or @import rule.
        /// </summary>
        void FillMediaList(MediaList list, CssTokenType end, ref CssToken token)
        {
            if (token.Type == end)
                return;

            var trivia = GetTrivia(ref token);

            while (token.Type != CssTokenType.Eof)
            {
                var medium = CreateMedium(ref token);

                if (medium != null)
                    list.Add(medium);

                if (token.Type != CssTokenType.Comma)
                    break;

                token = _tokenizer.Get();
                trivia = GetTrivia(ref token);
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

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        /// <param name="token">The associated token.</param>
        void RaiseErrorOccurred(CssParseError code, CssToken token)
        {
            _tokenizer.RaiseErrorOccurred(code, token.Position);
        }

        /// <summary>
        /// Tries to read and set media constraints for the provided medium.
        /// </summary>
        Boolean TrySetConstraint(CssMedium medium, ref CssToken token)
        {
            if (token.Type != CssTokenType.Ident)
            {
                _tokenizer.JumpToClosedArguments();
                token = _tokenizer.Get();
                return false;
            }

            var value = Pool.NewValueBuilder();
            var featureName = token.Data;
            var val = CssValue.Empty;
            var feature = _parser.Options.IsToleratingInvalidConstraints ?
                new UnknownMediaFeature(featureName) : Factory.MediaFeatures.Create(featureName);

            token = _tokenizer.Get();

            if (token.Type == CssTokenType.Colon)
            {
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
                medium.AddConstraint(feature);
                return true;
            }

            return false;
        }

        #endregion
    }
}
