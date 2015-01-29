namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using System.Collections;
    using System.Collections.Generic;

    sealed class StyleCollection : IEnumerable<CssStyleRule>
    {
        #region Fields

        readonly IEnumerable<CssStyleSheet> _sheets;
        readonly RenderDevice _device;

        #endregion

        #region ctor

        public StyleCollection(IEnumerable<CssStyleSheet> sheets, RenderDevice device)
        {
            _sheets = sheets;
            _device = device;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the responsible device.
        /// </summary>
        public RenderDevice Device
        {
            get { return _device; }
        }

        #endregion

        #region Methods

        public IEnumerator<CssStyleRule> GetEnumerator()
        {
            foreach (var sheet in _sheets)
            {
                if (sheet.IsDisabled == false && sheet.Media.Validate(_device))
                {
                    var rules = GetRules(sheet.Rules);

                    foreach (var rule in rules)
                        yield return rule;
                }
            }
        }

        #endregion

        #region Helpers

        IEnumerable<CssStyleRule> GetRules(CssRuleList rules)
        {
            foreach (var rule in rules)
            {
                if (rule.Type == CssRuleType.Media)
                {
                    var media = (CssMediaRule)rule;

                    if (media.IsValid(_device) == false)
                        continue;

                    var subrules = GetRules(media.Rules);

                    foreach (var subrule in subrules)
                        yield return subrule;
                }
                else if (rule.Type == CssRuleType.Supports)
                {
                    var support = (CssSupportsRule)rule;

                    if (support.IsSupported == false)
                        continue;

                    var subrules = GetRules(support.Rules);

                    foreach (var subrule in subrules)
                        yield return subrule;
                }
                else if (rule.Type == CssRuleType.Style)
                {
                    yield return (CssStyleRule)rule;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
