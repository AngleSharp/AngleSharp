namespace AngleSharp.Dom
{
    using AngleSharp.Html.Dom;
    using System;

    // Allocated only for documents that have created or adopted an HTML base.
    internal sealed class DocumentBaseUrl
    {
        private readonly Document _document;
        private Boolean _baseUrlInitialized;
        private HtmlBaseElement? _activeBaseElement;
        private Url? _frozenBaseUrl;

        internal DocumentBaseUrl(Document document)
        {
            _document = document;
        }

        internal Url Get()
        {
            if (!_baseUrlInitialized)
            {
                Refresh();
            }

            return _frozenBaseUrl ?? _document.FallbackBaseUrl;
        }

        // Construction uses SetupElement; DOM insertion/removal calls this after
        // changing the tree, even when an enclosing operation suppresses records.
        internal void RefreshForSubtree(Node subtree)
        {
            if (subtree is not HtmlBaseElement && !subtree.HasChildNodes)
            {
                return;
            }

            var containsBase = false;

            foreach (var node in subtree.GetDescendantsAndSelf())
            {
                if (node is HtmlBaseElement element)
                {
                    element.ActivateInDom();
                    containsBase = true;
                }
            }

            if (containsBase)
            {
                Refresh();
            }
        }

        internal void Refresh(HtmlBaseElement? changed = null)
        {
            HtmlBaseElement? first = null;

            foreach (var node in _document.GetDescendants())
            {
                if (node is HtmlBaseElement element && element.HasAttribute(AttributeNames.Href) &&
                    !element.IsInertTemplateBase && !IsStagedTemplateContent(element))
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
                var fallback = _document.FallbackBaseUrl;
                var candidate = new Url(fallback, first.GetAttribute(AttributeNames.Href)!);
                // Invalid and forbidden bases freeze the fallback too. Keep a
                // separate URL record so history cannot mutate the frozen value.
                _frozenBaseUrl = candidate.IsInvalid || candidate.Scheme == "data" || candidate.Scheme == "javascript"
                    ? new Url(fallback.Href)
                    : candidate;
            }
        }

        private static Boolean IsStagedTemplateContent(Node node)
        {
            // Template.InnerHtml uses ReplaceAll before PopulateFragment. Its
            // temporary children cannot displace and re-freeze an existing base.
            for (var parent = node.Parent; parent is not null; parent = parent.Parent)
            {
                if (parent is HtmlTemplateElement template && template.IsStagingContent)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
