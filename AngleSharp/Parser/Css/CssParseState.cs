namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css.MediaFeatures;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using System;

    abstract class CssParseState
    {
        #region Fields

        protected readonly CssTokenizer _tokenizer;
        protected readonly CssParserOptions _options;

        #endregion

        #region ctor

        public CssParseState(CssTokenizer tokenizer, CssParserOptions options)
        {
            _tokenizer = tokenizer;
            _options = options;
        }

        #endregion

        #region API

        public abstract CssRule Create(CssToken current);

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        public void FillDeclarations(CssStyleDeclaration style)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclaration(ref token);

                if (property != null && property.HasValue)
                    style.SetProperty(property);
            }
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public CssProperty CreateDeclaration(ref CssToken token)
        {
            var property = default(CssProperty);

            if (token.Type == CssTokenType.Ident)
            {
                var propertyName = token.Data;
                var unknown = false;
                token = _tokenizer.Get();

                if (token.Type != CssTokenType.Colon)
                {
                    RaiseErrorOccurred(CssParseError.ColonMissing, token);
                }
                else
                {
                    property = Factory.Properties.Create(propertyName);

                    if (property == null)
                    {
                        RaiseErrorOccurred(CssParseError.UnknownDeclarationName, token);
                        unknown = true;
                        property = new CssUnknownProperty(propertyName);
                    }

                    var important = false;
                    var val = CreateValue(ref token, out important);

                    if (val == null)
                        RaiseErrorOccurred(CssParseError.ValueMissing, token);
                    else if (property.TrySetValue(val))
                        property.IsImportant = important;
                    else if (_options.IsToleratingInvalidValues)
                        (property = new CssUnknownProperty(propertyName)).TrySetValue(val);

                    if (unknown && _options.IsIncludingUnknownDeclarations == false)
                        property = null;
                }

                _tokenizer.JumpToEndOfDeclaration();
                token = _tokenizer.Get();

                if (token.Type == CssTokenType.Semicolon)
                    token = _tokenizer.Get();
            }
            else if (token.Type != CssTokenType.Eof)
            {
                RaiseErrorOccurred(CssParseError.IdentExpected, token);
                token = _tokenizer.Get();
            }

            return property;
        }

        /// <summary>
        /// Scans the current medium for the @media or @import rule.
        /// </summary>
        public CssMedium CreateMedium(ref CssToken token)
        {
            var medium = new CssMedium();

            if (token.Type == CssTokenType.Ident)
            {
                var identifier = token.Data;

                if (identifier.Equals(Keywords.Not, StringComparison.OrdinalIgnoreCase))
                {
                    medium.IsInverse = true;
                    token = _tokenizer.Get();
                }
                else if (identifier.Equals(Keywords.Only, StringComparison.OrdinalIgnoreCase))
                {
                    medium.IsExclusive = true;
                    token = _tokenizer.Get();
                }
            }

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = token.Data;
                token = _tokenizer.Get();

                if (token.Type != CssTokenType.Ident || String.Compare(token.Data, Keywords.And, StringComparison.OrdinalIgnoreCase) != 0)
                    return medium;

                token = _tokenizer.Get();
            }

            do
            {
                if (token.Type != CssTokenType.RoundBracketOpen)
                    return null;

                token = _tokenizer.Get();
                var couldSetConstraint = TrySetConstraint(medium, ref token);

                if (token.Type != CssTokenType.RoundBracketClose)
                    return null;

                token = _tokenizer.Get();

                if (couldSetConstraint == false)
                    return null;

                if (token.Type != CssTokenType.Ident || String.Compare(token.Data, Keywords.And, StringComparison.OrdinalIgnoreCase) != 0)
                    break;

                token = _tokenizer.Get();
            }
            while (token.Type != CssTokenType.Eof);

            return medium;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Skips the current declaration.
        /// </summary>
        protected CssRule SkipDeclarations(CssToken token)
        {
            RaiseErrorOccurred(CssParseError.InvalidToken, token);
            _tokenizer.SkipUnknownRule();
            return null;
        }

        /// <summary>
        /// Fills the given parent rule with rules given by the tokens.
        /// </summary>
        protected void FillRules(CssGroupingRule group)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var rule = _tokenizer.CreateRule(token, _options);
                group.AddRule(rule);
                token = _tokenizer.Get();
            }
        }

        /// <summary>
        /// State that is called once we are in a CSS selector.
        /// </summary>
        protected ISelector CreateSelector(ref CssToken token)
        {
            var selector = Pool.NewSelectorConstructor();
            _tokenizer.State = CssParseMode.Selector;
            var start = token;

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketOpen, CssTokenType.CurlyBracketClose))
            {
                selector.Apply(token);
                token = _tokenizer.Get();
            }

            if (selector.IsValid == false)
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);

            _tokenizer.State = CssParseMode.Data;
            return selector.ToPool();
        }

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        protected CssValue CreateValue(ref CssToken token, out Boolean important)
        {
            var value = Pool.NewValueBuilder();
            _tokenizer.State = CssParseMode.Value;
            token = _tokenizer.Get();

            while (token.Type != CssTokenType.Eof)
            {
                if (token.Is(CssTokenType.Semicolon, CssTokenType.CurlyBracketClose) ||
                   (token.Type == CssTokenType.RoundBracketClose && value.IsReady))
                    break;

                value.Apply(token);
                token = _tokenizer.Get();
            }

            important = value.IsImportant;
            _tokenizer.State = CssParseMode.Data;
            return value.ToPool();
        }

        /// <summary>
        /// Before the name of a rule has been detected.
        /// </summary>
        protected String GetRuleName(ref CssToken token)
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
        protected MediaList CreateMediaList(ref CssToken token)
        {
            var list = new MediaList();

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                while (token.Type != CssTokenType.Eof)
                {
                    var medium = CreateMedium(ref token);

                    if (medium != null)
                        list.Add(medium);

                    if (token.Type != CssTokenType.Comma)
                        break;

                    token = _tokenizer.Get();
                }

                if (token.Type != CssTokenType.CurlyBracketOpen)
                {
                    do
                    {
                        if (token.Type == CssTokenType.Eof || token.Type == CssTokenType.Semicolon)
                            break;

                        token = _tokenizer.Get();
                    }
                    while (token.Type != CssTokenType.CurlyBracketOpen);

                    list.Clear();
                }

                if (list.Length == 0)
                {
                    list.Add(new CssMedium
                    {
                        IsInverse = true,
                        Type = Keywords.All
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        /// <param name="token">The associated token.</param>
        protected void RaiseErrorOccurred(CssParseError code, CssToken token)
        {
            _tokenizer.RaiseErrorOccurred(code, token.Position);
        }

        #endregion

        #region Helpers

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
            token = _tokenizer.Get();

            if (token.Type == CssTokenType.Colon)
            {
                _tokenizer.State = CssParseMode.Value;
                token = _tokenizer.Get();

                while (token.Type != CssTokenType.RoundBracketClose || value.IsReady == false)
                {
                    if (token.Type == CssTokenType.Eof)
                        break;

                    value.Apply(token);
                    token = _tokenizer.Get();
                }

                _tokenizer.State = CssParseMode.Data;

                var val = value.ToPool();
                var feature = Factory.MediaFeatures.Create(featureName);

                if (feature == null || !feature.TrySetValue(val))
                {
                    if (_options.IsToleratingInvalidConstraints == false)
                        return false;

                    feature = new UnknownMediaFeature(featureName);
                    feature.TrySetValue(val);
                }

                medium.AddConstraint(feature);
                return true;
            }

            return token.Type != CssTokenType.Eof;
        }

        #endregion
    }
}
