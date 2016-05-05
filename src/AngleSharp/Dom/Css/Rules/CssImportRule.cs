namespace AngleSharp.Dom.Css
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a CSS import rule.
    /// </summary>
    sealed class CssImportRule : CssRule, ICssImportRule
    {
        #region Fields

        String _href;
        CssStyleSheet _styleSheet;

        #endregion

        #region ctor

        internal CssImportRule(CssParser parser)
            : base(CssRuleType.Import, parser)
        {
            AppendChild(new MediaList(parser));
        }

        #endregion

        #region Properties

        public String Href
        {
            get { return _href; }
            set { _href = value; }
        }

        public MediaList Media
        {
            get { return Children.OfType<MediaList>().FirstOrDefault(); }
        }

        IMediaList ICssImportRule.Media
        {
            get { return Media; }
        }

        public ICssStyleSheet Sheet
        {
            get { return _styleSheet; }
        }

        #endregion

        #region Internal Methods

        internal async Task LoadStylesheetFromAsync(Document document)
        {
            if (document != null)
            {
                var loader = document.Loader;
                var baseUrl = Url.Create(Owner.Href ?? document.BaseUri);
                var url = new Url(baseUrl, _href);

                if (!IsRecursion(url) && loader != null)
                {
                    var element = Owner.OwnerNode;
                    var request = element.CreateRequestFor(url);
                    var download = loader.DownloadAsync(request);

                    using (var response = await download.Task.ConfigureAwait(false))
                    {
                        var sheet = new CssStyleSheet(this, response.Address.Href);
                        var source = new TextSource(response.Content);
                        _styleSheet = await Parser.ParseStylesheetAsync(sheet, source).ConfigureAwait(false);
                    }
                }
            }
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssImportRule;
            _href = newRule._href;
            _styleSheet = null;
            //TODO Load New StyleSheet
            base.ReplaceWith(rule);
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var media = Media.MediaText;
            var space = String.IsNullOrEmpty(media) ? String.Empty : " ";
            var value = String.Concat(_href.CssUrl(), space, media);
            writer.Write(formatter.Rule("@import", value));
        }

        #endregion

        #region Helpers

        Boolean IsRecursion(Url url)
        {
            var href = url.Href;
            var owner = Owner;

            while (owner != null && !owner.Href.Is(href))
            {
                owner = owner.Parent;
            }

            return owner != null;
        }

        #endregion
    }
}
