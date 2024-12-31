namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Construction;
    using Dom;
    using Tokens.Struct;

    /// <summary>
    /// Represents the Tree construction as specified in
    /// 8.2.5 Tree construction, on the following page:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    class HtmlDomBuilder<TDocument, TElement> : IDisposable
        where TElement : class, IConstructableElement
        where TDocument : class, IConstructableDocument
    {
        #region Fields

        private readonly TokenConsumer _consumeAsDelegate;
        private readonly HtmlTokenizer _tokenizer;
        private readonly TDocument _document;
        private readonly List<IConstructableElement> _openElements;
        private readonly List<IConstructableElement> _formattingElements;
        private readonly Stack<HtmlTreeMode> _templateModes;

        private IConstructableElement? _currentFormElement;

        private HtmlTreeMode _currentMode;
        private HtmlTreeMode _previousMode;

        private HtmlParserOptions _options;

        private IConstructableElement? _fragmentContext;

        private Boolean _foster;
        private Boolean _frameset;
        private Boolean _ended;

        private Func<IConstructableElement, Boolean>? _shouldEnd;
        private readonly IDomConstructionElementFactory<TDocument, TElement> _elementFactory;
        private Task? _waiting;
        private readonly Boolean _emitWhitespaceTextNodes;

        #endregion

        #region Events

        public event EventHandler<HtmlErrorEvent> Error
        {
            add { _tokenizer.Error += value; }
            remove { _tokenizer.Error -= value; }
        }

        #endregion

        #region ctor

        public HtmlDomBuilder(
            IDomConstructionElementFactory<TDocument, TElement> elementFactory,
            TDocument document,
            HtmlTokenizerOptions? maybeOptions = null,
            Boolean emitWhitespaceTextNodes = false,
            Func<IConstructableElement, Boolean>? shouldEnd = null)
        {
            _shouldEnd = shouldEnd;
            _elementFactory = elementFactory;
            _tokenizer = new HtmlTokenizer(document.Source, HtmlEntityProvider.ResolverExtended);
            _document = document;
            _openElements = [];
            _templateModes = new Stack<HtmlTreeMode>();
            _formattingElements = [];
            _frameset = true;
            _currentMode = HtmlTreeMode.Initial;
            _ended = false;
            _consumeAsDelegate = Consume;
            _emitWhitespaceTextNodes = emitWhitespaceTextNodes;
            document.Builder = this;

            if (maybeOptions.HasValue)
            {
                var options = maybeOptions.Value;

                _tokenizer.IsStrictMode = options.IsStrictMode;
                _tokenizer.IsSupportingProcessingInstructions = options.IsSupportingProcessingInstructions;
                _tokenizer.IsNotConsumingCharacterReferences = options.IsNotConsumingCharacterReferences;
                _tokenizer.IsPreservingAttributeNames = options.IsPreservingAttributeNames;
                _tokenizer.SkipRawText = options.SkipRawText;
                _tokenizer.SkipScriptText = options.SkipScriptText;
                _tokenizer.SkipDataText = options.SkipDataText;
                _tokenizer.ShouldEmitAttribute = options.ShouldEmitAttribute;
                _tokenizer.SkipDataText = options.SkipDataText;
                _tokenizer.SkipScriptText = options.SkipScriptText;
                _tokenizer.SkipRawText = options.SkipRawText;
                _tokenizer.SkipComments = options.SkipComments;
                _tokenizer.SkipPlaintext = options.SkipPlaintext;
                _tokenizer.SkipRCDataText = options.SkipRCDataText;
                _tokenizer.SkipCDATA = options.SkipCDATA;
                _tokenizer.SkipProcessingInstructions = options.SkipProcessingInstructions;
                _tokenizer.DisableElementPositionTracking = options.DisableElementPositionTracking;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the tree builder has been created for parsing fragments.
        /// </summary>
        public Boolean IsFragmentCase => _fragmentContext is not null;

        /// <summary>
        /// Gets the adjusted current node.
        /// </summary>
        public IConstructableElement? AdjustedCurrentNode =>
            (_fragmentContext is not null && _openElements.Count == 1) ? _fragmentContext : CurrentNode;

        /// <summary>
        /// Gets the current node.
        /// </summary>
        public IConstructableElement CurrentNode => _openElements.Count > 0 ? _openElements[_openElements.Count - 1] : null!;

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        /// <param name="middleware"></param>
        public TDocument Parse(HtmlParserOptions options, TokenizerMiddleware? middleware = null)
        {
            SetOptions(options);

            do
            {
                ref var token = ref _tokenizer.GetStructToken();
                if (token.Type == HtmlTokenType.EndOfFile)
                {
                    Consume(ref token);
                    break;
                }

                if (middleware is null)
                {
                    Consume(ref token);
                }
                else
                {
                    var result = middleware(ref token, _consumeAsDelegate);

                    if (result == TokenConsumptionResult.Stop)
                    {
                        var EOF = StructHtmlToken.EndOfFile(default);
                        Consume(ref EOF);
                        break;
                    }
                }

            } while (!_ended);

            return _document;
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        /// <param name="middleware"></param>
        /// <param name="cancelToken">The cancellation token to use.</param>
        public async Task<TDocument> ParseAsync(HtmlParserOptions options, TokenizerMiddleware? middleware = null,
            CancellationToken cancelToken = default)
        {
            var source = _document.Source;
            SetOptions(options);
            middleware ??= static (ref StructHtmlToken token, TokenConsumer next) =>
            {
                next(ref token);
                return TokenConsumptionResult.Continue;
            };

            do
            {
                if (source.Length - source.Index < 1024)
                {
                    await source.PrefetchAsync(8192, cancelToken).ConfigureAwait(false);
                }
                cancelToken.ThrowIfCancellationRequested();

                var @break = Worker(middleware);
                if (@break) { break; }

                if (_waiting is not null)
                {
                    await _waiting.ConfigureAwait(false);
                    _waiting = null;
                }
            } while (!_ended);

            if (_waiting is not null)
            {
                await _waiting.ConfigureAwait(false);
                _waiting = null;
            }

            return _document;

            Boolean Worker(TokenizerMiddleware middleware)
            {
                var token = _tokenizer.GetStructToken();
                if (token.Type == HtmlTokenType.EndOfFile)
                {
                    Consume(ref token);
                    return true;
                }
                var result = middleware(ref token, _consumeAsDelegate);
                if (result == TokenConsumptionResult.Stop)
                {
                    var EOF = StructHtmlToken.EndOfFile(default);
                    Consume(ref EOF);
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Restarts the parser by resetting the internal state.
        /// </summary>
        private void Restart()
        {
            _currentMode = HtmlTreeMode.Initial;
            _tokenizer.State = HtmlParseMode.PCData;
            _document.Clear();
            _ended = false;
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

                if (last && _fragmentContext is not null)
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
        /// Switches to the fragment algorithm with the specified context
        /// element. Then parses the given source and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        /// <param name="context">
        /// The context element where the algorithm is applied to.
        /// </param>
        public TDocument ParseFragment(HtmlParserOptions options, TElement context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            _fragmentContext = context;

            var tagName = context.LocalName;

            if (tagName.IsOneOf(TagNames.Title, TagNames.Textarea))
            {
                _tokenizer.State = HtmlParseMode.RCData;
            }
            else if (tagName.IsOneOf(TagNames.Style, TagNames.Xmp, TagNames.Iframe, TagNames.NoEmbed))
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
            else if (tagName.Is(TagNames.NoScript) &&
                     (options.IsScripting || context.Flags.HasFlag(NodeFlags.LiteralText)))
            {
                _tokenizer.State = HtmlParseMode.Rawtext;
            }
            else if (tagName.Is(TagNames.NoFrames) && !options.IsNotSupportingFrames)
            {
                _tokenizer.State = HtmlParseMode.Rawtext;
            }

            var root = _elementFactory.Create(_document, TagNames.Html);

            _document.AddNode(root);
            _openElements.Add(root);

            if (context is IConstructableTemplateElement)
            {
                _templateModes.Push(HtmlTreeMode.InTemplate);
            }

            Reset();

            _tokenizer.IsAcceptingCharacterData = !AdjustedCurrentNode!.Flags.HasFlag(NodeFlags.HtmlMember);

            IConstructableNode? contextNode = context;

            do
            {
                if (contextNode is IConstructableFormElement formEl)
                {
                    _currentFormElement = formEl;
                    break;
                }

                contextNode = contextNode.Parent;
            }
            while (contextNode is not null);

            return Parse(options);
        }

        /// <summary>
        /// Consumes a token and processes it.0
        /// </summary>
        /// <param name="token">The token to consume.</param>
        private void Consume(ref StructHtmlToken token)
        {
            var node = AdjustedCurrentNode;
            
            if (node is null || token.Type == HtmlTokenType.EndOfFile ||
                node.Flags.HasFlag(NodeFlags.HtmlMember) ||
                (node.Flags.HasFlag(NodeFlags.HtmlTip) && token.IsHtmlCompatible) ||
                (node.Flags.HasFlag(NodeFlags.MathTip) && token.IsMathCompatible) ||
                (node.Flags.HasFlag(NodeFlags.MathMember) && token.IsSvg &&
                 node.LocalName.Is(TagNames.AnnotationXml)))
            {
                Home(ref token);
            }
            else
            {
                Foreign(ref token);
            }
        }

        private void SetOptions(HtmlParserOptions options)
        {
            _tokenizer.IsStrictMode = options.IsStrictMode;
            _tokenizer.IsSupportingProcessingInstructions = options.IsSupportingProcessingInstructions;
            _tokenizer.IsNotConsumingCharacterReferences = options.IsNotConsumingCharacterReferences;
            _tokenizer.IsPreservingAttributeNames = options.IsPreservingAttributeNames;
            _tokenizer.SkipRawText = options.SkipRawText;
            _tokenizer.SkipScriptText = options.SkipScriptText;
            _tokenizer.SkipDataText = options.SkipDataText;
            _tokenizer.SkipScriptText = options.SkipScriptText;
            _tokenizer.SkipRawText = options.SkipRawText;
            _tokenizer.SkipComments = options.SkipComments;
            _tokenizer.SkipPlaintext = options.SkipPlaintext;
            _tokenizer.SkipRCDataText = options.SkipRCDataText;
            _tokenizer.SkipCDATA = options.SkipCDATA;
            _tokenizer.SkipProcessingInstructions = options.SkipProcessingInstructions;
            _tokenizer.ShouldEmitAttribute = options.ShouldEmitAttribute!;
            _tokenizer.DisableElementPositionTracking = options.DisableElementPositionTracking;
            _tokenizer.OnToken = options.OnToken;
            _options = options;
        }

        #endregion

        #region Home

        /// <summary>
        /// Takes the method corresponding to the current insertation mode.
        /// </summary>
        /// <param name="token">The token to insert / use.</param>
        private void Home(ref StructHtmlToken token)
        {
            switch (_currentMode)
            {
                case HtmlTreeMode.Initial:
                    Initial(ref token);
                    return;

                case HtmlTreeMode.BeforeHtml:
                    BeforeHtml(ref token);
                    return;

                case HtmlTreeMode.BeforeHead:
                    BeforeHead(ref token);
                    return;

                case HtmlTreeMode.InHead:
                    InHead(ref token);
                    return;

                case HtmlTreeMode.InHeadNoScript:
                    InHeadNoScript(ref token);
                    return;

                case HtmlTreeMode.AfterHead:
                    AfterHead(ref token);
                    return;

                case HtmlTreeMode.InBody:
                    InBody(ref token);
                    return;

                case HtmlTreeMode.Text:
                    Text(ref token);
                    return;

                case HtmlTreeMode.InTable:
                    InTable(ref token);
                    return;

                case HtmlTreeMode.InCaption:
                    InCaption(ref token);
                    return;

                case HtmlTreeMode.InColumnGroup:
                    InColumnGroup(ref token);
                    return;

                case HtmlTreeMode.InTableBody:
                    InTableBody(ref token);
                    return;

                case HtmlTreeMode.InRow:
                    InRow(ref token);
                    return;

                case HtmlTreeMode.InCell:
                    InCell(ref token);
                    return;

                case HtmlTreeMode.InSelect:
                    InSelect(ref token);
                    return;

                case HtmlTreeMode.InSelectInTable:
                    InSelectInTable(ref token);
                    return;

                case HtmlTreeMode.InTemplate:
                    InTemplate(ref token);
                    return;

                case HtmlTreeMode.AfterBody:
                    AfterBody(ref token);
                    return;

                case HtmlTreeMode.InFrameset:
                    InFrameset(ref token);
                    return;

                case HtmlTreeMode.AfterFrameset:
                    AfterFrameset(ref token);
                    return;

                case HtmlTreeMode.AfterAfterBody:
                    AfterAfterBody(ref token);
                    return;

                case HtmlTreeMode.AfterAfterFrameset:
                    AfterAfterFrameset(ref token);
                    return;
            }
        }

        /// <summary>
        /// See 8.2.5.4.1 The "initial" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void Initial(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Doctype:
                {
                    if (!token.IsValid)
                    {
                        RaiseErrorOccurred(HtmlParseError.DoctypeInvalid, ref token);
                    }

                    var doctype = _elementFactory.CreateDocumentType(_document, token.Name, token.PublicIdentifier,
                        token.SystemIdentifier);

                    _document.AddNode(doctype);

                    _document.QuirksMode = token.GetQuirksMode();
                    _currentMode = HtmlTreeMode.BeforeHtml;
                    return;
                }
                case HtmlTokenType.Character:
                {
                    token.CleanStart();

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    _document.AddComment(ref token);
                    return;
                }
            }

            if (!_options.IsEmbedded)
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeMissing, ref token);
                _document.QuirksMode = QuirksMode.On;
            }

            _currentMode = HtmlTreeMode.BeforeHtml;
            BeforeHtml(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.2 The "before html" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void BeforeHtml(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    token.CleanStart();

                    if (!token.IsEmpty)
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    _document.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (!token.Name.Is(TagNames.Html))
                    {
                        break;
                    }

                    AddRoot(ref token);
                    _currentMode = HtmlTreeMode.BeforeHead;
                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (TagNames.AllBeforeHead.Contains(token.Name))
                    {
                        break;
                    }

                    RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
            }

            var temp = StructHtmlToken.Open(TagNames.Html);
            BeforeHtml(ref temp);
            BeforeHead(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.3 The "before head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void BeforeHead(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    token.CleanStart();

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
                        InBody(ref token);
                        return;
                    }
                    else if (tagName.Is(TagNames.Head))
                    {
                        var head = _elementFactory.Create(_document, TagNames.Head);
                        AddElement(head, ref token);
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

                    RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
            }

            var temp = StructHtmlToken.Open(TagNames.Head);
            BeforeHead(ref temp);
            InHead(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.4 The "in head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InHead(ref StructHtmlToken token)
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
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                        return;
                    }
                    else if (tagName.Is(TagNames.Meta))
                    {
                        var element = _elementFactory.CreateMeta(_document);
                        AddElement(element, ref token, acknowledgeSelfClosing: true);
                        CloseCurrentNode();

                        try
                        {
                            element.Handle();
                        }
                        catch (NotSupportedException ex)
                        {
                            _document.TrackError(ex);
                            Restart();
                        }

                        return;
                    }
                    else if (TagNames.AllHeadBase.Contains(tagName))
                    {
                        AddElement(ref token, acknowledgeSelfClosing: true);
                        CloseCurrentNode();
                        return;
                    }
                    else if (tagName.Is(TagNames.Title))
                    {
                        RCDataAlgorithm(ref token);
                        return;
                    }
                    else if (tagName.Is(TagNames.Style))
                    {
                        RawtextAlgorithm(ref token);
                        return;
                    }
                    else if (tagName.Is(TagNames.NoScript))
                    {
                        var scripting = _options.IsScripting;
                        var element = _elementFactory.CreateNoScript(_document, scripting);
                        AddElement(element, ref token);

                        if (scripting)
                        {
                            SwitchToRawtext();
                        }
                        else
                        {
                            _currentMode = HtmlTreeMode.InHeadNoScript;
                        }

                        return;
                    }
                    else if (tagName.Is(TagNames.NoFrames))
                    {
                        if (_options.IsNotSupportingFrames)
                        {
                            AddElement(ref token);
                            _currentMode = HtmlTreeMode.InBody;
                        }
                        else
                        {
                            RawtextAlgorithm(ref token);
                        }

                        return;
                    }
                    else if (tagName.Is(TagNames.Script))
                    {
                        var script = _elementFactory.CreateScript(_document, parserInserted: true, started: IsFragmentCase);
                        AddElement(script, ref token);
                        _tokenizer.State = HtmlParseMode.Script;
                        _previousMode = _currentMode;
                        _currentMode = HtmlTreeMode.Text;
                        return;
                    }
                    else if (tagName.Is(TagNames.Head))
                    {
                        RaiseErrorOccurred(HtmlParseError.HeadTagMisplaced, ref token);
                        return;
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        AddTemplateElement(ref token);
                        return;
                    }
                    else if (IsCustomElementEverywhere(tagName))
                    {
                        AddElement(ref token);
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
                        _waiting = _document.WaitForReadyAsync(CancellationToken.None);
                        return;
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        if (TagCurrentlyOpen(TagNames.Template))
                        {
                            GenerateImpliedEndTags();

                            if (!CurrentNode.LocalName.Is(TagNames.Template))
                            {
                                RaiseErrorOccurred(HtmlParseError.TagClosingMismatch, ref token);
                            }

                            CloseTemplate();
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TagInappropriate, ref token);
                        }

                        return;
                    }
                    else if (IsCustomElementEverywhere(tagName))
                    {
                        GenerateImpliedEndTags();
                        CloseCurrentNode();
                        return;
                    }
                    else if (!tagName.IsOneOf(TagNames.Html, TagNames.Body, TagNames.Br))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                        return;
                    }

                    break;
                }
            }

            CloseCurrentNode();
            _currentMode = HtmlTreeMode.AfterHead;
            AfterHead(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.5 The "in head noscript" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InHeadNoScript(ref StructHtmlToken token)
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
                    InHead(ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllNoScript.Contains(tagName))
                    {
                        InHead(ref token);
                    }
                    else if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                    }
                    else if (tagName.IsOneOf(TagNames.Head, TagNames.NoScript))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagInappropriate, ref token);
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
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
            CloseCurrentNode();
            _currentMode = HtmlTreeMode.InHead;
            InHead(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.6 The "after head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterHead(ref StructHtmlToken token)
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
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                    }
                    else if (tagName.Is(TagNames.Body))
                    {
                        AfterHeadStartTagBody(ref token);
                    }
                    else if (tagName.Is(TagNames.Frameset))
                    {
                        var frameSetElement = _elementFactory.Create(_document, TagNames.Frameset);
                        AddElement(frameSetElement, ref token);
                        _currentMode = HtmlTreeMode.InFrameset;
                    }
                    else if (TagNames.AllHeadNoTemplate.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagMustBeInHead, ref token);
                        var head = _document.Head;
                        _openElements.Add(head!);
                        InHead(ref token);
                        CloseNode(head!);
                    }
                    else if (tagName.Is(TagNames.Head))
                    {
                        RaiseErrorOccurred(HtmlParseError.HeadTagMisplaced, ref token);
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

                    RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    return;
                }
            }

            var temp = StructHtmlToken.Open(TagNames.Body);
            AfterHeadStartTagBody(ref temp);
            _frameset = true;
            Home(ref token);
        }

        private void InBodyStartTag(ref StructHtmlToken tag)
        {
            var tagName = tag.Name;

            if (tagName.Is(TagNames.Div))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                AddElement(ref tag);
            }
            else if (tagName.Is(TagNames.A))
            {
                for (var i = _formattingElements.Count - 1; i >= 0 && _formattingElements[i] is not null; i--)
                {
                    if (_formattingElements[i].LocalName.Is(TagNames.A))
                    {
                        var format = _formattingElements[i];
                        RaiseErrorOccurred(HtmlParseError.AnchorNested, ref tag);
                        var temp = StructHtmlToken.Close(TagNames.A);
                        HeisenbergAlgorithm(ref temp);
                        CloseNode(format);
                        _formattingElements.Remove(format);
                        break;
                    }
                }

                ReconstructFormatting();
                var element = _elementFactory.Create(_document, TagNames.A);
                AddElement(element, ref tag);
                _formattingElements.AddFormatting(element);
            }
            else if (tagName.Is(TagNames.Span))
            {
                ReconstructFormatting();
                AddElement(ref tag);
            }
            else if (tagName.Is(TagNames.Li))
            {
                InBodyStartTagListItem(ref tag);
            }
            else if (tagName.Is(TagNames.Img))
            {
                InBodyStartTagBreakrow(ref tag);
            }
            else if (tagName.IsOneOf(TagNames.Ul, TagNames.P))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                AddElement(ref tag);
            }
            else if (TagNames.AllSemanticFormatting.Contains(tagName))
            {
                ReconstructFormatting();
                _formattingElements.AddFormatting(AddElement(ref tag));
            }
            else if (tagName.Is(TagNames.Script))
            {
                InHead(ref tag);
            }
            else if (TagNames.AllHeadings.Contains(tagName))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                if (TagNames.AllHeadings.Contains(CurrentNode.LocalName))
                {
                    RaiseErrorOccurred(HtmlParseError.HeadingNested, ref tag);
                    CloseCurrentNode();
                }

                var headingElement = _elementFactory.Create(_document, tagName);
                AddElement(headingElement, ref tag);
            }
            else if (tagName.Is(TagNames.Input))
            {
                ReconstructFormatting();
                var inputElement = _elementFactory.Create(_document, TagNames.Input);
                AddElement(inputElement, ref tag, acknowledgeSelfClosing: true);
                CloseCurrentNode();

                if (!tag.GetAttribute(AttributeNames.Type).Isi(AttributeNames.Hidden))
                {
                    _frameset = false;
                }
            }
            else if (tagName.Is(TagNames.Form))
            {
                if (_currentFormElement is null)
                {
                    if (IsInButtonScope())
                    {
                        InBodyEndTagParagraph(ref tag);
                    }

                    _currentFormElement = _elementFactory.CreateForm(_document);
                    AddElement(_currentFormElement, ref tag);
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.FormAlreadyOpen, ref tag);
                }
            }
            else if (TagNames.AllBody.Contains(tagName))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                AddElement(ref tag);
            }
            else if (TagNames.AllClassicFormatting.Contains(tagName))
            {
                ReconstructFormatting();
                _formattingElements.AddFormatting(AddElement(ref tag));
            }
            else if (TagNames.AllHead.Contains(tagName))
            {
                InHead(ref tag);
            }
            else if (tagName.IsOneOf(TagNames.Pre, TagNames.Listing))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                AddElement(ref tag);
                _frameset = false;
                PreventNewLine();
            }
            else if (tagName.Is(TagNames.Button))
            {
                if (IsInScope(TagNames.Button))
                {
                    RaiseErrorOccurred(HtmlParseError.ButtonInScope, ref tag);
                    InBodyEndTagBlock(ref tag);
                    InBody(ref tag);
                }
                else
                {
                    ReconstructFormatting();
                    var button = _elementFactory.Create(_document, TagNames.Button);
                    AddElement(button, ref tag);
                    _frameset = false;
                }
            }
            else if (tagName.Is(TagNames.Table))
            {
                if (_document.QuirksMode != QuirksMode.On && IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                var table = _elementFactory.Create(_document, TagNames.Table);
                AddElement(table, ref tag);
                _frameset = false;
                _currentMode = HtmlTreeMode.InTable;
            }
            else if (TagNames.AllBodyBreakrow.Contains(tagName))
            {
                InBodyStartTagBreakrow(ref tag);
            }
            else if (TagNames.AllBodyClosed.Contains(tagName))
            {
                AddElement(ref tag, acknowledgeSelfClosing: true);
                CloseCurrentNode();
            }
            else if (tagName.Is(TagNames.Hr))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                var hr = _elementFactory.Create(_document, TagNames.Hr);
                AddElement(hr, ref tag, acknowledgeSelfClosing: true);
                CloseCurrentNode();
                _frameset = false;
            }
            else if (tagName.Is(TagNames.Textarea))
            {
                var textarea = _elementFactory.Create(_document, TagNames.Textarea);
                AddElement(textarea, ref tag);
                _tokenizer.State = HtmlParseMode.RCData;
                _previousMode = _currentMode;
                _frameset = false;
                _currentMode = HtmlTreeMode.Text;
                PreventNewLine();
            }
            else if (tagName.Is(TagNames.Select))
            {
                ReconstructFormatting();
                var select = _elementFactory.Create(_document, TagNames.Select);
                AddElement(select, ref tag);
                _frameset = false;
                _currentMode = _currentMode switch
                {
                    HtmlTreeMode.InTable or HtmlTreeMode.InTableBody or HtmlTreeMode.InCaption or HtmlTreeMode.InRow or HtmlTreeMode.InCell => HtmlTreeMode.InSelectInTable,
                    _ => HtmlTreeMode.InSelect,
                };
            }
            else if (tagName.IsOneOf(TagNames.Optgroup, TagNames.Option))
            {
                if (CurrentNode.LocalName.Is(TagNames.Option))
                {
                    var temp = StructHtmlToken.Close(TagNames.Option);
                    InBodyEndTagAnythingElse(ref temp);
                }

                ReconstructFormatting();
                AddElement(ref tag);
            }
            else if (tagName.IsOneOf(TagNames.Dd, TagNames.Dt))
            {
                InBodyStartTagDefinitionItem(ref tag);
            }
            else if (tagName.Is(TagNames.Iframe))
            {
                _frameset = false;
                RawtextAlgorithm(ref tag);
            }
            else if (TagNames.AllBodyObsolete.Contains(tagName))
            {
                ReconstructFormatting();
                AddElement(ref tag);
                _formattingElements.AddScopeMarker();
                _frameset = false;
            }
            else if (tagName.Is(TagNames.Image))
            {
                RaiseErrorOccurred(HtmlParseError.ImageTagNamedWrong, ref tag);
                tag.Name = TagNames.Img;
                InBodyStartTagBreakrow(ref tag);
            }
            else if (tagName.Is(TagNames.NoBr))
            {
                ReconstructFormatting();

                if (IsInScope(TagNames.NoBr))
                {
                    RaiseErrorOccurred(HtmlParseError.NobrInScope, ref tag);
                    HeisenbergAlgorithm(ref tag);
                    ReconstructFormatting();
                }

                _formattingElements.AddFormatting(AddElement(ref tag));
            }
            else if (tagName.Is(TagNames.Xmp))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                ReconstructFormatting();
                _frameset = false;
                RawtextAlgorithm(ref tag);
            }
            else if (tagName.IsOneOf(TagNames.Rb, TagNames.Rtc))
            {
                if (IsInScope(TagNames.Ruby))
                {
                    GenerateImpliedEndTags();

                    if (!CurrentNode.LocalName.Is(TagNames.Ruby))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref tag);
                    }
                }

                AddElement(ref tag);
            }
            else if (tagName.IsOneOf(TagNames.Rp, TagNames.Rt))
            {
                if (IsInScope(TagNames.Ruby))
                {
                    GenerateImpliedEndTagsExceptFor(TagNames.Rtc);

                    if (!CurrentNode.LocalName.IsOneOf(TagNames.Ruby, TagNames.Rtc))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref tag);
                    }
                }

                AddElement(ref tag);
            }
            else if (tagName.Is(TagNames.NoEmbed))
            {
                RawtextAlgorithm(ref tag);
            }
            else if (tagName.Is(TagNames.NoScript))
            {
                var scripting = _options.IsScripting;
                var element = _elementFactory.CreateNoScript(_document, scripting);

                if (scripting)
                {
                    AddElement(element, ref tag);
                    SwitchToRawtext();
                }
                else
                {
                    ReconstructFormatting();
                    AddElement(element, ref tag);
                }
            }
            else if (tagName.Is(TagNames.Math))
            {
                var element = _elementFactory.CreateMath(_document, tagName);
                ReconstructFormatting();
                AddElement(element.Setup(ref tag));

                if (tag.IsSelfClosing)
                {
                    CloseNode(element);
                }
            }
            else if (tagName.Is(TagNames.Svg))
            {
                var element = _elementFactory.CreateSvg(_document, tagName);
                ReconstructFormatting();
                AddElement(element.Setup(ref tag));

                if (tag.IsSelfClosing)
                {
                    CloseNode(element);
                }
            }
            else if (tagName.Is(TagNames.Plaintext))
            {
                if (IsInButtonScope())
                {
                    InBodyEndTagParagraph(ref tag);
                }

                AddElement(ref tag);
                _tokenizer.State = HtmlParseMode.Plaintext;
            }
            else if (tagName.Is(TagNames.Frameset))
            {
                RaiseErrorOccurred(HtmlParseError.FramesetMisplaced, ref tag);

                if (_openElements.Count != 1 && _openElements[1].LocalName.Is(TagNames.Body) && _frameset)
                {
                    _openElements[1].RemoveFromParent();

                    while (_openElements.Count > 1)
                    {
                        CloseCurrentNode();
                    }

                    var frameset = _elementFactory.Create(_document, TagNames.Frameset);
                    AddElement(frameset, ref tag);
                    _currentMode = HtmlTreeMode.InFrameset;
                }
            }
            else if (tagName.Is(TagNames.Html))
            {
                RaiseErrorOccurred(HtmlParseError.HtmlTagMisplaced, ref tag);

                if (_templateModes.Count == 0)
                {
                    _openElements[0].SetUniqueAttributes(ref tag);
                }
            }
            else if (tagName.Is(TagNames.Body))
            {
                RaiseErrorOccurred(HtmlParseError.BodyTagMisplaced, ref tag);

                if (_templateModes.Count == 0 && _openElements.Count > 1 && _openElements[1].LocalName.Is(TagNames.Body))
                {
                    _frameset = false;
                    _openElements[1].SetUniqueAttributes(ref tag);
                }
            }
            else if (tagName.Is(TagNames.IsIndex))
            {
                RaiseErrorOccurred(HtmlParseError.TagInappropriate, ref tag);

                if (_currentFormElement is null)
                {
                    var temp = StructHtmlToken.Open(TagNames.Form);
                    InBody(ref temp);

                    if (tag.GetAttribute(AttributeNames.Action).Length > 0)
                    {
                        _currentFormElement!.SetAttribute(
                            null,
                            AttributeNames.Action,
                            tag.GetAttribute(AttributeNames.Action));
                    }

                    temp = StructHtmlToken.Open(TagNames.Hr);
                    InBody(ref temp);
                    temp = StructHtmlToken.Open(TagNames.Label);
                    InBody(ref temp);

                    if (tag.GetAttribute(AttributeNames.Prompt).Length > 0)
                    {
                        AddCharacters(tag.GetAttribute(AttributeNames.Prompt));
                    }
                    else
                    {
                        AddCharacters("This is a searchable index. Enter search keywords: ");
                    }

                    var input = StructHtmlToken.Open(TagNames.Input);
                    input.AddAttribute(AttributeNames.Name, TagNames.IsIndex);

                    for (var i = 0; i < tag.Attributes.Count; i++)
                    {
                        var attr = tag.Attributes[i];

                        if (!attr.Name.IsOneOf(AttributeNames.Name, AttributeNames.Action, AttributeNames.Prompt))
                        {
                            input.AddAttribute(attr.Name, attr.Value);
                        }
                    }

                    InBody(ref input);
                    temp = StructHtmlToken.Close(TagNames.Label);
                    InBody(ref temp);
                    temp = StructHtmlToken.Open(TagNames.Hr);
                    InBody(ref temp);
                    temp = StructHtmlToken.Close(TagNames.Form);
                    InBody(ref temp);
                }
            }
            else if (TagNames.AllNested.Contains(tagName))
            {
                RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, ref tag);
            }
            else
            {
                ReconstructFormatting();
                AddElement(ref tag);
            }
        }

        private void InBodyEndTag(ref StructHtmlToken tag)
        {
            var tagName = tag.Name;

            if (tagName.Is(TagNames.Div))
            {
                InBodyEndTagBlock(ref tag);
            }
            else if (tagName.Is(TagNames.A))
            {
                HeisenbergAlgorithm(ref tag);
            }
            else if (tagName.Is(TagNames.Li))
            {
                if (IsInListItemScope())
                {
                    GenerateImpliedEndTagsExceptFor(tagName);

                    if (!CurrentNode.LocalName.Is(TagNames.Li))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref tag);
                    }

                    ClearStackBackTo(TagNames.Li);
                    CloseCurrentNode();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.ListItemNotInScope, ref tag);
                }
            }
            else if (tagName.Is(TagNames.P))
            {
                InBodyEndTagParagraph(ref tag);
            }
            else if (TagNames.AllBlocks.Contains(tagName))
            {
                InBodyEndTagBlock(ref tag);
            }
            else if (TagNames.AllFormatting.Contains(tagName))
            {
                HeisenbergAlgorithm(ref tag);
            }
            else if (tagName.Is(TagNames.Form))
            {
                var node = _currentFormElement;
                _currentFormElement = null;

                if (node is not null && IsInScope(node.LocalName))
                {
                    GenerateImpliedEndTags();

                    if (CurrentNode != node)
                    {
                        RaiseErrorOccurred(HtmlParseError.FormClosedWrong, ref tag);
                    }

                    CloseNode(node);
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.FormNotInScope, ref tag);
                }
            }
            else if (tagName.Is(TagNames.Br))
            {
                RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref tag);
                var temp = StructHtmlToken.Open(TagNames.Br);
                Consume(ref temp);
            }
            else if (TagNames.AllHeadings.Contains(tagName))
            {
                if (IsInScope(TagNames.AllHeadings))
                {
                    GenerateImpliedEndTags();

                    if (!CurrentNode.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref tag);
                    }

                    ClearStackBackTo(TagNames.AllHeadings);
                    CloseCurrentNode();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.HeadingNotInScope, ref tag);
                }
            }
            else if (tagName.IsOneOf(TagNames.Dd, TagNames.Dt))
            {
                if (IsInScope(tagName))
                {
                    GenerateImpliedEndTagsExceptFor(tagName);

                    if (!CurrentNode.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref tag);
                    }

                    ClearStackBackTo(tagName);
                    CloseCurrentNode();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.ListItemNotInScope, ref tag);
                }
            }
            else if (tagName.IsOneOf(TagNames.Applet, TagNames.Marquee, TagNames.Object))
            {
                if (IsInScope(tagName))
                {
                    GenerateImpliedEndTags();

                    if (!CurrentNode.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref tag);
                    }

                    ClearStackBackTo(tagName);
                    CloseCurrentNode();
                    _formattingElements.ClearFormatting();
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.ObjectNotInScope, ref tag);
                }
            }
            else if (tagName.Is(TagNames.Body))
            {
                InBodyEndTagBody(ref tag);
            }
            else if (tagName.Is(TagNames.Html))
            {
                if (InBodyEndTagBody(ref tag))
                {
                    AfterBody(ref tag);
                }
            }
            else if (tagName.Is(TagNames.Template))
            {
                InHead(ref tag);
            }
            else
            {
                InBodyEndTagAnythingElse(ref tag);
            }
        }

        /// <summary>
        /// See 8.2.5.4.7 The "in body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InBody(ref StructHtmlToken token)
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
                    InBodyStartTag(ref token);
                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    InBodyEndTag(ref token);
                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    CheckBodyOnClosing(ref token);

                    if (_templateModes.Count != 0)
                    {
                        InTemplate(ref token);
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
        private void Text(ref StructHtmlToken token)
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
                        HandleScript((IConstructableScriptElement)CurrentNode);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    RaiseErrorOccurred(HtmlParseError.EOF, ref token);
                    CloseCurrentNode();
                    _currentMode = _previousMode;
                    Consume(ref token);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.9 The "in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTable(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Caption))
                    {
                        ClearStackBackTo(TagNames.Table);
                        _formattingElements.AddScopeMarker();
                        var caption = _elementFactory.Create(_document, TagNames.Caption);
                        AddElement(caption, ref token);
                        _currentMode = HtmlTreeMode.InCaption;
                    }
                    else if (tagName.Is(TagNames.Colgroup))
                    {
                        ClearStackBackTo(TagNames.Table);
                        var colgroup = _elementFactory.Create(_document, TagNames.Colgroup);
                        AddElement(colgroup, ref token);
                        _currentMode = HtmlTreeMode.InColumnGroup;
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        var temp = StructHtmlToken.Open(TagNames.Colgroup);
                        InTable(ref temp);
                        InColumnGroup(ref token);
                    }
                    else if (TagNames.AllTableSections.Contains(tagName))
                    {
                        ClearStackBackTo(TagNames.Table);
                        var section = _elementFactory.Create(_document, tagName);
                        AddElement(section, ref token);
                        _currentMode = HtmlTreeMode.InTableBody;
                    }
                    else if (TagNames.AllTableCellsRows.Contains(tagName))
                    {
                        var temp = StructHtmlToken.Open(TagNames.Tbody);
                        InTable(ref temp);
                        InTableBody(ref token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        RaiseErrorOccurred(HtmlParseError.TableNesting, ref token);

                        if (InTableEndTagTable(ref token))
                        {
                            Home(ref token);
                        }
                    }
                    else if (tagName.Is(TagNames.Input))
                    {
                        var tag = token;

                        if (tag.GetAttribute(AttributeNames.Type).Isi(AttributeNames.Hidden))
                        {
                            RaiseErrorOccurred(HtmlParseError.InputUnexpected, ref token);
                            var input = _elementFactory.Create(_document, TagNames.Input);
                            AddElement(input, ref tag, true);
                            CloseCurrentNode();
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
                            InBodyWithFoster(ref token);
                        }
                    }
                    else if (tagName.Is(TagNames.Form))
                    {
                        RaiseErrorOccurred(HtmlParseError.FormInappropriate, ref token);

                        if (_currentFormElement is null)
                        {
                            _currentFormElement = _elementFactory.CreateForm(_document);
                            AddElement(_currentFormElement, ref token);
                            CloseCurrentNode();
                        }
                    }
                    else if (TagNames.AllTableHead.Contains(tagName))
                    {
                        InHead(ref token);
                    }
                    else if (IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInTableDetected, ref token);
                        InBodyWithFoster(ref token);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Table))
                    {
                        InTableEndTagTable(ref token);
                    }
                    else if (tagName.Is(TagNames.Template))
                    {
                        InHead(ref token);
                    }
                    else if (TagNames.AllTableSpecial.Contains(tagName) || TagNames.AllTableInner.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    }
                    else if (IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInTableDetected, ref token);
                        InBodyWithFoster(ref token);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    InBody(ref token);
                    return;
                }
                case HtmlTokenType.Character:
                {
                    if (TagNames.AllTableMajor.Contains(CurrentNode.LocalName))
                    {
                        InTableText(ref token);
                        return;
                    }

                    break;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
            InBodyWithFoster(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.10 The "in table text" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTableText(ref StructHtmlToken token)
        {
            if (token.HasContent)
            {
                RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
                InBodyWithFoster(ref token);
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
        private void InCaption(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Caption))
                    {
                        InCaptionEndTagCaption(ref token);
                    }
                    else if (TagNames.AllCaptionStart.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        RaiseErrorOccurred(HtmlParseError.TableNesting, ref token);

                        if (InCaptionEndTagCaption(ref token))
                        {
                            InTable(ref token);
                        }
                    }
                    else if (IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
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
                        RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, ref token);

                        if (InCaptionEndTagCaption(ref token))
                        {
                            InTable(ref token);
                        }
                    }
                    else if (IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
            }

            InBody(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.12 The "in column group" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InColumnGroup(ref StructHtmlToken token)
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
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        var col = _elementFactory.Create(_document, TagNames.Col);
                        AddElement(col, ref token, acknowledgeSelfClosing: true);
                        CloseCurrentNode();
                    }
                    else if (tagName.Is(TagNames.Template) || IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
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
                        InColumnGroupEndTagColgroup(ref token);
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagClosedWrong, ref token);
                    }
                    else if (tagName.Is(TagNames.Template) || IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    InBody(ref token);
                    return;
                }
            }

            if (InColumnGroupEndTagColgroup(ref token))
            {
                InTable(ref token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.13 The "in table body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTableBody(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Tr))
                    {
                        ClearStackBackTo(TagNames.AllTableSections);
                        var row = _elementFactory.Create(_document, TagNames.Tr);
                        AddElement(row, ref token);
                        _currentMode = HtmlTreeMode.InRow;
                    }
                    else if (TagNames.AllTableCells.Contains(tagName))
                    {
                        var temp = StructHtmlToken.Open(TagNames.Tr);
                        InTableBody(ref temp);
                        InRow(ref token);
                    }
                    else if (TagNames.AllTableGeneral.Contains(tagName))
                    {
                        InTableBodyCloseTable(ref token);
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
                            RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, ref token);
                        }
                    }
                    else if (tagName.Is(TagNames.Tr) || TagNames.AllTableSpecial.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        InTableBodyCloseTable(ref token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
            }

            InTable(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.14 The "in row" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InRow(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableCells.Contains(tagName))
                    {
                        ClearStackBackTo(TagNames.Tr);
                        AddElement(ref token);
                        _currentMode = HtmlTreeMode.InCell;
                        _formattingElements.AddScopeMarker();
                    }
                    else if (tagName.Is(TagNames.Tr) || TagNames.AllTableGeneral.Contains(tagName))
                    {
                        if (InRowEndTagTablerow(ref token))
                        {
                            InTableBody(ref token);
                        }
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
                        InRowEndTagTablerow(ref token);
                    }
                    else if (tagName.Is(TagNames.Table))
                    {
                        if (InRowEndTagTablerow(ref token))
                        {
                            InTableBody(ref token);
                        }
                    }
                    else if (TagNames.AllTableSections.Contains(tagName))
                    {
                        if (IsInTableScope(tagName))
                        {
                            InRowEndTagTablerow(ref token);
                            InTableBody(ref token);
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, ref token);
                        }
                    }
                    else if (TagNames.AllTableSpecial.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    }
                    else
                    {
                        break;
                    }

                    return;
                }
            }

            InTable(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.15 The "in cell" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InCell(ref StructHtmlToken token)
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
                            InCellEndTagCell(ref token);
                            Home(ref token);
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TableCellNotInScope, ref token);
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
                        InCellEndTagCell(ref token);
                    }
                    else if (TagNames.AllTableCore.Contains(tagName))
                    {
                        if (IsInTableScope(tagName))
                        {
                            InCellEndTagCell(ref token);
                            Home(ref token);
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.TableNotInScope, ref token);
                        }
                    }
                    else if (!TagNames.AllTableSpecial.Contains(tagName))
                    {
                        InBody(ref token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    }

                    return;
                }
            }

            InBody(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.16 The "in select" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InSelect(ref StructHtmlToken token)
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
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                    }
                    else if (tagName.Is(TagNames.Option))
                    {
                        if (CurrentNode.LocalName.Is(TagNames.Option))
                        {
                            InSelectEndTagOption(ref token);
                        }

                        var option = _elementFactory.Create(_document, TagNames.Option);
                        AddElement(option, ref token);
                    }
                    else if (tagName.Is(TagNames.Optgroup))
                    {
                        if (CurrentNode.LocalName.Is(TagNames.Option))
                        {
                            InSelectEndTagOption(ref token);
                        }

                        if (CurrentNode.LocalName.Is(TagNames.Optgroup))
                        {
                            InSelectEndTagOptgroup(ref token);
                        }

                        var optgroup = _elementFactory.Create(_document, TagNames.Optgroup);
                        AddElement(optgroup, ref token);
                    }
                    else if (tagName.Is(TagNames.Select))
                    {
                        RaiseErrorOccurred(HtmlParseError.SelectNesting, ref token);
                        InSelectEndTagSelect();
                    }
                    else if (TagNames.AllInput.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, ref token);

                        if (IsInSelectScope(TagNames.Select))
                        {
                            InSelectEndTagSelect();
                            Home(ref token);
                        }
                    }
                    else if (tagName.IsOneOf(TagNames.Template, TagNames.Script) || IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, ref token);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Template) || IsCustomElementEverywhere(tagName))
                    {
                        InHead(ref token);
                    }
                    else if (tagName.Is(TagNames.Optgroup))
                    {
                        InSelectEndTagOptgroup(ref token);
                    }
                    else if (tagName.Is(TagNames.Option))
                    {
                        InSelectEndTagOption(ref token);
                    }
                    else if (tagName.Is(TagNames.Select) && IsInSelectScope(TagNames.Select))
                    {
                        InSelectEndTagSelect();
                    }
                    else if (tagName.Is(TagNames.Select))
                    {
                        RaiseErrorOccurred(HtmlParseError.SelectNotInScope, ref token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    InBody(ref token);
                    return;
                }
                default:
                {
                    RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.17 The "in select in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InSelectInTable(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableSelects.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, ref token);
                        InSelectEndTagSelect();
                        Home(ref token);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (TagNames.AllTableSelects.Contains(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);

                        if (IsInTableScope(tagName))
                        {
                            InSelectEndTagSelect();
                            Home(ref token);
                        }

                        return;
                    }

                    break;
                }
            }

            InSelect(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.18 The "in template" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InTemplate(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Script) || TagNames.AllHead.Contains(tagName))
                    {
                        InHead(ref token);
                    }
                    else if (TagNames.AllTableRoot.Contains(tagName))
                    {
                        TemplateStep(ref token, HtmlTreeMode.InTable);
                    }
                    else if (tagName.Is(TagNames.Col))
                    {
                        TemplateStep(ref token, HtmlTreeMode.InColumnGroup);
                    }
                    else if (tagName.Is(TagNames.Tr))
                    {
                        TemplateStep(ref token, HtmlTreeMode.InTableBody);
                    }
                    else if (TagNames.AllTableCells.Contains(tagName))
                    {
                        TemplateStep(ref token, HtmlTreeMode.InRow);
                    }
                    else
                    {
                        TemplateStep(ref token, HtmlTreeMode.InBody);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.Is(TagNames.Template))
                    {
                        InHead(ref token);
                    }
                    else
                    {
                        RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, ref token);
                    }

                    return;
                }
                case HtmlTokenType.EndOfFile:
                {
                    if (TagCurrentlyOpen(TagNames.Template))
                    {
                        RaiseErrorOccurred(HtmlParseError.EOF, ref token);
                        CloseTemplate();
                        Home(ref token);
                        return;
                    }

                    End();
                    return;
                }
                default:
                {
                    InBody(ref token);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.19 The "after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterBody(ref StructHtmlToken token)
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
                    _openElements[0].AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (token.Name.Is(TagNames.Html))
                    {
                        InBody(ref token);
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
                            RaiseErrorOccurred(HtmlParseError.TagInvalidInFragmentMode, ref token);
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

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
            _currentMode = HtmlTreeMode.InBody;
            InBody(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.20 The "in frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void InFrameset(ref StructHtmlToken token)
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
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                    }
                    else if (tagName.Is(TagNames.Frameset))
                    {
                        var frameset = _elementFactory.Create(_document, TagNames.Frameset);
                        AddElement(frameset, ref token);
                    }
                    else if (tagName.Is(TagNames.Frame))
                    {
                        if (_options.IsNotSupportingFrames)
                        {
                            var frame = _elementFactory.CreateUnknown(_document, tagName);
                            AddElement(frame, ref token);
                        }
                        else
                        {
                            var frame = _elementFactory.CreateFrame(_document);
                            AddElement(frame, ref token, acknowledgeSelfClosing: true);
                        }

                        CloseCurrentNode();
                    }
                    else if (tagName.Is(TagNames.NoFrames))
                    {
                        InHead(ref token);
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
                            RaiseErrorOccurred(HtmlParseError.CurrentNodeIsRoot, ref token);
                        }

                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndOfFile:
                {
                    if (CurrentNode != _document.DocumentElement)
                    {
                        RaiseErrorOccurred(HtmlParseError.CurrentNodeIsNotRoot, ref token);
                    }

                    End();
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
        }

        /// <summary>
        /// See 8.2.5.4.21 The "after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterFrameset(ref StructHtmlToken token)
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
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                    }
                    else if (tagName.Is(TagNames.NoFrames))
                    {
                        InHead(ref token);
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

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
        }

        /// <summary>
        /// See 8.2.5.4.22 The "after after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterAfterBody(ref StructHtmlToken token)
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
                    _document.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    InBody(ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (!token.Name.Is(TagNames.Html))
                    {
                        break;
                    }

                    InBody(ref token);
                    return;
                }
            }

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
            _currentMode = HtmlTreeMode.InBody;
            InBody(ref token);
        }

        /// <summary>
        /// See 8.2.5.4.23 The "after after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        private void AfterAfterFrameset(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Comment:
                {
                    _document.AddComment(ref token);
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
                    InBody(ref token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.Is(TagNames.Html))
                    {
                        InBody(ref token);
                    }
                    else if (tagName.Is(TagNames.NoFrames))
                    {
                        InHead(ref token);
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

            RaiseErrorOccurred(HtmlParseError.TokenNotPossible, ref token);
        }

        #endregion

        #region Substates

        /// <summary>
        /// Inserting something in the template.
        /// </summary>
        /// <param name="token">The token to insert.</param>
        /// <param name="mode">The mode to push.</param>
        private void TemplateStep(ref StructHtmlToken token, HtmlTreeMode mode)
        {
            _templateModes.Pop();
            _templateModes.Push(mode);
            _currentMode = mode;
            Home(ref token);
        }

        /// <summary>
        /// Closes the template element.
        /// </summary>
        private void CloseTemplate()
        {
            // Well, obviously there is nothing to close - so let's abort
            if (_templateModes.Count == 0)
            {
                return;
            }

            while (_openElements.Count > 0)
            {
                var currentNode = CurrentNode;
                CloseCurrentNode();

                if (currentNode is IConstructableTemplateElement template)
                {
                    template.PopulateFragment();
                    break;
                }
            }

            CloseTemplateMode();
        }

        /// <summary>
        /// Finished the template mode.
        /// </summary>
        private void CloseTemplateMode()
        {
            _formattingElements.ClearFormatting();
            _templateModes.Pop();
            Reset();
        }

        /// <summary>
        /// Closes the table if the section is in table scope.
        /// </summary>
        /// <param name="tag">The tag to insert (closes table).</param>
        private void InTableBodyCloseTable(ref StructHtmlToken tag)
        {
            if (IsInTableScope(TagNames.AllTableSections))
            {
                ClearStackBackTo(TagNames.AllTableSections);
                CloseCurrentNode();
                _currentMode = HtmlTreeMode.InTable;
                InTable(ref tag);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, ref tag);
            }
        }

        /// <summary>
        /// Acts if a option end tag had been seen in the InSelect state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        private void InSelectEndTagOption(ref StructHtmlToken token)
        {
            if (CurrentNode.LocalName.Is(TagNames.Option))
            {
                CloseCurrentNode();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref token);
            }
        }

        /// <summary>
        /// Acts if a optgroup end tag had been seen in the InSelect state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        private void InSelectEndTagOptgroup(ref StructHtmlToken token)
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
                RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref token);
            }
        }

        /// <summary>
        /// Act as if an colgroup end tag has been found in the InColumnGroup state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InColumnGroupEndTagColgroup(ref StructHtmlToken token)
        {
            if (CurrentNode.LocalName.Is(TagNames.Colgroup))
            {
                CloseCurrentNode();
                _currentMode = HtmlTreeMode.InTable;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref token);
                return false;
            }
        }

        /// <summary>
        /// Act as if a body start tag has been found in the AfterHead state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        private void AfterHeadStartTagBody(ref StructHtmlToken token)
        {
            var body = _elementFactory.Create(_document, TagNames.Body);
            AddElement(body, ref token);
            _frameset = false;
            _currentMode = HtmlTreeMode.InBody;
        }

        /// <summary>
        /// Follows the generic rawtext parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        private void RawtextAlgorithm(ref StructHtmlToken tag)
        {
            AddElement(ref tag);
            SwitchToRawtext();
        }

        private void SwitchToRawtext()
        {
            _previousMode = _currentMode;
            _currentMode = HtmlTreeMode.Text;
            _tokenizer.State = HtmlParseMode.Rawtext;
        }

        /// <summary>
        /// Follows the generic RCData parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        private void RCDataAlgorithm(ref StructHtmlToken tag)
        {
            AddElement(ref tag);
            _previousMode = _currentMode;
            _currentMode = HtmlTreeMode.Text;
            _tokenizer.State = HtmlParseMode.RCData;
        }

        /// <summary>
        /// Acts if a li tag in the InBody state has been found.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        private void InBodyStartTagListItem(ref StructHtmlToken tag)
        {
            var index = _openElements.Count - 1;
            var node = _openElements[index];
            _frameset = false;

            while (true)
            {
                if (node.LocalName.Is(TagNames.Li))
                {
                    var temp = StructHtmlToken.Close(node.LocalName);
                    InBody(ref temp);
                    break;
                }

                if (node.Flags.HasFlag(NodeFlags.Special) && !TagNames.AllBasicBlocks.Contains(node.LocalName))
                {
                    break;
                }

                node = _openElements[--index];
            }

            if (IsInButtonScope())
            {
                InBodyEndTagParagraph(ref tag);
            }

            AddElement(ref tag);
        }

        /// <summary>
        /// Acts if a dd or dt tag in the InBody state has been found.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        private void InBodyStartTagDefinitionItem(ref StructHtmlToken tag)
        {
            _frameset = false;
            var index = _openElements.Count - 1;
            var node = _openElements[index];

            while (true)
            {
                if (node.LocalName.IsOneOf(TagNames.Dd, TagNames.Dt))
                {
                    var temp = StructHtmlToken.Close(node.LocalName);
                    InBody(ref temp);
                    break;
                }

                if (node.Flags.HasFlag(NodeFlags.Special) && !TagNames.AllBasicBlocks.Contains(node.LocalName))
                {
                    break;
                }

                node = _openElements[--index];
            }

            if (IsInButtonScope())
            {
                InBodyEndTagParagraph(ref tag);
            }

            AddElement(ref tag);
        }

        /// <summary>
        /// Acts if a block (button) end tag had been seen in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InBodyEndTagBlock(ref StructHtmlToken tag)
        {
            if (IsInScope(tag.Name))
            {
                GenerateImpliedEndTags();

                if (!CurrentNode.LocalName.Is(tag.Name))
                {
                    RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref tag);
                }

                ClearStackBackTo(tag.Name);
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BlockNotInScope, ref tag);
                return false;
            }
        }

        /// <summary>
        /// Acts if a nobr tag had been seen in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        private void HeisenbergAlgorithm(ref StructHtmlToken tag)
        {
            var currentNode = CurrentNode;

            // This check intends to ensure that for properly nested tags, closing tags will match
            // against the stack instead of the _formattingElements.
            if (currentNode.NamespaceUri.Is(NamespaceNames.HtmlUri) && currentNode.LocalName.Is(tag.Name) && !_formattingElements.Contains(currentNode))
            {
                // If the current element matches the name but isn't on the list of active
                // formatting elements, then it is possible that the list was mangled by the Noah's Ark
                // clause. In this case, we want to match the end tag against the stack instead of
                // proceeding with the AAA algorithm that may match against the list of
                // active formatting elements (and possibly mangle the tree in unexpected ways).
                CloseCurrentNode();
                return;
            }

            for (var outer = 0; outer < 8; outer++)
            {
                var formattingElement = default(IConstructableElement);
                var furthestBlock = default(IConstructableElement);
                var index = 0;
                var inner = 0;

                for (var j = _formattingElements.Count - 1; j >= 0 && _formattingElements[j] is not null; j--)
                {
                    if (_formattingElements[j].LocalName.Is(tag.Name))
                    {
                        index = j;
                        formattingElement = _formattingElements[j];
                        break;
                    }
                }

                if (formattingElement is null)
                {
                    InBodyEndTagAnythingElse(ref tag);
                    break;
                }

                var openIndex = _openElements.IndexOf(formattingElement);

                if (openIndex == -1)
                {
                    RaiseErrorOccurred(HtmlParseError.FormattingElementNotFound, ref tag);
                    _formattingElements.Remove(formattingElement);
                    break;
                }

                if (!IsInScope(formattingElement.LocalName))
                {
                    RaiseErrorOccurred(HtmlParseError.ElementNotInScope, ref tag);
                    break;
                }

                if (openIndex != _openElements.Count - 1)
                {
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong, ref tag);
                }

                var bookmark = index;

                for (var j = openIndex + 1; j < _openElements.Count; j++)
                {
                    if (_openElements[j].Flags.HasFlag(NodeFlags.Special))
                    {
                        index = j;
                        furthestBlock = _openElements[j];
                        break;
                    }
                }

                if (furthestBlock is null)
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
                var lastNode = furthestBlock;

                while (true)
                {
                    inner++;
                    var node = _openElements[--index];

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

                if (bookmark > _formattingElements.Count)
                {
                    bookmark = _formattingElements.Count;
                }

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
        private IConstructableElement CopyElement(IConstructableElement element)
        {
            return (IConstructableElement)element.ShallowCopy();
        }

        /// <summary>
        /// Performs the InBody state with foster parenting.
        /// </summary>
        /// <param name="token">The given token.</param>
        private void InBodyWithFoster(ref StructHtmlToken token)
        {
            _foster = true;
            InBody(ref token);
            _foster = false;
        }

        /// <summary>
        /// Act as if an anything else tag has been found in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag found.</param>
        private void InBodyEndTagAnythingElse(ref StructHtmlToken tag)
        {
            var index = _openElements.Count - 1;
            var node = CurrentNode;

            while (node is not null)
            {
                if (node.LocalName.Is(tag.Name))
                {
                    GenerateImpliedEndTagsExceptFor(tag.Name);

                    if (!node.LocalName.Is(tag.Name))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagClosedWrong, ref tag);
                    }

                    CloseNodesFrom(index);
                    break;
                }
                else if (node.Flags.HasFlag(NodeFlags.Special))
                {
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong, ref tag);
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
        private Boolean InBodyEndTagBody(ref StructHtmlToken token)
        {
            if (IsInScope(TagNames.Body))
            {
                CheckBodyOnClosing(ref token);
                _currentMode = HtmlTreeMode.AfterBody;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BodyNotInScope, ref token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an br start tag has been found in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag found.</param>
        private void InBodyStartTagBreakrow(ref StructHtmlToken tag)
        {
            ReconstructFormatting();
            AddElement(ref tag, acknowledgeSelfClosing: true);
            CloseCurrentNode();
            _frameset = false;
        }

        /// <summary>
        /// Act as if an p end tag has been found in the InBody state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was found, otherwise false.</returns>
        private Boolean InBodyEndTagParagraph(ref StructHtmlToken token)
        {
            if (IsInButtonScope())
            {
                GenerateImpliedEndTagsExceptFor(TagNames.P);

                if (!CurrentNode.LocalName.Is(TagNames.P))
                {
                    RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref token);
                }

                ClearStackBackTo(TagNames.P);
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.ParagraphNotInScope, ref token);
                var temp = StructHtmlToken.Open(TagNames.P);
                Consume(ref temp);
                InBodyEndTagParagraph(ref token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an table end tag has been found in the InTable state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InTableEndTagTable(ref StructHtmlToken token)
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
                RaiseErrorOccurred(HtmlParseError.TableNotInScope, ref token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an tr end tag has been found in the InRow state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        private Boolean InRowEndTagTablerow(ref StructHtmlToken token)
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
                RaiseErrorOccurred(HtmlParseError.TableRowNotInScope, ref token);
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
        private Boolean InCaptionEndTagCaption(ref StructHtmlToken token)
        {
            if (IsInTableScope(TagNames.Caption))
            {
                GenerateImpliedEndTags();

                if (!CurrentNode.LocalName.Is(TagNames.Caption))
                {
                    RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref token);
                }

                ClearStackBackTo(TagNames.Caption);
                CloseCurrentNode();
                _formattingElements.ClearFormatting();
                _currentMode = HtmlTreeMode.InTable;
                return true;
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.CaptionNotInScope, ref token);
                return false;
            }
        }

        /// <summary>
        /// Act as if an td or th end tag has been found in the InCell state.
        /// </summary>
        /// <param name="token">The actual tag token.</param>
        private void InCellEndTagCell(ref StructHtmlToken token)
        {
            GenerateImpliedEndTags();

            if (!TagNames.AllTableCells.Contains(CurrentNode.LocalName))
            {
                RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, ref token);
            }

            ClearStackBackTo(TagNames.AllTableCells);
            CloseCurrentNode();
            _formattingElements.ClearFormatting();
            _currentMode = HtmlTreeMode.InRow;
        }

        #endregion

        #region Foreign Content

        /// <summary>
        /// 8.2.5.5 The rules for parsing tokens in foreign content
        /// </summary>
        /// <param name="token">The token to examine.</param>
        private void Foreign(ref StructHtmlToken token)
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


                    if (tagName.Is(TagNames.Font))
                    {
                        for (var i = 0; i != token.Attributes.Count; i++)
                        {
                            if (token.Attributes[i].Name.IsOneOf(AttributeNames.Color, AttributeNames.Face, AttributeNames.Size))
                            {
                                ForeignNormalTag(ref token);
                                return;
                            }
                        }

                        ForeignSpecialTag(ref token);
                    }
                    else if (TagNames.AllForeignExceptions.Contains(tagName))
                    {
                        ForeignNormalTag(ref token);
                    }
                    else
                    {
                        ForeignSpecialTag(ref token);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;
                    var node = CurrentNode;

                    if (node is IConstructableScriptElement script)
                    {
                        HandleScript(script);
                        return;
                    }

                    if (!node.LocalName.Is(tagName))
                    {
                        RaiseErrorOccurred(HtmlParseError.TagClosingMismatch, ref token);
                    }

                    for (var i = _openElements.Count - 1; i > 0; i--)
                    {
                        if (node.LocalName.Isi(tagName))
                        {
                            CloseNodesFrom(i);
                            break;
                        }

                        node = _openElements[i - 1];

                        if (node.Flags.HasFlag(NodeFlags.HtmlMember))
                        {
                            Home(ref token);
                            break;
                        }
                    }

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(ref token);
                    return;
                }
                case HtmlTokenType.Doctype:
                {
                    RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, ref token);
                    return;
                }
            }
        }

        /// <summary>
        /// Processes a special start tag token.
        /// </summary>
        /// <param name="tag">The tag token to process.</param>
        private void ForeignSpecialTag(ref StructHtmlToken tag)
        {
            var node = CreateForeignElementFrom(ref tag);

            if (node is not null)
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
                    var temp = StructHtmlToken.Close(TagNames.Script);
                    Foreign(ref temp);
                }
            }
        }

        /// <summary>
        /// Creates a foreign element from the given html tag.
        /// </summary>
        /// <param name="tag">The tag of the foreign element.</param>
        /// <returns>The element or NULL if it is no MathML or SVG element.</returns>
        private IConstructableElement? CreateForeignElementFrom(ref StructHtmlToken tag)
        {
            if (AdjustedCurrentNode!.Flags.HasFlag(NodeFlags.MathMember))
            {
                var tagName = tag.Name;
                var element = _elementFactory.CreateMath(_document, tagName);
                AuxiliarySetupSteps(element, ref tag);
                return element.Setup(ref tag);
            }
            else if (AdjustedCurrentNode.Flags.HasFlag(NodeFlags.SvgMember))
            {
                var tagName = tag.Name.SanatizeSvgTagName();
                var element = _elementFactory.CreateSvg(_document, tagName);
                AuxiliarySetupSteps(element, ref tag);
                return element.Setup(ref tag);
            }

            return null;
        }

        /// <summary>
        /// Processes a normal start tag token.
        /// </summary>
        /// <param name="tag">The token to process.</param>
        private void ForeignNormalTag(ref StructHtmlToken tag)
        {
            RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, ref tag);

            if (!IsFragmentCase)
            {
                const NodeFlags Annotated = NodeFlags.HtmlTip | NodeFlags.MathTip | NodeFlags.HtmlMember;
                var node = CurrentNode;

                do
                {
                    if (node.LocalName.Is(TagNames.AnnotationXml))
                    {
                        var value = node.GetAttribute(default, AttributeNames.Encoding);

                        if (value.Isi(MimeTypeNames.Html) || value.Isi(MimeTypeNames.ApplicationXHtml))
                        {
                            AddElement(ref tag);
                            return;
                        }
                    }

                    CloseCurrentNode();
                    node = CurrentNode;
                }
                while ((node.Flags & Annotated) == NodeFlags.None);

                Consume(ref tag);
            }
            else
            {
                ForeignSpecialTag(ref tag);
            }
        }

        #endregion

        #region Scope

        /// <summary>
        /// Determines if the given tag name is in the global scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        private Boolean IsInScope(StringOrMemory tagName)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(tagName))
                {
                    return true;
                }
                else if (node.Flags.HasFlag(NodeFlags.Scoped))
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
        private Boolean IsInScope(HashSet<StringOrMemory> tags)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (tags.Contains(node.LocalName))
                {
                    return true;
                }
                else if (node.Flags.HasFlag(NodeFlags.Scoped))
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
                else if (node.Flags.HasFlag(NodeFlags.Scoped) || (node.Flags.HasFlag(NodeFlags.HtmlListScoped)))
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
                else if (node.Flags.HasFlag(NodeFlags.Scoped) || node.LocalName.Is(TagNames.Button))
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
        private Boolean IsInTableScope(HashSet<StringOrMemory> tags)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (tags.Contains(node.LocalName))
                {
                    return true;
                }
                else if (node.Flags.HasFlag(NodeFlags.HtmlTableScoped))
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
        private Boolean IsInTableScope(StringOrMemory tagName)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(tagName))
                {
                    return true;
                }
                else if (node.Flags.HasFlag(NodeFlags.HtmlTableScoped))
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
        private Boolean IsInSelectScope(StringOrMemory tagName)
        {
            for (var i = _openElements.Count - 1; i >= 0; i--)
            {
                var node = _openElements[i];

                if (node.LocalName.Is(tagName))
                {
                    return true;
                }
                else if (!node.Flags.HasFlag(NodeFlags.HtmlSelectScoped))
                {
                    return false;
                }
            }

            return false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks if the given tag name should be considered as a "custom element everywhere".
        /// </summary>
        private Boolean IsCustomElementEverywhere(StringOrMemory tagName) => _options.IsAcceptingCustomElementsEverywhere && tagName.IsCustomElement();

        /// <summary>
        /// Runs a script given by the current node.
        /// </summary>
        private void HandleScript(IConstructableScriptElement script)
        {
            if (script is not null)
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
        private async Task RunScript(IConstructableScriptElement script)
        {
            await _document.WaitForReadyAsync(CancellationToken.None).ConfigureAwait(false);
            await script.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// If there is a node in the stack of open elements that is not either
        /// a dd element, a dt element, an li element, a p element, a tbody
        /// element, a td element, a tfoot element, a th element, a thead
        /// element, a tr element, the body element, or the html element, then
        /// this is a parse error.
        /// </summary>
        private void CheckBodyOnClosing(ref StructHtmlToken token)
        {
            for (var i = 0; i < _openElements.Count; i++)
            {
                if (!_openElements[i].Flags.HasFlag(NodeFlags.ImplicitlyClosed))
                {
                    RaiseErrorOccurred(HtmlParseError.BodyClosedWrong, ref token);
                    break;
                }
            }
        }

        /// <summary>
        /// Checks if a tag with the given name is currently open.
        /// </summary>
        /// <param name="tagName">The name of the tag to check for.</param>
        /// <returns>True if such a tag is open, otherwise false.</returns>
        private Boolean TagCurrentlyOpen(StringOrMemory tagName)
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
            var temp = _tokenizer.GetStructToken();

            if (temp.Type == HtmlTokenType.Character)
            {
                temp.RemoveNewLine();
            }

            Home(ref temp);
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
        private void AddRoot(ref StructHtmlToken tag)
        {
            var element = _elementFactory.Create(_document, TagNames.Html);
            _document.AddNode(element);
            SetupElement(element, ref tag, false);
            _openElements.Add(element);
            _tokenizer.IsAcceptingCharacterData = false;
            _document.ApplyManifest();
        }

        private void CheckEnded(IConstructableElement element)
        {
            if (_shouldEnd?.Invoke(element) ?? false)
            {
                _ended = true;
            }
        }

        private void CloseNodeAt(Int32 index)
        {
            var openElement = _openElements[index];
            openElement.SetupElement();
            _openElements.RemoveAt(index);
            CheckEnded(openElement);
        }

        private void CloseNode(IConstructableElement element)
        {
            element.SetupElement();
            _openElements.Remove(element);
            CheckEnded(element);
        }

        private void CloseNodesFrom(Int32 index)
        {
            for (var i = _openElements.Count - 1; i > index; i--)
            {
                CloseNodeAt(i);
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
                CloseNodeAt(_openElements.Count - 1);
                var node = AdjustedCurrentNode;
                _tokenizer.IsAcceptingCharacterData = node is not null && !node.Flags.HasFlag(NodeFlags.HtmlMember);
            }
        }

        /// <summary>
        /// Modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        private void SetupElement(IConstructableElement element, ref StructHtmlToken tag, Boolean acknowledgeSelfClosing)
        {
            if (tag.IsSelfClosing && !acknowledgeSelfClosing)
            {
                RaiseErrorOccurred(HtmlParseError.TagCannotBeSelfClosed, ref tag);
            }

            AuxiliarySetupSteps(element, ref tag);
            element.SetAttributes(tag.Attributes);
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        private TElement AddElement(ref StructHtmlToken tag, Boolean acknowledgeSelfClosing = false)
        {
            var element = _elementFactory.Create(_document, tag.Name);
            SetupElement(element, ref tag, acknowledgeSelfClosing);
            AddElement(element);
            return element;
        }

        /// <summary>
        /// Appends a template element.
        /// </summary>
        /// <param name="tag">The associated tag token.</param>
        private void AddTemplateElement(ref StructHtmlToken tag)
        {
            var template = _elementFactory.CreateTemplate(_document);
            AddElement(template, ref tag);
            _formattingElements.AddScopeMarker();
            _frameset = false;
            _currentMode = HtmlTreeMode.InTemplate;
            _templateModes.Push(HtmlTreeMode.InTemplate);
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        private void AddElement(IConstructableElement element, ref StructHtmlToken tag, Boolean acknowledgeSelfClosing = false)
        {
            SetupElement(element, ref tag, acknowledgeSelfClosing);
            AddElement(element);
        }

        /// <summary>
        /// Appends a configured node to the current node.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        private void AddElement(IConstructableElement element)
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
            _tokenizer.IsAcceptingCharacterData = !element.Flags.HasFlag(NodeFlags.HtmlMember);
        }

        /// <summary>
        /// Appends a node to the appropriate foster parent.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#foster-parent
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        private void AddElementWithFoster(IConstructableElement element)
        {
            var table = false;
            var index = _openElements.Count;

            while (--index != 0)
            {
                var el = _openElements[index];

                if (el is HtmlTemplateElement)
                {
                    el.AddNode(element);
                    return;
                }
                else if (el is HtmlTableElement)
                {
                    table = true;
                    break;
                }
            }

            var current = _openElements[index];
            var foster = current.Parent ?? _openElements[index + 1];

            if (table && current.Parent is not null)
            {
                for (var i = 0; i < foster.ChildNodes.Length; i++)
			    {
                    if (foster.ChildNodes[i] == current)
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
        private void AddCharacters(StringOrMemory text)
        {
            if (text.Length != 0)
            {
                var node = CurrentNode;

                if (_foster && TagNames.AllTableMajor.Contains(node.LocalName))
                {
                    AddCharactersWithFoster(text);
                }
                else
                {
                    node.AppendText(text, _emitWhitespaceTextNodes);
                }
            }
        }

        /// <summary>
        /// Inserts the given character into the foster parent.
        /// </summary>
        /// <param name="text">The character to insert.</param>
        private void AddCharactersWithFoster(StringOrMemory text)
        {
            var table = false;
            var index = _openElements.Count;

            while (--index != 0)
            {
                if (_openElements[index].LocalName.Is(TagNames.Template))
                {
                    _openElements[index].AppendText(text, _emitWhitespaceTextNodes);
                    return;
                }
                else if (_openElements[index].LocalName.Is(TagNames.Table))
                {
                    table = true;
                    break;
                }
            }

            var foster = _openElements[index].Parent ?? _openElements[index + 1];

            if (table && _openElements[index].Parent is not null)
            {
                for (var i = 0; i < foster.ChildNodes.Length; i++)
                {
                    if (foster.ChildNodes[i] == _openElements[index])
                    {
                        foster.InsertText(i, text, _emitWhitespaceTextNodes);
                        break;
                    }
                }
            }
            else
            {
                foster.AppendText(text, _emitWhitespaceTextNodes);
            }
        }

        private void AuxiliarySetupSteps(IConstructableElement element, ref StructHtmlToken tag)
        {
            if (_options.IsKeepingSourceReferences)
            {
                element.SourceReference = tag.ToHtmlToken();
            }

            if (_options.OnCreated is not null && element is IElement e)
            {
                _options.OnCreated.Invoke(e, tag.Position);
            }
        }

        #endregion

        #region Closing Nodes

        /// <summary>
        /// Clears the stack of open elements back to the given element name.
        /// </summary>
        /// <param name="tagName">The tag that will be the CurrentNode.</param>
        private void ClearStackBackTo(StringOrMemory tagName)
        {
            var node = CurrentNode;

            while (!node.LocalName.Is(tagName) && !(node is HtmlHtmlElement || node is HtmlTemplateElement))
            {
                CloseCurrentNode();
                node = CurrentNode;
            }
        }

        /// <summary>
        /// Clears the stack of open elements back to any heading element.
        /// </summary>
        private void ClearStackBackTo(HashSet<StringOrMemory> tags)
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
        private void GenerateImpliedEndTagsExceptFor(StringOrMemory tagName)
        {
            var node = CurrentNode;

            while (node.Flags.HasFlag(NodeFlags.ImpliedEnd) && !node.LocalName.Is(tagName))
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
            while (CurrentNode.Flags.HasFlag(NodeFlags.ImpliedEnd))
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
            {
                return;
            }

            var index = _formattingElements.Count - 1;
            var entry = _formattingElements[index];

            if (entry is null || _openElements.Contains(entry))
            {
                return;
            }

            while (index > 0)
            {
                entry = _formattingElements[--index];

                if (entry is null || _openElements.Contains(entry))
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

        private void RaiseErrorOccurred(HtmlParseError code, ref StructHtmlToken token) => _tokenizer.RaiseErrorOccurred(code, token.Position);

        #endregion

        public void Dispose()
        {
            _tokenizer.Dispose();
        }
    }

    sealed class HtmlDomBuilder : HtmlDomBuilder<Document, Element>
    {
        public HtmlDomBuilder(
            IHtmlElementConstructionFactory elementFactory,
            HtmlDocument document,
            HtmlTokenizerOptions? maybeOptions = null,
            String? stopAt = null)
            : base(
                elementFactory: elementFactory,
                document: document,
                maybeOptions: maybeOptions,
                emitWhitespaceTextNodes: true,
                shouldEnd: stopAt is not null ? e => e.Prefix.Length == 0 && e.LocalName.Is(stopAt) : null)
        {
        }
    }
}
