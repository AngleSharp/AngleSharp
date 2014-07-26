using AngleSharp.DOM.Css;
using System;
using System.Collections.ObjectModel;

namespace Samples.ViewModels
{
    public class CssRuleViewModel : BaseViewModel
    {
        ObservableCollection<CssRuleViewModel> children;
        String name;
        String typeName;

        public CssRuleViewModel(ICssRule rule)
        {
            Init(rule);

            switch (rule.Type)
            {
                case CssRuleType.FontFace:
                    var font = (ICssFontFaceRule)rule;
                    name = "@font-face";
                    //How to populate ?
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

        public CssRuleViewModel(CSSProperty declaration)
        {
            Init(declaration);
            name = declaration.Name;
            children.Add(new CssRuleViewModel(declaration.Value));
        }

        public CssRuleViewModel(CSSValue value)
        {
            Init(value);
            name = value.CssText;
        }

        void Init(Object o)
        {
            children = new ObservableCollection<CssRuleViewModel>();
            typeName = o.GetType().Name;
        }

        void Populate(CSSStyleDeclaration declarations)
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
            set { name = value; }
        }

        public String TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        public ObservableCollection<CssRuleViewModel> Children
        {
            get { return children; }
        }
    }
}
