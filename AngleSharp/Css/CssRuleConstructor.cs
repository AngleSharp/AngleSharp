using System;
using System.Collections.Generic;
using AngleSharp.DOM;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Collections;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// Class for construction of CSS rules.
    /// </summary>
    class CssRuleConstructor
    {
        #region Members

        CssParser _parser;

        #endregion

        #region ctor

        private CssRuleConstructor(CssParser parser)
        {
            _parser = parser;
        }

        #endregion

        #region Methods

        CSSRule Investigate(CssRule baseRule)
        {
            var result = Create(baseRule);

            if(result == null)
                throw new DOMException(ErrorCode.SyntaxError);

            return result;
        }

        CSSRule Create(CssRule baseRule)
        {
            if (baseRule is CssQualifiedRule)
                return Create((CssQualifiedRule)baseRule);
            else if (baseRule is CssDeclaration)
                return Create((CssDeclaration)baseRule);
            else if (baseRule is CssAtRule)
                return Create((CssAtRule)baseRule);
            
            return null;
        }

        CSSRule Create(CssDeclaration rule)
        {
            var style = new CSSStyleRule();
            var property = CreateProperty(rule);
            style.Style.AppendProperty(property);
            return style;
        }

        CSSRule Create(CssQualifiedRule rule)
        {
            var stylerule = new CSSStyleRule();
            stylerule.Selector = CssSelectorConstructor.Create(rule.Prelude);

            for (int i = 0; i < rule.Value.Count; i++)
            {
                if(rule.Value[i] is CssDeclaration)
                {
                    var decl = (CssDeclaration)rule.Value[i];
                    var property = CreateProperty(decl);
                    stylerule.Style.AppendProperty(property);
                }
            }

            return stylerule;
        }

        CSSRule Create(CssAtRule rule)
        {
            switch (rule.Name)
            {
                case "media":
                    return CreateMediaRule(rule);

                case "page":
                    return CreatePageRule(rule);

                case "import":
                    return CreateImportRule(rule);

                case "font-face":
                    return CreateFontFaceRule(rule);

                case "charset":
                    return CreateCharsetRule(rule);

                case "namespace":
                    return CreateNamespaceRule(rule);

                case "supports":
                    return CreateSupportsRule(rule);

                case "keyframes":
                    return CreateKeyframesRule(rule);

                default:
                    return null;
            }
        }

        #endregion

        #region Helpers

        CSSProperty CreateProperty(CssDeclaration rule)
        {
            var property = CSSProperty.Factory(rule.Name);
            property.Important = rule.Important;
            property.Value = CSSValue.Create(rule.Value);
            return property;
        }

        void ModifyMediaList(MediaList media, List<CssToken> tokens)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == CssTokenType.Whitespace)
                    continue;

                for (; i < tokens.Count; i++)
                {
                    if (tokens[i].Type == CssTokenType.Comma)
                        break;

                    sb.Append(tokens[i].ToValue());
                }

                media.AppendMedium(sb.ToString());
                sb.Clear();
            }
        }

        CSSRule CreateKeyframesRule(CssAtRule rule)
        {
            var keyframes = new CSSKeyframesRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null && rule.Prelude[i].Preserved.Type == CssTokenType.Ident)
                {
                    keyframes.Name = rule.Prelude[i].Preserved.ToValue();
                    break;
                }
            }
            
            //rule.Value.Tokens

            //TODO
            //@keyframes IDENTIFIER { ... }
            //where ... is a reptition of a list of component values and { a list of declarations }
            //this will insert a list of @keyframe

            return keyframes;
        }

        CSSRule CreateSupportsRule(CssAtRule rule)
        {
            var support = new CSSSupportsRule();
            var sb = new StringBuilder();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null)
                    sb.Append(rule.Prelude[i].ToString());
            }

            support.ConditionText = sb.ToString();
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var rules = _parser.ConsumeRules(it);

            for (int i = 0; i < rules.Count; i++)
                support.CssRules.InsertAt(i, Create(_parser, rules[i]));

            return support;
        }

        CSSRule CreateNamespaceRule(CssAtRule rule)
        {
            var ns = new CSSNamespaceRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved == null)
                    continue;

                if (rule.Prelude[i].Preserved.Type == CssTokenType.Ident)
                    ns.Prefix = rule.Prelude[i].Preserved.ToValue();
                else if (rule.Prelude[i].Preserved.Type == CssTokenType.String)
                    ns.NamespaceURI = rule.Prelude[i].Preserved.ToValue();
            }

            return ns;
        }

        CSSRule CreateCharsetRule(CssAtRule rule)
        {
            var charset = new CSSCharsetRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null && rule.Prelude[i].Preserved.Type == CssTokenType.String)
                {
                    charset.Encoding = ((CssStringToken)rule.Prelude[i].Preserved).Data;
                    break;
                }
            }

            return charset;
        }

        CSSRule CreateFontFaceRule(CssAtRule rule)
        {
            var fontface = new CSSFontFaceRule();
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var decls = _parser.ConsumeDeclarations(it);

            foreach (var decl in decls)
            {
                if (decl is CssDeclaration)
                    fontface.AppendRule(CreateProperty((CssDeclaration)decl));
            }

            return fontface;
        }

        CSSRule CreateImportRule(CssAtRule rule)
        {
            var import = new CSSImportRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved == null)
                    continue;
                
                switch(rule.Prelude[i].Preserved.Type)
                {
                    case CssTokenType.String:
                    case CssTokenType.Url:
                        import.Href = ((CssStringToken)rule.Prelude[i].Preserved).Data;

                        if (++i < rule.Prelude.Count)
                        {
                            var tokens = new List<CssToken>();

                            for (; i < rule.Prelude.Count; i++)
                            {
                                if (rule.Prelude[i].Preserved != null)
                                    tokens.Add(rule.Prelude[i].Preserved);
                            }

                            ModifyMediaList(import.Media, tokens);
                        }
                        
                        break;
                }
            }

            return import;
        }

        CSSRule CreatePageRule(CssAtRule rule)
        {
            var page = new CSSPageRule();
            var selector = string.Empty;

            for (int i = 0; i < rule.Prelude.Count; i++)
                selector += rule.Prelude[i].ToString();

            page.SelectorText = selector;
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var decls = _parser.ConsumeDeclarations(it);

            foreach (var decl in decls)
            {
                if (decl is CssDeclaration)
                    page.AppendRule(CreateProperty((CssDeclaration)decl));
            }

            return page;
        }

        CSSRule CreateMediaRule(CssAtRule rule)
        {
            var media = new CSSMediaRule();
            var tokens = new List<CssToken>();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null)
                    tokens.Add(rule.Prelude[i].Preserved);
            }

            ModifyMediaList(media.Media, tokens);
            tokens.Clear();
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var rules = _parser.ConsumeRules(it);

            for (int i = 0; i < rules.Count; i++)
                media.CssRules.InsertAt(i, Create(rules[i]));

            return media;
        }

        #endregion

        #region Creators

        internal static CSSRule Create(CssParser parser, CssAtRule atrule)
        {
            var ctor = new CssRuleConstructor(parser);
            return ctor.Create(atrule);
        }

        internal static CSSRule Create(CssParser parser, CssQualifiedRule qrule)
        {
            var ctor = new CssRuleConstructor(parser);
            return ctor.Create(qrule);
        }

        internal static CSSRule Create(CssParser parser, CssDeclaration declaration)
        {
            var ctor = new CssRuleConstructor(parser);
            return ctor.Create(declaration);
        }

        internal static CSSRule Create(CssParser parser, CssRule baseRule)
        {
            var ctor = new CssRuleConstructor(parser);
            return ctor.Investigate(baseRule);
        }

        #endregion

        #region Factory

        internal static CssFillType GetFillType(string type)
        {
            switch (type)
            {
                case "media":
                case "supports":
                case "keyframes":
                    return CssFillType.Rule;

                case "page":
                case "font-face":
                case "keyframe":
                    return CssFillType.Declaration;

                case "charset":
                case "namespace":
                case "import":
                default:
                    return CssFillType.None;
            }
        }

        #endregion
    }
}
