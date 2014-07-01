namespace AngleSharp.Parser.Html
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
    using AngleSharp.DOM.Mathml;
    using AngleSharp.DOM.Svg;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the Tree construction as specified in
    /// 8.2.5 Tree construction, on the following page:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    [DebuggerStepThrough]
    public sealed class HtmlParser : IParser
    {
        #region Fields

        HtmlTokenizer tokenizer;
        HTMLDocument doc;
        HtmlTreeMode insert;
        HtmlTreeMode originalInsert;
        List<Element> open;
        List<Element> formatting;
        HTMLFormElement form;
        Boolean frameset;
        Node fragmentContext;
        Boolean foster;
        Int32 nesting;
        Boolean started;
        HTMLScriptElement pendingParsingBlock;
        Stack<HtmlTreeMode> templateMode;
        Task task;
		Object sync;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ParseError;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the HTML parser with an new document
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public HtmlParser(String source, IConfiguration configuration = null)
            : this(new HTMLDocument { Options = configuration }, new SourceManager(source, configuration.DefaultEncoding()))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with an new document
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public HtmlParser(Stream stream, IConfiguration configuration = null)
            : this(new HTMLDocument { Options = configuration }, new SourceManager(stream, configuration.DefaultEncoding()))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with the specified document
        /// based on the given source.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="source">The source code as a string.</param>
        public HtmlParser(HTMLDocument document, String source)
            : this(document, new SourceManager(source, document.Options.DefaultEncoding()))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with the specified document
        /// based on the given stream.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="stream">The stream to use as source.</param>
        public HtmlParser(HTMLDocument document, Stream stream)
            : this(document, new SourceManager(stream, document.Options.DefaultEncoding()))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with the specified document
        /// based on the given source manager.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="source">The source to use.</param>
        internal HtmlParser(HTMLDocument document, SourceManager source)
        {
            tokenizer = new HtmlTokenizer(source);

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ParseError != null)
                    ParseError(this, ev);
            };

			sync = new Object();
            started = false;
            doc = document;
            open = new List<Element>();
            templateMode = new Stack<HtmlTreeMode>();
            formatting = new List<Element>();
            frameset = true;
            insert = HtmlTreeMode.Initial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the (maybe intermediate) result of the parsing process.
        /// </summary>
        public HTMLDocument Result
        {
            get 
            {
                Parse();
                return doc; 
            }
        }

        /// <summary>
        /// Gets if the parser has been started asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return task != null; }
        }

        /// <summary>
        /// Gets if the tree builder has been created for
        /// parsing HTML fragments.
        /// </summary>
        public Boolean IsFragmentCase
        {
            get { return fragmentContext != null; }
        }

        /// <summary>
        /// Gets the adjusted current node.
        /// </summary>
        internal Node AdjustedCurrentNode
        {
            get { return (fragmentContext != null && open.Count == 1) ? fragmentContext : CurrentNode; }
        }

        /// <summary>
        /// Gets the current node.
        /// </summary>
        internal Element CurrentNode
        {
            get { return open.Count > 0 ? open[open.Count - 1] : null; }
        }

        /// <summary>
        /// Gets or sets the pending parsing block script.
        /// </summary>
        internal HTMLScriptElement PendingParsingBlock
        {
            get { return pendingParsingBlock; }
            set { pendingParsingBlock = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task ParseAsync()
        {
			lock (sync)
			{
				if (!started)
				{
					started = true;
					task = Task.Factory.StartNew(() => Kernel());
				}
				else if (task == null)
					throw new InvalidOperationException("The parser has already run synchronously.");

				return task;
			}
        }

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        public void Parse()
        {
			var run = false;

			lock (sync)
			{
				if (!started)
				{
					started = true;
					run = true;
				}
			}

			if(run)
				Kernel();
        }

        /// <summary>
        /// Switches to the fragment algorithm with the specified context element.
        /// </summary>
        /// <param name="context">The context element where the algorithm is applied to.</param>
        public void SwitchToFragment(Node context)
        {
            if (started)
                throw new InvalidOperationException("Fragment mode has to be activated before running the parser!");

            switch (context.NodeName)
            {
                case Tags.Title:
                case Tags.Textarea:
                {
                    tokenizer.Switch(HtmlParseMode.RCData);
                    break;
                }
                case Tags.Style:
                case Tags.Xmp:
                case Tags.Iframe:
                case Tags.NoEmbed:
                case Tags.NoFrames:
                {
                    tokenizer.Switch(HtmlParseMode.Rawtext);
                    break;
                }
                case Tags.Script:
                {
                    tokenizer.Switch(HtmlParseMode.Script);
                    break;
                }
                case Tags.NoScript:
                {
                    if (doc.Options.IsScripting) 
                        tokenizer.Switch(HtmlParseMode.Rawtext);

                    break;
                }
                case Tags.Plaintext:
                {
                    tokenizer.Switch(HtmlParseMode.Plaintext);
                    break;
                }
            }

            var root = new HTMLHtmlElement();
            doc.AppendChild(root);
            open.Add(root);

            if (context is HTMLTemplateElement)
                templateMode.Push(HtmlTreeMode.InTemplate);

            Reset(context);

            fragmentContext = context;
            tokenizer.AcceptsCharacterData = !AdjustedCurrentNode.IsInHtml;

            do
            {
                if (context is HTMLFormElement)
                {
                    form = (HTMLFormElement)context;
                    break;
                }

                context = context.Parent;
            }
            while (context != null);
        }

        /// <summary>
        /// Resets the current insertation mode to the rules according to the algorithm specified
        /// in 8.2.3.1 The insertion mode.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#the-insertion-mode
        /// </summary>
        void Reset(Node context = null)
        {
            var last = false;
            Node node;

            for (var i = open.Count - 1; i >= 0; i--)
            {
                node = open[i];

                if (i == 0)
                {
                    last = true;
                    node = context ?? node;
                }

                switch (node.NodeName)
                {
                    case Tags.Select:
                        insert = HtmlTreeMode.InSelect;
                        break;

                    case Tags.Th:
                    case Tags.Td:
                        insert = last ? HtmlTreeMode.InBody : HtmlTreeMode.InCell;
                        break;

                    case Tags.Tr:
                        insert = HtmlTreeMode.InRow;
                        break;

                    case Tags.Thead:
                    case Tags.Tfoot:
                    case Tags.Tbody:
                        insert = HtmlTreeMode.InTableBody;
                        break;

                    case Tags.Caption:
                        insert = HtmlTreeMode.InCaption;
                        break;

                    case Tags.Colgroup:
                        insert = HtmlTreeMode.InColumnGroup;
                        break;

                    case Tags.Table:
                        insert = HtmlTreeMode.InTable;
                        break;

                    case Tags.Template:
                        insert = templateMode.Peek();
                        break;

                    case Tags.Head:
                        insert = last ? HtmlTreeMode.InBody : HtmlTreeMode.InHead;
                        break;

                    case Tags.Body:
                        insert = HtmlTreeMode.InBody;
                        break;

                    case Tags.Frameset:
                        insert = HtmlTreeMode.InFrameset;
                        break;

                    case Tags.Html:
                        insert = HtmlTreeMode.BeforeHead;
                        break;

                    default:
                        if (last)
                        {
                            insert = HtmlTreeMode.InBody;
                            break;
                        }

                        continue;
                }

                break;
            }
        }

        /// <summary>
        /// Consumes a token and processes it.
        /// </summary>
        /// <param name="token">The token to consume.</param>
        void Consume(HtmlToken token)
        {
            var node = AdjustedCurrentNode;

            if (node == null || node.IsInHtml || token.IsEof || (node.IsHtmlTIP && token.IsHtmlCompatible) ||
                (node.IsMathMLTIP && token.IsMathCompatible) || (node.IsInMathMLSVGReady && token.IsSvg))
                Home(token);
            else
                Foreign(token);
        }

        #endregion

        #region Home

        /// <summary>
        /// Takes the method corresponding to the current insertation mode.
        /// </summary>
        /// <param name="token">The token to insert / use.</param>
        void Home(HtmlToken token)
        {
            switch (insert)
            {
                case HtmlTreeMode.Initial:
                    Initial(token);
                    break;

                case HtmlTreeMode.BeforeHtml:
                    BeforeHtml(token);
                    break;

                case HtmlTreeMode.BeforeHead:
                    BeforeHead(token);
                    break;

                case HtmlTreeMode.InHead:
                    InHead(token);
                    break;

                case HtmlTreeMode.InHeadNoScript:
                    InHeadNoScript(token);
                    break;

                case HtmlTreeMode.AfterHead:
                    AfterHead(token);
                    break;

                case HtmlTreeMode.InBody:
                    InBody(token);
                    break;

                case HtmlTreeMode.Text:
                    Text(token);
                    break;

                case HtmlTreeMode.InTable:
                    InTable(token);
                    break;

                case HtmlTreeMode.InCaption:
                    InCaption(token);
                    break;

                case HtmlTreeMode.InColumnGroup:
                    InColumnGroup(token);
                    break;

                case HtmlTreeMode.InTableBody:
                    InTableBody(token);
                    break;

                case HtmlTreeMode.InRow:
                    InRow(token);
                    break;

                case HtmlTreeMode.InCell:
                    InCell(token);
                    break;

                case HtmlTreeMode.InSelect:
                    InSelect(token);
                    break;

                case HtmlTreeMode.InSelectInTable:
                    InSelectInTable(token);
                    break;

                case HtmlTreeMode.InTemplate:
                    InTemplate(token);
                    break;

                case HtmlTreeMode.AfterBody:
                    AfterBody(token);
                    break;

                case HtmlTreeMode.InFrameset:
                    InFrameset(token);
                    break;

                case HtmlTreeMode.AfterFrameset:
                    AfterFrameset(token);
                    break;

                case HtmlTreeMode.AfterAfterBody:
                    AfterAfterBody(token);
                    break;

                case HtmlTreeMode.AfterAfterFrameset:
                    AfterAfterFrameset(token);
                    break;
            }
        }

        /// <summary>
        /// See 8.2.5.4.1 The "initial" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void Initial(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.DOCTYPE)
            {
                var doctype = (HtmlDoctypeToken)token;

                if (!doctype.IsValid)
                    RaiseErrorOccurred(ErrorCode.DoctypeInvalid);

                AddDoctype(doctype);

                if (doctype.IsFullQuirks)
                    doc.QuirksMode = QuirksMode.On;
                else if (doctype.IsLimitedQuirks)
                    doc.QuirksMode = QuirksMode.Limited;

                insert = HtmlTreeMode.BeforeHtml;
                return;
            }
            else if (token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                chars.TrimStart();

                if (chars.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(doc, token);
                return;
            }

            if (!doc.Options.IsEmbedded)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeMissing);
                doc.QuirksMode = QuirksMode.On;
            }

            insert = HtmlTreeMode.BeforeHtml;
            BeforeHtml(token);
        }

        /// <summary>
        /// See 8.2.5.4.2 The "before html" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void BeforeHtml(HtmlToken token)
        {
            if (token.IsStartTag(Tags.Html))
            {
                AddRoot(token.AsTag());
                insert = HtmlTreeMode.BeforeHead;
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }
            else if(token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                chars.TrimStart();

                if (chars.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(doc, token);
                return;
            }
            else if (token.IsEndTagInv(Tags.Html, Tags.Body, Tags.Br, Tags.Head))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                return;
            }

            BeforeHtml(HtmlToken.OpenTag(Tags.Html));
            BeforeHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.3 The "before head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void BeforeHead(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                chars.TrimStart();

                if (chars.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == Tags.Html)
            {
                InBody(token);
                return;
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == Tags.Head)
            {
                AddElement(new HTMLHeadElement(), token.AsTag());
                insert = HtmlTreeMode.InHead;
                return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }
            else if (token.IsEndTagInv(Tags.Html, Tags.Body, Tags.Br, Tags.Head))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                return;
            }

            BeforeHead(HtmlToken.OpenTag(Tags.Head));
            InHead(token);
        }
        
        /// <summary>
        /// See 8.2.5.4.4 The "in head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InHead(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                var str = chars.TrimStart();
                AddCharacters(str);

                if (chars.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }
            else if (token.IsStartTag(Tags.Html))
            {
                InBody(token);
                return;
            }
            else if (token.IsStartTag(Tags.Meta))
            {
                var element = new HTMLMetaElement();
                AddElement(element, token.AsTag(), true);
                CloseCurrentNode();

                var charset = element.GetAttribute(AttributeNames.Charset);

                if (charset != null && DocumentEncoding.IsSupported(charset))
                {
                    SetCharset(charset);
                    return;
                }

                charset = element.GetAttribute(AttributeNames.HttpEquiv);

                if (charset != null && charset.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    charset = element.GetAttribute(AttributeNames.Content) ?? String.Empty;
                    charset = DocumentEncoding.Extract(charset);

                    if (DocumentEncoding.IsSupported(charset))
                        SetCharset(charset);
                }

                return;
            }
            else if (token.IsStartTag(Tags.Link, Tags.Base, Tags.BaseFont, Tags.Bgsound))
            {
                AddElement(token.AsTag(), true);
                CloseCurrentNode();
                return;
            }
            else if (token.IsStartTag(Tags.Title))
            {
                RCDataAlgorithm(token.AsTag());
                return;
            }
            else if (token.IsStartTag(Tags.Style, Tags.NoFrames) || (doc.Options.IsScripting && token.IsStartTag(Tags.NoScript)))
            {
                RawtextAlgorithm(token.AsTag());
                return;
            }
            else if (token.IsStartTag(Tags.NoScript))
            {
                AddElement(token.AsTag());
                insert = HtmlTreeMode.InHeadNoScript;
                return;
            }
            else if (token.IsStartTag(Tags.Script))
            {
                AddElement(token.AsTag());
                //element.IsParserInserted = true;
                //element.IsAlreadyStarted = fragment;
                tokenizer.Switch(HtmlParseMode.Script);
                originalInsert = insert;
                insert = HtmlTreeMode.Text;
                return;
            }
            else if (token.IsEndTag(Tags.Head))
            {
                CloseCurrentNode();
                insert = HtmlTreeMode.AfterHead;
                return;
            }
            else if (token.IsStartTag(Tags.Head))
            {
                RaiseErrorOccurred(ErrorCode.HeadTagMisplaced);
                return;
            }
            else if (token.IsTag(Tags.Template))
            {
                if (token.Type == HtmlTokenType.StartTag)
                {
                    var element = new HTMLTemplateElement();
                    AddElement(element, token.AsTag());
                    AddScopeMarker();
                    frameset = false;
                    insert = HtmlTreeMode.InTemplate;
                    templateMode.Push(HtmlTreeMode.InTemplate);
                }
                else if (TagCurrentlyOpen(Tags.Template))
                {
                    GenerateImpliedEndTags();

                    if (!(CurrentNode is HTMLTemplateElement))
                        RaiseErrorOccurred(ErrorCode.TagClosingMismatch);

                    CloseTemplate();
                }
                else
                    RaiseErrorOccurred(ErrorCode.TagInappropriate);

                return;
            }
            else if (token.IsEndTagInv(Tags.Html, Tags.Body, Tags.Br))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                return;
            }

            CloseCurrentNode();
            insert = HtmlTreeMode.AfterHead;
            AfterHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.5 The "in head noscript" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InHeadNoScript(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                var str = chars.TrimStart();
                AddCharacters(str);

                if (chars.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                InHead(token);
                return;
            }
            else if (token.IsEndTag(Tags.NoScript))
            {
                CloseCurrentNode();
                insert = HtmlTreeMode.InHead;
                return;
            }
            else if (token.IsStartTag(Tags.Style, Tags.Link, Tags.BaseFont, Tags.Meta, Tags.NoFrames, Tags.Bgsound))
            {
                InHead(token);
                return;
            }
            else if (token.IsStartTag(Tags.Html))
            {
                InBody(token);
                return;
            }
            else if (token.IsStartTag(Tags.Head, Tags.NoScript))
            {
                RaiseErrorOccurred(ErrorCode.TagInappropriate);
                return;
            }
            else if (token.IsEndTagInv(Tags.Br))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            CloseCurrentNode();
            insert = HtmlTreeMode.InHead;
            InHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.6 The "after head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterHead(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                var str = chars.TrimStart();
                AddCharacters(str);

                if (chars.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }
            else if (token.IsStartTag(Tags.Html))
            {
                InBody(token);
                return;
            }
            else if (token.IsStartTag(Tags.Body))
            {
                AfterHeadStartTagBody((HtmlTagToken)token);
                return;
            }
            else if (token.IsStartTag(Tags.Frameset))
            {
                var element = new HTMLFrameSetElement();
                AddElement(element, token.AsTag());
                insert = HtmlTreeMode.InFrameset;
                return;
            }
            else if (token.IsStartTag(Tags.Base, Tags.BaseFont, Tags.Bgsound, Tags.Link, Tags.Meta, Tags.NoFrames, Tags.Script, Tags.Style, Tags.Title))
            {
                RaiseErrorOccurred(ErrorCode.TagMustBeInHead);
                var index = open.Count;
                open.Add(doc.Head as Element);//TODO remove cast ASAP
                InHead(token);
                open.RemoveAt(index);
                return;
            }
            else if (token.IsStartTag(Tags.Head))
            {
                RaiseErrorOccurred(ErrorCode.HeadTagMisplaced);
                return;
            }
            else if (token.IsEndTagInv(Tags.Html, Tags.Body, Tags.Br))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                return;
            }

            AfterHeadStartTagBody(HtmlToken.OpenTag(Tags.Body));
            frameset = true;
            Home(token);
        }

        /// <summary>
        /// See 8.2.5.4.7 The "in body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InBody(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chrs = (HtmlCharacterToken)token;
                ReconstructFormatting();
                AddCharacters(chrs.Data);

                if(chrs.HasContent)
                    frameset = false;
            }
            else if (token.Type == HtmlTokenType.Comment)
                AddComment(token);
            else if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Html:
                    {
                        RaiseErrorOccurred(ErrorCode.HtmlTagMisplaced);

                        if (templateMode.Count == 0)
                            open[0].AppendAttributes(tag);

                        break;
                    }
                    case Tags.Base:
                    case Tags.BaseFont:
                    case Tags.Bgsound:
                    case Tags.Link:
                    case Tags.MenuItem:
                    case Tags.Meta:
                    case Tags.NoFrames:
                    case Tags.Script:
                    case Tags.Style:
                    case Tags.Title:
                    case Tags.Template:
                    {
                        InHead(token);
                        break;
                    }
                    case Tags.Body:
                    {
                        RaiseErrorOccurred(ErrorCode.BodyTagMisplaced);

                        if (templateMode.Count == 0 && open.Count > 1 && open[1] is HTMLBodyElement)
                        {
                            frameset = false;
                            open[1].AppendAttributes(tag);
                        }

                        break;
                    }
                    case Tags.Frameset:
                    {
                        RaiseErrorOccurred(ErrorCode.FramesetMisplaced);

                        if (open.Count != 1 && open[1] is HTMLBodyElement && frameset)
                        {
                            open[1].Parent.RemoveChild(open[1]);

                            while (open.Count > 1)
                                CloseCurrentNode();

                            var element = new HTMLFrameSetElement();
                            AddElement(element, tag);
                            insert = HtmlTreeMode.InFrameset;
                        }

                        break;
                    }
                    case Tags.Address:
                    case Tags.Article:
                    case Tags.Aside:
                    case Tags.BlockQuote:
                    case Tags.Center:
                    case Tags.Details:
                    case Tags.Dialog:
                    case Tags.Dir:
                    case Tags.Div:
                    case Tags.Dl:
                    case Tags.Fieldset:
                    case Tags.Figcaption:
                    case Tags.Figure:
                    case Tags.Footer:
                    case Tags.Header:
                    case Tags.Hgroup:
                    case Tags.Menu:
                    case Tags.Nav:
                    case Tags.Ol:
                    case Tags.P:
                    case Tags.Section:
                    case Tags.Summary:
                    case Tags.Ul:
                    {
                        if (IsInButtonScope())
                            InBodyEndTagParagraph();

                        AddElement(tag);
                        break;
                    }
                    case Tags.H1:
                    case Tags.H2:
                    case Tags.H3:
                    case Tags.H4:
                    case Tags.H5:
                    case Tags.H6:
                    {
                        if (IsInButtonScope())
                            InBodyEndTagParagraph();

                        if (CurrentNode is HTMLHeadingElement)
                        {
                            RaiseErrorOccurred(ErrorCode.HeadingNested);
                            CloseCurrentNode();
                        }

                        var element = new HTMLHeadingElement();
                        AddElement(element, tag);
                        break;
                    }
                    case Tags.Pre:
                    case Tags.Listing:
                    {
                        if (IsInButtonScope())
                            InBodyEndTagParagraph();

                        var element = new HTMLPreElement();
                        AddElement(element, tag);
                        frameset = false;
                        PreventNewLine();
                        break;
                    }
                    case Tags.Form:
                    {
                        if (form == null)
                        {
                            if (IsInButtonScope())
                                InBodyEndTagParagraph();

                            var element = new HTMLFormElement();
                            AddElement(element, tag);
                            form = element;
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.FormAlreadyOpen);

                        break;
                    }
                    case Tags.Li:
                    {
                        InBodyStartTagListItem(tag);
                        break;
                    }
                    case Tags.Dd:
                    case Tags.Dt:
                    {
                        InBodyStartTagDefinitionItem(tag);
                        break;
                    }
                    case Tags.Plaintext:
                    {
                        if (IsInButtonScope())
                            InBodyEndTagParagraph();

                        AddElement(token.AsTag());
                        tokenizer.Switch(HtmlParseMode.Plaintext);
                        break;
                    }
                    case Tags.Button:
                    {
                        if (IsInScope<HTMLButtonElement>())
                        {
                            RaiseErrorOccurred(ErrorCode.ButtonInScope);
                            InBodyEndTagBlock(Tags.Button);
                            InBody(token);
                        }
                        else
                        {
                            ReconstructFormatting();
                            var element = new HTMLButtonElement();
                            AddElement(element, tag);
                            frameset = false;
                        }
                        break;
                    }
                    case Tags.A:
                    {
                        for (var i = formatting.Count - 1; i >= 0; i--)
                        {
                            if (formatting[i] == null)
                                break;
                            
                            if (formatting[i] is HTMLAnchorElement)
                            {
                                var format = formatting[i];
                                RaiseErrorOccurred(ErrorCode.AnchorNested);
                                HeisenbergAlgorithm(HtmlToken.CloseTag(Tags.A));

                                if(open.Contains(format)) 
                                    open.Remove(format);

                                if(formatting.Contains(format)) 
                                    formatting.RemoveAt(i);

                                break;
                            }
                        }

                        ReconstructFormatting();
                        var element = new HTMLAnchorElement();
                        AddElement(element, tag);
                        AddFormattingElement(element);
                        break;
                    }
                    case Tags.B:
                    case Tags.Big:
                    case Tags.Code:
                    case Tags.Em:
                    case Tags.Font:
                    case Tags.I:
                    case Tags.S:
                    case Tags.Small:
                    case Tags.Strike:
                    case Tags.Strong:
                    case Tags.Tt:
                    case Tags.U:
                    {
                        ReconstructFormatting();
                        var element = HtmlElementFactory.Create(tag.Name, doc);
                        AddElement(element, tag);
                        AddFormattingElement(element);
                        break;
                    }
                    case Tags.NoBr:
                    {
                        ReconstructFormatting();

                        if (IsInScope<HTMLNoNewlineElement>())
                        {
                            RaiseErrorOccurred(ErrorCode.NobrInScope);
                            HeisenbergAlgorithm(tag);
                            ReconstructFormatting();
                        }

                        var element = HtmlElementFactory.Create(tag.Name, doc);
                        AddElement(element, tag);
                        AddFormattingElement(element);
                        break;
                    }
                    case Tags.Applet:
                    case Tags.Marquee:
                    case Tags.Object:
                    {
                        ReconstructFormatting();
                        AddElement(tag);
                        AddScopeMarker();
                        frameset = false;
                        break;
                    }
                    case Tags.Table:
                    {
                        if (doc.QuirksMode == QuirksMode.Off && IsInButtonScope())
                            InBodyEndTagParagraph();

                        var element = new HTMLTableElement();
                        AddElement(element, tag);
                        frameset = false;
                        insert = HtmlTreeMode.InTable;
                        break;
                    }
                    case Tags.Area:
                    case Tags.Br:
                    case Tags.Embed:
                    case Tags.Keygen:
                    case Tags.Wbr:
                    case Tags.Img:
                    {
                        InBodyStartTagBreakrow(tag);
                        break;
                    }
                    case Tags.Image:
                    {
                        RaiseErrorOccurred(ErrorCode.ImageTagNamedWrong);
                        tag.Name = Tags.Img;
                        InBodyStartTagBreakrow(tag);
                        break;
                    }
                    case Tags.Input:
                    {
                        ReconstructFormatting();
                        var element = new HTMLInputElement();
                        AddElement(element, tag, true);
                        CloseCurrentNode();

                        if (!tag.GetAttribute(AttributeNames.Type).Equals("hidden", StringComparison.OrdinalIgnoreCase))
                            frameset = false;

                        break;
                    }
                    case Tags.Param:
                    case Tags.Source:
                    case Tags.Track:
                    {
                        AddElement(tag, true);
                        CloseCurrentNode();
                        break;
                    }
                    case Tags.Hr:
                    {
                        if (IsInButtonScope())
                            InBodyEndTagParagraph();

                        var element = new HTMLHRElement();
                        AddElement(element, tag, true);
                        CloseCurrentNode();
                        frameset = false;
                        break;
                    }
                    case Tags.IsIndex:
                    {
                        RaiseErrorOccurred(ErrorCode.TagInappropriate);

                        if (form == null)
                        {
                            InBody(HtmlToken.OpenTag(Tags.Form));

                            if (tag.GetAttribute(AttributeNames.Action) != String.Empty)
                                form.SetAttribute(AttributeNames.Action, tag.GetAttribute(AttributeNames.Action));

                            InBody(HtmlToken.OpenTag(Tags.Hr));
                            InBody(HtmlToken.OpenTag(Tags.Label));

                            if (tag.GetAttribute(AttributeNames.Prompt) != String.Empty)
                                AddCharacters(tag.GetAttribute(AttributeNames.Prompt));
                            else
                                AddCharacters("This is a searchable index. Enter search keywords: ");

                            var input = HtmlToken.OpenTag(Tags.Input);
                            input.AddAttribute(AttributeNames.Name, Tags.IsIndex);

                            for (int i = 0; i < tag.Attributes.Count; i++)
                            {
                                if (tag.Attributes[i].Key.IsOneOf(AttributeNames.Name, AttributeNames.Action, AttributeNames.Prompt))
                                    continue;

                                input.AddAttribute(tag.Attributes[i].Key, tag.Attributes[i].Value);
                            }

                            InBody(input);
                            InBody(HtmlToken.CloseTag(Tags.Label));
                            InBody(HtmlToken.OpenTag(Tags.Hr));
                            InBody(HtmlToken.CloseTag(Tags.Form));
                        }
                        break;
                    }
                    case Tags.Textarea:
                    {
                        var element = new HTMLTextAreaElement();
                        AddElement(element, tag);
                        tokenizer.Switch(HtmlParseMode.RCData);
                        originalInsert = insert;
                        frameset = false;
                        insert = HtmlTreeMode.Text;
                        PreventNewLine();
                        break;
                    }
                    case Tags.Xmp:
                    {
                        if (IsInButtonScope())
                            InBodyEndTagParagraph();

                        ReconstructFormatting();
                        frameset = false;
                        RawtextAlgorithm(tag);
                        break;
                    }
                    case Tags.Iframe:
                    {
                        frameset = false;
                        RawtextAlgorithm(tag);
                        break;
                    }
                    case Tags.Select:
                    {
                        ReconstructFormatting();
                        var element = new HTMLSelectElement();
                        AddElement(element, tag);
                        frameset = false;

                        switch (insert)
                        {
                            case HtmlTreeMode.InTable:
                            case HtmlTreeMode.InCaption:
                            case HtmlTreeMode.InRow:
                            case HtmlTreeMode.InCell:
                                insert = HtmlTreeMode.InSelectInTable;
                                break;

                            default:
                                insert = HtmlTreeMode.InSelect;
                                break;
                        }
                        break;
                    }
                    case Tags.Optgroup:
                    case Tags.Option:
                    {
                        if (CurrentNode is HTMLOptionElement)
                            InBodyEndTagAnythingElse(HtmlToken.CloseTag(Tags.Option));

                        ReconstructFormatting();
                        AddElement(tag);
                        break;
                    }
                    case Tags.Rp:
                    case Tags.Rt:
                    {
                        if (IsInScope<HTMLRubyElement>())
                        {
                            GenerateImpliedEndTags();

                            if (!(CurrentNode is HTMLRubyElement))
                                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
                        }
                            
                        AddElement(tag);
                        break;
                    }
                    case Tags.NoEmbed:
                    {
                        RawtextAlgorithm(tag);
                        break;
                    }
                    case Tags.NoScript:
                    {
                        if (doc.Options.IsScripting)
                        {
                            RawtextAlgorithm(tag);
                            break;
                        }

                        ReconstructFormatting();
                        AddElement(tag);
                        break;
                    }
                    case Tags.Math:
                    {
                        var element = new MathElement();
                        element.NodeName = tag.Name;
                        ReconstructFormatting();

                        for (int i = 0; i < tag.Attributes.Count; i++)
                        {
                            var name = tag.Attributes[i].Key;
                            var value = tag.Attributes[i].Value;
                            element.SetAdjustedAttribute(name.AdjustMathMLAttributeName(), value);
                        }

                        AddElement(element);

                        if (tag.IsSelfClosing)
                            open.Remove(element);

                        break;
                    }
                    case Tags.Svg:
                    {
                        var element = new SVGElement();
                        element.NodeName = tag.Name;
                        ReconstructFormatting();

                        for (int i = 0; i < tag.Attributes.Count; i++)
                        {
                            var name = tag.Attributes[i].Key;
                            var value = tag.Attributes[i].Value;
                            element.SetAdjustedAttribute(name.AdjustSvgAttributeName(), value);
                        }

                        AddElement(element);

                        if (tag.IsSelfClosing)
                            open.Remove(element);

                        break;
                    }
                    case Tags.Caption:
                    case Tags.Col:
                    case Tags.Colgroup:
                    case Tags.Frame:
                    case Tags.Head:
                    case Tags.Tbody:
                    case Tags.Td:
                    case Tags.Tfoot:
                    case Tags.Th:
                    case Tags.Thead:
                    case Tags.Tr:
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotStartHere);
                        break;
                    }
                    default:
                    {
                        ReconstructFormatting();
                        AddElement(tag);
                        break;
                    }
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Body:
                    {
                        InBodyEndTagBody();
                        break;
                    }
                    case Tags.Html:
                    {
                        if (InBodyEndTagBody())
                            AfterBody(token);

                        break;
                    }
                    case Tags.Address:
                    case Tags.Article:
                    case Tags.Aside:
                    case Tags.BlockQuote:
                    case Tags.Button:
                    case Tags.Center:
                    case Tags.Details:
                    case Tags.Dialog:
                    case Tags.Dir:
                    case Tags.Div:
                    case Tags.Dl:
                    case Tags.Fieldset:
                    case Tags.Figcaption:
                    case Tags.Figure:
                    case Tags.Footer:
                    case Tags.Header:
                    case Tags.Hgroup:
                    case Tags.Listing:
                    case Tags.Main:
                    case Tags.Menu:
                    case Tags.Nav:
                    case Tags.Ol:
                    case Tags.Pre:
                    case Tags.Section:
                    case Tags.Summary:
                    case Tags.Ul:
                    {
                        InBodyEndTagBlock(tag.Name);
                        break;
                    }
                    case Tags.Template:
                    {
                        InHead(tag);
                        break;
                    }
                    case Tags.Form:
                    {
                        var node = form;
                        form = null;

                        if (node != null && IsInScope(node.NodeName))
                        {
                            GenerateImpliedEndTags();

                            if (CurrentNode != node)
                                RaiseErrorOccurred(ErrorCode.FormClosedWrong);

                            open.Remove(node);
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.FormNotInScope);

                        break;
                    }
                    case Tags.P:
                    {
                        InBodyEndTagParagraph();
                        break;
                    }
                    case Tags.Li:
                    {
                        if (IsInListItemScope())
                        {
                            GenerateImpliedEndTagsExceptFor(tag.Name);

                            if (!(CurrentNode is HTMLLIElement))
                                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                            ClearStackBackTo<HTMLLIElement>();
                            CloseCurrentNode();
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.ListItemNotInScope);

                        break;
                    }
                    case Tags.Dd:
                    case Tags.Dt:
                    {
                        if (IsInScope(tag.Name))
                        {
                            GenerateImpliedEndTagsExceptFor(tag.Name);

                            if (CurrentNode.NodeName != tag.Name)
                                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                            ClearStackBackTo(tag.Name);
                            CloseCurrentNode();
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.ListItemNotInScope);

                        break;
                    }
                    case Tags.H1:
                    case Tags.H2:
                    case Tags.H3:
                    case Tags.H4:
                    case Tags.H5:
                    case Tags.H6:
                    {
                        if (IsInScope<HTMLHeadingElement>())
                        {
                            GenerateImpliedEndTags();

                            if (CurrentNode.NodeName != tag.Name)
                                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                            ClearStackBackTo<HTMLHeadingElement>();
                            CloseCurrentNode();
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.HeadingNotInScope);

                        break;
                    }
                    case Tags.A:
                    case Tags.B:
                    case Tags.Big:
                    case Tags.Code:
                    case Tags.Em:
                    case Tags.Font:
                    case Tags.I:
                    case Tags.NoBr:
                    case Tags.S:
                    case Tags.Small:
                    case Tags.Strike:
                    case Tags.Strong:
                    case Tags.Tt:
                    case Tags.U:
                    {
                        HeisenbergAlgorithm(tag);
                        break;
                    }
                    case Tags.Applet:
                    case Tags.Marquee:
                    case Tags.Object:
                    {
                        if (IsInScope(tag.Name))
                        {
                            GenerateImpliedEndTags();

                            if (CurrentNode.NodeName != tag.Name)
                                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                            ClearStackBackTo(tag.Name);
                            CloseCurrentNode();
                            ClearFormattingElements();
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.ObjectNotInScope);

                        break;
                    }
                    case Tags.Br:
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        InBodyStartTagBreakrow(HtmlToken.OpenTag(Tags.Br));
                        break;
                    }
                    default:
                    {
                        InBodyEndTagAnythingElse(tag);
                        break;
                    }
                }
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                CheckBodyOnClosing();

                if (templateMode.Count != 0)
                    InTemplate(token);
                else
                    End();
            }
        }

        /// <summary>
        /// See 8.2.5.4.8 The "text" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void Text(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                AddCharacters(((HtmlCharacterToken)token).Data);
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                CloseCurrentNode();
                insert = originalInsert;
                Consume(token);
            }
            else if (token.Type == HtmlTokenType.EndTag && ((HtmlTagToken)token).Name != Tags.Script)
            {
                CloseCurrentNode();
                insert = originalInsert;
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                RunScript();
            }
        }

        /// <summary>
        /// See 8.2.5.4.9 The "in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InTable(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Caption:
                    {
                        ClearStackBackTo<HTMLTableElement>();
                        AddScopeMarker();
                        var element = new HTMLTableCaptionElement();
                        AddElement(element, tag);
                        insert = HtmlTreeMode.InCaption;
                        break;
                    }
                    case Tags.Colgroup:
                    {
                        ClearStackBackTo<HTMLTableElement>();
                        var element = new HTMLTableColElement();
                        AddElement(element, tag);
                        insert = HtmlTreeMode.InColumnGroup;
                        break;
                    }
                    case Tags.Col:
                    {
                        InTable(HtmlToken.OpenTag(Tags.Colgroup));
                        InColumnGroup(token);
                        break;
                    }
                    case Tags.Tbody:
                    case Tags.Thead:
                    case Tags.Tfoot:
                    {
                        ClearStackBackTo<HTMLTableElement>();
                        var element = new HTMLTableSectionElement();
                        AddElement(element, tag);
                        insert = HtmlTreeMode.InTableBody;
                        break;
                    }
                    case Tags.Td:
                    case Tags.Th:
                    case Tags.Tr:
                    {
                        InTable(HtmlToken.OpenTag(Tags.Tbody));
                        InTableBody(token);
                        break;
                    }
                    case Tags.Table:
                    {
                        RaiseErrorOccurred(ErrorCode.TableNesting);

                        if (InTableEndTagTable())
                            Home(token);

                        break;
                    }
                    case Tags.Script:
                    case Tags.Style:
                    case Tags.Template:
                    {
                        InHead(token);
                        break;
                    }
                    case Tags.Input:
                    {
                        if (tag.GetAttribute(AttributeNames.Type).Equals("hidden", StringComparison.OrdinalIgnoreCase))
                        {
                            RaiseErrorOccurred(ErrorCode.InputUnexpected);
                            var element = new HTMLInputElement();
                            AddElement(element, tag, true);
                            CloseCurrentNode();
                        }
                        else
                        {
                            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                            InBodyWithFoster(token);
                        }

                        break;
                    }
                    case Tags.Form:
                    {
                        RaiseErrorOccurred(ErrorCode.FormInappropriate);

                        if (form == null)
                        {
                            var element = new HTMLFormElement();
                            AddElement(element, tag);
                            form = element;
                            CloseCurrentNode();
                        }

                        break;
                    }
                    default:
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInTableDetected);
                        InBodyWithFoster(token);
                        break;
                    }
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Table:
                    {
                        InTableEndTagTable();
                        break;
                    }
                    case Tags.Template:
                    {
                        InHead(token);
                        break;
                    }
                    case Tags.Body:
                    case Tags.Colgroup:
                    case Tags.Col:
                    case Tags.Caption:
                    case Tags.Html:
                    case Tags.Tbody:
                    case Tags.Tr:
                    case Tags.Thead:
                    case Tags.Th:
                    case Tags.Tfoot:
                    case Tags.Td:
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        break;
                    }
                    default:
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInTableDetected);
                        InBodyWithFoster(token);
                        break;
                    }
                }
            }
            else if (token.Type == HtmlTokenType.Character && CurrentNode != null && CurrentNode.IsTableElement())
            {
                InTableText((HtmlCharacterToken)token);
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                InBody(token);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                InBodyWithFoster(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.10 The "in table text" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InTableText(HtmlCharacterToken token)
        {
            var anyAreNotSpaceCharacters = token.HasContent;

            if (anyAreNotSpaceCharacters)
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);

            if (anyAreNotSpaceCharacters)
                InBodyWithFoster(token);
            else
                AddCharacters(token.Data);
        }

        /// <summary>
        /// See 8.2.5.4.11 The "in caption" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InCaption(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Caption:
                    {
                        InCaptionEndTagCaption();
                        break;
                    }
                    case Tags.Body:
                    case Tags.Th:
                    case Tags.Colgroup:
                    case Tags.Html:
                    case Tags.Tbody:
                    case Tags.Col:
                    case Tags.Tfoot:
                    case Tags.Td:
                    case Tags.Thead:
                    case Tags.Tr:
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        break;
                    }
                    case Tags.Table:
                    {
                        RaiseErrorOccurred(ErrorCode.TableNesting);

                        if (InCaptionEndTagCaption())
                            InTable(token);

                        break;
                    }
                    default:
                    {
                        InBody(token);
                        break;
                    }
                }
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Caption:
                    case Tags.Col:
                    case Tags.Colgroup:
                    case Tags.Tbody:
                    case Tags.Td:
                    case Tags.Tfoot:
                    case Tags.Th:
                    case Tags.Thead:
                    case Tags.Tr: 
                        RaiseErrorOccurred(ErrorCode.TagCannotStartHere);

                        if (InCaptionEndTagCaption())
                            InTable(token);

                        break;

                    default:
                        InBody(token);
                        break;
                }
            }
            else
                InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.12 The "in column group" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InColumnGroup(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                var str = chars.TrimStart();
                AddCharacters(str);
            }
            else if (token.Type == HtmlTokenType.Comment)
                AddComment(token);
            else if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.IsStartTag(Tags.Html))
                InBody(token);
            else if (token.IsStartTag(Tags.Col))
            {
                var element = new HTMLTableColElement();
                AddElement(element, token.AsTag(), true);
                CloseCurrentNode();
            }
            else if (token.IsEndTag(Tags.Colgroup))
                InColumnGroupEndTagColgroup();
            else if (token.IsEndTag(Tags.Col))
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
            else if (token.IsTag(Tags.Template))
                InHead(token);
            else if (token.Type == HtmlTokenType.EOF)
                InBody(token);
            else if (InColumnGroupEndTagColgroup())
                InTable(token);
        }

        /// <summary>
        /// See 8.2.5.4.13 The "in table body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InTableBody(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name == Tags.Tr)
                {
                    ClearStackBackTo<HTMLTableSectionElement>();
                    var element = new HTMLTableRowElement();
                    AddElement(element, token.AsTag());
                    insert = HtmlTreeMode.InRow;
                }
                else if (tag.Name.IsTableCellElement())
                {
                    InTableBody(HtmlToken.OpenTag(Tags.Tr));
                    InRow(token);
                }
                else if (tag.Name.IsGeneralTableElement())
                {
                    InTableBodyCloseTable(tag);
                }
                else
                {
                    InTable(token);
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name.IsTableSectionElement())
                {
                    if (IsInTableScope(((HtmlTagToken)token).Name))
                    {
                        ClearStackBackTo<HTMLTableSectionElement>();
                        CloseCurrentNode();
                        insert = HtmlTreeMode.InTable;
                    }
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.TableSectionNotInScope);
                    }
                }
                else if (tag.Name.IsSpecialTableElement(true))
                {
                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                }
                else if(tag.Name == Tags.Table)
                {
                    InTableBodyCloseTable(tag);
                }
                else
                {
                    InTable(token);
                }
            }
            else
            {
                InTable(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.14 The "in row" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InRow(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name.IsTableCellElement())
                {
                    ClearStackBackTo<HTMLTableRowElement>();
                    var element = HtmlElementFactory.Create(tag.Name, doc);
                    AddElement(element, token.AsTag());
                    insert = HtmlTreeMode.InCell;
                    AddScopeMarker();
                }
                else if (tag.Name.IsGeneralTableElement(true))
                {
                    if (InRowEndTagTablerow())
                        InTableBody(token);
                }
                else
                {
                    InTable(token);
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                if(tag.Name == Tags.Tr)   
                {
                    InRowEndTagTablerow();
                }
                else if (tag.Name == Tags.Table)
                {
                    if (InRowEndTagTablerow())
                        InTableBody(token);
                }
                else if (tag.Name.IsTableSectionElement())
                {
                    if (IsInTableScope(tag.Name))
                    {
                        InRowEndTagTablerow();
                        InTableBody(token);
                    }
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.TableSectionNotInScope);
                    }
                }
                else if (tag.Name.IsSpecialTableElement())
                {
                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                }
                else
                {
                    InTable(token);
                }
            }
            else
            {
                InTable(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.15 The "in cell" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InCell(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name.IsTableCellElement())
                    InCellEndTagCell();
                else if (tag.Name.IsSpecialTableElement())
                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                else if (tag.Name.IsTableElement())
                {
                    if (IsInTableScope(tag.Name))
                    {
                        InCellEndTagCell();
                        Home(token);
                    }
                    else
                        RaiseErrorOccurred(ErrorCode.TableNotInScope);
                }
                else
                    InBody(token);
            }
            else if (token.Type == HtmlTokenType.StartTag && (((HtmlTagToken)token).Name.IsGeneralTableElement(true) || ((HtmlTagToken)token).Name.IsTableCellElement()))
            {
                var tag = (HtmlTagToken)token;

                if (IsInTableScope(Tags.Td) || IsInTableScope(Tags.Th))
                {
                    InCellEndTagCell();
                    Home(token);
                }
                else
                    RaiseErrorOccurred(ErrorCode.TableCellNotInScope);
            }
            else
                InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.16 The "in select" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InSelect(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                AddCharacters(((HtmlCharacterToken)token).Data);
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Html:
                    {
                        InBody(token);
                        break;
                    }
                    case Tags.Option:
                    {
                        if (CurrentNode is HTMLOptionElement)
                            InSelectEndTagOption();

                        var element = new HTMLOptionElement();
                        AddElement(element, token.AsTag());
                        break;
                    }
                    case Tags.Optgroup:
                    {
                        if (CurrentNode is HTMLOptionElement)
                            InSelectEndTagOption();
                        
                        if (CurrentNode is HTMLOptGroupElement)
                            InSelectEndTagOptgroup();

                        var element = new HTMLOptGroupElement();
                        AddElement(element, token.AsTag());
                        break;
                    }
                    case Tags.Select:
                    {
                        RaiseErrorOccurred(ErrorCode.SelectNesting);
                        InSelectEndTagSelect();
                        break;
                    }
                    case Tags.Input:
                    case Tags.Keygen:
                    case Tags.Textarea:
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);

                        if (IsInSelectScope(Tags.Select))
                        {
                            InSelectEndTagSelect();
                            Home(token);
                        }

                        break;
                    }
                    case Tags.Template:
                    case Tags.Script:
                    {
                        InHead(token);
                        break;
                    }
                    default:
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);
                        break;
                    }
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.Template:
                    {
                        InHead(token);
                        break;
                    }
                    case Tags.Optgroup:
                    {
                        InSelectEndTagOptgroup();
                        break;
                    }
                    case Tags.Option:
                    {
                        InSelectEndTagOption();
                        break;
                    }
                    case Tags.Select:
                    {
                        if (IsInSelectScope(Tags.Select))
                            InSelectEndTagSelect();
                        else
                            RaiseErrorOccurred(ErrorCode.SelectNotInScope);

                        break;
                    }
                    default:
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        break;
                    }
                }
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                InBody(token);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            }
        }

        /// <summary>
        /// See 8.2.5.4.17 The "in select in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InSelectInTable(HtmlToken token)
        {
            var tag = token as HtmlTagToken;

            if (tag != null && (tag.Name.IsTableCellElement() || tag.Name.IsTableElement() || tag.Name == Tags.Caption))
            {
                if (token.Type == HtmlTokenType.StartTag)
                {
                    RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);
                    InSelectEndTagSelect();
                    Home(token);
                }
                else
                {
                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);

                    if (IsInTableScope(tag.Name))
                    {
                        InSelectEndTagSelect();
                        Home(token);
                    }
                }
            }
            else
            {
                InSelect(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.18 The "in template" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InTemplate(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                case HtmlTokenType.Comment:
                case HtmlTokenType.DOCTYPE:
                {
                    InBody(token);
                    break;
                }
                case HtmlTokenType.StartTag:
                {
                    var tag = (HtmlTagToken)token;

                    switch (tag.Name)
                    {
                        case Tags.Base:
                        case Tags.BaseFont:
                        case Tags.Link:
                        case Tags.Meta:
                        case Tags.Bgsound:
                        case Tags.NoFrames:
                        case Tags.Script:
                        case Tags.Style:
                        case Tags.Template:
                        case Tags.Title:
                            InHead(token);
                            break;

                        case Tags.Caption:
                        case Tags.Colgroup:
                        case Tags.Tbody:
                        case Tags.Tfoot:
                        case Tags.Thead:
                            TemplateStep(token, HtmlTreeMode.InTable);
                            break;

                        case Tags.Col:
                            TemplateStep(token, HtmlTreeMode.InColumnGroup);
                            break;

                        case Tags.Tr:
                            TemplateStep(token, HtmlTreeMode.InTableBody);
                            break;

                        case Tags.Td:
                        case Tags.Th:
                            TemplateStep(token, HtmlTreeMode.InRow);
                            break;

                        default:
                            TemplateStep(token, HtmlTreeMode.InBody);
                            break;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tag = (HtmlTagToken)token;

                    if (tag.Name == Tags.Template)
                        InHead(token);
                    else
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);

                    break;
                }
                case HtmlTokenType.EOF:
                    if (TagCurrentlyOpen(Tags.Template))
                    {
                        RaiseErrorOccurred(ErrorCode.EOF);
                        CloseTemplate();
                        Home(token);
                        return;
                    }

                    End();
                    break;
            }
        }

        /// <summary>
        /// See 8.2.5.4.19 The "after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterBody(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chars = (HtmlCharacterToken)token;
                var str = chars.TrimStart();
                ReconstructFormatting();
                AddCharacters(str);

                if (chars.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(open[0], token);
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }
            else if(token.IsTag(Tags.Html))
            {
                if (token.Type == HtmlTokenType.StartTag)
                    InBody(token);
                else if (IsFragmentCase)
                    RaiseErrorOccurred(ErrorCode.TagInvalidInFragmentMode);
                else
                    insert = HtmlTreeMode.AfterAfterBody;

                return;
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                End();
                return;
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            insert = HtmlTreeMode.InBody;
            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.20 The "in frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InFrameset(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chrs = (HtmlCharacterToken)token;
                var str = chrs.TrimStart();
                AddCharacters(str);

                if (chrs.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name == Tags.Html)
                {
                    InBody(token);
                    return;
                }
                else if (tag.Name == Tags.Frameset)
                {
                    var element = new HTMLFrameSetElement();
                    AddElement(element, token.AsTag());
                    return;
                }
                else if (tag.Name == Tags.Frame)
                {
                    var element = new HTMLFrameElement();
                    AddElement(element, token.AsTag(), true);
                    CloseCurrentNode();
                    return;
                }
                else if (tag.Name == Tags.NoFrames)
                {
                    InHead(token);
                    return;
                }
            }
            else if (token.IsEndTag(Tags.Frameset))
            {
                if (CurrentNode != open[0])
                {
                    CloseCurrentNode();

                    if (!IsFragmentCase && !(CurrentNode is HTMLFrameSetElement))
                        insert = HtmlTreeMode.AfterFrameset;
                }
                else
                    RaiseErrorOccurred(ErrorCode.CurrentNodeIsRoot);

                return;
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                if (CurrentNode != doc.DocumentElement)
                    RaiseErrorOccurred(ErrorCode.CurrentNodeIsNotRoot);

                End();
                return;
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
        }

        /// <summary>
        /// See 8.2.5.4.21 The "after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterFrameset(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chrs = (HtmlCharacterToken)token;
                var str = chrs.TrimStart();
                AddCharacters(str);

                if (chrs.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
                return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                return;
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name == Tags.Html)
                {
                    InBody(token);
                    return;
                }
                else if (tag.Name == Tags.NoFrames)
                {
                    InHead(token);
                    return;
                }
            }
            else if (token.IsEndTag(Tags.Html))
            {
                insert = HtmlTreeMode.AfterAfterFrameset;
                return;
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                End();
                return;
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
        }

        /// <summary>
        /// See 8.2.5.4.22 The "after after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterAfterBody(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(doc, token);
                return;
            }
            else if (token.Type == HtmlTokenType.Character)
            {
                var chrs = (HtmlCharacterToken)token;
                var str = chrs.TrimStart();
                ReconstructFormatting();
                AddCharacters(str);

                if (chrs.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE || token.IsStartTag(Tags.Html))
            {
                InBody(token);
                return;
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                End();
                return;
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            insert = HtmlTreeMode.InBody;
            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.23 The "after after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterAfterFrameset(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(doc, token);
                return;
            }
            else if (token.Type == HtmlTokenType.Character)
            {
                var chrs = (HtmlCharacterToken)token;
                var str = chrs.TrimStart();
                ReconstructFormatting();
                AddCharacters(str);

                if (chrs.IsEmpty)
                    return;
            }
            else if (token.Type == HtmlTokenType.DOCTYPE || token.IsStartTag(Tags.Html))
            {
                InBody(token);
                return;
            }
            else if (token.IsStartTag(Tags.NoFrames))
            {
                InHead(token);
                return;
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                End();
                return;
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
        }

        #endregion

        #region Substates

        /// <summary>
        /// Inserting something in the template.
        /// </summary>
        /// <param name="token">The token to insert.</param>
        /// <param name="mode">The mode to push.</param>
        void TemplateStep(HtmlToken token, HtmlTreeMode mode)
        {
            templateMode.Pop();
            templateMode.Push(mode);
            insert = mode;
            Home(token);
        }

        /// <summary>
        /// Closes the template element.
        /// </summary>
        void CloseTemplate()
        {
            while (open.Count > 0)
            {
                var node = CurrentNode;
                CloseCurrentNode();

                if (node is HTMLTemplateElement)
                    break;
            }

            ClearFormattingElements();
            templateMode.Pop();
            Reset();
        }

        /// <summary>
        /// Closes the table if the section is in table scope.
        /// </summary>
        /// <param name="tag">The tag to insert which triggers the closing of the table.</param>
        void InTableBodyCloseTable(HtmlTagToken tag)
        {
            if (IsInTableScope<HTMLTableSectionElement>())
            {
                ClearStackBackTo<HTMLTableSectionElement>();
                CloseCurrentNode();
                insert = HtmlTreeMode.InTable;
                InTable(tag);
            }
            else
                RaiseErrorOccurred(ErrorCode.TableSectionNotInScope);
        }

        /// <summary>
        /// Acts if a option end tag had been seen in the InSelect state.
        /// </summary>
        void InSelectEndTagOption()
        {
            if (CurrentNode is HTMLOptionElement)
                CloseCurrentNode();
            else
                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
        }

        /// <summary>
        /// Acts if a optgroup end tag had been seen in the InSelect state.
        /// </summary>
        void InSelectEndTagOptgroup()
        {
            if (open.Count > 1 && open[open.Count - 1] is HTMLOptionElement && open[open.Count - 2] is HTMLOptGroupElement)
                CloseCurrentNode();

            if (CurrentNode is HTMLOptGroupElement)
                CloseCurrentNode();
            else
                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
        }

        /// <summary>
        /// Act as if an colgroup end tag has been found in the InColumnGroup state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InColumnGroupEndTagColgroup()
        {
            if (CurrentNode.NodeName == Tags.Colgroup)
            {
                CloseCurrentNode();
                insert = HtmlTreeMode.InTable;
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
                return false;
            }
        }

        /// <summary>
        /// Act as if a body start tag has been found in the AfterHead state.
        /// </summary>
        /// <param name="token"></param>
        void AfterHeadStartTagBody(HtmlTagToken token)
        {
            var element = new HTMLBodyElement();
            AddElement(element, token);
            frameset = false;
            insert = HtmlTreeMode.InBody;
        }

        /// <summary>
        /// Follows the generic rawtext parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        void RawtextAlgorithm(HtmlTagToken tag)
        {
            var element = HtmlElementFactory.Create(tag.Name, doc);
            AddElement(element, tag);
            originalInsert = insert;
            insert = HtmlTreeMode.Text;
            tokenizer.Switch(HtmlParseMode.Rawtext);
        }

        /// <summary>
        /// Follows the generic RCData parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        void RCDataAlgorithm(HtmlTagToken tag)
        {
            var element = HtmlElementFactory.Create(tag.Name, doc);
            AddElement(element, tag);
            originalInsert = insert;
            insert = HtmlTreeMode.Text;
            tokenizer.Switch(HtmlParseMode.RCData);
        }

        /// <summary>
        /// Acts if a li start tag in the InBody state has been found.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        void InBodyStartTagListItem(HtmlTagToken tag)
        {
            frameset = false;
            var index = open.Count - 1;
            var node = open[index];

            while (true)
            {
                if (node is HTMLLIElement && node.NodeName == Tags.Li)
                {
                    InBody(HtmlToken.CloseTag(node.NodeName));
                    break;
                }

                if (node is HTMLAddressElement == false && node is HTMLDivElement == false && node is HTMLParagraphElement == false && node.IsSpecial)
                    break;
                
                node = open[--index];
            }

            if (IsInButtonScope())
                InBodyEndTagParagraph();

            var element = HtmlElementFactory.Create(tag.Name, doc);
            AddElement(element, tag);
        }

        /// <summary>
        /// Acts if a dd or dt start tag in the InBody state has been found.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        void InBodyStartTagDefinitionItem(HtmlTagToken tag)
        {
            frameset = false;
            var index = open.Count - 1;
            var node = open[index];

            while (true)
            {
                if (node is HTMLLIElement && (node.NodeName == Tags.Dd || node.NodeName == Tags.Dt))
                {
                    InBody(HtmlToken.CloseTag(node.NodeName));
                    break;
                }

                if (node is HTMLAddressElement == false && node is HTMLDivElement == false && node is HTMLParagraphElement == false && node.IsSpecial)
                    break;

                node = open[--index];
            }

            if (IsInButtonScope())
                InBodyEndTagParagraph();

            var element = HtmlElementFactory.Create(tag.Name, doc);
            AddElement(element, tag);
        }

        /// <summary>
        /// Acts if a button or similar end tag had been seen in the InBody state.
        /// </summary>
        /// <param name="tagName">The name of the block element.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InBodyEndTagBlock(String tagName)
        {
            if (IsInScope(tagName))
            {
                GenerateImpliedEndTags();

                if (CurrentNode.NodeName != tagName)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo(tagName);
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.BlockNotInScope);
                return false;
            }
        }

        /// <summary>
        /// Acts if a nobr tag had been seen in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        void HeisenbergAlgorithm(HtmlTagToken tag)
        {
            var outer = 0;
            var inner = 0;
            var bookmark = 0;
            var index = 0;

            Element formattingElement;
            Element furthestBlock;
            Element commonAncestor;
            Element node;
            Element lastNode;

            while (outer < 8)
            {
                outer++;
                index = 0;
                formattingElement = null;

                for (var j = formatting.Count - 1; j >= 0; j--)
                {
                    if (formatting[j] == null)
                        break;
                    
                    if (formatting[j].NodeName == tag.Name)
                    {
                        index = j;
                        formattingElement = formatting[j];
                        break;
                    }
                }

                if (formattingElement == null)
                {
                    InBodyEndTagAnythingElse(tag);
                    break;
                }

                var openIndex = open.IndexOf(formattingElement);

                if (openIndex == -1)
                {
                    RaiseErrorOccurred(ErrorCode.FormattingElementNotFound);
                    formatting.Remove(formattingElement);
                    break;
                }

                if (!IsInScope(formattingElement.NodeName))
                {
                    RaiseErrorOccurred(ErrorCode.ElementNotInScope);
                    break;
                }

                if (openIndex != open.Count - 1)
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);

                furthestBlock = null;
                bookmark = index;

                for (var j = openIndex + 1; j < open.Count; j++)
                {
                    if (open[j].IsSpecial)
                    {
                        index = j;
                        furthestBlock = open[j];
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

                    formatting.Remove(formattingElement);
                    break;
                }

                commonAncestor = open[openIndex - 1];
                inner = 0;
                node = furthestBlock;
                lastNode = furthestBlock;

                while (true)
                {
                    inner++;
                    node = open[--index];

                    if (node == formattingElement)
                        break;

                    if (inner > 3 && formatting.Contains(node))
                        formatting.Remove(node);

                    if (!formatting.Contains(node))
                    {
                        open.Remove(node);
                        continue;
                    }

                    var newElement = CopyElement(node);
                    commonAncestor.AppendChild(newElement);
                    open[index] = newElement;
                    
                    for(var l = 0; l != formatting.Count; l++)
                    {
                        if(formatting[l] == node)
                        {
                            formatting[l] = newElement;
                            break;
                        }
                    }

                    node = newElement;

                    if (lastNode == furthestBlock)
                        bookmark++;

                    if (lastNode.Parent != null)
                        lastNode.Parent.RemoveChild(lastNode);

                    node.AppendChild(lastNode);
                    lastNode = node;
                }

                if (commonAncestor.IsTableElement())
                    AddElementWithFoster(lastNode);
                else
                {
                    if (lastNode.Parent != null)
                        lastNode.Parent.RemoveChild(lastNode);

                    commonAncestor.AppendChild(lastNode);
                }

                var element = CopyElement(formattingElement);

                while (furthestBlock.ChildNodes.Length > 0)
                    element.AppendChild(furthestBlock.RemoveChild(furthestBlock.ChildNodes[0]));

                furthestBlock.AppendChild(element);
                formatting.Remove(formattingElement);
                formatting.Insert(bookmark, element);
                open.Remove(formattingElement);
                open.Insert(open.IndexOf(furthestBlock) + 1, element);
            }
        }

        /// <summary>
        /// Copies the element and its attributes to create a new element.
        /// </summary>
        /// <param name="element">The old element (source).</param>
        /// <returns>The new element (target).</returns>
        Element CopyElement(Element element)
        {
            var newElement = HtmlElementFactory.Create(element.NodeName, doc);

            foreach (var attr in element.Attributes)
                newElement.SetAttribute(attr.Name, attr.Value);

            return newElement;
        }

        /// <summary>
        /// Performs the InBody state with foster parenting.
        /// </summary>
        /// <param name="token">The given token.</param>
        void InBodyWithFoster(HtmlToken token)
        {
            foster = true;
            InBody(token);
            foster = false;
        }

        /// <summary>
        /// Act as if an anything else end tag has been found in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag found.</param>
        void InBodyEndTagAnythingElse(HtmlTagToken tag)
        {
            var index = open.Count - 1;
            var node = CurrentNode;

            do
            {
                if (node.NodeName == tag.Name)
                {
                    GenerateImpliedEndTagsExceptFor(tag.Name);

                    if (node.NodeName == tag.Name)
                        RaiseErrorOccurred(ErrorCode.TagClosedWrong);

                    for (int i = open.Count - 1; index <= i; i--)
                        CloseCurrentNode();

                    break;
                }
                else if (node.IsSpecial)
                {
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    break;
                }

                node = open[--index];
            }
            while (true);
        }

        /// <summary>
        /// Act as if an body end tag has been found in the InBody state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InBodyEndTagBody()
        {
            if (IsInScope<HTMLBodyElement>())
            {
                CheckBodyOnClosing();
                insert = HtmlTreeMode.AfterBody;
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.BodyNotInScope);
                return false;
            }
        }

        /// <summary>
        /// Act as if an br start tag has been found in the InBody state.
        /// </summary>
        /// <param name="tag">The actual tag found.</param>
        void InBodyStartTagBreakrow(HtmlTagToken tag)
        {
            ReconstructFormatting();
            var element = HtmlElementFactory.Create(tag.Name, doc);
            AddElement(element, tag);
            CloseCurrentNode();
            frameset = false;
        }

        /// <summary>
        /// Act as if an p end tag has been found in the InBody state.
        /// </summary>
        /// <returns>True if the token was found, otherwise false.</returns>
        Boolean InBodyEndTagParagraph()
        {
            if (IsInButtonScope())
            {
                GenerateImpliedEndTagsExceptFor(Tags.P);

                if (CurrentNode is HTMLParagraphElement == false)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo<HTMLParagraphElement>();
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.ParagraphNotInScope);
                InBody(HtmlToken.OpenTag(Tags.P));
                InBodyEndTagParagraph();
                return false;
            }
        }

        /// <summary>
        /// Act as if an table end tag has been found in the InTable state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InTableEndTagTable()
        {
            if (IsInTableScope<HTMLTableElement>())
            {
                ClearStackBackTo<HTMLTableElement>();
                CloseCurrentNode();
                Reset();
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TableNotInScope);
                return false;
            }
        }

        /// <summary>
        /// Act as if an tr end tag has been found in the InRow state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InRowEndTagTablerow()
        {
            if (IsInTableScope<HTMLTableRowElement>())
            {
                ClearStackBackTo<HTMLTableRowElement>();
                CloseCurrentNode();
                insert = HtmlTreeMode.InTableBody;
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TableRowNotInScope);
                return false;
            }
        }

        /// <summary>
        /// Act as if an select end tag has been found in the InSelect state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        void InSelectEndTagSelect()
        {
            ClearStackBackTo<HTMLSelectElement>();
            CloseCurrentNode();
            Reset();
        }

        /// <summary>
        /// Act as if an caption end tag has been found in the InCaption state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InCaptionEndTagCaption()
        {
            if (IsInTableScope<HTMLTableCaptionElement>())
            {
                GenerateImpliedEndTags();

                if (CurrentNode is HTMLTableCaptionElement == false)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo<HTMLTableCaptionElement>();
                CloseCurrentNode();
                ClearFormattingElements();
                insert = HtmlTreeMode.InTable;
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.CaptionNotInScope);
                return false;
            }
        }

        /// <summary>
        /// Act as if an td or th end tag has been found in the InCell state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InCellEndTagCell()
        {
            if (IsInTableScope<HTMLTableCellElement>())
            {
                GenerateImpliedEndTags();

                if (CurrentNode is HTMLTableCellElement == false)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo<HTMLTableCellElement>();
                CloseCurrentNode();
                ClearFormattingElements();
                insert = HtmlTreeMode.InRow;
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TableCellNotInScope);
                return false;
            }
        }

        #endregion

        #region Foreign Content

        /// <summary>
        /// 8.2.5.5 The rules for parsing tokens in foreign content
        /// </summary>
        /// <param name="token">The token to examine.</param>
        void Foreign(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Character)
            {
                var chrs = (HtmlCharacterToken)token;
                AddCharacters(chrs.Data.Replace(Specification.Null, Specification.Replacement));

                if(chrs.HasContent)
                    frameset = false;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case Tags.B:
                    case Tags.Big:
                    case Tags.BlockQuote:
                    case Tags.Body:
                    case Tags.Br:
                    case Tags.Center:
                    case Tags.Code:
                    case Tags.Dd:
                    case Tags.Div:
                    case Tags.Dl:
                    case Tags.Dt:
                    case Tags.Em:
                    case Tags.Embed:
                    case Tags.H1:
                    case Tags.H2:
                    case Tags.H3:
                    case Tags.H4:
                    case Tags.H5:
                    case Tags.H6:
                    case Tags.Head:
                    case Tags.Hr:
                    case Tags.I:
                    case Tags.Img:
                    case Tags.Li:
                    case Tags.Listing:
                    case Tags.Main:
                    case Tags.Menu:
                    case Tags.Meta:
                    case Tags.NoBr:
                    case Tags.Ol:
                    case Tags.P:
                    case Tags.Pre:
                    case Tags.Ruby:
                    case Tags.S:
                    case Tags.Small:
                    case Tags.Span:
                    case Tags.Strong:
                    case Tags.Strike:
                    case Tags.Sub:
                    case Tags.Sup:
                    case Tags.Table:
                    case Tags.Tt:
                    case Tags.U:
                    case Tags.Ul:
                    case Tags.Var:
                        ForeignNormalTag(token);
                        break;

                    case Tags.Font:
                        for (var i = 0; i != tag.Attributes.Count; i++)
                        {
                            if (tag.Attributes[i].Key.IsOneOf(HTMLFontElement.AttrColor, HTMLFontElement.AttrFace, HTMLFontElement.AttrSize))
                            {
                                ForeignNormalTag(token);
                                return;
                            }
                        }

                        ForeignSpecialTag(tag);
                        break;

                    default:
                        ForeignSpecialTag(tag);
                        break;
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                if (CurrentNode is HTMLScriptElement && tag.Name == Tags.Script)
                {
                    RunScript();
                }
                else
                {
                    var node = CurrentNode;

                    if (node.NodeName != tag.Name)
                        RaiseErrorOccurred(ErrorCode.TagClosingMismatch);

                    for (int i = open.Count - 1; i > 0; i--)
                    {
                        if (node.NodeName.Equals(tag.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            open.RemoveRange(i + 1, open.Count - i - 1);
                            CloseCurrentNode();
                            break;
                        }

                        node = open[i - 1];

                        if (node.IsInHtml)
                        {
                            Home(token);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes a special start tag token.
        /// </summary>
        /// <param name="tag">The tag token to process.</param>
        void ForeignSpecialTag(HtmlTagToken tag)
        {
            var node = CreateForeignElementFrom(tag);

            if (node != null)
            {
                AddForeignElement(node);

                if (!tag.IsSelfClosing)
                {
                    open.Add(node);
                    tokenizer.AcceptsCharacterData = true;
                }
                else if (tag.Name == Tags.Script)
                    Foreign(HtmlToken.CloseTag(Tags.Script));
            }
        }

        /// <summary>
        /// Creates a foreign element from the given html tag.
        /// </summary>
        /// <param name="tag">The tag of the foreign element.</param>
        /// <returns>The element or NULL if it is no MathML or SVG element.</returns>
        Element CreateForeignElementFrom(HtmlTagToken tag)
        {
            if (AdjustedCurrentNode.IsInMathML)
            {
                var node = MathElementFactory.Create(tag.Name, doc);

                for (int i = 0; i < tag.Attributes.Count; i++)
                {
                    var name = tag.Attributes[i].Key;
                    var value = tag.Attributes[i].Value;
                    node.SetAdjustedAttribute(name.AdjustMathMLAttributeName(), value);
                }

                return node;
            }
            else if (AdjustedCurrentNode.IsInSvg)
            {
                var node = SvgElementFactory.Create(tag.Name.AdjustSvgTagName(), doc);

                for (int i = 0; i < tag.Attributes.Count; i++)
                {
                    var name = tag.Attributes[i].Key;
                    var value = tag.Attributes[i].Value;
                    node.SetAdjustedAttribute(name.AdjustSvgAttributeName(), value);
                }

                return node;
            }

            return null;
        }

        /// <summary>
        /// Processes a normal start tag token.
        /// </summary>
        /// <param name="token">The token to process.</param>
        void ForeignNormalTag(HtmlToken token)
        {
            RaiseErrorOccurred(ErrorCode.TagCannotStartHere);

            do
            {
                CloseCurrentNode();
            }
            while (!CurrentNode.IsHtmlTIP && !CurrentNode.IsMathMLTIP && !CurrentNode.IsInHtml);

            Consume(token);
        }

        #endregion

        #region Scope

        /// <summary>
        /// Determines if the given tag name is in the global scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        Boolean IsInScope(String tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;

                if (node is IScopeElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given type is in the global scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        Boolean IsInScope<T>()
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node is T)
                    return true;

                if (node is IScopeElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the list scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        Boolean IsInListItemScope()
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node is HTMLLIElement)
                    return true;

                if (node is IListScopeElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if a paragraph is in the button scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        Boolean IsInButtonScope()
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node is HTMLParagraphElement)
                    return true;

                if (node is IScopeElement || node is HTMLButtonElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given type is in the table scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        Boolean IsInTableScope<T>()
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node is T)
                    return true;

                if (node is ITableScopeElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the table scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        Boolean IsInTableScope(String tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;

                if (node is ITableScopeElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the select scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        Boolean IsInSelectScope(String tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;

                if (node is ISelectScopeElement)
                    continue;

                return false;
            }

            return false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        void Kernel()
        {
            HtmlToken token;

            do
            {
                token = tokenizer.Get();
                Consume(token);
            }
            while (token.Type != HtmlTokenType.EOF);
        }

        /// <summary>
        /// Runs a script given by the current node.
        /// </summary>
        void RunScript()
        {
            doc.PerformMicrotaskCheckpoint();
            doc.ProvideStableState();
            var script = (HTMLScriptElement)CurrentNode;
            CloseCurrentNode();
            insert = originalInsert;
            var oldInsertion = tokenizer.Stream.InsertionPoint;
            nesting++;
            script.Prepare();
            nesting--;
            tokenizer.Stream.InsertionPoint = oldInsertion;

            if (pendingParsingBlock != null)
            {
                if (nesting != 0)
                {
                    //Wait here ?
                    return;
                }

                do
                {
                    script = pendingParsingBlock;
                    pendingParsingBlock = null;
                    doc.WaitForReady();
                    oldInsertion = tokenizer.Stream.InsertionPoint;
                    nesting++;
                    script.Run();
                    nesting--;
                    tokenizer.Stream.ResetInsertionPoint();
                }
                while (pendingParsingBlock != null);
            }
        }

        /// <summary>
        /// If there is a node in the stack of open elements that is not either a dd element, a dt element, an
        /// li element, a p element, a tbody element, a td element, a tfoot element, a th element, a thead
        /// element, a tr element, the body element, or the html element, then this is a parse error.
        /// </summary>
        void CheckBodyOnClosing()
        {
            for (var i = 0; i < open.Count; i++)
            {
                if (open[i] is IImplClosed == false)
                {
                    RaiseErrorOccurred(ErrorCode.BodyClosedWrong);
                    break;
                }
            }
        }

        /// <summary>
        /// Checks if a tag with the given name is currently open.
        /// </summary>
        /// <param name="tagName">The name of the tag to check for.</param>
        /// <returns>True if such a tag is open, otherwise false.</returns>
        Boolean TagCurrentlyOpen(String tagName)
        {
            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].TagName == tagName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the next token and removes the starting newline, if it has one.
        /// </summary>
        void PreventNewLine()
        {
            var temp = tokenizer.Get();

            if (temp.Type == HtmlTokenType.Character)
                ((HtmlCharacterToken)temp).RemoveNewLine();

            Home(temp);
        }

        /// <summary>
        /// Resolves the encoding from the given charset and sets it.
        /// </summary>
        /// <param name="charset">The charset string.</param>
        void SetCharset(String charset)
        {
            var enc = DocumentEncoding.Resolve(charset);

            if (enc != null)
            {
                doc.InputEncoding = enc.WebName;
                tokenizer.Stream.Encoding = enc;
            }
        }

        /// <summary>
        /// 8.2.6 The end.
        /// </summary>
        void End()
        {
            doc.ReadyState = DocumentReadyState.Interactive;

            while (open.Count != 0)
                CloseCurrentNode();

            while (doc.ScriptsWaiting != 0)
                doc.RunNextScript();

            doc.QueueTask(doc.RaiseDomContentLoaded);
            doc.QueueTask(doc.RaiseLoadedEvent);

            if (doc.IsInBrowsingContext)
                doc.QueueTask(doc.ShowPage);

            doc.QueueTask(doc.EmptyAppCache);

            if (doc.IsToBePrinted)
                doc.Print();

            doc.QueueTask(doc.FinishLoading);
        }

        #endregion

        #region Appending Nodes

        /// <summary>
        /// Appends the doctype token to the document.
        /// </summary>
        /// <param name="doctypeToken">The doctypen token.</param>
        void AddDoctype(HtmlDoctypeToken doctypeToken)
        {
            var node = new DocumentType();
            node.SystemIdentifier = doctypeToken.SystemIdentifier;
            node.PublicIdentifier = doctypeToken.PublicIdentifier;
            node.Name = doctypeToken.Name;
            doc.AppendChild(node);
        }

        /// <summary>
        /// Adds the root element (html) to the document.
        /// </summary>
        /// <param name="tag">The token which started this process.</param>
        void AddRoot(HtmlTagToken tag)
        {
            var element = new HTMLHtmlElement();
            doc.AppendChild(element);
            SetupElement(element, tag, false);
            open.Add(element);
            tokenizer.AcceptsCharacterData = false;
            element.ApplyManifest();
        }

        /// <summary>
        /// Appends a comment node to the current node.
        /// </summary>
        /// <param name="commentToken">The comment token.</param>
        void AddComment(HtmlToken commentToken)
        {
            var tag = (HtmlCommentToken)commentToken;
            var comment = new Comment { Data = tag.Data };
            CurrentNode.AppendChild(comment);
        }

        /// <summary>
        /// Appends a comment node to the specified node.
        /// </summary>
        /// <param name="parent">The node which will contain the comment node.</param>
        /// <param name="commentToken">The comment token.</param>
        void AddComment(Node parent, HtmlToken commentToken)
        {
            var tag = (HtmlCommentToken)commentToken;
            var comment = new Comment { Data = tag.Data };
            parent.AppendChild(comment);
        }

        /// <summary>
        /// Pops the last node from the stack of open nodes.
        /// </summary>
        void CloseCurrentNode()
        {
            if (open.Count > 0)
            {
                open.RemoveAt(open.Count - 1);
                var node = AdjustedCurrentNode;
                tokenizer.AcceptsCharacterData = node != null && !node.IsInHtml;
            }
        }

        /// <summary>
        /// Modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        void SetupElement(Element element, HtmlTagToken tag, Boolean acknowledgeSelfClosing)
        {
            element.NodeName = tag.Name;

            if (tag.IsSelfClosing && !acknowledgeSelfClosing)
                RaiseErrorOccurred(ErrorCode.TagCannotBeSelfClosed);

            for (var i = 0; i < tag.Attributes.Count; i++)
                element.SetAttribute(tag.Attributes[i].Key, tag.Attributes[i].Value);
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        void AddElement(HtmlTagToken tag, Boolean acknowledgeSelfClosing = false)
        {
            var element = HtmlElementFactory.Create(tag.Name, doc);
            SetupElement(element, tag, acknowledgeSelfClosing);
            AddElement(element);
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        void AddElement(Element element, HtmlTagToken tag, Boolean acknowledgeSelfClosing = false)
        {
            SetupElement(element, tag, acknowledgeSelfClosing);
            AddElement(element);
        }

        /// <summary>
        /// Appends a configured node to the current node.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        void AddElement(Element element)
        {
            var node = CurrentNode;

            if (foster && node.IsTableElement())
                AddElementWithFoster(element);
            else
                node.AppendChild(element);

            open.Add(element);
            tokenizer.AcceptsCharacterData = !element.IsInHtml;
        }

        /// <summary>
        /// Appends a node to the appropriate foster parent.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#foster-parent
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        void AddElementWithFoster(Element element)
        {
            var table = false;
            var index = open.Count;

            while (--index != 0)
            {
                if (open[index] is HTMLTemplateElement)
                {
                    var template = (HTMLTemplateElement)open[index];
                    template.Content.AppendChild(element);
                    return;
                }
                else if (open[index] is HTMLTableElement)
                {
                    table = true;
                    break;
                }
            }

            var foster = open[index].Parent ?? open[index + 1];

            if (table && open[index].Parent != null)
            {
                for (int i = 0; i < foster.ChildNodes.Length; i++)
			    {
                    if (foster.ChildNodes[i] == open[index])
                    {
                        foster.InsertChild(i, element);
                        break;
                    }
			    }
            }
            else
                foster.AppendChild(element);
        }

        /// <summary>
        /// Appends a configured foreign node to the current node.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        void AddForeignElement(Element element)
        {
            element.NamespaceUri = ((Element)AdjustedCurrentNode).NamespaceUri;
            CurrentNode.AppendChild(element);
        }

        /// <summary>
        /// Inserts the given characters into the current node.
        /// </summary>
        /// <param name="text">The characters to insert.</param>
        void AddCharacters(String text)
        {
            if (String.IsNullOrEmpty(text))
                return;

            if (foster && CurrentNode.IsTableElement())
                AddCharactersWithFoster(text);
            else
                CurrentNode.AppendText(text);
        }

        /// <summary>
        /// Inserts the given character into the foster parent.
        /// </summary>
        /// <param name="text">The character to insert.</param>
        void AddCharactersWithFoster(String text)
        {
            var table = false;
            var index = open.Count;

            while (--index != 0)
            {
                if (open[index] is HTMLTemplateElement)
                {
                    var template = (HTMLTemplateElement)open[index];
                    template.Container.AppendText(text);
                    return;
                }
                else if (open[index] is HTMLTableElement)
                {
                    table = true;
                    break;
                }
            }

            var foster = open[index].Parent ?? open[index + 1];

            if (table && open[index].Parent != null)
            {
                for (int i = 0; i < foster.ChildNodes.Length; i++)
                {
                    if (foster.ChildNodes[i] == open[index])
                    {
                        foster.InsertText(i, text);
                        break;
                    }
                }
            }
            else
                foster.AppendText(text);
        }

        #endregion

        #region Closing Nodes

        /// <summary>
        /// Clears the stack of open elements back to the given element name.
        /// </summary>
        /// <param name="tagName">The tag that will be the CurrentNode.</param>
        void ClearStackBackTo(String tagName)
        {
            while (CurrentNode.NodeName != tagName && !(CurrentNode is HTMLHtmlElement) && !(CurrentNode is HTMLTemplateElement))
                CloseCurrentNode();
        }

        /// <summary>
        /// Clears the stack of open elements back to any heading element.
        /// </summary>
        void ClearStackBackTo<T>()
        {
            while (!(CurrentNode is T) && !(CurrentNode is HTMLHtmlElement) && !(CurrentNode is HTMLTemplateElement))
                CloseCurrentNode();
        }

        /// <summary>
        /// Generates the implied end tags for the dd, dt, li, option, optgroup, p, rp, rt elements except for
        /// the tag given.
        /// </summary>
        /// <param name="tagName">The tag that will be excluded.</param>
        void GenerateImpliedEndTagsExceptFor(String tagName)
        {
            while (CurrentNode is IImpliedEnd && CurrentNode.NodeName != tagName)
                CloseCurrentNode();
        }

        /// <summary>
        /// Generates the implied end tags for the dd, dt, li, option, optgroup, p, rp, rt elements.
        /// </summary>
        void GenerateImpliedEndTags()
        {
            while (CurrentNode is IImpliedEnd)
                CloseCurrentNode();
        }

        #endregion

        #region Formatting

        /// <summary>
        /// Inserts a scope marker at the end of the list of active formatting elements.
        /// </summary>
        void AddScopeMarker()
        {
            formatting.Add(null);
        }

        /// <summary>
        /// Adds an element to the list of active formatting elements.
        /// </summary>
        /// <param name="element">The element to add.</param>
        void AddFormattingElement(Element element)
        {
            var count = 0;

            for (var i = formatting.Count - 1; i >= 0; i--)
            {
                var format = formatting[i];

                if (format == null)
                    break;

                if (format.NodeName == element.NodeName && format.Attributes.IsEqualTo(element.Attributes) && format.NamespaceUri == element.NamespaceUri && ++count == 3)
                {
                    formatting.RemoveAt(i);
                    break;
                }
            }

            formatting.Add(element);
        }

        /// <summary>
        /// Clear the list of active formatting elements up to the last marker.
        /// </summary>
        void ClearFormattingElements()
        {
            while (formatting.Count != 0)
            {
                var index = formatting.Count - 1;
                var entry = formatting[index];
                formatting.RemoveAt(index);

                if (entry == null)
                    break;
            }
        }

        /// <summary>
        /// Reconstruct the list of active formatting elements, if any.
        /// </summary>
        void ReconstructFormatting()
        {
            if (formatting.Count == 0)
                return;

            var index = formatting.Count - 1;
            var entry = formatting[index];

            if (entry == null|| open.Contains(entry))
                return;

            while (index > 0)
            {
                entry = formatting[--index];

                if (entry == null || open.Contains(entry))
                {
                    index++;
                    break;
                }
            }

            for (; index < formatting.Count; index++)
            {
                var element = CopyElement(formatting[index]);
                AddElement(element);

                formatting[index] = element;
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        void RaiseErrorOccurred(ErrorCode code)
        {
            if (ParseError != null)
            {
                var pck = new ParseErrorEventArgs((int)code, code.GetErrorMessage());
                pck.Line = tokenizer.Stream.Line;
                pck.Column = tokenizer.Stream.Column;
                ParseError(this, pck);
            }
        }

        #endregion
    }
}
