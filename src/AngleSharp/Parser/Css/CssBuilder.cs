namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for details.
    /// </summary>
    sealed class CssBuilder
    {
        #region Fields

        private readonly CssTokenizer _tokenizer;
        private readonly CssParser _parser;
        private readonly Stack<CssNode> _nodes;

        #endregion

        #region ctor

        public CssBuilder(CssTokenizer tokenizer, CssParser parser)
        {
            _tokenizer = tokenizer;
            _parser = parser;
            _nodes = new Stack<CssNode>();
        }

        #endregion

        #region Create Rules

        public CssRule CreateAtRule(CssToken token)
        {
            if (token.Data.Is(RuleNames.Media))
            {
                return CreateMedia(token);
            }
            else if (token.Data.Is(RuleNames.FontFace))
            {
                return CreateFontFace(token);
            }
            else if (token.Data.Is(RuleNames.Keyframes))
            {
                return CreateKeyframes(token);
            }
            else if (token.Data.Is(RuleNames.Import))
            {
                return CreateImport(token);
            }
            else if (token.Data.Is(RuleNames.Charset))
            {
                return CreateCharset(token);
            }
            else if (token.Data.Is(RuleNames.Namespace))
            {
                return CreateNamespace(token);
            }
            else if (token.Data.Is(RuleNames.Page))
            {
                return CreatePage(token);
            }
            else if (token.Data.Is(RuleNames.Supports))
            {
                return CreateSupports(token);
            }
            else if (token.Data.Is(RuleNames.ViewPort))
            {
                return CreateViewport(token);
            }
            else if (token.Data.Is(RuleNames.Document))
            {
                return CreateDocument(token);
            }

            return CreateUnknown(token);
        }

        public CssRule CreateRule(CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                    return CreateAtRule(token);

                case CssTokenType.CurlyBracketOpen:
                    RaiseErrorOccurred(CssParseError.InvalidBlockStart, token.Position);
                    JumpToRuleEnd(ref token);
                    return null;

                case CssTokenType.String:
                case CssTokenType.Url:
                case CssTokenType.CurlyBracketClose:
                case CssTokenType.RoundBracketClose:
                case CssTokenType.SquareBracketClose:
                    RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
                    JumpToRuleEnd(ref token);
                    return null;

                default:
                    return CreateStyle(token);
            }
        }
        
        public CssRule CreateCharset(CssToken current)
        {
            var rule = new CssCharsetRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.String)
            {
                rule.CharacterSet = token.Data;
            }

            JumpToEnd(ref token);
            rule.SourceCode = CreateView(start, token.Position);
            _nodes.Pop();
            return rule;
        }

        public CssRule CreateDocument(CssToken current)
        {
            var rule = new CssDocumentRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);
            FillFunctions(function => rule.AppendChild(function), ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                var end = FillRules(rule);
                rule.SourceCode = CreateView(start, end);
                _nodes.Pop();
                return rule;
            }

            _nodes.Pop();
            return SkipDeclarations(token);
        }

        public CssRule CreateViewport(CssToken current)
        {
            var rule = new CssViewportRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                var end = FillDeclarations(rule, Factory.Properties.CreateViewport);
                rule.SourceCode = CreateView(start, end);
                _nodes.Pop();
                return rule;
            }

            _nodes.Pop();
            return SkipDeclarations(token);
        }

        public CssRule CreateFontFace(CssToken current)
        {
            var rule = new CssFontFaceRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                var end = FillDeclarations(rule, Factory.Properties.CreateFont);
                rule.SourceCode = CreateView(start, end);
                _nodes.Pop();
                return rule;
            }

            _nodes.Pop();
            return SkipDeclarations(token);
        }

        public CssRule CreateImport(CssToken current)
        {
            var rule = new CssImportRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = NextToken();
                CollectTrivia(ref token);
                FillMediaList(rule.Media, CssTokenType.Semicolon, ref token);
            }

            CollectTrivia(ref token);
            JumpToEnd(ref token);
            rule.SourceCode = CreateView(start, token.Position);
            _nodes.Pop();
            return rule;
        }

        public CssRule CreateKeyframes(CssToken current)
        {
            var rule = new CssKeyframesRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);
            rule.Name = GetRuleName(ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                var end = FillKeyframeRules(rule);
                rule.SourceCode = CreateView(start, end);
                _nodes.Pop();
                return rule;
            }

            _nodes.Pop();
            return SkipDeclarations(token);
        }

        public CssRule CreateMedia(CssToken current)
        {
            var rule = new CssMediaRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);
            FillMediaList(rule.Media, CssTokenType.CurlyBracketOpen, ref token);
            CollectTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                while (token.Type != CssTokenType.EndOfFile)
                {
                    if (token.Type == CssTokenType.Semicolon)
                    {
                        _nodes.Pop();
                        return null;
                    }
                    else if (token.Type == CssTokenType.CurlyBracketOpen)
                    {
                        break;
                    }

                    token = NextToken();
                }
            }

            var end = FillRules(rule);
            rule.SourceCode = CreateView(start, end);
            _nodes.Pop();
            return rule;
        }

        public CssRule CreateNamespace(CssToken current)
        {
            var rule = new CssNamespaceRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);
            rule.Prefix = GetRuleName(ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.Url)
            {
                rule.NamespaceUri = token.Data;
            }

            JumpToEnd(ref token);
            rule.SourceCode = CreateView(start, token.Position);
            _nodes.Pop();
            return rule;
        }

        public CssRule CreatePage(CssToken current)
        {
            var rule = new CssPageRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);
            rule.Selector = CreateSelector(ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                var end = FillDeclarations(rule.Style);
                rule.SourceCode = CreateView(start, end);
                _nodes.Pop();
                return rule;
            }

            _nodes.Pop();
            return SkipDeclarations(token);
        }

        public CssRule CreateSupports(CssToken current)
        {
            var rule = new CssSupportsRule(_parser);
            var start = current.Position;
            var token = NextToken();
            _nodes.Push(rule);
            CollectTrivia(ref token);
            rule.Condition = AggregateCondition(ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                var end = FillRules(rule);
                rule.SourceCode = CreateView(start, end);
                _nodes.Pop();
                return rule;
            }

            _nodes.Pop();
            return SkipDeclarations(token);
        }

        public CssRule CreateStyle(CssToken current)
        {
            var rule = new CssStyleRule(_parser);
            var start = current.Position;
            _nodes.Push(rule);
            CollectTrivia(ref current);
            rule.Selector = CreateSelector(ref current);
            var end = FillDeclarations(rule.Style);
            rule.SourceCode = CreateView(start, end);
            _nodes.Pop();
            return rule.Selector != null ? rule : null;
        }

        public CssKeyframeRule CreateKeyframeRule(CssToken current)
        {
            var rule = new CssKeyframeRule(_parser);
            var start = current.Position;
            _nodes.Push(rule);
            CollectTrivia(ref current);
            rule.Key = CreateKeyframeSelector(ref current);
            var end = FillDeclarations(rule.Style);
            rule.SourceCode = CreateView(start, end);
            _nodes.Pop();
            return rule.Key != null ? rule : null;
        }

        public CssRule CreateUnknown(CssToken current)
        {
            var start = current.Position;

            if (_parser.Options.IsIncludingUnknownRules)
            {
                var token = NextToken();
                var rule = new CssUnknownRule(current.Data, _parser);
                _nodes.Push(rule);

                while (token.IsNot(CssTokenType.CurlyBracketOpen, CssTokenType.Semicolon, CssTokenType.EndOfFile))
                {
                    token = NextToken();
                }

                if (token.Type == CssTokenType.CurlyBracketOpen)
                {
                    var curly = 1;

                    do
                    {
                        token = NextToken();

                        switch (token.Type)
                        {
                            case CssTokenType.CurlyBracketOpen:
                                curly++;
                                break;
                            case CssTokenType.CurlyBracketClose:
                                curly--;
                                break;
                            case CssTokenType.EndOfFile:
                                curly = 0;
                                break;
                        }
                    }
                    while (curly != 0);
                }

                rule.SourceCode = CreateView(start, token.Position);
                _nodes.Pop();
                return rule;
            }
            else
            {
                RaiseErrorOccurred(CssParseError.UnknownAtRule, start);
                JumpToRuleEnd(ref current);
                return default(CssUnknownRule);
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
            CollectTrivia(ref token);

            while (token.Type != CssTokenType.EndOfFile)
            {
                var medium = CreateMedium(ref token);

                if (medium == null || token.IsNot(CssTokenType.Comma, CssTokenType.EndOfFile))
                    throw new DomException(DomError.Syntax);

                token = NextToken();
                CollectTrivia(ref token);
                list.Add(medium);
            }

            return list;
        }

        /// <summary>
        /// Creates as many rules as possible.
        /// </summary>
        /// <returns>The found rules.</returns>
        public TextPosition CreateRules(CssStyleSheet sheet)
        {
            var token = NextToken();
            _nodes.Push(sheet);
            CollectTrivia(ref token);

            while (token.Type != CssTokenType.EndOfFile)
            {
                var rule = CreateRule(token);
                token = NextToken();
                CollectTrivia(ref token);
                sheet.Rules.Add(rule);
            }

            _nodes.Pop();
            return token.Position;
        }

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        public IConditionFunction CreateCondition(ref CssToken token)
        {
            CollectTrivia(ref token);
            return AggregateCondition(ref token);
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        public KeyframeSelector CreateKeyframeSelector(ref CssToken token)
        {
            var keys = new List<Percent>();
            var valid = true;
            var start = token.Position;
            CollectTrivia(ref token);

            while (token.Type != CssTokenType.EndOfFile)
            {
                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                    {
                        break;
                    }
                    else if (token.Type != CssTokenType.Comma)
                    {
                        valid = false;
                    }
                    else
                    {
                        token = NextToken();
                    }

                    CollectTrivia(ref token);
                }

                if (token.Type == CssTokenType.Percentage)
                {
                    keys.Add(new Percent(((CssUnitToken)token).Value));
                }
                else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.From))
                {
                    keys.Add(Percent.Zero);
                }
                else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.To))
                {
                    keys.Add(Percent.Hundred);
                }
                else
                {
                    valid = false;
                }

                token = NextToken();
                CollectTrivia(ref token);
            }

            if (!valid)
            {
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);
            }

            return new KeyframeSelector(keys);
        }

        /// <summary>
        /// Called when the document functions have to been found.
        /// </summary>
        public List<DocumentFunction> CreateFunctions(ref CssToken token)
        {
            var functions = new List<DocumentFunction>();
            CollectTrivia(ref token);
            FillFunctions(function => functions.Add(function), ref token);
            return functions;
        }

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        public TextPosition FillDeclarations(CssStyleDeclaration style)
        {
            var token = NextToken();
            _nodes.Push(style);
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(Factory.Properties.Create, ref token);

                if (property != null && property.HasValue)
                {
                    style.SetProperty(property);
                }

                CollectTrivia(ref token);
            }

            _nodes.Pop();
            return token.Position;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public CssProperty CreateDeclarationWith(Func<String, CssProperty> createProperty, ref CssToken token)
        {
            var property = default(CssProperty);

            var sb = Pool.NewStringBuilder();
            var start = token.Position;

            while (token.IsDeclarationName())
            {
                sb.Append(token.ToValue());
                token = NextToken();
            }

            var propertyName = sb.ToPool();

            if (propertyName.Length > 0)
            {
                property = _parser.Options.IsIncludingUnknownDeclarations || 
                           _parser.Options.IsToleratingInvalidValues ?
                    new CssUnknownProperty(propertyName) : createProperty(propertyName);

                if (property == null)
                {
                    RaiseErrorOccurred(CssParseError.UnknownDeclarationName, start);
                }
                else
                {
                    _nodes.Push(property);
                }

                CollectTrivia(ref token);

                if (token.Type == CssTokenType.Colon)
                {
                    var important = false;
                    var value = CreateValue(CssTokenType.CurlyBracketClose, ref token, out important);

                    if (value == null)
                    {
                        RaiseErrorOccurred(CssParseError.ValueMissing, token.Position);
                    }
                    else if (property != null && property.TrySetValue(value))
                    {
                        property.IsImportant = important;
                    }

                    CollectTrivia(ref token);
                }
                else
                {
                    RaiseErrorOccurred(CssParseError.ColonMissing, token.Position);
                }

                JumpToDeclEnd(ref token);

                if (property != null)
                {
                    _nodes.Pop();
                }
            }
            else if (token.Type != CssTokenType.EndOfFile)
            {
                RaiseErrorOccurred(CssParseError.IdentExpected, start);
                JumpToDeclEnd(ref token);
            }

            if (token.Type == CssTokenType.Semicolon)
            {
                token = NextToken();
            }

            return property;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public CssProperty CreateDeclaration(ref CssToken token)
        {
            CollectTrivia(ref token);
            return CreateDeclarationWith(Factory.Properties.Create, ref token);
        }

        /// <summary>
        /// Scans the current medium for the @media or @import rule.
        /// </summary>
        public CssMedium CreateMedium(ref CssToken token)
        {
            var medium = new CssMedium();
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.Ident)
            {
                var identifier = token.Data;

                if (identifier.Isi(Keywords.Not))
                {
                    medium.IsInverse = true;
                    token = NextToken();
                    CollectTrivia(ref token);
                }
                else if (identifier.Isi(Keywords.Only))
                {
                    medium.IsExclusive = true;
                    token = NextToken();
                    CollectTrivia(ref token);
                }
            }

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = token.Data;
                token = NextToken();
                CollectTrivia(ref token);

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(Keywords.And))
                {
                    return medium;
                }

                token = NextToken();
                CollectTrivia(ref token);
            }

            do
            {
                if (token.Type != CssTokenType.RoundBracketOpen)
                {
                    return null;
                }

                token = NextToken();
                CollectTrivia(ref token);
                var feature = CreateFeature(ref token);

                if (feature != null)
                {
                    medium.AppendChild(feature);
                }

                if (token.Type != CssTokenType.RoundBracketClose)
                {
                    return null;
                }

                token = NextToken();
                CollectTrivia(ref token);

                if (feature == null)
                {
                    return null;
                }

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(Keywords.And))
                {
                    break;
                }

                token = NextToken();
                CollectTrivia(ref token);
            }
            while (token.Type != CssTokenType.EndOfFile);

            return medium;
        }

        #endregion

        #region Helpers

        private void JumpToEnd(ref CssToken current)
        {
            while (current.IsNot(CssTokenType.EndOfFile, CssTokenType.Semicolon))
            {
                current = NextToken();
            }
        }

        private void JumpToRuleEnd(ref CssToken current)
        {
            var scopes = 0;

            while (current.Type != CssTokenType.EndOfFile)
            {
                if (current.Type == CssTokenType.CurlyBracketOpen)
                {
                    scopes++;
                }
                else if (current.Type == CssTokenType.CurlyBracketClose)
                {
                    scopes--;
                }
                
                if (scopes <= 0 && (current.Is(CssTokenType.CurlyBracketClose, CssTokenType.Semicolon)))
                {
                    break;
                }

                current = NextToken();
            }
        }

        private void JumpToArgEnd(ref CssToken current)
        {
            var arguments = 0;

            while (current.Type != CssTokenType.EndOfFile)
            {
                if (current.Type == CssTokenType.RoundBracketOpen)
                {
                    arguments++;
                }
                else if (arguments <= 0 && current.Type == CssTokenType.RoundBracketClose)
                {
                    break;
                }
                else if (current.Type == CssTokenType.RoundBracketClose)
                {
                    arguments--;
                }

                current = NextToken();
            }
        }

        private void JumpToDeclEnd(ref CssToken current)
        {
            var scopes = 0;

            while (current.Type != CssTokenType.EndOfFile)
            {
                if (current.Type == CssTokenType.CurlyBracketOpen)
                {
                    scopes++;
                }
                else if (scopes <= 0 && (current.Is(CssTokenType.CurlyBracketClose, CssTokenType.Semicolon)))
                {
                    break;
                }
                else if (current.Type == CssTokenType.CurlyBracketClose)
                {
                    scopes--;
                }

                current = NextToken();
            }
        }

        private CssToken NextToken()
        {
            return _tokenizer.Get();
        }

        private TextView CreateView(TextPosition start, TextPosition end)
        {
            var range = new TextRange(start, end);
            return new TextView(range, _tokenizer.Source);
        }

        private void CollectTrivia(ref CssToken token)
        {
            var storeComments = _parser.Options.IsStoringTrivia;

            while (token.Type == CssTokenType.Whitespace || token.Type == CssTokenType.Comment || token.Type == CssTokenType.Cdc || token.Type == CssTokenType.Cdo)
            {
                if (storeComments && token.Type == CssTokenType.Comment)
                {
                    var current = _nodes.Peek();
                    var comment = new CssComment(token.Data);
                    var start = token.Position;
                    var end = start.After(token.ToValue());
                    comment.SourceCode = CreateView(start, end);
                    current.AppendChild(comment);
                }

                token = _tokenizer.Get();
            }
        }

        private CssRule SkipDeclarations(CssToken token)
        {
            RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
            JumpToRuleEnd(ref token);
            return default(CssRule);
        }

        private void RaiseErrorOccurred(CssParseError code, TextPosition position)
        {
            _tokenizer.RaiseErrorOccurred(code, position);
        }

        #endregion

        #region Conditions

        private IConditionFunction AggregateCondition(ref CssToken token)
        {
            var condition = ExtractCondition(ref token);

            if (condition != null)
            {
                CollectTrivia(ref token);
                var conjunction = token.Data;
                var creator = conjunction.GetCreator();

                if (creator != null)
                {
                    token = NextToken();
                    CollectTrivia(ref token);
                    var conditions = MultipleConditions(condition, conjunction, ref token);
                    condition = creator.Invoke(conditions);
                }
            }

            return condition;
        }

        private IConditionFunction ExtractCondition(ref CssToken token)
        {
            if (token.Type == CssTokenType.RoundBracketOpen)
            {
                token = NextToken();
                CollectTrivia(ref token);
                var condition = AggregateCondition(ref token);

                if (condition != null)
                {
                    var group = new GroupCondition();
                    group.Content = condition;
                    condition = group;
                }
                else if (token.Type == CssTokenType.Ident)
                {
                    condition = DeclarationCondition(ref token);
                }

                if (token.Type == CssTokenType.RoundBracketClose)
                {
                    token = NextToken();
                    CollectTrivia(ref token);
                }

                return condition;
            }
            else if (token.Data.Isi(Keywords.Not))
            {
                var condition = new NotCondition();
                token = NextToken();
                CollectTrivia(ref token);
                condition.Content = ExtractCondition(ref token);
                return condition;
            }

            return null;
        }

        private IConditionFunction DeclarationCondition(ref CssToken token)
        {
            var property = Factory.Properties.Create(token.Data) ?? new CssUnknownProperty(token.Data);
            var declaration = default(DeclarationCondition);
            token = NextToken();
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.Colon)
            {
                var important = false;
                var result = CreateValue(CssTokenType.RoundBracketClose, ref token, out important);
                property.IsImportant = important;

                if (result != null)
                {
                    declaration = new DeclarationCondition(property, result);
                }
            }

            return declaration;
        }

        private List<IConditionFunction> MultipleConditions(IConditionFunction condition, String connector, ref CssToken token)
        {
            var list = new List<IConditionFunction>();
            CollectTrivia(ref token);
            list.Add(condition);

            while (token.Type != CssTokenType.EndOfFile)
            {
                condition = ExtractCondition(ref token);

                if (condition == null)
                {
                    break;
                }

                list.Add(condition);

                if (!token.Data.Isi(connector))
                {
                    break;
                }

                token = NextToken();
                CollectTrivia(ref token);
            }

            return list;
        }

        #endregion

        #region Fill Inner

        private void FillFunctions(Action<DocumentFunction> add, ref CssToken token)
        {
            do
            {
                var function = token.ToDocumentFunction();

                if (function == null)
                {
                    break;
                }

                token = NextToken();
                CollectTrivia(ref token);
                add(function);

                if (token.Type != CssTokenType.Comma)
                {
                    break;
                }

                token = NextToken();
                CollectTrivia(ref token);
            }
            while (token.Type != CssTokenType.EndOfFile);
        }

        private TextPosition FillKeyframeRules(CssKeyframesRule parentRule)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateKeyframeRule(token);
                token = NextToken();
                CollectTrivia(ref token);
                parentRule.Rules.Add(rule);
            }

            return token.Position;
        }

        private TextPosition FillDeclarations(CssDeclarationRule rule, Func<String, CssProperty> createProperty)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(createProperty, ref token);

                if (property != null && property.HasValue)
                {
                    rule.SetProperty(property);
                }

                CollectTrivia(ref token);
            }

            return token.Position;
        }

        private TextPosition FillRules(CssGroupingRule group)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateRule(token);
                token = NextToken();
                CollectTrivia(ref token);
                group.Rules.Add(rule);
            }

            return token.Position;
        }

        private void FillMediaList(MediaList list, CssTokenType end, ref CssToken token)
        {
            _nodes.Push(list);

            if (token.Type != end)
            {
                while (token.Type != CssTokenType.EndOfFile)
                {
                    var medium = CreateMedium(ref token);

                    if (medium != null)
                    {
                        list.AppendChild(medium);
                    }

                    if (token.Type != CssTokenType.Comma)
                    {
                        break;
                    }

                    token = NextToken();
                    CollectTrivia(ref token);
                }

                if (token.Type != end || list.Length == 0)
                {
                    list.Clear();
                    list.AppendChild(new CssMedium
                    {
                        IsInverse = true,
                        Type = Keywords.All
                    });
                }
            }

            _nodes.Pop();
        }

        #endregion

        #region Create Values

        private ISelector CreateSelector(ref CssToken token)
        {
            var selector = _parser.GetSelectorCreator();
            var start = token.Position;

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketOpen, CssTokenType.CurlyBracketClose))
            {
                selector.Apply(token);
                token = NextToken();
            }

            var selectorIsValid = selector.IsValid;
            var result = selector.ToPool();
            var node = result as CssNode;

            if (node != null)
            {
                var end = token.Position.Shift(-1);
                node.SourceCode = CreateView(start, end);
            }

            if (!selectorIsValid && !_parser.Options.IsToleratingInvalidValues)
            {
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);
                result = null;
            }

            return result;
        }

        private CssValue CreateValue(CssTokenType closing, ref CssToken token, out Boolean important)
        {
            var value = Pool.NewValueBuilder();
            _tokenizer.IsInValue = true;
            token = NextToken();
            var start = token.Position;

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.Semicolon, closing))
            {
                value.Apply(token);
                token = NextToken();
            }

            important = value.IsImportant;
            _tokenizer.IsInValue = false;
            var valueIsValid = value.IsValid;
            var result = value.ToPool();
            var node = result as CssNode;

            if (node != null)
            {
                var end = token.Position.Shift(-1);
                node.SourceCode = CreateView(start, end);
            }

            if (!valueIsValid && !_parser.Options.IsToleratingInvalidValues)
            {
                RaiseErrorOccurred(CssParseError.InvalidValue, start);
                result = null;
            }

            return result;
        }

        private String GetRuleName(ref CssToken token)
        {
            var name = String.Empty;

            if (token.Type == CssTokenType.Ident)
            {
                name = token.Data;
                token = NextToken();
            }

            return name;
        }

        private MediaFeature CreateFeature(ref CssToken token)
        {
            if (token.Type == CssTokenType.Ident)
            {
                var start = token.Position;
                var val = CssValue.Empty;
                var feature = _parser.Options.IsToleratingInvalidConstraints ?
                    new UnknownMediaFeature(token.Data) : Factory.MediaFeatures.Create(token.Data);

                token = NextToken();

                if (token.Type == CssTokenType.Colon)
                {
                    var value = Pool.NewValueBuilder();
                    token = NextToken();

                    while (token.IsNot(CssTokenType.RoundBracketClose, CssTokenType.EndOfFile) || !value.IsReady)
                    {
                        value.Apply(token);
                        token = NextToken();
                    }

                    val = value.ToPool();
                }
                else if (token.Type == CssTokenType.EndOfFile)
                {
                    return null;
                }

                if (feature != null && feature.TrySetValue(val))
                {
                    var node = feature as CssNode;

                    if (node != null)
                    {
                        var end = token.Position.Shift(-1);
                        node.SourceCode = CreateView(start, end);
                    }

                    return feature;
                }
            }
            else
            {
                JumpToArgEnd(ref token);
            }

            return null;
        }

        #endregion
    }
}
