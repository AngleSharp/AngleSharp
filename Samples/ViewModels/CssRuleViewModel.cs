using AngleSharp.DOM.Css;
using System;
using System.Collections.ObjectModel;

namespace Samples.ViewModels
{
    public class CssRuleViewModel : BaseViewModel
    {
        readonly ObservableCollection<CssRuleViewModel> children;
        readonly String typeName;
        readonly String name;

        private CssRuleViewModel(Object o)
        {
            children = new ObservableCollection<CssRuleViewModel>();
            typeName = o.GetType().Name;
        }

        public CssRuleViewModel(ICssRule rule)
            : this((Object)rule)
        {
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

        public CssRuleViewModel(ICssProperty declaration)
            : this((Object)declaration)
        {
            name = declaration.Name;
            children.Add(new CssRuleViewModel(declaration.Value));
        }

        public CssRuleViewModel(ICssValue value)
            : this((Object)value)
        {
            name = value.CssText;
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
    }
}
