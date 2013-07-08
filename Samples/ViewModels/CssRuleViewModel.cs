using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
    public class CssRuleViewModel : BaseViewModel
    {
        ObservableCollection<CssRuleViewModel> children;
        String name;
        String typeName;

        public CssRuleViewModel(CSSRule rule)
        {
            Init(rule);

            switch (rule.Type)
            {
                case CssRuleType.FontFace:
                    var font = (CSSFontFaceRule)rule;
                    name = "@font-face";
                    Populate(font.CssRules);
                    break;

                case CssRuleType.Keyframe:
                    var keyframe = (CSSKeyframeRule)rule;
                    name = keyframe.KeyText;
                    Populate(keyframe.Style);
                    break;

                case CssRuleType.Keyframes:
                    var keyframes = (CSSKeyframesRule)rule;
                    name = "@keyframes " + keyframes.Name;
                    Populate(keyframes.CssRules);
                    break;

                case CssRuleType.Media:
                    var media = (CSSMediaRule)rule;
                    name = "@media " + media.ConditionText;
                    Populate(media.CssRules);
                    break;

                case CssRuleType.Page:
                    var page = (CSSPageRule)rule;
                    name = "@page " + page.SelectorText;
                    Populate(page.Style);
                    break;

                case CssRuleType.Style:
                    var style = (CSSStyleRule)rule;
                    name = style.SelectorText;
                    Populate(style.Style);
                    break;

                case CssRuleType.Supports:
                    var support = (CSSSupportsRule)rule;
                    name = "@supports " + support.ConditionText;
                    Populate(support.CssRules);
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

        void Populate(CSSRuleList rules)
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
