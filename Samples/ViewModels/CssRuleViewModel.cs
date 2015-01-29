namespace Samples.ViewModels
{
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.ObjectModel;

    public class CssRuleViewModel : BaseViewModel
    {
        readonly ObservableCollection<CssRuleViewModel> children;
        readonly String typeName;
        readonly String name;

        private CssRuleViewModel(String typeName)
        {
            this.children = new ObservableCollection<CssRuleViewModel>();
            this.typeName = typeName;
        }

        private CssRuleViewModel(Object o)
            : this(o.GetType().Name)
        {
        }

        private CssRuleViewModel(String name, String value)
            : this(name, new PseudoValue(value))
        {
        }

        private CssRuleViewModel(String name, ICssValue value)
            : this("CSSProperty")
        {
            this.name = name;
            this.children.Add(new CssRuleViewModel(value));
        }

        public CssRuleViewModel(ICssRule rule)
            : this((Object)rule)
        {
            switch (rule.Type)
            {
                case CssRuleType.FontFace:
                    var font = (ICssFontFaceRule)rule;
                    name = "@font-face";
                    Populate(font);
                    break;

                case CssRuleType.Keyframe:
                    var keyframe = (ICssKeyframeRule)rule;
                    name = keyframe.KeyText;
                    Populate(keyframe.Style);
                    break;

                case CssRuleType.Keyframes:
                    var keyframes = (ICssKeyframesRule)rule;
                    name = "@keyframes " + keyframes.Name;
                    Populate(keyframes.Rules);
                    break;

                case CssRuleType.Media:
                    var media = (ICssMediaRule)rule;
                    name = "@media " + media.Media.MediaText;
                    Populate(media.Rules);
                    break;

                case CssRuleType.Page:
                    var page = (ICssPageRule)rule;
                    name = "@page " + page.SelectorText;
                    Populate(page.Style);
                    break;

                case CssRuleType.Style:
                    var style = (ICssStyleRule)rule;
                    name = style.SelectorText;
                    Populate(style.Style);
                    break;

                case CssRuleType.Supports:
                    var support = (ICssSupportsRule)rule;
                    name = "@supports " + support.ConditionText;
                    Populate(support.Rules);
                    break;

                default:
                    name = rule.CssText;
                    break;
            }
        }

        public CssRuleViewModel(ICssProperty declaration)
            : this(declaration.Name, declaration.Value)
        {
        }

        public CssRuleViewModel(ICssValue value)
            : this("CSSValue")
        {
            name = value.CssText;
        }

        void Populate(ICssFontFaceRule font)
        {
            AddIfNotEmpty("Family", font.Family);
            AddIfNotEmpty("Features", font.Features);
            AddIfNotEmpty("Range", font.Range);
            AddIfNotEmpty("Source", font.Source);
            AddIfNotEmpty("Stretch", font.Stretch);
            AddIfNotEmpty("Style", font.Style);
            AddIfNotEmpty("Variant", font.Variant);
            AddIfNotEmpty("Weight", font.Weight);
        }

        void AddIfNotEmpty(String name, String value)
        {
            if (!String.IsNullOrEmpty(value))
                children.Add(new CssRuleViewModel(name, value));
        }

        void Populate(ICssStyleDeclaration declarations)
        {
            foreach (var declaration in declarations)
                children.Add(new CssRuleViewModel(declaration));
        }

        void Populate(ICssRuleList rules)
        {
            foreach (var rule in rules)
                children.Add(new CssRuleViewModel(rule));
        }

        public String Name
        {
            get { return name; }
        }

        public String TypeName
        {
            get { return typeName; }
        }

        public ObservableCollection<CssRuleViewModel> Children
        {
            get { return children; }
        }

        class PseudoValue : ICssValue
        {
            public PseudoValue(String value)
            {
                CssText = value;
            }

            public CssValueType Type
            {
                get { return CssValueType.Custom; }
            }

            public String CssText
            {
                get;
                private set;
            }
        }
    }
}
