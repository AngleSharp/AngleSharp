namespace AngleSharp.Dom
{
    using AngleSharp.Html.Dom;
    using System;

    public abstract partial class Document
    {
        private Boolean _hasBaseElements;
        private Boolean _baseUrlInitialized;
        private HtmlBaseElement? _activeBaseElement;
        private Url? _frozenBaseUrl;

        internal Url FallbackBaseUrl => BaseUrlOverride ?? DocumentUrl;

        internal void RegisterBaseElement() => _hasBaseElements = true;

        internal Url GetDocumentBaseUrl()
        {
            if (!_baseUrlInitialized && _hasBaseElements)
            {
                RefreshBaseUrl();
            }

            return _frozenBaseUrl ?? FallbackBaseUrl;
        }

        // Construction uses SetupElement; DOM insertion/removal calls this after
        // changing the tree, even when an enclosing operation suppresses records.
        internal void RefreshBaseUrlForSubtree(Node subtree)
        {
            if (!_hasBaseElements)
            {
                return;
            }

            if (subtree is not HtmlBaseElement && !subtree.HasChildNodes)
            {
                return;
            }

            foreach (var node in subtree.GetDescendantsAndSelf())
            {
                if (node is HtmlBaseElement)
                {
                    RefreshBaseUrl();
                    return;
                }
            }
        }

        internal void RefreshBaseUrl(HtmlBaseElement? changed = null)
        {
            HtmlBaseElement? first = null;

            foreach (var node in this.GetDescendants())
            {
                if (node is HtmlBaseElement element && element.HasAttribute(AttributeNames.Href))
                {
                    first = element;
                    break;
                }
            }

            _baseUrlInitialized = true;

            if (ReferenceEquals(first, _activeBaseElement) && (first is null || !ReferenceEquals(first, changed)))
            {
                return;
            }

            _activeBaseElement = first;
            _frozenBaseUrl = null;

            if (first is not null)
            {
                var fallback = FallbackBaseUrl;
                var candidate = new Url(fallback, first.GetAttribute(AttributeNames.Href)!);
                // Invalid and forbidden bases freeze the fallback too. Keep a
                // separate URL record so history cannot mutate the frozen value.
                _frozenBaseUrl = candidate.IsInvalid || candidate.Scheme == "data" || candidate.Scheme == "javascript"
                    ? new Url(fallback.Href)
                    : candidate;
            }
        }
    }
}
