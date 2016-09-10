namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Mathml;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the Tree construction as specified in
    /// 8.2.5 Tree construction, on the following page:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    sealed class HtmlDomBuilder
    {
        #region Fields

        private readonly HtmlTokenizer _tokenizer;
        private readonly HtmlDocument _document;
        private readonly List<Element> _openElements;
        private readonly List<Element> _formattingElements;
        private readonly Stack<HtmlTreeMode> _templateModes;
        private readonly IElementFactory<HtmlElement> _htmlFactory;
        private readonly IElementFactory<MathElement> _mathFactory;
        private readonly IElementFactory<SvgElement> _svgFactory;

        private HtmlFormElement _currentFormElement;
        private HtmlTreeMode _currentMode;
        private HtmlTreeMode _previousMode;
        private HtmlParserOptions _options;
        private Element _fragmentContext;
        private Boolean _foster;
        private Boolean _frameset;
        private Task _waiting;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the HTML parser with the specified
        /// document based on the given source manager.
        /// </summary>
        /// <param name="document">
        /// The document instance to be constructed.
        /// </param>
        public HtmlDomBuilder(HtmlDocument document)
        {
            var options = document.Options;
            var context = document.Context;
            var resolver = options.GetProvider<IEntityProvider>() ?? HtmlEntityService.Resolver;
            _tokenizer = new HtmlTokenizer(document.Source, resolver);
            _tokenizer.Error += (_, error) => context.Fire(error);
            _document = document;
            _openElements = new List<Element>();
            _templateModes = new Stack<HtmlTreeMode>();
            _formattingElements = new List<Element>();
            _frameset = true;
            _currentMode = HtmlTreeMode.Initial;
            _htmlFactory = options.GetFactory<IElementFactory<HtmlElement>>();
            _mathFactory = options.GetFactory<IElementFactory<MathElement>>();
            _svgFactory = options.GetFactory<IElementFactory<SvgElement>>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the tree builder has been created for parsing fragments.
        /// </summary>
        public Boolean IsFragmentCase
        {
            get { return _fragmentContext != null; }
        }

        /// <summary>
        /// Gets the adjusted current node.
        /// </summary>
        public Element AdjustedCurrentNode
        {
            get { return (_fragmentContext != null && _openElements.Count == 1) ? _fragmentContext : CurrentNode; }
        }

        /// <summary>
        /// Gets the current node.
        /// </summary>
        public Element CurrentNode
        {
            get { return _openElements.Count > 0 ? _openElements[_openElements.Count - 1] : null; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        /// <param name="cancelToken">The cancellation token to use.</param>
        public async Task<HtmlDocument> ParseAsync(HtmlParserOptions options, CancellationToken cancelToken)
        {
            var source = _document.Source;
            var token = default(HtmlToken);
            _tokenizer.IsStrictMode = options.IsStrictMode;
            _options = options;

            do
            {
                if (source.Length - source.Index < 1024)
                {
                    await source.PrefetchAsync(8192, cancelToken).ConfigureAwait(false);
                }

                token = _tokenizer.Get();
                Consume(token);

                if (_waiting != null)
                {
                    await _waiting.ConfigureAwait(false);
                    _waiting = null;
                }
            }
            while (token.Type != HtmlTokenType.EndOfFile);

            return _document;
        }

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        public HtmlDocument Parse(HtmlParserOptions options)
        {
            var token = default(HtmlToken);
            _tokenizer.IsStrictMode = options.IsStrictMode;
            _options = options;

            do
            {
                token = _tokenizer.Get();
                Consume(token);
                _waiting?.Wait();
                _waiting = null;
            }
            while (token.Type != HtmlTokenType.EndOfFile);

            return _document;
        }

        /// <summary>
        /// Switches to the fragment algorithm with the specified context
        /// element. Then parses the given source and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        /// <param name="context">
        /// The context element where the algorithm is applied to.
        /// </param>
        public HtmlDocument ParseFragment(HtmlParserOptions options, Element context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _fragmentContext = context;
            var tagName = context.LocalName;

            if (tagName.IsOneOf(TagNames.Title, TagNames.Textarea))
            {
                _tokenizer.State = HtmlParseMode.RCData;
            }
            else if (tagName.IsOneOf(TagNames.Style, TagNames.Xmp, TagNames.Iframe, TagNames.NoEmbed, TagNames.NoFrames))
            {
                _tokenizer.State = HtmlParseMode.Rawtext;
            }
            else if (tagName.Is(TagNames.Script))
            {
                _tokenizer.State = HtmlParseMode.Script;
            }
            else if (tagName.Is(TagNames.Plaintext))
            {
                _tokenizer.State = HtmlParseMode.Plaintext;
            }
            else if (tagName.Is(TagNames.NoScript) && options.IsScripting)
            {
                _tokenizer.State = HtmlParseMode.Rawtext;
            }

            var root = new HtmlHtmlElement(_document);
            _document.AddNode(root);
            _openElements.Add(root);

            if (context is HtmlTemplateElement)
            {
                _templateModes.Push(HtmlTreeMode.InTemplate);
            }

            Reset();

            _tokenizer.IsAcceptingCharacterData = (this.AdjustedCurrentNode.Flags & NodeFlags.HtmlMember) != NodeFlags.HtmlMember;

            do
            {
                if (context is HtmlFormElement)
                {
                    _currentFormElement = (HtmlFormElement)context;
                    break;
                }

                context = context.ParentElement as Element;
            }
            while (context != null);

            return Parse(options);
        }

        /// <summary>
        /// Restarts the parser by resetting the internal state.
        /// </summary>
        private void Restart()
        {
            _currentMode = HtmlTreeMode.Initial;
            _tokenizer.State = HtmlParseMode.PCData;
            _document.ReplaceAll(null, true);
            _frameset = true;
            _openElements.Clear();
            _formattingElements.Clear();
            _templateModes.Clear();
        }

        /// <summary>
        /// Resets the current insertation mode to the rules according to the
        /// algorithm specified in 8.2.3.1 The insertion mode.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#the-insertion-mode
        /// </summary>
        private void Reset()
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var element = _openElements[i];
                var last = i == 0;

                if (last && _fragmentContext != null)
                {
                    element = _fragmentContext;
                }

                var mode = element.SelectMode(last, _templateModes);

                if (mode.HasValue)
                {
                    _currentMode = mode.Value;
                    break;
                }
            }
        }

        /// <summary>
        /// Consumes a token and processes it.
        /// </summary>
        /// <param name="token">The token to consume.</param>
        private void Consume(HtmlToken token)
        {
            var node = AdjustedCurrentNode;

            if (node == null || token.Type == HtmlTokenType.EndOfFile || ((node.Flags & NodeFlags.HtmlMember) == NodeFlags.HtmlMember) ||
                (((node.Flags & NodeFlags.HtmlTip) == NodeFlags.HtmlTip) && token.IsHtmlCompatible) ||
                (((node.Flags & NodeFlags.MathTip) == NodeFlags.MathTip) && token.IsMathCompatible) ||
                (((node.Flags & NodeFlags.MathMember) == NodeFlags.MathMember) && token.IsSvg && node.LocalName.Is(TagNames.AnnotationXml)))
            {
                Home(token);
            }
            else
            {
                Foreign(token);
            }
        }

        #endregion

        #region Home

        /// <summary>
        /// Takes the method corresponding to the current insertation mode.
        /// </summary>
        /// <param name="token">The token to insert / use.</param>
        private void Home(HtmlToken token)
        {
            switch (_currentMode)
            {
                case HtmlTreeMode.Initial:
                    Initial(token);
                    return;

                case HtmlTreeMode.BeforeHtml:
                    BeforeHtml(token);
                    return;

                case HtmlTreeMode.BeforeHead:
                    BeforeHead(token);
                    return;

                case HtmlTreeMode.InHead:
                    InHead(token);
                    return;

                case HtmlTreeMode.InHeadNoScript:
                    InHeadNoScript(token);
                    return;

                case HtmlTreeMode.AfterHead:
                    AfterHead(token);
                    return;

                case HtmlTreeMode.InBody:
                    InBody(token);
                    return;

                case HtmlTreeMode.Text:
                    Text(token);
                    return;

                case HtmlTreeMode.InTable:
                    InTable(token);
                    return;

                case HtmlTreeMode.InCaption:
                    InCaption(token);
                    return;

                case HtmlTreeMode.InColumnGroup:
                    InColumnGroup(token);
                    return;

                case HtmlTreeMode.InTableBody:
                    InTableBody(token);
                    return;

                case HtmlTreeMode.InRow:
                    InRow(token);
                    return;

                case HtmlTreeMode.InCell:
                    InCell(token);
                    return;

                case HtmlTreeMode.InSelect:
                    InSelect(token);
                    return;

                case HtmlTreeMode.InSelectInTable:
                    InSelectInTable(token);
                    return;

                case HtmlTreeMode.InTemplate:
                    InTemplate(token);
                    return;

                case HtmlTreeMode.AfterBody:
                    AfterBody(token);
                    return;

                case HtmlTreeMode.InFrameset:
                    InFrameset(token);
                    return;

                case HtmlTreeMode.AfterFrameset:
                    AfterFrameset(token);
                    return;

                case HtmlTreeMode.AfterAfterBody:
                    AfterAfterBody(token);
                    return;

                case HtmlTreeMode.AfterAfterFrameset:
                    AfterAfterFrameset(token);
                    return;
            }
        }

        /// <summary>
        /// See 8.2.5.4.1 The "initial" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void Initial(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Doctype:
                {
                    var doctype = (HtmlDoctypeToken)token;

                    if (!doctype.IsValid)
                    {
                        RaiseErrorOccurred(HtmlParseError.DoctypeInvalid, token);
                    }

                    _document.AddNode(new DocumentType(_document, doctype.Name ?? String.Empty)
                    {
                        SystemIdentifier = doctype.SystemIdentifier,
                        PublicIdentifier = doctype.PublicIdentifier
                    });

                    _document.QuirksMode = doctype.GetQuirksMode();
                    _currentMode = HtmlTreeMode.BeforeHtml;
                    return;
                }
                case HtmlTokenType.Character:
                {
                    token.TrimStart();

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    _document.AddComment(token);
                    return;
                }
            }

            if (!_options.IsEmbedded)
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeMissing, token);
                _document.QuirksMode = QuirksMode.On;
            }

            _currentMode = HtmlTreeMode.BeforeHtml;
            BeforeHtml(token);
        }

        /// <summary>
        /// See 8.2.5.4.2 The "before html" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void BeforeHtml(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    token.TrimStart();

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    _document.AddComment(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (!token.Name.Is(TagNames.Html))
                    {
                        break;
                    }

                    AddRoot(token.AsTag());
                    _currentMode = HtmlTreeMode.BeforeHead;
                    return;
                }                    
                case HtmlTokenType.EndTag:
                {
                    if (TagNames.AllBeforeHead.Contains(token.Name))
                    {
                        break;
                    }

                    RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
            }

            BeforeHtml(HtmlTagToken.Open(TagNames.Html));
            BeforeHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.3 The "before head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void BeforeHead(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    token.TrimStart();

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                        return;
                    }
                    else if (tagName.Is(TagNames.Head))
                    {
                        AddElement(new HtmlHeadElement(_document), token.AsTag());
                        _currentMode = HtmlTreeMode.InHead;
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    if (TagNames.AllBeforeHead.Contains(token.Name))
                    {
                        break;
                    }

                    RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
            }

            BeforeHead(HtmlTagToken.Open(TagNames.Head));
            InHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.4 The "in head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InHead(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    AddCharacters(str);

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                        return;
                    }
                    else if (tagName.Is(TagNames.Meta))
                    {
                        var element = new HtmlMetaElement(_document);
                        AddElement(element, token.AsTag(), acknowledgeSelfClosing: true);
                        var encoding = element.GetEncoding();
                        CloseCurrentNode();

                        if (encoding != null)
                        {
                            try
                            {
                                _document.Source.CurrentEncoding = encoding;
                            }
                            catch (NotSupportedException)
                            {
                                Restart();
                            }
                        }

                        return;
                    }
                    else if (TagNames.AllHeadBase.Contains(tagName))
                    {
                        AddElement(token.AsTag(), acknowledgeSelfClosing: true);
                        CloseCurrentNode();
                        return;
                    }
                    else if (tagName.Is(TagNames.Title))
                    {
                        RCDataAlgorithm(token.AsTag());
                        return;
                    }
                    else if (tagName.IsOneOf(TagNames.Style, TagNames.NoFrames) || (_options.IsScripting && tagName.Is(TagNames.NoScript)))
                    {
                        RawtextAlgorithm(token.AsTag());
                        return;
                    }
                    else if (tagName.Is(TagNames.NoScript))
                    {
                        AddElement(token.AsTag());
                        _currentMode = HtmlTreeMode.InHeadNoScript;
                        return;
                    }
                    else if (tagName.Is(TagNames.Script))
                    {
                        var script = new HtmlScriptElement(_document, parserInserted: true, started: IsFragmentCase);
                        AddElement(script, token.AsTag());
                        _tokenizer.State = HtmlParseMode.Script;
                        _previousMode = _currentMode;
                        _currentMode = HtmlTreeMode.Text;
                        return;
                    }
                    else if (tagName.Is(TagNames.Head))
                    {
                        RaiseErrorOccurred(HtmlParseError.HeadTagMisplaced, token);
                        return;
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        AddElement(new HtmlTemplateElement(_document), token.AsTag());
                        _formattingElements.AddScopeMarker();
                        _frameset = false;
                        _currentMode = HtmlTreeMode.InTemplate;
                        _templateModes.Push(HtmlTreeMode.InTemplate);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Head))
                    {
                        CloseCurrentNode();

                        _currentMode = HtmlTreeMode.AfterHead;
                        _waiting = _document.WaitForReadyAsync();
                        return;
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        if (TagCurrentlyOpen(TagNames.Template))
                        {
                            GenerateImpliedEndTags();

                            if (!CurrentNode.LocalName.Is(TagNames.Template))
                            {
                                RaiseErrorOccurred(HtmlParseError.TagClosingMismatch, token);
                            }

                            CloseTemplate();
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TagInappropriate, token);
                        }

                        return;
                    }
                    else if (!tagName.IsOneOf(TagNames.Html, TagNames.Body, TagNames.Br))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                        return;
                    }

                    break;
                }
            }

            CloseCurrentNode();
            _currentMode = HtmlTreeMode.AfterHead;
            AfterHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.5 The "in head noscript" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InHeadNoScript(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    AddCharacters(str);

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    InHead(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllNoScript.Contains(tagName))
                    {
                        InHead(token);
                    }
                    else if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                    }
                    else if (tagName.IsOneOf(TagNames.Head, TagNames.NoScript))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagInappropriate, token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.NoScript))
                    {
                        CloseCurrentNode();
                        _currentMode = HtmlTreeMode.InHead;
                        return;
                    }
                    else if (!tagName.Is(TagNames.Br))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
            CloseCurrentNode();
            _currentMode = HtmlTreeMode.InHead;
            InHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.6 The "after head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterHead(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    AddCharacters(str);

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                    }
                    else if (tagName.Is(TagNames.Body))
                    {
                        AfterHeadStartTagBody(token.AsTag());
                    }
                    else if (tagName.Is(TagNames.Frameset))
                    {
                        AddElement(new HtmlFrameSetElement(_document), token.AsTag());
                        _currentMode = HtmlTreeMode.InFrameset;
                    }
                    else if (TagNames.AllHeadNoTemplate.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagMustBeInHead, token);
                        var index = _openElements.Count;
                        var head = _document.Head as Element;
                        _openElements.Add(head);
                        InHead(token);
                        CloseNode(head);
                    }
                    else if (tagName.Is(TagNames.Head))
                    {
                        RaiseErrorOccurred(HtmlParseError.HeadTagMisplaced, token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.IsOneOf(TagNames.Html, TagNames.Body, TagNames.Br))
                    {
                        break;
                    }

                    RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    return;
                }
            }

            AfterHeadStartTagBody(HtmlTagToken.Open(TagNames.Body));
            _frameset = true;
            Home(token);
        }

        private void InBodyStartTag(HtmlTagToken tag)
        {
            var tagName = tag.Name;

            if (tagName.Is(TagNames.Div))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                AddElement(tag);
            }
            else if (tagName.Is(TagNames.A))
            {
                for (var i = _formattingElements.Count - 1; i >= 0 && _formattingElements[i] != null; i--)
                {
                    if (_formattingElements[i].LocalName.Is(TagNames.A))
                    {
                        var format = _formattingElements[i];
                        RaiseErrorOccurred(HtmlParseError.AnchorNested, tag);
                        HeisenbergAlgorithm(HtmlTagToken.Close(TagNames.A));
                        CloseNode(format);
                        _formattingElements.Remove(format);
                        break;
                    }
                }

                ReconstructFormatting();
                var element = new HtmlAnchorElement(_document);
                AddElement(element, tag);
                _formattingElements.AddFormatting(element);
            }
            else if (tagName.Is(TagNames.Span))
            {
                ReconstructFormatting();
                AddElement(tag);
            }
            else if (tagName.Is(TagNames.Li))
            {
                InBodyStartTagListItem(tag);
            }
            else if (tagName.Is(TagNames.Img))
            {
                InBodyStartTagBreakrow(tag);
            }
            else if (tagName.IsOneOf(TagNames.Ul, TagNames.P))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                AddElement(tag);
            }
            else if (TagNames.AllSemanticFormatting.Contains(tagName))
            {
                ReconstructFormatting();
                _formattingElements.AddFormatting(AddElement(tag));
            }
            else if (tagName.Is(TagNames.Script))
            {
                InHead(tag);
            }
            else if (TagNames.AllHeadings.Contains(tagName))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                if (TagNames.AllHeadings.Contains(CurrentNode.LocalName))
                {
                    RaiseErrorOccurred(HtmlParseError.HeadingNested, tag);
                    CloseCurrentNode();
                }

                AddElement(new HtmlHeadingElement(_document, tagName), tag);
            }
            else if (tagName.Is(TagNames.Input))
            {
                ReconstructFormatting();
                AddElement(new HtmlInputElement(_document), tag, acknowledgeSelfClosing: true);
                CloseCurrentNode();

                if (!tag.GetAttribute(AttributeNames.Type).Isi(AttributeNames.Hidden))
                {
                    _frameset = false;
                }
            }
            else if (tagName.Is(TagNames.Form))
            {
                if (_currentFormElement == null)
                {
                    if (IsInButtonScope())
                    {
                        InBodyEndTagParagraph(tag);
                    }

                    _currentFormElement = new HtmlFormElement(_document);
                    AddElement(_currentFormElement, tag);
                }
                else
                    RaiseErrorOccurred(HtmlParseError.FormAlreadyOpen, tag);
            }
            else if (TagNames.AllBody.Contains(tagName))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                AddElement(tag);
            }
            else if (TagNames.AllClassicFormatting.Contains(tagName))
            {
                ReconstructFormatting();
                _formattingElements.AddFormatting(AddElement(tag));
            }
            else if (TagNames.AllHead.Contains(tagName))
            {
                InHead(tag);
            }
            else if (tagName.IsOneOf(TagNames.Pre, TagNames.Listing))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                AddElement(tag);
                _frameset = false;
                PreventNewLine();
            }
            else if (tagName.Is(TagNames.Button))
            {
                if (IsInScope(TagNames.Button))
                {
                    RaiseErrorOccurred(HtmlParseError.ButtonInScope, tag);
                    InBodyEndTagBlock(tag);
                    InBody(tag);
                }
                else
                {
                    ReconstructFormatting();
                    AddElement(new HtmlButtonElement(_document), tag);
                    _frameset = false;
                }
            }
            else if (tagName.Is(TagNames.Table))
            {
                if (_document.QuirksMode != QuirksMode.On && IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                AddElement(new HtmlTableElement(_document), tag);
                _frameset = false;
                _currentMode = HtmlTreeMode.InTable;
            }
            else if (TagNames.AllBodyBreakrow.Contains(tagName))
            {
                InBodyStartTagBreakrow(tag);
            }
            else if (TagNames.AllBodyClosed.Contains(tagName))
            {
                AddElement(tag, acknowledgeSelfClosing: true);
                CloseCurrentNode();
            }
            else if (tagName.Is(TagNames.Hr))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                AddElement(new HtmlHrElement(_document), tag, acknowledgeSelfClosing: true);
                CloseCurrentNode();
                _frameset = false;
            }
            else if (tagName.Is(TagNames.Textarea))
            {
                AddElement(new HtmlTextAreaElement(_document), tag);
                _tokenizer.State = HtmlParseMode.RCData;
                _previousMode = _currentMode;
                _frameset = false;
                _currentMode = HtmlTreeMode.Text;
                PreventNewLine();
            }
            else if (tagName.Is(TagNames.Select))
            {
                ReconstructFormatting();
                AddElement(new HtmlSelectElement(_document), tag);
                _frameset = false;

                switch (_currentMode)
                {
                    case HtmlTreeMode.InTable:
                    case HtmlTreeMode.InTableBody:
                    case HtmlTreeMode.InCaption:
                    case HtmlTreeMode.InRow:
                    case HtmlTreeMode.InCell:
                        _currentMode = HtmlTreeMode.InSelectInTable;
                        break;

                    default:
                        _currentMode = HtmlTreeMode.InSelect;
                        break;
                }
            }
            else if (tagName.IsOneOf(TagNames.Optgroup, TagNames.Option))
            {
                if (CurrentNode.LocalName.Is(TagNames.Option))
                {
                    InBodyEndTagAnythingElse(HtmlTagToken.Close(TagNames.Option));
                }

                ReconstructFormatting();
                AddElement(tag);
            }
            else if (tagName.IsOneOf(TagNames.Dd, TagNames.Dt))
            {
                InBodyStartTagDefinitionItem(tag);
            }
            else if (tagName.Is(TagNames.Iframe))
            {
                _frameset = false;
                RawtextAlgorithm(tag);
            }
            else if (TagNames.AllBodyObsolete.Contains(tagName))
            {
                ReconstructFormatting();
                AddElement(tag);
                _formattingElements.AddScopeMarker();
                _frameset = false;
            }
            else if (tagName.Is(TagNames.Image))
            {
                RaiseErrorOccurred(HtmlParseError.ImageTagNamedWrong, tag);
                tag.Name = TagNames.Img;
                InBodyStartTagBreakrow(tag);
            }
            else if (tagName.Is(TagNames.NoBr))
            {
                ReconstructFormatting();

                if (IsInScope(TagNames.NoBr))
                {
                    RaiseErrorOccurred(HtmlParseError.NobrInScope, tag);
                    HeisenbergAlgorithm(tag);
                    ReconstructFormatting();
                }

                _formattingElements.AddFormatting(AddElement(tag));
            }
            else if (tagName.Is(TagNames.Xmp))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                ReconstructFormatting();
                _frameset = false;
                RawtextAlgorithm(tag);
            }
            else if (tagName.IsOneOf(TagNames.Rb, TagNames.Rtc))
            {
                if (IsInScope(TagNames.Ruby))
                {
                    GenerateImpliedEndTags();

                    if (!CurrentNode.LocalName.Is(TagNames.Ruby))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
                    }
                }

                AddElement(tag);
            }
            else if (tagName.IsOneOf(TagNames.Rp, TagNames.Rt))
            {
                if (IsInScope(TagNames.Ruby))
                {
                    GenerateImpliedEndTagsExceptFor(TagNames.Rtc);

                    if (!CurrentNode.LocalName.IsOneOf(TagNames.Ruby, TagNames.Rtc))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
                    }
                }

                AddElement(tag);
            }
            else if (tagName.Is(TagNames.NoEmbed))
            {
                RawtextAlgorithm(tag);
            }
            else if (tagName.Is(TagNames.NoScript))
            {
                if (_options.IsScripting)
                {
                    RawtextAlgorithm(tag);
                    return;
                }

                ReconstructFormatting();
                AddElement(tag);
            }
            else if (tagName.Is(TagNames.Math))
            {
                var element = new MathElement(_document, tagName);
                ReconstructFormatting();
                AddElement(element.Setup(tag));

                if (tag.IsSelfClosing)
                {
                    CloseNode(element);
                }
            }
            else if (tagName.Is(TagNames.Svg))
            {
                var element = new SvgElement(_document, tagName);
                ReconstructFormatting();
                AddElement(element.Setup(tag));

                if (tag.IsSelfClosing)
                {
                    CloseNode(element);
                }
            }
            else if (tagName.Is(TagNames.Plaintext))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(tag);
                }

                AddElement(tag);
                _tokenizer.State = HtmlParseMode.Plaintext;
            }
            else if (tagName.Is(TagNames.Frameset))
            {
                RaiseErrorOccurred(HtmlParseError.FramesetMisplaced, tag);

                if (_openElements.Count != 1 && _openElements[1].LocalName.Is(TagNames.Body) && _frameset)
                {
                    _openElements[1].RemoveFromParent();

                    while (_openElements.Count > 1)
                    {
                        CloseCurrentNode();
                    }

                    AddElement(new HtmlFrameSetElement(_document), tag);
                    _currentMode = HtmlTreeMode.InFrameset;
                }
            }
            else if (tagName.Is(TagNames.Html))
            {
                RaiseErrorOccurred(HtmlParseError.HtmlTagMisplaced, tag);

                if (_templateModes.Count == 0)
                {
                    _openElements[0].SetUniqueAttributes(tag.Attributes);
                }
            }
            else if (tagName.Is(TagNames.Body))
            {
                RaiseErrorOccurred(HtmlParseError.BodyTagMisplaced, tag);

                if (_templateModes.Count == 0 && _openElements.Count > 1 && _openElements[1].LocalName.Is(TagNames.Body))
                {
                    _frameset = false;
                    _openElements[1].SetUniqueAttributes(tag.Attributes);
                }
            }
            else if (tagName.Is(TagNames.IsIndex))
            {
                RaiseErrorOccurred(HtmlParseError.TagInappropriate, tag);

                if (_currentFormElement == null)
                {
                    InBody(HtmlTagToken.Open(TagNames.Form));

                    if (tag.GetAttribute(AttributeNames.Action).Length > 0)
                    {
                        _currentFormElement.SetAttribute(AttributeNames.Action, tag.GetAttribute(AttributeNames.Action));
                    }

                    InBody(HtmlTagToken.Open(TagNames.Hr));
                    InBody(HtmlTagToken.Open(TagNames.Label));

                    if (tag.GetAttribute(AttributeNames.Prompt).Length > 0)
                    {
                        AddCharacters(tag.GetAttribute(AttributeNames.Prompt));
                    }
                    else
                    {
                        AddCharacters("This is a searchable index. Enter search keywords: ");
                    }

                    var input = HtmlTagToken.Open(TagNames.Input);
                    input.AddAttribute(AttributeNames.Name, TagNames.IsIndex);

                    for (var i = 0; i < tag.Attributes.Count; i++)
                    {
                        if (!tag.Attributes[i].Key.IsOneOf(AttributeNames.Name, AttributeNames.Action, AttributeNames.Prompt))
                        {
                            input.AddAttribute(tag.Attributes[i].Key, tag.Attributes[i].Value);
                        }
                    }

                    InBody(input);
                    InBody(HtmlTagToken.Close(TagNames.Label));
                    InBody(HtmlTagToken.Open(TagNames.Hr));
                    InBody(HtmlTagToken.Close(TagNames.Form));
                }
            }
            else if (TagNames.AllNested.Contains(tagName))
            {
                RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, tag);
            }
            else
            {
                ReconstructFormatting();
                AddElement(tag);
            }
        }

        private void InBodyEndTag(HtmlTagToken tag)
        {
            var tagName = tag.Name;

            if (tagName.Is(TagNames.Div))
            {
                InBodyEndTagBlock(tag);
            }
            else if (tagName.Is(TagNames.A))
            {
                HeisenbergAlgorithm(tag);
            }
            else if (tagName.Is(TagNames.Li))
            {
                if (IsInListItemScope())
                {
                    GenerateImpliedEndTagsExceptFor(tagName);

                    if (!CurrentNode.LocalName.Is(TagNames.Li))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
                    }

                    ClearStackBackTo(TagNames.Li);
                    CloseCurrentNode();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.ListItemNotInScope, tag);
                }
            }
            else if (tagName.Is(TagNames.P))
            {
                InBodyEndTagParagraph(tag);
            }
            else if (TagNames.AllBlocks.Contains(tagName))
            {
                InBodyEndTagBlock(tag);
            }
            else if (TagNames.AllFormatting.Contains(tagName))
            {
                HeisenbergAlgorithm(tag);
            }
            else if (tagName.Is(TagNames.Form))
            {
                var node = _currentFormElement;
                _currentFormElement = null;

                if (node != null && IsInScope(node.LocalName))
                {
                    GenerateImpliedEndTags();

                    if (CurrentNode != node)
                    {
                        RaiseErrorOccurred(HtmlParseError.FormClosedWrong, tag);
                    }

                    CloseNode(node);
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.FormNotInScope, tag);
                }
            }
            else if (tagName.Is(TagNames.Br))
            {
                RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, tag);
                InBodyStartTagBreakrow(HtmlTagToken.Open(TagNames.Br));
            }
            else if (TagNames.AllHeadings.Contains(tagName))
            {
                if (IsInScope(TagNames.AllHeadings))
                {
                    GenerateImpliedEndTags();

                    if (!CurrentNode.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
                    }

                    ClearStackBackTo(TagNames.AllHeadings);
                    CloseCurrentNode();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.HeadingNotInScope, tag);
                }
            }
            else if (tagName.IsOneOf(TagNames.Dd, TagNames.Dt))
            {
                if (IsInScope(tagName))
                {
                    GenerateImpliedEndTagsExceptFor(tagName);

                    if (!CurrentNode.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
                    }

                    ClearStackBackTo(tagName);
                    CloseCurrentNode();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.ListItemNotInScope, tag);
                }
            }
            else if (tagName.IsOneOf(TagNames.Applet, TagNames.Marquee, TagNames.Object))
            {
                if (IsInScope(tagName))
                {
                    GenerateImpliedEndTags();

                    if (!CurrentNode.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
                    }

                    ClearStackBackTo(tagName);
                    CloseCurrentNode();
                    _formattingElements.ClearFormatting();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.ObjectNotInScope, tag);
                }
            }
            else if (tagName.Is(TagNames.Body))
            {
                InBodyEndTagBody(tag);
            }
            else if (tagName.Is(TagNames.Html))
            {
                if (InBodyEndTagBody(tag))
                {
                    AfterBody(tag);
                }
            }
            else if (tagName.Is(TagNames.Template))
            {
                InHead(tag);
            }
            else
            {
                InBodyEndTagAnythingElse(tag);
            }
        }

        /// <summary>
        /// See 8.2.5.4.7 The "in body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    ReconstructFormatting();
                    AddCharacters(token.Data);
                    _frameset = !token.HasContent && _frameset;
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    InBodyStartTag(token.AsTag());
                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    InBodyEndTag(token.AsTag());
                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    CheckBodyOnClosing(token);

                    if (_templateModes.Count != 0)
                    {
                        InTemplate(token);
                    }
                    else
                    {
                        End();
                    }

                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.8 The "text" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void Text(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    AddCharacters(token.Data);
                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (!token.Name.Is(TagNames.Script))
                    {
                        CloseCurrentNode();
                        _currentMode = _previousMode;
                    }
                    else
                    {
                        HandleScript(CurrentNode as HtmlScriptElement);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    RaiseErrorOccurred(HtmlParseError.EOF, token);
                    CloseCurrentNode();
                    _currentMode = _previousMode;
                    Consume(token);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.9 The "in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTable(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Caption))
                    {
                        ClearStackBackTo(TagNames.Table);
                        _formattingElements.AddScopeMarker();
                        AddElement(new HtmlTableCaptionElement(_document), token.AsTag());
                        _currentMode = HtmlTreeMode.InCaption;
                    }
                    else if (tagName.Is(TagNames.Colgroup))
                    {
                        ClearStackBackTo(TagNames.Table);
                        AddElement(new HtmlTableColgroupElement(_document), token.AsTag());
                        _currentMode = HtmlTreeMode.InColumnGroup;
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        InTable(HtmlTagToken.Open(TagNames.Colgroup));
                        InColumnGroup(token);
                    }
                    else if (TagNames.AllTableSections.Contains(tagName))
                    {
                        ClearStackBackTo(TagNames.Table);
                        AddElement(new HtmlTableSectionElement(_document, tagName), token.AsTag());
                        _currentMode = HtmlTreeMode.InTableBody;
                    }
                    else if (TagNames.AllTableCellsRows.Contains(tagName))
                    {
                        InTable(HtmlTagToken.Open(TagNames.Tbody));
                        InTableBody(token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        RaiseErrorOccurred(HtmlParseError.TableNesting, token);

                        if (InTableEndTagTable(token))
                        {
                            Home(token);
                        }
                    }
                    else if (tagName.Is(TagNames.Input))
                    {
                        var tag = token.AsTag();

                        if (tag.GetAttribute(AttributeNames.Type).Isi(AttributeNames.Hidden))
                        {
                            RaiseErrorOccurred(HtmlParseError.InputUnexpected, token);
                            AddElement(new HtmlInputElement(_document), tag, true);
                            CloseCurrentNode();
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
                            InBodyWithFoster(token);
                        }
                    }
                    else if (tagName.Is(TagNames.Form))
                    {
                        RaiseErrorOccurred(HtmlParseError.FormInappropriate, token);

                        if (_currentFormElement == null)
                        {
                            _currentFormElement = new HtmlFormElement(_document);
                            AddElement(_currentFormElement, token.AsTag());
                            CloseCurrentNode();
                        }
                    }
                    else if (TagNames.AllTableHead.Contains(tagName))
                    {
                        InHead(token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInTableDetected, token);
                        InBodyWithFoster(token);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Table))
                    {
                        InTableEndTagTable(token);
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        InHead(token);
                    }
                    else if (TagNames.AllTableSpecial.Contains(tagName) || TagNames.AllTableInner.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInTableDetected, token);
                        InBodyWithFoster(token);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    InBody(token);
                    return;
                }
                case HtmlTokenType.Character:
                {
                    if (TagNames.AllTableMajor.Contains(CurrentNode.LocalName))
                    {
                        InTableText(token);
                        return;
                    }

                    break;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
            InBodyWithFoster(token);
        }

        /// <summary>
        /// See 8.2.5.4.10 The "in table text" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTableText(HtmlToken token)
        {
            if (token.HasContent)
            {
                RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
                InBodyWithFoster(token);
            }
            else
            {
                AddCharacters(token.Data);
            }
        }

        /// <summary>
        /// See 8.2.5.4.11 The "in caption" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InCaption(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Caption))
                    {
                        InCaptionEndTagCaption(token);
                    }
                    else if (TagNames.AllCaptionStart.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        RaiseErrorOccurred(HtmlParseError.TableNesting, token);

                        if (InCaptionEndTagCaption(token))
                        {
                            InTable(token);
                        }
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllCaptionEnd.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, token);

                        if (InCaptionEndTagCaption(token))
                        {
                            InTable(token);
                        }
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
            }

            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.12 The "in column group" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InColumnGroup(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    AddCharacters(str);

                    if (token.IsEmpty)
                        return;

                    break;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        AddElement(new HtmlTableColElement(_document), token.AsTag(), acknowledgeSelfClosing: true);
                        CloseCurrentNode();
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        InHead(token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Colgroup))
                    {
                        InColumnGroupEndTagColgroup(token);
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagClosedWrong, token);
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        InHead(token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    InBody(token);
                    return;
                }
            }

            if (InColumnGroupEndTagColgroup(token))
            {
                InTable(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.13 The "in table body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTableBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Tr))
                    {
                        ClearStackBackTo(TagNames.AllTableSections);
                        AddElement(new HtmlTableRowElement(_document), token.AsTag());
                        _currentMode = HtmlTreeMode.InRow;
                    }
                    else if (TagNames.AllTableCells.Contains(tagName))
                    {
                        InTableBody(HtmlTagToken.Open(TagNames.Tr));
                        InRow(token);
                    }
                    else if (TagNames.AllTableGeneral.Contains(tagName))
                    {
                        InTableBodyCloseTable(token.AsTag());
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableSections.Contains(tagName))
                    {
                        if (IsInTableScope(tagName))
                        {
                            ClearStackBackTo(TagNames.AllTableSections);
                            CloseCurrentNode();
                            _currentMode = HtmlTreeMode.InTable;
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, token);
                        }
                    }
                    else if (tagName.Is(TagNames.Tr) || TagNames.AllTableSpecial.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        InTableBodyCloseTable(token.AsTag());
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
            }

            InTable(token);
        }

        /// <summary>
        /// See 8.2.5.4.14 The "in row" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InRow(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableCells.Contains(tagName))
                    {
                        ClearStackBackTo(TagNames.Tr);
                        AddElement(token.AsTag());
                        _currentMode = HtmlTreeMode.InCell;
                        _formattingElements.AddScopeMarker();
                    }
                    else if (tagName.Is(TagNames.Tr) || TagNames.AllTableGeneral.Contains(tagName))
                    {
                        if (InRowEndTagTablerow(token))
                            InTableBody(token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Tr))
                    {
                        InRowEndTagTablerow(token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        if (InRowEndTagTablerow(token))
                        {
                            InTableBody(token);
                        }
                    }
                    else if (TagNames.AllTableSections.Contains(tagName))
                    {
                        if (IsInTableScope(tagName))
                        {
                            InRowEndTagTablerow(token);
                            InTableBody(token);
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, token);
                        }
                    }
                    else if (TagNames.AllTableSpecial.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
            }

            InTable(token);
        }

        /// <summary>
        /// See 8.2.5.4.15 The "in cell" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InCell(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableCellsRows.Contains(tagName) || TagNames.AllTableGeneral.Contains(tagName))
                    {
                        if (IsInTableScope(TagNames.AllTableCells))
                        {
                            InCellEndTagCell(token);
                            Home(token);
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TableCellNotInScope, token);
                        }

                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableCells.Contains(tagName))
                    {
                        InCellEndTagCell(token);
                    }
                    else if (TagNames.AllTableCore.Contains(tagName))
                    {
                        if (IsInTableScope(tagName))
                        {
                            InCellEndTagCell(token);
                            Home(token);
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TableNotInScope, token);
                        }
                    }
                    else if (!TagNames.AllTableSpecial.Contains(tagName))
                    {
                        InBody(token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    }

                    return;
                }
            }

            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.16 The "in select" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InSelect(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    AddCharacters(token.Data);
                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                    }
                    else if (tagName.Is(TagNames.Option))
                    {
                        if (CurrentNode.LocalName.Is(TagNames.Option))
                        {
                            InSelectEndTagOption(token);
                        }

                        AddElement(new HtmlOptionElement(_document), token.AsTag());
                    }
                    else if (tagName.Is(TagNames.Optgroup))
                    {
                        if (CurrentNode.LocalName.Is(TagNames.Option))
                        {
                            InSelectEndTagOption(token);
                        }

                        if (CurrentNode.LocalName.Is(TagNames.Optgroup))
                        {
                            InSelectEndTagOptgroup(token);
                        }

                        AddElement(new HtmlOptionsGroupElement(_document), token.AsTag());
                    }
                    else if (tagName.Is(TagNames.Select))
                    {
                        RaiseErrorOccurred(HtmlParseError.SelectNesting, token);
                        InSelectEndTagSelect();
                    }
                    else if (TagNames.AllInput.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, token);

                        if (IsInSelectScope(TagNames.Select))
                        {
                            InSelectEndTagSelect();
                            Home(token);
                        }
                    }
                    else if (tagName.IsOneOf(TagNames.Template, TagNames.Script))
                    {
                        InHead(token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, token);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Template))
                    {
                        InHead(token);
                    }
                    else if (tagName.Is(TagNames.Optgroup))
                    {
                        InSelectEndTagOptgroup(token);
                    }
                    else if (tagName.Is(TagNames.Option))
                    {
                        InSelectEndTagOption(token);
                    }
                    else if (tagName.Is(TagNames.Select) && IsInSelectScope(TagNames.Select))
                    {
                        InSelectEndTagSelect();
                    }
                    else if (tagName.Is(TagNames.Select))
                    {
                        RaiseErrorOccurred(HtmlParseError.SelectNotInScope, token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    InBody(token);
                    return;
                }
                default:
                {
                    RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.17 The "in select in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InSelectInTable(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableSelects.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, token);
                        InSelectEndTagSelect();
                        Home(token);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableSelects.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);

                        if (IsInTableScope(tagName))
                        {
                            InSelectEndTagSelect();
                            Home(token);
                        }

                        return;
                    }

                    break;
                }
            }

            InSelect(token);
        }

        /// <summary>
        /// See 8.2.5.4.18 The "in template" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTemplate(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Script) || TagNames.AllHead.Contains(tagName))
                    {
                        InHead(token);
                    }
                    else if (TagNames.AllTableRoot.Contains(tagName))
                    {
                        TemplateStep(token, HtmlTreeMode.InTable);
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        TemplateStep(token, HtmlTreeMode.InColumnGroup);
                    }
                    else if (tagName.Is(TagNames.Tr))
                    {
                        TemplateStep(token, HtmlTreeMode.InTableBody);
                    }
                    else if (TagNames.AllTableCells.Contains(tagName))
                    {
                        TemplateStep(token, HtmlTreeMode.InRow);
                    }
                    else
                    {
                        TemplateStep(token, HtmlTreeMode.InBody);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.Is(TagNames.Template))
                    {
                        InHead(token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    if (TagCurrentlyOpen(TagNames.Template))
                    {
                        RaiseErrorOccurred(HtmlParseError.EOF, token);
                        CloseTemplate();
                        Home(token);
                        return;
                    }

                    End();
                    return;
                }
                default:
                {
                    InBody(token);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.19 The "after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    ReconstructFormatting();
                    AddCharacters(str);

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    _openElements[0].AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (token.Name.Is(TagNames.Html))
                    {
                        InBody(token);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.Is(TagNames.Html))
                    {
                        if (IsFragmentCase)
                        {
                            RaiseErrorOccurred(HtmlParseError.TagInvalidInFragmentMode, token);
                        }
                        else
                        {
                            _currentMode = HtmlTreeMode.AfterAfterBody;
                        }

                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndOfFile:
                {
                    End();
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
            _currentMode = HtmlTreeMode.InBody;
            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.20 The "in frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InFrameset(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    AddCharacters(str);

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                    }
                    else if (tagName.Is(TagNames.Frameset))
                    {
                        AddElement(new HtmlFrameSetElement(_document), token.AsTag());
                    }
                    else if (tagName.Is(TagNames.Frame))
                    {
                        AddElement(new HtmlFrameElement(_document), token.AsTag(), acknowledgeSelfClosing: true);
                        CloseCurrentNode();
                    }
                    else if (tagName.Is(TagNames.NoFrames))
                    {
                        InHead(token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.Is(TagNames.Frameset))
                    {
                        if (CurrentNode != _openElements[0])
                        {
                            CloseCurrentNode();

                            if (!IsFragmentCase && !CurrentNode.LocalName.Is(TagNames.Frameset))
                            {
                                _currentMode = HtmlTreeMode.AfterFrameset;
                            }
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.CurrentNodeIsRoot, token);
                        }

                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndOfFile:
                {
                    if (CurrentNode != _document.DocumentElement)
                    {
                        RaiseErrorOccurred(HtmlParseError.CurrentNodeIsNotRoot, token);
                    }

                    End();
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
        }

        /// <summary>
        /// See 8.2.5.4.21 The "after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterFrameset(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    AddCharacters(str);

                    if (token.IsEmpty)
                    {
                        return;
                    }

                    break;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                    }
                    else if (tagName.Is(TagNames.NoFrames))
                    {
                        InHead(token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (!token.Name.Is(TagNames.Html))
                    {
                        break;
                    }

                    _currentMode = HtmlTreeMode.AfterAfterFrameset;
                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    End();
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
        }

        /// <summary>
        /// See 8.2.5.4.22 The "after after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterAfterBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    ReconstructFormatting();
                    AddCharacters(str);

                    if (token.IsEmpty)
                    {
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndOfFile:
                {
                    End();
                    return;
                }
                case HtmlTokenType.Comment:
                {
                    _document.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    InBody(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (!token.Name.Is(TagNames.Html))
                    {
                        break;
                    }

                    InBody(token);
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
            _currentMode = HtmlTreeMode.InBody;
            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.23 The "after after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterAfterFrameset(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Comment:
                {
                    _document.AddComment(token);
                    return;
                }
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    ReconstructFormatting();
                    AddCharacters(str);

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;

                }
                case HtmlTokenType.Doctype:
                {
                    InBody(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(token);
                    }
                    else if (tagName.Is(TagNames.NoFrames))
                    {
                        InHead(token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    End();
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
        }

        #endregion

        #region Substates

        /// <summary>
        /// Inserting something in the template.
        /// </summary>
        /// <param name="token">The token to insert.</param>
        /// <param name="mode">The mode to push.</param>
        private void TemplateStep(HtmlToken token, HtmlTreeMode mode)
        {
            _templateModes.Pop();
            _templateModes.Push(mode);
            _currentMode = mode;
            Home(token);
        }

        /// <summary>
        /// Closes the template element.
        /// </summary>
        private void CloseTemplate()
        {
            while (_openElements.Count > 0)
            {
                var template = CurrentNode as HtmlTemplateElement;
                CloseCurrentNode();

                if (template != null)
                {
                    template.PopulateFragment();
                    break;
                }
            }

            _formattingElements.ClearFormatting();
            _templateModes.Pop();
            Reset();
        }

        /// <summary>
        /// Closes the table if the section is in table scope.
        /// </summary>
        /// <param name="tag">The tag to insert (closes table).</param>
        private void InTableBodyCloseTable(HtmlTagToken tag)
        {
            if (IsInTableScope(TagNames.AllTableSections))
            {
                ClearStackBackTo(TagNames.AllTableSections);
                CloseCurrentNode();
                _currentMode = HtmlTreeMode.InTable;
                InTable(tag);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, tag);
            }
        }

        /// <summary>
        /// Acts if a option end tag had been seen in the InSelect state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        private void InSelectEndTagOption(HtmlToken token)
        {
            if (CurrentNode.LocalName.Is(TagNames.Option))
            {
                CloseCurrentNode();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
            }
        }

        /// <summary>
        /// Acts if a optgroup end tag had been seen in the InSelect state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        private void InSelectEndTagOptgroup(HtmlToken token)
        {
            if (_openElements.Count > 1 &&
                _openElements[_openElements.Count - 1].LocalName.Is(TagNames.Option) &&
                _openElements[_openElements.Count - 2].LocalName.Is(TagNames.Optgroup))
            {
                CloseCurrentNode();
            }

            if (CurrentNode.LocalName.Is(TagNames.Optgroup))
            {
                CloseCurrentNode();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
            }
        }

        /// <summary>
        /// Act as if an colgroup end tag has been found in the InColumnGroup state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InColumnGroupEndTagColgroup(HtmlToken token)
        {
            if (CurrentNode.LocalName.Is(TagNames.Colgroup))
            {
                CloseCurrentNode();
                _currentMode = HtmlTreeMode.InTable;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
                return false;
            }
        }

        /// <summary>
        /// Act as if a body start tag has been found in the AfterHead state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        private void AfterHeadStartTagBody(HtmlTagToken token)
        {
            AddElement(new HtmlBodyElement(_document), token);
            _frameset = false;
            _currentMode = HtmlTreeMode.InBody;
        }

        /// <summary>
        /// Follows the generic rawtext parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        private void RawtextAlgorithm(HtmlTagToken tag)
        {
            AddElement(tag);
            _previousMode = _currentMode;
            _currentMode = HtmlTreeMode.Text;
            _tokenizer.State = HtmlParseMode.Rawtext;
        }

        /// <summary>
        /// Follows the generic RCData parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        private void RCDataAlgorithm(HtmlTagToken tag)
        {
            AddElement(tag);
            _previousMode = _currentMode;
            _currentMode = HtmlTreeMode.Text;
            _tokenizer.State = HtmlParseMode.RCData;
        }

        /// <summary>
        /// Acts if a li tag in the InBody state has been found.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        private void InBodyStartTagListItem(HtmlTagToken tag)
        {
            var index = _openElements.Count - 1;
            var node = _openElements[index];
            _frameset = false;

            while (true)
            {
                if (node.LocalName.Is(TagNames.Li))
                {
                    InBody(HtmlTagToken.Close(node.LocalName));
                    break;
                }

                if (((node.Flags & NodeFlags.Special) == NodeFlags.Special) && !TagNames.AllBasicBlocks.Contains(node.LocalName))
                {
                    break;
                }
                
                node = _openElements[--index];
            }

            if (IsInButtonScope())
            {
                InBodyEndTagParagraph(tag);
            }

            AddElement(tag);
        }

        /// <summary>
        /// Acts if a dd or dt tag in the InBody state has been found.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        private void InBodyStartTagDefinitionItem(HtmlTagToken tag)
        {
            _frameset = false;
            var index = _openElements.Count - 1;
            var node = _openElements[index];

            while (true)
            {
                if (node.LocalName.IsOneOf(TagNames.Dd, TagNames.Dt))
                {
                    InBody(HtmlTagToken.Close(node.LocalName));
                    break;
                }

                if (((node.Flags & NodeFlags.Special) == NodeFlags.Special) && !TagNames.AllBasicBlocks.Contains(node.LocalName))
                {
                    break;
                }

                node = _openElements[--index];
            }

            if (IsInButtonScope())
            {
                InBodyEndTagParagraph(tag);
            }

            AddElement(tag);
        }

        /// <summary>
        /// Acts if a block (button) end tag had been seen in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InBodyEndTagBlock(HtmlTagToken tag)
        {
            if (IsInScope(tag.Name))
            {
                GenerateImpliedEndTags();

                if (!CurrentNode.LocalName.Is(tag.Name))
                {
                    RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
                }

                ClearStackBackTo(tag.Name);
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BlockNotInScope, tag);
                return false;
            }
        }

        /// <summary>
        /// Acts if a nobr tag had been seen in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        private void HeisenbergAlgorithm(HtmlTagToken tag)
        {
            var outer = 0;
            var inner = 0;
            var bookmark = 0;
            var index = 0;

            while (outer < 8)
            {
                var formattingElement = default(Element);
                var furthestBlock = default(Element);

                outer++;
                index = 0;
                inner = 0;

                for (var j = _formattingElements.Count - 1; j >= 0 && _formattingElements[j] != null; j--)
                {
                    if (_formattingElements[j].LocalName.Is(tag.Name))
                    {
                        index = j;
                        formattingElement = _formattingElements[j];
                        break;
                    }
                }

                if (formattingElement == null)
                {
                    InBodyEndTagAnythingElse(tag);
                    break;
                }

                var openIndex = _openElements.IndexOf(formattingElement);

                if (openIndex == -1)
                {
                    RaiseErrorOccurred(HtmlParseError.FormattingElementNotFound, tag);
                    _formattingElements.Remove(formattingElement);
                    break;
                }

                if (!IsInScope(formattingElement.LocalName))
                {
                    RaiseErrorOccurred(HtmlParseError.ElementNotInScope, tag);
                    break;
                }

                if (openIndex != _openElements.Count - 1)
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong, tag);

                bookmark = index;

                for (var j = openIndex + 1; j < _openElements.Count; j++)
                {
                    if ((_openElements[j].Flags & NodeFlags.Special) == NodeFlags.Special)
                    {
                        index = j;
                        furthestBlock = _openElements[j];
                        break;
                    }
                }

                if (furthestBlock == null)
                {
                    do
                    {
                        furthestBlock = CurrentNode;
                        CloseCurrentNode();
                    }
                    while (furthestBlock != formattingElement);

                    _formattingElements.Remove(formattingElement);
                    break;
                }

                var commonAncestor = _openElements[openIndex - 1];
                var node = furthestBlock;
                var lastNode = furthestBlock;

                while (true)
                {
                    inner++;
                    node = _openElements[--index];

                    if (node == formattingElement)
                    {
                        break;
                    }

                    if (inner > 3 && _formattingElements.Contains(node))
                    {
                        _formattingElements.Remove(node);
                    }

                    if (!_formattingElements.Contains(node))
                    {
                        CloseNode(node);
                        continue;
                    }

                    var newElement = CopyElement(node);
                    commonAncestor.AddNode(newElement);
                    _openElements[index] = newElement;
                    
                    for (var l = 0; l != _formattingElements.Count; l++)
                    {
                        if (_formattingElements[l] == node)
                        {
                            _formattingElements[l] = newElement;
                            break;
                        }
                    }

                    node = newElement;

                    if (lastNode == furthestBlock)
                    {
                        bookmark++;
                    }

                    lastNode.Parent?.RemoveChild(lastNode);
                    node.AddNode(lastNode);
                    lastNode = node;
                }

                lastNode.Parent?.RemoveChild(lastNode);

                if (!TagNames.AllTableMajor.Contains(commonAncestor.LocalName))
                {
                    commonAncestor.AddNode(lastNode);
                }
                else
                {
                    AddElementWithFoster(lastNode);
                }

                var element = CopyElement(formattingElement);

                while (furthestBlock.ChildNodes.Length > 0)
                {
                    var childNode = furthestBlock.ChildNodes[0];
                    furthestBlock.RemoveNode(0, childNode);
                    element.AddNode(childNode);
                }

                furthestBlock.AddNode(element);
                _formattingElements.Remove(formattingElement);
                _formattingElements.Insert(bookmark, element);
                CloseNode(formattingElement);
                _openElements.Insert(_openElements.IndexOf(furthestBlock) + 1, element);
            }
        }

        /// <summary>
        /// Copies the element and its attributes to create a new element.
        /// </summary>
        /// <param name="element">The old element (source).</param>
        /// <returns>The new element (target).</returns>
        private Element CopyElement(Element element)
        {
            return (Element)element.Clone(false);
        }

        /// <summary>
        /// Performs the InBody state with foster parenting.
        /// </summary>
        /// <param name="token">The given token.</param>
        private void InBodyWithFoster(HtmlToken token)
        {
            _foster = true;
            InBody(token);
            _foster = false;
        }

        /// <summary>
        /// Act as if an anything else tag has been found in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag found.</param>
        private void InBodyEndTagAnythingElse(HtmlTagToken tag)
        {
            var index = _openElements.Count - 1;
            var node = CurrentNode;

            while (node != null)
            {
                if (node.LocalName.Is(tag.Name))
                {
                    GenerateImpliedEndTagsExceptFor(tag.Name);

                    if (!node.LocalName.Is(tag.Name))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagClosedWrong, tag);
                    }

                    CloseNodesFrom(index);
                    break;
                }
                else if ((node.Flags & NodeFlags.Special) == NodeFlags.Special)
                {
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong, tag);
                    break;
                }

                node = _openElements[--index];
            }
        }

        /// <summary>
        /// Act as if an body end tag has been found in the InBody state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InBodyEndTagBody(HtmlToken token)
        {
            if (IsInScope(TagNames.Body))
            {
                CheckBodyOnClosing(token);
                _currentMode = HtmlTreeMode.AfterBody;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BodyNotInScope, token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an br start tag has been found in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag found.</param>
        private void InBodyStartTagBreakrow(HtmlTagToken tag)
        {
            ReconstructFormatting();
            AddElement(tag, acknowledgeSelfClosing: true);
            CloseCurrentNode();
            _frameset = false;
        }

        /// <summary>
        /// Act as if an p end tag has been found in the InBody state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was found, otherwise false.</returns>
        private Boolean InBodyEndTagParagraph(HtmlToken token)
        {
            if (IsInButtonScope())
            {
                GenerateImpliedEndTagsExceptFor(TagNames.P);

                if (!CurrentNode.LocalName.Is(TagNames.P))
                {
                    RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
                }

                ClearStackBackTo(TagNames.P);
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.ParagraphNotInScope, token);
                InBody(HtmlTagToken.Open(TagNames.P));
                InBodyEndTagParagraph(token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an table end tag has been found in the InTable state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InTableEndTagTable(HtmlToken token)
        {
            if (IsInTableScope(TagNames.Table))
            {
                ClearStackBackTo(TagNames.Table);
                CloseCurrentNode();
                Reset();
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TableNotInScope, token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an tr end tag has been found in the InRow state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InRowEndTagTablerow(HtmlToken token)
        {
            if (IsInTableScope(TagNames.Tr))
            {
                ClearStackBackTo(TagNames.Tr);
                CloseCurrentNode();
                _currentMode = HtmlTreeMode.InTableBody;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TableRowNotInScope, token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an select end tag has been found in the InSelect state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private void InSelectEndTagSelect()
        {
            ClearStackBackTo(TagNames.Select);
            CloseCurrentNode();
            Reset();
        }

        /// <summary>
        /// Act as if an caption end tag has been found in the InCaption state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InCaptionEndTagCaption(HtmlToken token)
        {
            if (IsInTableScope(TagNames.Caption))
            {
                GenerateImpliedEndTags();

                if (!CurrentNode.LocalName.Is(TagNames.Caption))
                {
                    RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
                }

                ClearStackBackTo(TagNames.Caption);
                CloseCurrentNode();
                _formattingElements.ClearFormatting();
                _currentMode = HtmlTreeMode.InTable;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.CaptionNotInScope, token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an td or th end tag has been found in the InCell state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InCellEndTagCell(HtmlToken token)
        {
            if (IsInTableScope(TagNames.AllTableCells))
            {
                GenerateImpliedEndTags();

                if (!TagNames.AllTableCells.Contains(CurrentNode.LocalName))
                {
                    RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
                }

                ClearStackBackTo(TagNames.AllTableCells);
                CloseCurrentNode();
                _formattingElements.ClearFormatting();
                _currentMode = HtmlTreeMode.InRow;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TableCellNotInScope, token);
                return false;
            }
        }

        #endregion

        #region Foreign Content

        /// <summary>
        /// 8.2.5.5 The rules for parsing tokens in foreign content
        /// </summary>
        /// <param name="token">The token to examine.</param>
        private void Foreign(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    AddCharacters(token.Data.Replace(Symbols.Null, Symbols.Replacement));
                    _frameset = !token.HasContent && _frameset;
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;
                    var tag = token.AsTag();

                    if (tagName.Is(TagNames.Font))
                    {
                        for (var i = 0; i != tag.Attributes.Count; i++)
                        {
                            if (tag.Attributes[i].Key.IsOneOf(AttributeNames.Color, AttributeNames.Face, AttributeNames.Size))
                            {
                                ForeignNormalTag(tag);
                                return;
                            }
                        }

                        ForeignSpecialTag(tag);
                    }
                    else if (TagNames.AllForeignExceptions.Contains(tagName))
                    {
                        ForeignNormalTag(tag);
                    }
                    else
                    {
                        ForeignSpecialTag(tag);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;
                    var node = CurrentNode;
                    var script = node as HtmlScriptElement;

                    if (script != null)
                    {
                        HandleScript(script);
                        return;
                    }

                    if (!node.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagClosingMismatch, token);
                    }

                    for (var i = _openElements.Count - 1; i > 0; i--)
                    {
                        if (node.LocalName.Isi(tagName))
                        {
                            CloseNodesFrom(i);
                            break;
                        }

                        node = _openElements[i - 1];

                        if ((node.Flags & NodeFlags.HtmlMember) == NodeFlags.HtmlMember)
                        {
                            Home(token);
                            break;
                        }
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
                    return;
                }
            }
        }

        /// <summary>
        /// Processes a special start tag token.
        /// </summary>
        /// <param name="tag">The tag token to process.</param>
        private void ForeignSpecialTag(HtmlTagToken tag)
        {
            var node = CreateForeignElementFrom(tag);

            if (node != null)
            {
                var selfClosing = tag.IsSelfClosing;
                CurrentNode.AddNode(node);

                if (selfClosing)
                {
                    node.SetupElement();
                }

                if (!selfClosing)
                {
                    _openElements.Add(node);
                    _tokenizer.IsAcceptingCharacterData = true;
                }
                else if (tag.Name.Is(TagNames.Script))
                {
                    Foreign(HtmlTagToken.Close(TagNames.Script));
                }
            }
        }

        /// <summary>
        /// Creates a foreign element from the given html tag.
        /// </summary>
        /// <param name="tag">The tag of the foreign element.</param>
        /// <returns>The element or NULL if it is no MathML or SVG element.</returns>
        private Element CreateForeignElementFrom(HtmlTagToken tag)
        {
            if ((AdjustedCurrentNode.Flags & NodeFlags.MathMember) == NodeFlags.MathMember)
            {
                var tagName = tag.Name;
                var element = _mathFactory.Create(_document, tagName);
                AuxiliarySetupSteps(element, tag);
                return element.Setup(tag);
            }
            else if ((AdjustedCurrentNode.Flags & NodeFlags.SvgMember) == NodeFlags.SvgMember)
            {
                var tagName = tag.Name.SanatizeSvgTagName();
                var element = _svgFactory.Create(_document, tagName);
                AuxiliarySetupSteps(element, tag);
                return element.Setup(tag);
            }

            return null;
        }

        /// <summary>
        /// Processes a normal start tag token.
        /// </summary>
        /// <param name="tag">The token to process.</param>
        private void ForeignNormalTag(HtmlTagToken tag)
        {
            RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, tag);

            if (!IsFragmentCase)
            {
                const NodeFlags Annotated = NodeFlags.HtmlTip | NodeFlags.MathTip | NodeFlags.HtmlMember;
                var node = CurrentNode;

                do
                {
                    if (node.LocalName.Is(TagNames.AnnotationXml))
                    {
                        var value = node.GetAttribute(null, AttributeNames.Encoding);

                        if (value.Isi(MimeTypeNames.Html) || value.Isi(MimeTypeNames.ApplicationXHtml))
                        {
                            AddElement(tag);
                            return;
                        }
                    }

                    CloseCurrentNode();
                    node = CurrentNode;
                }
                while ((node.Flags & Annotated) == NodeFlags.None);

                Consume(tag);
            }
            else
            {
                ForeignSpecialTag(tag);
            }
        }

        #endregion

        #region Scope

        /// <summary>
        /// Determines if the given tag name is in the global scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInScope(String tagName)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(tagName))
                {
                    return true;
                }
                else if ((node.Flags & NodeFlags.Scoped) == NodeFlags.Scoped)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the given type is in the global scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInScope(HashSet<String> tags)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (tags.Contains(node.LocalName))
                {
                    return true;
                }
                else if ((node.Flags & NodeFlags.Scoped) == NodeFlags.Scoped)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the list scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInListItemScope()
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(TagNames.Li))
                {
                    return true;
                }
                else if ((node.Flags & NodeFlags.HtmlListScoped) == NodeFlags.HtmlListScoped)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if a paragraph is in the button scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInButtonScope()
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(TagNames.P))
                {
                    return true;
                }
                else if (((node.Flags & NodeFlags.Scoped) == NodeFlags.Scoped) || node.LocalName.Is(TagNames.Button))
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the given type is in the table scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInTableScope(HashSet<String> tags)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (tags.Contains(node.LocalName))
                {
                    return true;
                }
                else if ((node.Flags & NodeFlags.HtmlTableScoped) == NodeFlags.HtmlTableScoped)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the table scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInTableScope(String tagName)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(tagName))
                {
                    return true;
                }
                else if ((node.Flags & NodeFlags.HtmlTableScoped) == NodeFlags.HtmlTableScoped)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the select scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInSelectScope(String tagName)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(tagName))
                {
                    return true;
                }
                else if ((node.Flags & NodeFlags.HtmlSelectScoped) != NodeFlags.HtmlSelectScoped)
                {
                    return false;
                }
            }

            return false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Runs a script given by the current node.
        /// </summary>
        private void HandleScript(HtmlScriptElement script)
        {
            if (script != null)
            {
                //Disable scripting for HTML fragments (security reasons)
                if (IsFragmentCase)
                {
                    CloseCurrentNode();
                    _currentMode = _previousMode;
                }
                else
                {
                    _document.PerformMicrotaskCheckpoint();
                    _document.ProvideStableState();
                    CloseCurrentNode();
                    _currentMode = _previousMode;

                    if (script.Prepare(_document))
                    {
                        _waiting = RunScript(script);
                    }
                }
            }
        }

        /// <summary>
        /// Runs the current script element, if there is one.
        /// </summary>
        /// <returns>The task waiting for the document to be ready.</returns>
        private async Task RunScript(HtmlScriptElement script)
        {
            await _document.WaitForReadyAsync().ConfigureAwait(false);
            await script.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// If there is a node in the stack of open elements that is not either
        /// a dd element, a dt element, an li element, a p element, a tbody
        /// element, a td element, a tfoot element, a th element, a thead
        /// element, a tr element, the body element, or the html element, then
        /// this is a parse error.
        /// </summary>
        private void CheckBodyOnClosing(HtmlToken token)
        {
            for (var i = 0; i < _openElements.Count; i++)
            {
                if ((_openElements[i].Flags & NodeFlags.ImplicitelyClosed) != NodeFlags.ImplicitelyClosed)
                {
                    RaiseErrorOccurred(HtmlParseError.BodyClosedWrong, token);
                    break;
                }
            }
        }

        /// <summary>
        /// Checks if a tag with the given name is currently open.
        /// </summary>
        /// <param name="tagName">The name of the tag to check for.</param>
        /// <returns>True if such a tag is open, otherwise false.</returns>
        private Boolean TagCurrentlyOpen(String tagName)
        {
            for (var i = 0; i < _openElements.Count; i++)
            {
                if (_openElements[i].LocalName.Is(tagName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the next token and removes the starting newline, if it has one.
        /// </summary>
        private void PreventNewLine()
        {
            var temp = _tokenizer.Get();

            if (temp.Type == HtmlTokenType.Character)
            {
                temp.RemoveNewLine();
            }

            Home(temp);
        }

        /// <summary>
        /// 8.2.6 The end.
        /// </summary>
        private void End()
        {
            while (_openElements.Count != 0)
            {
                CloseCurrentNode();
            }

            if (_document.IsLoading)
            {
                _waiting = _document.FinishLoadingAsync();
            }
        }

        #endregion

        #region Appending Nodes

        /// <summary>
        /// Adds the root element (html) to the document.
        /// </summary>
        /// <param name="tag">The token which started this process.</param>
        private void AddRoot(HtmlTagToken tag)
        {
            var element = new HtmlHtmlElement(_document);
            _document.AddNode(element);
            SetupElement(element, tag, false);
            _openElements.Add(element);
            _tokenizer.IsAcceptingCharacterData = false;
            _document.ApplyManifest();
        }

        private void CloseNode(Element element)
        {
            element.SetupElement();
            _openElements.Remove(element);
        }

        private void CloseNodesFrom(Int32 index)
        {
            for (var i = _openElements.Count - 1; i > index; i--)
            {
                _openElements[i].SetupElement();
                _openElements.RemoveAt(i);
            }

            CloseCurrentNode();
        }

        /// <summary>
        /// Pops the last node from the stack of open nodes.
        /// </summary>
        private void CloseCurrentNode()
        {
            if (_openElements.Count > 0)
            {
                var index = _openElements.Count - 1;
                _openElements[index].SetupElement();
                _openElements.RemoveAt(index);
                var node = AdjustedCurrentNode;
                _tokenizer.IsAcceptingCharacterData = node != null && ((node.Flags & NodeFlags.HtmlMember) != NodeFlags.HtmlMember);
            }
        }

        /// <summary>
        /// Modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        private void SetupElement(Element element, HtmlTagToken tag, Boolean acknowledgeSelfClosing)
        {
            if (tag.IsSelfClosing && !acknowledgeSelfClosing)
            {
                RaiseErrorOccurred(HtmlParseError.TagCannotBeSelfClosed, tag);
            }

            AuxiliarySetupSteps(element, tag);
            element.SetAttributes(tag.Attributes);
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        private Element AddElement(HtmlTagToken tag, Boolean acknowledgeSelfClosing = false)
        {
            var element = _htmlFactory.Create(_document, tag.Name);
            SetupElement(element, tag, acknowledgeSelfClosing);
            AddElement(element);
            return element;
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        private void AddElement(Element element, HtmlTagToken tag, Boolean acknowledgeSelfClosing = false)
        {
            SetupElement(element, tag, acknowledgeSelfClosing);
            AddElement(element);
        }

        /// <summary>
        /// Appends a configured node to the current node.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        private void AddElement(Element element)
        {
            var node = CurrentNode;

            if (_foster && TagNames.AllTableMajor.Contains(node.LocalName))
            {
                AddElementWithFoster(element);
            }
            else
            {
                node.AddNode(element);
            }

            _openElements.Add(element);
            _tokenizer.IsAcceptingCharacterData = (element.Flags & NodeFlags.HtmlMember) != NodeFlags.HtmlMember;
        }

        /// <summary>
        /// Appends a node to the appropriate foster parent.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#foster-parent
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        private void AddElementWithFoster(Element element)
        {
            var table = false;
            var index = _openElements.Count;

            while (--index != 0)
            {
                if (_openElements[index].LocalName.Is(TagNames.Template))
                {
                    _openElements[index].AddNode(element);
                    return;
                }
                else if (_openElements[index].LocalName.Is(TagNames.Table))
                {
                    table = true;
                    break;
                }
            }

            var foster = _openElements[index].Parent ?? _openElements[index + 1];

            if (table && _openElements[index].Parent != null)
            {
                for (var i = 0; i < foster.ChildNodes.Length; i++)
			    {
                    if (foster.ChildNodes[i] == _openElements[index])
                    {
                        foster.InsertNode(i, element);
                        break;
                    }
			    }
            }
            else
            {
                foster.AddNode(element);
            }
        }

        /// <summary>
        /// Inserts the given characters into the current node.
        /// </summary>
        /// <param name="text">The characters to insert.</param>
        private void AddCharacters(String text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var node = CurrentNode;

                if (_foster && TagNames.AllTableMajor.Contains(node.LocalName))
                {
                    AddCharactersWithFoster(text);
                }
                else
                {
                    node.AppendText(text);
                }
            }
        }

        /// <summary>
        /// Inserts the given character into the foster parent.
        /// </summary>
        /// <param name="text">The character to insert.</param>
        private void AddCharactersWithFoster(String text)
        {
            var table = false;
            var index = _openElements.Count;

            while (--index != 0)
            {
                if (_openElements[index].LocalName.Is(TagNames.Template))
                {
                    _openElements[index].AppendText(text);
                    return;
                }
                else if (_openElements[index].LocalName.Is(TagNames.Table))
                {
                    table = true;
                    break;
                }
            }

            var foster = _openElements[index].Parent ?? _openElements[index + 1];

            if (table && _openElements[index].Parent != null)
            {
                for (var i = 0; i < foster.ChildNodes.Length; i++)
                {
                    if (foster.ChildNodes[i] == _openElements[index])
                    {
                        foster.InsertText(i, text);
                        break;
                    }
                }
            }
            else
            {
                foster.AppendText(text);
            }
        }

        private void AuxiliarySetupSteps(Element element, HtmlTagToken tag)
        {
            if (_options.OnCreated != null)
            {
                _options.OnCreated.Invoke(element, tag.Position);
            }
        }

        #endregion

        #region Closing Nodes

        /// <summary>
        /// Clears the stack of open elements back to the given element name.
        /// </summary>
        /// <param name="tagName">The tag that will be the CurrentNode.</param>
        private void ClearStackBackTo(String tagName)
        {
            var node = CurrentNode;

            while (!node.LocalName.IsOneOf(tagName, TagNames.Html, TagNames.Template))
            {
                CloseCurrentNode();
                node = CurrentNode;
            }
        }

        /// <summary>
        /// Clears the stack of open elements back to any heading element.
        /// </summary>
        private void ClearStackBackTo(HashSet<String> tags)
        {
            var node = CurrentNode;

            while (!tags.Contains(node.LocalName) && !node.LocalName.IsOneOf(TagNames.Html, TagNames.Template))
            {
                CloseCurrentNode();
                node = CurrentNode;
            }
        }

        /// <summary>
        /// Generates the implied end tags for the dd, dt, li, option, optgroup, p, rp, rt elements except for
        /// the tag given.
        /// </summary>
        /// <param name="tagName">The tag that will be excluded.</param>
        private void GenerateImpliedEndTagsExceptFor(String tagName)
        {
            var node = CurrentNode;

            while (((node.Flags & NodeFlags.ImpliedEnd) == NodeFlags.ImpliedEnd) && !node.LocalName.Is(tagName))
            {
                CloseCurrentNode();
                node = CurrentNode;
            }
        }

        /// <summary>
        /// Generates the implied end tags for the dd, dt, li, option, optgroup, p, rp, rt elements.
        /// </summary>
        private void GenerateImpliedEndTags()
        {
            while ((CurrentNode.Flags & NodeFlags.ImpliedEnd) == NodeFlags.ImpliedEnd)
            {
                CloseCurrentNode();
            }
        }

        #endregion

        #region Formatting

        /// <summary>
        /// Reconstruct the list of active formatting elements, if any.
        /// </summary>
        private void ReconstructFormatting()
        {
            if (_formattingElements.Count == 0)
                return;

            var index = _formattingElements.Count - 1;
            var entry = _formattingElements[index];

            if (entry == null || _openElements.Contains(entry))
                return;

            while (index > 0)
            {
                entry = _formattingElements[--index];

                if (entry == null || _openElements.Contains(entry))
                {
                    index++;
                    break;
                }
            }

            for (; index < _formattingElements.Count; index++)
            {
                var element = CopyElement(_formattingElements[index]);
                AddElement(element);
                _formattingElements[index] = element;
            }
        }

        #endregion

        #region Handlers

        private void RaiseErrorOccurred(HtmlParseError code, HtmlToken token)
        {
            _tokenizer.RaiseErrorOccurred(code, token.Position);
        }

        #endregion
    }
}
