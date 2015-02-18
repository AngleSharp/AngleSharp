namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Mathml;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the Tree construction as specified in
    /// 8.2.5 Tree construction, on the following page:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    [DebuggerStepThrough]
    public sealed class HtmlParser
    {
        #region Fields

        readonly HtmlTokenizer tokenizer;
        readonly Document doc;
        readonly List<Element> open;
        readonly List<Element> formatting;
        readonly Stack<HtmlTreeMode> templateMode;
        readonly Object sync;

        HtmlTreeMode insert;
        HtmlTreeMode originalInsert;
        HtmlFormElement form;
        Boolean frameset;
        Element fragmentContext;
        Boolean foster;
        Int32 nesting;
        Boolean started;
        HtmlScriptElement pendingParsingBlock;
        Task<IDocument> task;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ParseError
        {
            add { tokenizer.ErrorOccurred += value; }
            remove { tokenizer.ErrorOccurred -= value; }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the HTML parser with an new document
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public HtmlParser(String source, IConfiguration configuration = null)
            : this(new Document(new SimpleBrowsingContext(configuration, Sandboxes.None), new TextSource(source)))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with an new document
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public HtmlParser(Stream stream, IConfiguration configuration = null)
            : this(new Document(new SimpleBrowsingContext(configuration, Sandboxes.None), new TextSource(stream, configuration.DefaultEncoding())))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with the specified document
        /// based on the given source manager.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        internal HtmlParser(Document document)
        {
            tokenizer = new HtmlTokenizer(document.Source);
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
        public IDocument Result
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
        internal Element AdjustedCurrentNode
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
        internal HtmlScriptElement PendingParsingBlock
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
        public Task<IDocument> ParseAsync()
        {
            return ParseAsync(CancellationToken.None);
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task<IDocument> ParseAsync(CancellationToken cancelToken)
        {
			lock (sync)
			{
				if (!started)
				{
					started = true;
                    task = KernelAsync(cancelToken);
				}
            }

            return task;
        }

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        public IDocument Parse()
        {
            if (!started)
            {
                started = true;
				Kernel();
            }

            return doc;
        }

        /// <summary>
        /// Switches to the fragment algorithm with the specified context element.
        /// </summary>
        /// <param name="context">The context element where the algorithm is applied to.</param>
        /// <returns>The current instance for chaining.</returns>
        internal HtmlParser SwitchToFragment(Element context)
        {
            if (started)
                throw new InvalidOperationException("Fragment mode has to be activated before running the parser!");

            var tagName = context.NodeName;

            if (tagName.IsOneOf(Tags.Title, Tags.Textarea))
                tokenizer.State = HtmlParseMode.RCData;
            else if (tagName.IsOneOf(Tags.Style, Tags.Xmp, Tags.Iframe, Tags.NoEmbed, Tags.NoFrames))
                tokenizer.State = HtmlParseMode.Rawtext;
            else if (tagName == Tags.Script)
                tokenizer.State = HtmlParseMode.Script;
            else if (tagName == Tags.Plaintext)
                tokenizer.State = HtmlParseMode.Plaintext;
            else if (tagName == Tags.NoScript && doc.Options.IsScripting)
                tokenizer.State = HtmlParseMode.Rawtext;

            var root = new HtmlHtmlElement(doc);
            doc.AddNode(root);
            open.Add(root);

            if (context is HtmlTemplateElement)
                templateMode.Push(HtmlTreeMode.InTemplate);

            Reset(context);

            fragmentContext = context;
            tokenizer.IsAcceptingCharacterData = !AdjustedCurrentNode.Flags.HasFlag(NodeFlags.HtmlMember);

            do
            {
                if (context is HtmlFormElement)
                {
                    form = (HtmlFormElement)context;
                    break;
                }

                context = context.ParentElement as Element;
            }
            while (context != null);

            return this;
        }

        void Restart()
        {
            insert = HtmlTreeMode.Initial;
            tokenizer.State = HtmlParseMode.PCData;
            doc.ReplaceAll(null, true);
            frameset = true;
            open.Clear();
            formatting.Clear();
            templateMode.Clear();
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

                var tagName = node.NodeName;

                if (tagName == Tags.Select)
                    insert = HtmlTreeMode.InSelect;
                else if (tagName.IsOneOf(Tags.Th, Tags.Td))
                    insert = last ? HtmlTreeMode.InBody : HtmlTreeMode.InCell;
                else if (tagName == Tags.Tr)
                    insert = HtmlTreeMode.InRow;
                else if (tagName.IsOneOf(Tags.Thead, Tags.Tfoot, Tags.Tbody))
                    insert = HtmlTreeMode.InTableBody;
                else if (tagName == Tags.Body)
                    insert = HtmlTreeMode.InBody;
                else if (tagName == Tags.Table)
                    insert = HtmlTreeMode.InTable;
                else if (tagName == Tags.Caption)
                    insert = HtmlTreeMode.InCaption;
                else if (tagName == Tags.Colgroup)
                    insert = HtmlTreeMode.InColumnGroup;
                else if (tagName == Tags.Template)
                    insert = templateMode.Peek();
                else if (tagName == Tags.Html)
                    insert = HtmlTreeMode.BeforeHead;
                else if (tagName == Tags.Head)
                    insert = last ? HtmlTreeMode.InBody : HtmlTreeMode.InHead;
                else if (tagName == Tags.Frameset)
                    insert = HtmlTreeMode.InFrameset;
                else if (last)
                    insert = HtmlTreeMode.InBody;
                else
                    continue;

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

            if (node == null || token.IsEof || node.Flags.HasFlag(NodeFlags.HtmlMember) || 
                (node.Flags.HasFlag(NodeFlags.HtmlTip) && token.IsHtmlCompatible) ||
                (node.Flags.HasFlag(NodeFlags.MathTip) && token.IsMathCompatible) || 
                (node.Flags.HasFlag(NodeFlags.MathMember) && token.IsSvg && node.NodeName == Tags.AnnotationXml))
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
        void Initial(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.DOCTYPE:
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
                case HtmlTokenType.Character:
                {
                    token.TrimStart();

                    if (token.IsEmpty)
                        return;

                    break;
                }
                case HtmlTokenType.Comment:
                {
                    doc.AddComment(token);
                    return;
                }
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
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    token.TrimStart();

                    if (token.IsEmpty)
                        return;

                    break;
                }
                case HtmlTokenType.Comment:
                {
                    doc.AddComment(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (token.Name != Tags.Html)
                        break;

                    AddRoot(token.AsTag());
                    insert = HtmlTreeMode.BeforeHead;
                    return;
                }                    
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.IsOneOf(Tags.Html, Tags.Body, Tags.Br, Tags.Head))
                        break;

                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    return;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
            }

            BeforeHtml(HtmlTagToken.Open(Tags.Html));
            BeforeHead(token);
        }

        /// <summary>
        /// See 8.2.5.4.3 The "before head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void BeforeHead(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    token.TrimStart();

                    if (token.IsEmpty)
                        return;

                    break;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                    {
                        InBody(token);
                        return;
                    }
                    else if (tagName == Tags.Head)
                    {
                        AddElement(new HtmlHeadElement(doc), token.AsTag());
                        insert = HtmlTreeMode.InHead;
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.IsOneOf(Tags.Html, Tags.Body, Tags.Br, Tags.Head))
                        break;

                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
            }

            BeforeHead(HtmlTagToken.Open(Tags.Head));
            InHead(token);
        }
        
        /// <summary>
        /// See 8.2.5.4.4 The "in head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InHead(HtmlToken token)
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
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                    {
                        InBody(token);
                        return;
                    }
                    else if (tagName == Tags.Meta)
                    {
                        var element = new HtmlMetaElement(doc);
                        AddElement(element, token.AsTag(), true);
                        var encoding = element.GetEncoding();
                        CloseCurrentNode();

                        if (encoding != null)
                        {
                            try
                            {
                                doc.Source.CurrentEncoding = encoding;
                            }
                            catch (NotSupportedException)
                            {
                                Restart();
                            }
                        }

                        return;
                    }
                    else if (tagName.IsOneOf(Tags.Link, Tags.Base, Tags.BaseFont, Tags.Bgsound))
                    {
                        AddElement(token.AsTag(), true);
                        CloseCurrentNode();
                        return;
                    }
                    else if (tagName == Tags.Title)
                    {
                        RCDataAlgorithm(token.AsTag());
                        return;
                    }
                    else if (tagName.IsOneOf(Tags.Style, Tags.NoFrames) || (doc.Options.IsScripting && tagName == Tags.NoScript))
                    {
                        RawtextAlgorithm(token.AsTag());
                        return;
                    }
                    else if (tagName == Tags.NoScript)
                    {
                        AddElement(token.AsTag());
                        insert = HtmlTreeMode.InHeadNoScript;
                        return;
                    }
                    else if (tagName == Tags.Script)
                    {
                        var script = new HtmlScriptElement(doc);
                        AddElement(script, token.AsTag());
                        script.IsParserInserted = true;
                        script.IsAlreadyStarted = IsFragmentCase;
                        tokenizer.State = HtmlParseMode.Script;
                        originalInsert = insert;
                        insert = HtmlTreeMode.Text;
                        return;
                    }
                    else if (tagName == Tags.Head)
                    {
                        RaiseErrorOccurred(ErrorCode.HeadTagMisplaced);
                        return;
                    }
                    else if (tagName == Tags.Template)
                    {
                        AddElement(new HtmlTemplateElement(doc), token.AsTag());
                        formatting.AddScopeMarker();
                        frameset = false;
                        insert = HtmlTreeMode.InTemplate;
                        templateMode.Push(HtmlTreeMode.InTemplate);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Head)
                    {
                        CloseCurrentNode();
                        doc.WaitForReady();
                        insert = HtmlTreeMode.AfterHead;
                        return;
                    }
                    else if (tagName == Tags.Template)
                    {
                        if (TagCurrentlyOpen(Tags.Template))
                        {
                            GenerateImpliedEndTags();

                            if (CurrentNode is HtmlTemplateElement == false)
                                RaiseErrorOccurred(ErrorCode.TagClosingMismatch);

                            CloseTemplate();
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.TagInappropriate);

                        return;
                    }
                    else if (!tagName.IsOneOf(Tags.Html, Tags.Body, Tags.Br))
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        return;
                    }

                    break;
                }
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
                    InHead(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsOneOf(Tags.Style, Tags.Link, Tags.BaseFont, Tags.Meta, Tags.NoFrames, Tags.Bgsound))
                        InHead(token);
                    else if (tagName == Tags.Html)
                        InBody(token);
                    else if (tagName.IsOneOf(Tags.Head, Tags.NoScript))
                        RaiseErrorOccurred(ErrorCode.TagInappropriate);
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.NoScript)
                    {
                        CloseCurrentNode();
                        insert = HtmlTreeMode.InHead;
                        return;
                    }
                    else if (tagName != Tags.Br)
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
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
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                    {
                        InBody(token);
                        return;
                    }
                    else if (tagName == Tags.Body)
                    {
                        AfterHeadStartTagBody(token.AsTag());
                        return;
                    }
                    else if (tagName == Tags.Frameset)
                    {
                        AddElement(new HtmlFrameSetElement(doc), token.AsTag());
                        insert = HtmlTreeMode.InFrameset;
                        return;
                    }
                    else if (tagName.IsOneOf(Tags.Link, Tags.Meta, Tags.Script, Tags.Style, Tags.Title) || tagName.IsOneOf(Tags.Base, Tags.BaseFont, Tags.Bgsound, Tags.NoFrames))
                    {
                        RaiseErrorOccurred(ErrorCode.TagMustBeInHead);
                        var index = open.Count;
                        var head = doc.Head as Element;
                        open.Add(head);
                        InHead(token);
                        open.Remove(head);
                        return;
                    }
                    else if (tagName == Tags.Head)
                    {
                        RaiseErrorOccurred(ErrorCode.HeadTagMisplaced);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name.IsOneOf(Tags.Html, Tags.Body, Tags.Br))
                        break;

                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    return;
                }
            }

            AfterHeadStartTagBody(HtmlTagToken.Open(Tags.Body));
            frameset = true;
            Home(token);
        }

        void InBodyStartTag(HtmlTagToken tag)
        {
            var tagName = tag.Name;

            if (tagName == Tags.Div)
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                AddElement(tag);
            }
            else if (tagName == Tags.A)
            {
                for (var i = formatting.Count - 1; i >= 0; i--)
                {
                    if (formatting[i] == null)
                        break;

                    if (formatting[i] is HtmlAnchorElement)
                    {
                        var format = formatting[i];
                        RaiseErrorOccurred(ErrorCode.AnchorNested);
                        HeisenbergAlgorithm(HtmlTagToken.Close(Tags.A));

                        if (open.Contains(format))
                            open.Remove(format);

                        if (formatting.Contains(format))
                            formatting.RemoveAt(i);

                        break;
                    }
                }

                ReconstructFormatting();
                var element = new HtmlAnchorElement(doc);
                AddElement(element, tag);
                formatting.AddFormatting(element);
            }
            else if (tagName == Tags.Span)
            {
                ReconstructFormatting();
                AddElement(tag);
            }
            else if (tagName == Tags.Li)
            {
                InBodyStartTagListItem(tag);
            }
            else if (tagName == Tags.Img)
            {
                InBodyStartTagBreakrow(tag);
            }
            else if (tagName.IsOneOf(Tags.Ul, Tags.P))
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                AddElement(tag);
            }
            else if (tagName == Tags.Script)
            {
                InHead(tag);
            }
            else if (tagName.IsOneOf(Tags.H3, Tags.H2, Tags.H4, Tags.H1, Tags.H6, Tags.H5))
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                if (CurrentNode is HtmlHeadingElement)
                {
                    RaiseErrorOccurred(ErrorCode.HeadingNested);
                    CloseCurrentNode();
                }

                AddElement(new HtmlHeadingElement(doc, tagName), tag);
            }
            else if (tagName == Tags.Input)
            {
                ReconstructFormatting();
                AddElement(new HtmlInputElement(doc), tag, true);
                CloseCurrentNode();

                if (!tag.GetAttribute(AttributeNames.Type).Equals(AttributeNames.Hidden, StringComparison.OrdinalIgnoreCase))
                    frameset = false;
            }
            else if (tagName == Tags.Form)
            {
                if (form == null)
                {
                    if (IsInButtonScope())
                        InBodyEndTagParagraph();

                    form = new HtmlFormElement(doc);
                    AddElement(form, tag);
                }
                else
                    RaiseErrorOccurred(ErrorCode.FormAlreadyOpen);
            }
            else if (tagName.IsOneOf(Tags.Ol, Tags.Dl, Tags.Fieldset) ||
                     tagName.IsOneOf(Tags.Figcaption, Tags.Figure, Tags.Article, Tags.Aside, Tags.BlockQuote, Tags.Center) ||
                     tagName.IsOneOf(Tags.Address, Tags.Dialog, Tags.Dir, Tags.Summary, Tags.Details, Tags.Main) ||
                     tagName.IsOneOf(Tags.Footer, Tags.Header, Tags.Nav, Tags.Section, Tags.Menu, Tags.Hgroup))
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                AddElement(tag);
            }
            else if (tagName.IsOneOf(Tags.B, Tags.Strong, Tags.Code, Tags.Em, Tags.U, Tags.I) ||
                     tagName.IsOneOf(Tags.Font, Tags.S, Tags.Small, Tags.Strike, Tags.Big, Tags.Tt))
            {
                ReconstructFormatting();
                formatting.AddFormatting(AddElement(tag));
            }
            else if (tagName.IsOneOf(Tags.Caption, Tags.Col, Tags.Colgroup) ||
                     tagName.IsOneOf(Tags.Frame, Tags.Head) ||
                     tagName.IsOneOf(Tags.Tbody, Tags.Td, Tags.Tfoot, Tags.Th, Tags.Thead, Tags.Tr))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotStartHere);
            }
            else if (tagName.IsOneOf(Tags.Style, Tags.Link) ||
                     tagName.IsOneOf(Tags.Meta, Tags.Title, Tags.NoFrames, Tags.Template) ||
                     tagName.IsOneOf(Tags.Base, Tags.BaseFont, Tags.Bgsound))
            {
                InHead(tag);
            }
            else if (tagName.IsOneOf(Tags.Pre, Tags.Listing))
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                AddElement(tag);
                frameset = false;
                PreventNewLine();
            }
            else if (tagName == Tags.Button)
            {
                if (IsInScope<HtmlButtonElement>())
                {
                    RaiseErrorOccurred(ErrorCode.ButtonInScope);
                    InBodyEndTagBlock(Tags.Button);
                    InBody(tag);
                }
                else
                {
                    ReconstructFormatting();
                    AddElement(new HtmlButtonElement(doc), tag);
                    frameset = false;
                }
            }
            else if (tagName == Tags.Table)
            {
                if (doc.QuirksMode != QuirksMode.On && IsInButtonScope())
                    InBodyEndTagParagraph();

                AddElement(new HtmlTableElement(doc), tag);
                frameset = false;
                insert = HtmlTreeMode.InTable;
            }
            else if (tagName.IsOneOf(Tags.Br, Tags.Area, Tags.Embed, Tags.Keygen, Tags.Wbr))
            {
                InBodyStartTagBreakrow(tag);
            }
            else if (tagName.IsOneOf(Tags.MenuItem, Tags.Param, Tags.Source, Tags.Track))
            {
                AddElement(tag, true);
                CloseCurrentNode();
            }
            else if (tagName == Tags.Hr)
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                AddElement(new HtmlHrElement(doc), tag, true);
                CloseCurrentNode();
                frameset = false;
            }
            else if (tagName == Tags.Textarea)
            {
                AddElement(new HtmlTextAreaElement(doc), tag);
                tokenizer.State = HtmlParseMode.RCData;
                originalInsert = insert;
                frameset = false;
                insert = HtmlTreeMode.Text;
                PreventNewLine();
            }
            else if (tagName == Tags.Select)
            {
                ReconstructFormatting();
                AddElement(new HtmlSelectElement(doc), tag);
                frameset = false;

                switch (insert)
                {
                    case HtmlTreeMode.InTable:
                    case HtmlTreeMode.InTableBody:
                    case HtmlTreeMode.InCaption:
                    case HtmlTreeMode.InRow:
                    case HtmlTreeMode.InCell:
                        insert = HtmlTreeMode.InSelectInTable;
                        break;

                    default:
                        insert = HtmlTreeMode.InSelect;
                        break;
                }
            }
            else if (tagName.IsOneOf(Tags.Optgroup, Tags.Option))
            {
                if (CurrentNode is HtmlOptionElement)
                    InBodyEndTagAnythingElse(HtmlTagToken.Close(Tags.Option));

                ReconstructFormatting();
                AddElement(tag);
            }
            else if (tagName.IsOneOf(Tags.Dd, Tags.Dt))
            {
                InBodyStartTagDefinitionItem(tag);
            }
            else if (tagName == Tags.Iframe)
            {
                frameset = false;
                RawtextAlgorithm(tag);
            }
            else if (tagName.IsOneOf(Tags.Applet, Tags.Marquee, Tags.Object))
            {
                ReconstructFormatting();
                AddElement(tag);
                formatting.AddScopeMarker();
                frameset = false;
            }
            else if (tagName == Tags.Image)
            {
                RaiseErrorOccurred(ErrorCode.ImageTagNamedWrong);
                tag.Name = Tags.Img;
                InBodyStartTagBreakrow(tag);
            }
            else if (tagName == Tags.NoBr)
            {
                ReconstructFormatting();

                if (IsInScope<HtmlNoNewlineElement>())
                {
                    RaiseErrorOccurred(ErrorCode.NobrInScope);
                    HeisenbergAlgorithm(tag);
                    ReconstructFormatting();
                }

                formatting.AddFormatting(AddElement(tag));
            }
            else if (tagName == Tags.Xmp)
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                ReconstructFormatting();
                frameset = false;
                RawtextAlgorithm(tag);
            }
            else if (tagName.IsOneOf(Tags.Rb, Tags.Rtc))
            {
                if (IsInScope<HtmlRubyElement>())
                {
                    GenerateImpliedEndTags();

                    if (CurrentNode is HtmlRubyElement == false)
                        RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
                }

                AddElement(tag);
            }
            else if (tagName.IsOneOf(Tags.Rp, Tags.Rt))
            {
                if (IsInScope<HtmlRubyElement>())
                {
                    GenerateImpliedEndTags();//TODO except for rtc elements

                    if (CurrentNode is HtmlRubyElement == false && CurrentNode is HtmlRtcElement == false)
                        RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
                }

                AddElement(tag);
            }
            else if (tagName == Tags.NoEmbed)
            {
                RawtextAlgorithm(tag);
            }
            else if (tagName == Tags.NoScript)
            {
                if (doc.Options.IsScripting)
                {
                    RawtextAlgorithm(tag);
                    return;
                }

                ReconstructFormatting();
                AddElement(tag);
            }
            else if (tagName == Tags.Math)
            {
                var element = new MathElement(doc, tagName);
                element.OriginalPosition = tag.Range;
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
            }
            else if (tagName == Tags.Svg)
            {
                var element = new SvgElement(doc, tagName);
                element.OriginalPosition = tag.Range;
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
            }
            else if (tagName == Tags.Plaintext)
            {
                if (IsInButtonScope())
                    InBodyEndTagParagraph();

                AddElement(tag);
                tokenizer.State = HtmlParseMode.Plaintext;
            }
            else if (tagName == Tags.Frameset)
            {
                RaiseErrorOccurred(ErrorCode.FramesetMisplaced);

                if (open.Count != 1 && open[1] is HtmlBodyElement && frameset)
                {
                    open[1].Parent.RemoveChild(open[1]);

                    while (open.Count > 1)
                        CloseCurrentNode();

                    AddElement(new HtmlFrameSetElement(doc), tag);
                    insert = HtmlTreeMode.InFrameset;
                }
            }
            else if (tagName == Tags.Html)
            {
                RaiseErrorOccurred(ErrorCode.HtmlTagMisplaced);

                if (templateMode.Count == 0)
                    open[0].SetUniqueAttributes(tag.Attributes);
            }
            else if (tagName == Tags.Body)
            {
                RaiseErrorOccurred(ErrorCode.BodyTagMisplaced);

                if (templateMode.Count == 0 && open.Count > 1 && open[1] is HtmlBodyElement)
                {
                    frameset = false;
                    open[1].SetUniqueAttributes(tag.Attributes);
                }
            }
            else if (tagName == Tags.IsIndex)
            {
                RaiseErrorOccurred(ErrorCode.TagInappropriate);

                if (form == null)
                {
                    InBody(HtmlTagToken.Open(Tags.Form));

                    if (tag.GetAttribute(AttributeNames.Action) != String.Empty)
                        form.SetAttribute(AttributeNames.Action, tag.GetAttribute(AttributeNames.Action));

                    InBody(HtmlTagToken.Open(Tags.Hr));
                    InBody(HtmlTagToken.Open(Tags.Label));

                    if (tag.GetAttribute(AttributeNames.Prompt) != String.Empty)
                        AddCharacters(tag.GetAttribute(AttributeNames.Prompt));
                    else
                        AddCharacters("This is a searchable index. Enter search keywords: ");

                    var input = HtmlTagToken.Open(Tags.Input);
                    input.AddAttribute(AttributeNames.Name, Tags.IsIndex);

                    for (int i = 0; i < tag.Attributes.Count; i++)
                    {
                        if (tag.Attributes[i].Key.IsOneOf(AttributeNames.Name, AttributeNames.Action, AttributeNames.Prompt))
                            continue;

                        input.AddAttribute(tag.Attributes[i].Key, tag.Attributes[i].Value);
                    }

                    InBody(input);
                    InBody(HtmlTagToken.Close(Tags.Label));
                    InBody(HtmlTagToken.Open(Tags.Hr));
                    InBody(HtmlTagToken.Close(Tags.Form));
                }
            }
            else
            {
                ReconstructFormatting();
                AddElement(tag);
            }
        }

        void InBodyEndTag(HtmlTagToken tag)
        {
            var tagName = tag.Name;

            if (tagName == Tags.Div)
            {
                InBodyEndTagBlock(tagName);
            }
            else if (tagName == Tags.A)
            {
                HeisenbergAlgorithm(tag);
            }
            else if (tagName == Tags.Li)
            {
                if (IsInListItemScope())
                {
                    GenerateImpliedEndTagsExceptFor(tagName);

                    if (CurrentNode is HtmlListItemElement == false)
                        RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                    ClearStackBackTo<HtmlListItemElement>();
                    CloseCurrentNode();
                }
                else
                    RaiseErrorOccurred(ErrorCode.ListItemNotInScope);
            }
            else if (tagName == Tags.P)
            {
                InBodyEndTagParagraph();
            }
            else if (tagName.IsOneOf(Tags.Ol, Tags.Ul, Tags.Dl, Tags.Fieldset, Tags.Button) ||
                     tagName.IsOneOf(Tags.Figcaption, Tags.Figure, Tags.Article, Tags.Aside, Tags.BlockQuote, Tags.Center) ||
                     tagName.IsOneOf(Tags.Address, Tags.Dialog, Tags.Dir, Tags.Summary, Tags.Details, Tags.Listing) ||
                     tagName.IsOneOf(Tags.Footer, Tags.Header, Tags.Nav, Tags.Section, Tags.Menu, Tags.Hgroup) ||
                     tagName.IsOneOf(Tags.Main, Tags.Pre))
            {
                InBodyEndTagBlock(tagName);
            }
            else if (tagName.IsOneOf(Tags.B, Tags.Strong, Tags.Code) ||
                     tagName.IsOneOf(Tags.NoBr, Tags.Em, Tags.U, Tags.I) ||
                     tagName.IsOneOf(Tags.Font, Tags.S, Tags.Small, Tags.Strike, Tags.Big, Tags.Tt))
            {
                HeisenbergAlgorithm(tag);
            }
            else if (tagName == Tags.Form)
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
            }
            else if (tagName == Tags.Br)
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                InBodyStartTagBreakrow(HtmlTagToken.Open(Tags.Br));
            }
            else if (tagName.IsOneOf(Tags.H3, Tags.H2, Tags.H4, Tags.H1, Tags.H6, Tags.H5))
            {
                if (IsInScope<HtmlHeadingElement>())
                {
                    GenerateImpliedEndTags();

                    if (CurrentNode.NodeName != tagName)
                        RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                    ClearStackBackTo<HtmlHeadingElement>();
                    CloseCurrentNode();
                }
                else
                    RaiseErrorOccurred(ErrorCode.HeadingNotInScope);
            }
            else if (tagName.IsOneOf(Tags.Dd, Tags.Dt))
            {
                if (IsInScope(tagName))
                {
                    GenerateImpliedEndTagsExceptFor(tagName);

                    if (CurrentNode.NodeName != tagName)
                        RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                    ClearStackBackTo(tagName);
                    CloseCurrentNode();
                }
                else
                    RaiseErrorOccurred(ErrorCode.ListItemNotInScope);
            }
            else if (tagName.IsOneOf(Tags.Applet, Tags.Marquee, Tags.Object))
            {
                if (IsInScope(tagName))
                {
                    GenerateImpliedEndTags();

                    if (CurrentNode.NodeName != tagName)
                        RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                    ClearStackBackTo(tagName);
                    CloseCurrentNode();
                    formatting.ClearFormatting();
                }
                else
                    RaiseErrorOccurred(ErrorCode.ObjectNotInScope);
            }
            else if (tagName == Tags.Body)
            {
                InBodyEndTagBody();
            }
            else if (tagName == Tags.Html)
            {
                if (InBodyEndTagBody())
                    AfterBody(tag);
            }
            else if (tagName == Tags.Template)
            {
                InHead(tag);
            }
            else
                InBodyEndTagAnythingElse(tag);
        }

        /// <summary>
        /// See 8.2.5.4.7 The "in body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    ReconstructFormatting();
                    AddCharacters(token.Data);

                    if (token.HasContent)
                        frameset = false;

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
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.EOF:
                {
                    CheckBodyOnClosing();

                    if (templateMode.Count != 0)
                        InTemplate(token);
                    else
                        End();

                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.8 The "text" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void Text(HtmlToken token)
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
                    if (token.Name != Tags.Script)
                    {
                        CloseCurrentNode();
                        insert = originalInsert;
                    }
                    else
                        RunScript(CurrentNode as HtmlScriptElement);

                    return;
                }
                case HtmlTokenType.EOF:
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    CloseCurrentNode();
                    insert = originalInsert;
                    Consume(token);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.9 The "in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InTable(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Caption)
                    {
                        ClearStackBackTo<HtmlTableElement>();
                        formatting.AddScopeMarker();
                        AddElement(new HtmlTableCaptionElement(doc), token.AsTag());
                        insert = HtmlTreeMode.InCaption;
                    }
                    else if (tagName == Tags.Colgroup)
                    {
                        ClearStackBackTo<HtmlTableElement>();
                        AddElement(new HtmlTableColgroupElement(doc), token.AsTag());
                        insert = HtmlTreeMode.InColumnGroup;
                    }
                    else if (tagName == Tags.Col)
                    {
                        InTable(HtmlTagToken.Open(Tags.Colgroup));
                        InColumnGroup(token);
                    }
                    else if (tagName.IsOneOf(Tags.Tbody, Tags.Thead, Tags.Tfoot))
                    {
                        ClearStackBackTo<HtmlTableElement>();
                        AddElement(new HtmlTableSectionElement(doc, tagName), token.AsTag());
                        insert = HtmlTreeMode.InTableBody;
                    }
                    else if (tagName.IsOneOf(Tags.Td, Tags.Th, Tags.Tr))
                    {
                        InTable(HtmlTagToken.Open(Tags.Tbody));
                        InTableBody(token);
                    }
                    else if (tagName == Tags.Table)
                    {
                        RaiseErrorOccurred(ErrorCode.TableNesting);

                        if (InTableEndTagTable())
                            Home(token);
                    }
                    else if (tagName.IsOneOf(Tags.Script, Tags.Style, Tags.Template))
                    {
                        InHead(token);
                    }
                    else if (tagName == Tags.Input)
                    {
                        var tag = token.AsTag();

                        if (tag.GetAttribute(AttributeNames.Type).Equals(AttributeNames.Hidden, StringComparison.OrdinalIgnoreCase))
                        {
                            RaiseErrorOccurred(ErrorCode.InputUnexpected);
                            AddElement(new HtmlInputElement(doc), tag, true);
                            CloseCurrentNode();
                        }
                        else
                        {
                            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                            InBodyWithFoster(token);
                        }
                    }
                    else if (tagName == Tags.Form)
                    {
                        RaiseErrorOccurred(ErrorCode.FormInappropriate);

                        if (form == null)
                        {
                            form = new HtmlFormElement(doc);
                            AddElement(form, token.AsTag());
                            CloseCurrentNode();
                        }
                    }
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInTableDetected);
                        InBodyWithFoster(token);
                    }

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Table)
                    {
                        InTableEndTagTable();
                    }
                    else if (tagName == Tags.Template)
                    {
                        InHead(token);
                    }
                    else if (tagName.IsOneOf(Tags.Body, Tags.Colgroup, Tags.Col, Tags.Caption, Tags.Html) || tagName.IsOneOf(Tags.Tbody, Tags.Tr, Tags.Thead, Tags.Th, Tags.Tfoot, Tags.Td))
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    }
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInTableDetected);
                        InBodyWithFoster(token);
                    }

                    return;
                }
                case HtmlTokenType.EOF:
                {
                    InBody(token);
                    return;
                }
                case HtmlTokenType.Character:
                {
                    if (CurrentNode.IsTableElement())
                    {
                        InTableText(token);
                        return;
                    }

                    break;
                }
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            InBodyWithFoster(token);
        }

        /// <summary>
        /// See 8.2.5.4.10 The "in table text" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InTableText(HtmlToken token)
        {
            if (token.HasContent)
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                InBodyWithFoster(token);
            }
            else
                AddCharacters(token.Data);
        }

        /// <summary>
        /// See 8.2.5.4.11 The "in caption" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InCaption(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Caption)
                    {
                        InCaptionEndTagCaption();
                    }
                    else if (tagName.IsOneOf(Tags.Body, Tags.Th, Tags.Colgroup, Tags.Html) || tagName.IsOneOf(Tags.Tbody, Tags.Col, Tags.Tfoot, Tags.Td, Tags.Thead, Tags.Tr))
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    }
                    else if (tagName == Tags.Table)
                    {
                        RaiseErrorOccurred(ErrorCode.TableNesting);

                        if (InCaptionEndTagCaption())
                            InTable(token);
                    }
                    else
                        break;

                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsOneOf(Tags.Caption, Tags.Th, Tags.Colgroup) || tagName.IsOneOf(Tags.Tbody, Tags.Col, Tags.Tfoot, Tags.Td, Tags.Thead, Tags.Tr))
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotStartHere);

                        if (InCaptionEndTagCaption())
                            InTable(token);
                    }
                    else
                        break;

                    return;
                }
            }

            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.12 The "in column group" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InColumnGroup(HtmlToken token)
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
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                    {
                        InBody(token);
                    }
                    else if (tagName == Tags.Col)
                    {
                        AddElement(new HtmlTableColElement(doc), token.AsTag(), true);
                        CloseCurrentNode();
                    }
                    else if (tagName == Tags.Template)
                    {
                        InHead(token);
                    }
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Colgroup)
                        InColumnGroupEndTagColgroup();
                    else if (tagName == Tags.Col)
                        RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    else if (tagName == Tags.Template)
                        InHead(token);
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EOF:
                {
                    InBody(token);
                    return;
                }
            }

            if (InColumnGroupEndTagColgroup())
                InTable(token);
        }

        /// <summary>
        /// See 8.2.5.4.13 The "in table body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InTableBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Tr)
                    {
                        ClearStackBackTo<HtmlTableSectionElement>();
                        AddElement(new HtmlTableRowElement(doc), token.AsTag());
                        insert = HtmlTreeMode.InRow;
                    }
                    else if (tagName.IsTableCellElement())
                    {
                        InTableBody(HtmlTagToken.Open(Tags.Tr));
                        InRow(token);
                    }
                    else if (tagName.IsGeneralTableElement())
                        InTableBodyCloseTable(token.AsTag());
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsTableSectionElement())
                    {
                        if (IsInTableScope(tagName))
                        {
                            ClearStackBackTo<HtmlTableSectionElement>();
                            CloseCurrentNode();
                            insert = HtmlTreeMode.InTable;
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.TableSectionNotInScope);
                    }
                    else if (tagName.IsSpecialTableElement(true))
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    else if (tagName == Tags.Table)
                        InTableBodyCloseTable(token.AsTag());
                    else
                        break;

                    return;
                }
            }

            InTable(token);
        }

        /// <summary>
        /// See 8.2.5.4.14 The "in row" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InRow(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsTableCellElement())
                    {
                        ClearStackBackTo<HtmlTableRowElement>();
                        AddElement(token.AsTag());
                        insert = HtmlTreeMode.InCell;
                        formatting.AddScopeMarker();
                    }
                    else if (tagName.IsGeneralTableElement(true))
                    {
                        if (InRowEndTagTablerow())
                            InTableBody(token);
                    }
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Tr)
                    {
                        InRowEndTagTablerow();
                    }
                    else if (tagName == Tags.Table)
                    {
                        if (InRowEndTagTablerow())
                            InTableBody(token);
                    }
                    else if (tagName.IsTableSectionElement())
                    {
                        if (IsInTableScope(tagName))
                        {
                            InRowEndTagTablerow();
                            InTableBody(token);
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.TableSectionNotInScope);
                    }
                    else if (tagName.IsSpecialTableElement())
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    else
                        break;

                    return;
                }
            }

            InTable(token);
        }

        /// <summary>
        /// See 8.2.5.4.15 The "in cell" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InCell(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsTableCellElement() || tagName.IsGeneralTableElement(true))
                    {
                        if (IsInTableScope(Tags.Td) || IsInTableScope(Tags.Th))
                        {
                            InCellEndTagCell();
                            Home(token);
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.TableCellNotInScope);

                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsTableCellElement())
                        InCellEndTagCell();
                    else if (tagName.IsSpecialTableElement())
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                    else if (tagName.IsTableElement())
                    {
                        if (IsInTableScope(tagName))
                        {
                            InCellEndTagCell();
                            Home(token);
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.TableNotInScope);
                    }
                    else
                        InBody(token);

                    return;
                }
            }

            InBody(token);
        }

        /// <summary>
        /// See 8.2.5.4.16 The "in select" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InSelect(HtmlToken token)
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
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                        InBody(token);
                    else if (tagName == Tags.Option)
                    {
                        if (CurrentNode is HtmlOptionElement)
                            InSelectEndTagOption();

                        AddElement(new HtmlOptionElement(doc), token.AsTag());
                    }
                    else if (tagName == Tags.Optgroup)
                    {
                        if (CurrentNode is HtmlOptionElement)
                            InSelectEndTagOption();
                        
                        if (CurrentNode is HtmlOptionsGroupElement)
                            InSelectEndTagOptgroup();

                        AddElement(new HtmlOptionsGroupElement(doc), token.AsTag());
                    }
                    else if (tagName == Tags.Select)
                    {
                        RaiseErrorOccurred(ErrorCode.SelectNesting);
                        InSelectEndTagSelect();
                    }
                    else if (tagName.IsOneOf(Tags.Input, Tags.Keygen, Tags.Textarea))
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);

                        if (IsInSelectScope(Tags.Select))
                        {
                            InSelectEndTagSelect();
                            Home(token);
                        }
                    }
                    else if (tagName.IsOneOf(Tags.Template, Tags.Script))
                        InHead(token);
                    else
                        RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Template)
                        InHead(token);
                    else if (tagName == Tags.Optgroup)
                        InSelectEndTagOptgroup();
                    else if (tagName == Tags.Option)
                        InSelectEndTagOption();
                    else if (tagName == Tags.Select)
                    {
                        if (IsInSelectScope(Tags.Select))
                            InSelectEndTagSelect();
                        else
                            RaiseErrorOccurred(ErrorCode.SelectNotInScope);
                    }
                    else
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);

                    return;
                }
                case HtmlTokenType.EOF:
                {
                    InBody(token);
                    return;
                }
                default:
                {
                    RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                    return;
                }
            }
        }

        /// <summary>
        /// See 8.2.5.4.17 The "in select in table" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InSelectInTable(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsTableCellElement() || tagName.IsTableElement() || tagName == Tags.Caption)
                    {
                        RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);
                        InSelectEndTagSelect();
                        Home(token);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsTableCellElement() || tagName.IsTableElement() || tagName == Tags.Caption)
                    {
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);

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
        void InTemplate(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName.IsOneOf(Tags.Link, Tags.Meta, Tags.Script, Tags.Style) || tagName.IsOneOf(Tags.Base, Tags.BaseFont, Tags.Bgsound, Tags.NoFrames, Tags.Template, Tags.Title))
                        InHead(token);
                    else if (tagName.IsOneOf(Tags.Caption, Tags.Colgroup, Tags.Tbody, Tags.Tfoot, Tags.Thead))
                        TemplateStep(token, HtmlTreeMode.InTable);
                    else if (tagName == Tags.Col)
                        TemplateStep(token, HtmlTreeMode.InColumnGroup);
                    else if (tagName == Tags.Tr)
                        TemplateStep(token, HtmlTreeMode.InTableBody);
                    else if (tagName.IsOneOf(Tags.Td, Tags.Th))
                        TemplateStep(token, HtmlTreeMode.InRow);
                    else
                        TemplateStep(token, HtmlTreeMode.InBody);

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name == Tags.Template)
                        InHead(token);
                    else
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);

                    return;
                }
                case HtmlTokenType.EOF:
                {
                    if (TagCurrentlyOpen(Tags.Template))
                    {
                        RaiseErrorOccurred(ErrorCode.EOF);
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
        void AfterBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    ReconstructFormatting();
                    AddCharacters(str);

                    if (token.IsEmpty)
                        return;
                    
                    break;
                }
                case HtmlTokenType.Comment:
                {
                    open[0].AddComment(token);
                    return;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (token.Name == Tags.Html)
                    {
                        InBody(token);
                        return;
                    }

                    break;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name == Tags.Html)
                    {
                        if (IsFragmentCase)
                            RaiseErrorOccurred(ErrorCode.TagInvalidInFragmentMode);
                        else
                            insert = HtmlTreeMode.AfterAfterBody;

                        return;
                    }

                    break;
                }
                case HtmlTokenType.EOF:
                {
                    End();
                    return;
                }
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
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                        InBody(token);
                    else if (tagName == Tags.Frameset)
                        AddElement(new HtmlFrameSetElement(doc), token.AsTag());
                    else if (tagName == Tags.Frame)
                    {
                        AddElement(new HtmlFrameElement(doc), token.AsTag(), true);
                        CloseCurrentNode();
                    }
                    else if (tagName == Tags.NoFrames)
                        InHead(token);
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name != Tags.Frameset)
                        break;

                    if (CurrentNode != open[0])
                    {
                        CloseCurrentNode();

                        if (!IsFragmentCase && CurrentNode is HtmlFrameSetElement == false)
                            insert = HtmlTreeMode.AfterFrameset;
                    }
                    else
                        RaiseErrorOccurred(ErrorCode.CurrentNodeIsRoot);

                    return;
                }
                case HtmlTokenType.EOF:
                {
                    if (CurrentNode != doc.DocumentElement)
                        RaiseErrorOccurred(ErrorCode.CurrentNodeIsNotRoot);

                    End();
                    return;
                }
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
        }

        /// <summary>
        /// See 8.2.5.4.21 The "after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterFrameset(HtmlToken token)
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
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                        InBody(token);
                    else if (tagName == Tags.NoFrames)
                        InHead(token);
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    if (token.Name != Tags.Html)
                        break;

                    insert = HtmlTreeMode.AfterAfterFrameset;
                    return;
                }
                case HtmlTokenType.EOF:
                {
                    End();
                    return;
                }
            }

            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
        }

        /// <summary>
        /// See 8.2.5.4.22 The "after after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterAfterBody(HtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.Comment:
                {
                    doc.AddComment(token);
                    return;
                }
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    ReconstructFormatting();
                    AddCharacters(str);

                    if (token.IsEmpty)
                        return;

                    break;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    InBody(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    if (token.Name != Tags.Html)
                        break;

                    InBody(token);
                    return;
                }
                case HtmlTokenType.EOF:
                {
                    End();
                    return;
                }
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
            switch (token.Type)
            {
                case HtmlTokenType.Comment:
                {
                    doc.AddComment(token);
                    return;
                }
                case HtmlTokenType.Character:
                {
                    var str = token.TrimStart();
                    ReconstructFormatting();
                    AddCharacters(str);

                    if (token.IsEmpty)
                        return;

                    break;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    InBody(token);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Html)
                        InBody(token);
                    else if (tagName == Tags.NoFrames)
                        InHead(token);
                    else
                        break;

                    return;
                }
                case HtmlTokenType.EOF:
                {
                    End();
                    return;
                }
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
                var template = CurrentNode as HtmlTemplateElement;
                CloseCurrentNode();

                if (template != null)
                {
                    template.PopulateFragment();
                    break;
                }
            }

            formatting.ClearFormatting();
            templateMode.Pop();
            Reset();
        }

        /// <summary>
        /// Closes the table if the section is in table scope.
        /// </summary>
        /// <param name="tag">The tag to insert which triggers the closing of the table.</param>
        void InTableBodyCloseTable(HtmlTagToken tag)
        {
            if (IsInTableScope<HtmlTableSectionElement>())
            {
                ClearStackBackTo<HtmlTableSectionElement>();
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
            if (CurrentNode is HtmlOptionElement)
                CloseCurrentNode();
            else
                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
        }

        /// <summary>
        /// Acts if a optgroup end tag had been seen in the InSelect state.
        /// </summary>
        void InSelectEndTagOptgroup()
        {
            if (open.Count > 1 && open[open.Count - 1] is HtmlOptionElement && open[open.Count - 2] is HtmlOptionsGroupElement)
                CloseCurrentNode();

            if (CurrentNode is HtmlOptionsGroupElement)
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
            AddElement(new HtmlBodyElement(doc), token);
            frameset = false;
            insert = HtmlTreeMode.InBody;
        }

        /// <summary>
        /// Follows the generic rawtext parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        void RawtextAlgorithm(HtmlTagToken tag)
        {
            AddElement(tag);
            originalInsert = insert;
            insert = HtmlTreeMode.Text;
            tokenizer.State = HtmlParseMode.Rawtext;
        }

        /// <summary>
        /// Follows the generic RCData parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        void RCDataAlgorithm(HtmlTagToken tag)
        {
            AddElement(tag);
            originalInsert = insert;
            insert = HtmlTreeMode.Text;
            tokenizer.State = HtmlParseMode.RCData;
        }

        /// <summary>
        /// Acts if a li start tag in the InBody state has been found.
        /// </summary>
        /// <param name="tag">The actual tag given.</param>
        void InBodyStartTagListItem(HtmlTagToken tag)
        {
            var index = open.Count - 1;
            var node = open[index];
            frameset = false;

            while (true)
            {
                if (node is HtmlListItemElement && node.NodeName == Tags.Li)
                {
                    InBody(HtmlTagToken.Close(node.NodeName));
                    break;
                }

                if (node is HtmlAddressElement == false && node is HtmlDivElement == false && node is HtmlParagraphElement == false && node.Flags.HasFlag(NodeFlags.Special))
                    break;
                
                node = open[--index];
            }

            if (IsInButtonScope())
                InBodyEndTagParagraph();

            AddElement(tag);
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
                if (node is HtmlListItemElement && (node.NodeName == Tags.Dd || node.NodeName == Tags.Dt))
                {
                    InBody(HtmlTagToken.Close(node.NodeName));
                    break;
                }

                if (node.Flags.HasFlag(NodeFlags.Special) && node is HtmlAddressElement == false && node is HtmlDivElement == false && node is HtmlParagraphElement == false)
                    break;

                node = open[--index];
            }

            if (IsInButtonScope())
                InBodyEndTagParagraph();

            AddElement(tag);
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

            while (outer < 8)
            {
                Element formattingElement = null;
                Element furthestBlock = null;

                outer++;
                index = 0;
                inner = 0;

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

                bookmark = index;

                for (var j = openIndex + 1; j < open.Count; j++)
                {
                    if (open[j].Flags.HasFlag(NodeFlags.Special))
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

                var commonAncestor = open[openIndex - 1];
                var node = furthestBlock;
                var lastNode = furthestBlock;

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
                    commonAncestor.AddNode(newElement);
                    open[index] = newElement;
                    
                    for (var l = 0; l != formatting.Count; l++)
                    {
                        if (formatting[l] == node)
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

                    node.AddNode(lastNode);
                    lastNode = node;
                }

                if (lastNode.Parent != null)
                    lastNode.Parent.RemoveChild(lastNode);

                if (!commonAncestor.IsTableElement())
                    commonAncestor.AddNode(lastNode);
                else
                    AddElementWithFoster(lastNode);

                var element = CopyElement(formattingElement);

                while (furthestBlock.ChildNodes.Length > 0)
                {
                    var childNode = furthestBlock.ChildNodes[0];
                    furthestBlock.RemoveNode(0, childNode);
                    element.AddNode(childNode);
                }

                furthestBlock.AddNode(element);
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
            return (Element)element.Clone(false);
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
                else if (node.Flags.HasFlag(NodeFlags.Special))
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
            if (IsInScope<HtmlBodyElement>())
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
            AddElement(tag);
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

                if (CurrentNode is HtmlParagraphElement == false)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo<HtmlParagraphElement>();
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.ParagraphNotInScope);
                InBody(HtmlTagToken.Open(Tags.P));
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
            if (IsInTableScope<HtmlTableElement>())
            {
                ClearStackBackTo<HtmlTableElement>();
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
            if (IsInTableScope<HtmlTableRowElement>())
            {
                ClearStackBackTo<HtmlTableRowElement>();
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
            ClearStackBackTo<HtmlSelectElement>();
            CloseCurrentNode();
            Reset();
        }

        /// <summary>
        /// Act as if an caption end tag has been found in the InCaption state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        Boolean InCaptionEndTagCaption()
        {
            if (IsInTableScope<HtmlTableCaptionElement>())
            {
                GenerateImpliedEndTags();

                if (CurrentNode is HtmlTableCaptionElement == false)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo<HtmlTableCaptionElement>();
                CloseCurrentNode();
                formatting.ClearFormatting();
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
            if (IsInTableScope<HtmlTableCellElement>())
            {
                GenerateImpliedEndTags();

                if (CurrentNode is HtmlTableCellElement == false)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo<HtmlTableCellElement>();
                CloseCurrentNode();
                formatting.ClearFormatting();
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
            switch (token.Type)
            {
                case HtmlTokenType.Character:
                {
                    AddCharacters(token.Data.Replace(Symbols.Null, Symbols.Replacement));

                    if (token.HasContent)
                        frameset = false;

                    return;
                }
                case HtmlTokenType.Comment:
                {
                    CurrentNode.AddComment(token);
                    return;
                }
                case HtmlTokenType.DOCTYPE:
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
                    return;
                }
                case HtmlTokenType.StartTag:
                {
                    var tagName = token.Name;

                    if (tagName == Tags.Font)
                    {
                        var tag = token.AsTag();

                        for (var i = 0; i != tag.Attributes.Count; i++)
                        {
                            if (tag.Attributes[i].Key.IsOneOf(AttributeNames.Color, AttributeNames.Face, AttributeNames.Size))
                            {
                                ForeignNormalTag(token);
                                return;
                            }
                        }

                        ForeignSpecialTag(tag);
                    }
                    else if (tagName.IsOneOf(Tags.B, Tags.Big, Tags.BlockQuote, Tags.Body, Tags.Br, Tags.Center) ||
                             tagName.IsOneOf(Tags.Code, Tags.Dd, Tags.Div, Tags.Dl, Tags.Dt, Tags.Em) ||
                             tagName.IsOneOf(Tags.Embed, Tags.Head, Tags.Hr, Tags.I, Tags.Img, Tags.Li, Tags.Ul) ||
                             tagName.IsOneOf(Tags.H3, Tags.H2, Tags.H4, Tags.H1, Tags.H6, Tags.H5) ||
                             tagName.IsOneOf(Tags.Listing, Tags.Menu, Tags.Meta, Tags.NoBr, Tags.Ol) ||
                             tagName.IsOneOf(Tags.P, Tags.Pre, Tags.Ruby, Tags.S, Tags.Small, Tags.Span, Tags.Strike) ||
                             tagName.IsOneOf(Tags.Strong, Tags.Sub, Tags.Sup, Tags.Table, Tags.Tt, Tags.U, Tags.Var))
                        ForeignNormalTag(token);
                    else
                        ForeignSpecialTag(token.AsTag());

                    return;
                }
                case HtmlTokenType.EndTag:
                {
                    var tagName = token.Name;
                    var node = CurrentNode;
                    var script = node as HtmlScriptElement;

                    if (script == null)
                    {
                        if (node.NodeName != tagName)
                            RaiseErrorOccurred(ErrorCode.TagClosingMismatch);

                        for (int i = open.Count - 1; i > 0; i--)
                        {
                            if (node.NodeName.Equals(tagName, StringComparison.OrdinalIgnoreCase))
                            {
                                open.RemoveRange(i + 1, open.Count - i - 1);
                                CloseCurrentNode();
                                break;
                            }

                            node = open[i - 1];

                            if (node.Flags.HasFlag(NodeFlags.HtmlMember))
                            {
                                Home(token);
                                break;
                            }
                        }
                    }
                    else
                        RunScript(script);

                    return;
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
                    tokenizer.IsAcceptingCharacterData = true;
                }
                else if (tag.Name == Tags.Script)
                    Foreign(HtmlTagToken.Close(Tags.Script));
            }
        }

        /// <summary>
        /// Creates a foreign element from the given html tag.
        /// </summary>
        /// <param name="tag">The tag of the foreign element.</param>
        /// <returns>The element or NULL if it is no MathML or SVG element.</returns>
        Element CreateForeignElementFrom(HtmlTagToken tag)
        {
            if (AdjustedCurrentNode.Flags.HasFlag(NodeFlags.MathMember))
            {
                var node = Factory.MathElements.Create(tag.Name, doc);

                for (int i = 0; i < tag.Attributes.Count; i++)
                {
                    var name = tag.Attributes[i].Key;
                    var value = tag.Attributes[i].Value;
                    node.SetAdjustedAttribute(name.AdjustMathMLAttributeName(), value);
                }

                return node;
            }
            else if (AdjustedCurrentNode.Flags.HasFlag(NodeFlags.SvgMember))
            {
                var node = Factory.SvgElements.Create(tag.Name.AdjustSvgTagName(), doc);

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

                if (CurrentNode is MathAnnotationXmlElement)
                {
                    var value = CurrentNode.GetAttribute(AttributeNames.Encoding);

                    if (!String.IsNullOrEmpty(value))
                    {
                        if (value.Equals(MimeTypes.Html, StringComparison.OrdinalIgnoreCase) || value.Equals(MimeTypes.ApplicationXHtml, StringComparison.OrdinalIgnoreCase))
                            break;
                    }
                }
            }
            while ((CurrentNode.Flags & (NodeFlags.HtmlTip | NodeFlags.MathTip | NodeFlags.HtmlMember)) == NodeFlags.None);

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

                if (node.Flags.HasFlag(NodeFlags.Scoped))
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

                if (node.Flags.HasFlag(NodeFlags.Scoped))
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

                if (node is HtmlListItemElement)
                    return true;

                if (node.Flags.HasFlag(NodeFlags.HtmlListScoped))
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

                if (node is HtmlParagraphElement)
                    return true;

                if (node.Flags.HasFlag(NodeFlags.Scoped) || node is HtmlButtonElement)
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

                if (node.Flags.HasFlag(NodeFlags.HtmlTableScoped))
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

                if (node.Flags.HasFlag(NodeFlags.HtmlTableScoped))
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

                if (node.Flags.HasFlag(NodeFlags.HtmlSelectScoped))
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
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to consider.</param>
        /// <returns>The task to await.</returns>
        async Task<IDocument> KernelAsync(CancellationToken cancelToken)
        {
            var source = doc.Source;
            HtmlToken token;

            do
            {
                if (source.Length - source.Index < 1024)
                    await source.Prefetch(8192, cancelToken).ConfigureAwait(false);

                token = tokenizer.Get();
                Consume(token);
            }
            while (token.Type != HtmlTokenType.EOF);

            return doc;
        }

        /// <summary>
        /// Runs a script given by the current node.
        /// </summary>
        void RunScript(HtmlScriptElement script)
        {
            //Disable scripting for HTML fragments (security reasons)
            if (script == null || IsFragmentCase)
                return;

            doc.PerformMicrotaskCheckpoint();
            doc.ProvideStableState();
            CloseCurrentNode();
            insert = originalInsert;
            nesting++;
            script.Prepare();
            nesting--;

            if (pendingParsingBlock == null || nesting != 0)
                return;

            do
            {
                script = pendingParsingBlock;
                pendingParsingBlock = null;
                doc.WaitForReady();
                nesting++;
                script.Run();
                nesting--;
                tokenizer.ResetInsertionPoint();
            }
            while (pendingParsingBlock != null);
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
                if (!open[i].Flags.HasFlag(NodeFlags.ImplicitelyClosed))
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
                if (open[i].NodeName == tagName)
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
                temp.RemoveNewLine();

            Home(temp);
        }

        /// <summary>
        /// 8.2.6 The end.
        /// </summary>
        void End()
        {
            while (open.Count != 0)
                CloseCurrentNode();

            if (doc.ReadyState == DocumentReadyState.Loading)
                doc.FinishLoading();
        }

        #endregion

        #region Appending Nodes

        /// <summary>
        /// Appends the doctype token to the document.
        /// </summary>
        /// <param name="token">The doctypen token.</param>
        void AddDoctype(HtmlDoctypeToken token)
        {
            doc.AddNode(new DocumentType(doc, token.Name ?? String.Empty)
            {
                SystemIdentifier = token.SystemIdentifier,
                PublicIdentifier = token.PublicIdentifier,
                OriginalPosition = token.Range
            });
        }

        /// <summary>
        /// Adds the root element (html) to the document.
        /// </summary>
        /// <param name="tag">The token which started this process.</param>
        void AddRoot(HtmlTagToken tag)
        {
            var element = new HtmlHtmlElement(doc);
            doc.AddNode(element);
            SetupElement(element, tag, false);
            open.Add(element);
            tokenizer.IsAcceptingCharacterData = false;
            doc.ApplyManifest(element);
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
                tokenizer.IsAcceptingCharacterData = node != null && !node.Flags.HasFlag(NodeFlags.HtmlMember);
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
            element.OriginalPosition = tag.Range;

            if (tag.IsSelfClosing && !acknowledgeSelfClosing)
                RaiseErrorOccurred(ErrorCode.TagCannotBeSelfClosed);

            element.SetAttributes(tag.Attributes);
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        Element AddElement(HtmlTagToken tag, Boolean acknowledgeSelfClosing = false)
        {
            var element = Factory.HtmlElements.Create(tag.Name, doc);
            SetupElement(element, tag, acknowledgeSelfClosing);
            AddElement(element);
            return element;
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <typeparam name="TElement">The type of element to create.</typeparam>
        /// <param name="tag">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        TElement AddElement<TElement>(HtmlTagToken tag, Boolean acknowledgeSelfClosing = false)
            where TElement : Element, new()
        {
            var element = new TElement { Owner = doc };
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
                node.AddNode(element);

            open.Add(element);
            tokenizer.IsAcceptingCharacterData = !element.Flags.HasFlag(NodeFlags.HtmlMember);
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
                if (open[index] is HtmlTemplateElement)
                {
                    open[index].AddNode(element);
                    return;
                }
                else if (open[index] is HtmlTableElement)
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
                        foster.InsertNode(i, element);
                        break;
                    }
			    }
            }
            else
                foster.AddNode(element);
        }

        /// <summary>
        /// Appends a configured foreign node to the current node.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        void AddForeignElement(Element element)
        {
            element.NamespaceUri = AdjustedCurrentNode.NamespaceUri;
            CurrentNode.AddNode(element);
        }

        /// <summary>
        /// Inserts the given characters into the current node.
        /// </summary>
        /// <param name="text">The characters to insert.</param>
        void AddCharacters(String text)
        {
            if (String.IsNullOrEmpty(text))
                return;

            var node = CurrentNode;

            if (foster && node.IsTableElement())
                AddCharactersWithFoster(text);
            else
                node.AppendText(text);
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
                if (open[index] is HtmlTemplateElement)
                {
                    open[index].AppendText(text);
                    return;
                }
                else if (open[index] is HtmlTableElement)
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
            var node = CurrentNode;

            while (node.NodeName != tagName && node is HtmlHtmlElement == false && node is HtmlTemplateElement == false)
            {
                CloseCurrentNode();
                node = CurrentNode;
            }
        }

        /// <summary>
        /// Clears the stack of open elements back to any heading element.
        /// </summary>
        void ClearStackBackTo<T>()
        {
            var node = CurrentNode;

            while (node is T == false && node is HtmlHtmlElement == false && node is HtmlTemplateElement == false)
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
        void GenerateImpliedEndTagsExceptFor(String tagName)
        {
            var node = CurrentNode;

            while (node.Flags.HasFlag(NodeFlags.ImpliedEnd) && node.NodeName != tagName)
            {
                CloseCurrentNode();
                node = CurrentNode;
            }
        }

        /// <summary>
        /// Generates the implied end tags for the dd, dt, li, option, optgroup, p, rp, rt elements.
        /// </summary>
        void GenerateImpliedEndTags()
        {
            while (CurrentNode.Flags.HasFlag(NodeFlags.ImpliedEnd))
                CloseCurrentNode();
        }

        #endregion

        #region Formatting

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
            tokenizer.RaiseErrorOccurred(code);
        }

        #endregion
    }
}
