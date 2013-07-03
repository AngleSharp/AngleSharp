using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Mathml;
using AngleSharp.DOM.Svg;
using AngleSharp.DOM.Xml;
using System.Threading.Tasks;

namespace AngleSharp.Html
{
    /// <summary>
    /// Represents the Tree construction as specified in
    /// 8.2.5 Tree construction, on the following page:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    public class HtmlParser : IParser
    {
        #region Members

        HtmlTokenizer tokenizer;
        HTMLDocument doc;
        HtmlTreeMode insert;
        HtmlTreeMode originalInsert;
        List<Element> open;
        List<Element> formatting;
        HTMLFormElement form;
        Boolean frameset;
        Boolean fragment;
        StringBuilder tableCharacters;
        Boolean foster;
        Int32 nesting;
        Boolean pause;
        Boolean started;
        TaskCompletionSource<Boolean> tcs;
        HTMLScriptElement pendingParsingBlock;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the HTML parser with an new document
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        public HtmlParser(String source)
            : this(new HTMLDocument(), new SourceManager(source))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with an new document
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        public HtmlParser(Stream stream)
            : this(new HTMLDocument(), new SourceManager(stream))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with the specified document
        /// based on the given source.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="source">The source code as a string.</param>
        public HtmlParser(HTMLDocument document, String source)
            : this(document, new SourceManager(source))
        {
        }

        /// <summary>
        /// Creates a new instance of the HTML parser with the specified document
        /// based on the given stream.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="stream">The stream to use as source.</param>
        public HtmlParser(HTMLDocument document, Stream stream)
            : this(document, new SourceManager(stream))
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
                if (ErrorOccurred != null)
                    ErrorOccurred(this, ev);
            };

            started = false;
            doc = document;
            open = new List<Element>();
            formatting = new List<Element>();
            frameset = true;
            insert = HtmlTreeMode.Initial;
            tableCharacters = new StringBuilder();
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
            get { return tcs != null; }
        }

        /// <summary>
        /// Gets if the tree builder has been created for
        /// parsing HTML fragments.
        /// </summary>
        public Boolean IsFragmentCase
        {
            get { return fragment; }
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
        /// WARNING: This method is not yet implemented.
        /// </summary>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task ParseAsync()
        {
            if (!started)
            {
                started = true;
                tcs = new TaskCompletionSource<bool>();
                //TODO
                return tcs.Task;
            }
            else if (tcs == null)
            {
                var temp = new TaskCompletionSource<bool>();
                temp.SetResult(true);
                return temp.Task;
            }

            return tcs.Task;
        }

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        public void Parse()
        {
            if (!started)
            {
                started = true;
                HtmlToken token;

                do
                {
                    token = tokenizer.Get();
                    Consume(token);
                }
                while (token.Type != HtmlTokenType.EOF);
            }
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
                case HTMLTitleElement.Tag:
                case HTMLTextAreaElement.Tag:
                    tokenizer.Switch(HtmlParseMode.RCData);
                    break;

                case HTMLStyleElement.Tag:
                case HTMLSemanticElement.XmpTag:
                case HTMLIFrameElement.Tag:
                case HTMLNoElement.NoEmbedTag:
                case HTMLNoElement.NoFramesTag:
                    tokenizer.Switch(HtmlParseMode.Rawtext);
                    break;

                case HTMLScriptElement.Tag:
                    tokenizer.Switch(HtmlParseMode.Script);
                    break;

                case HTMLNoElement.NoScriptTag:
                    if (doc.IsScripting) tokenizer.Switch(HtmlParseMode.Rawtext);
                    break;

                case HTMLSemanticElement.PlaintextTag:
                    tokenizer.Switch(HtmlParseMode.Plaintext);
                    break;
            }

            var root = new HTMLHtmlElement();
            fragment = true;
            doc.AppendChild(root);
            open.Add(root);
            Reset(context);

            do
            {
                if (context is HTMLFormElement)
                {
                    form = (HTMLFormElement)context;
                    break;
                }

                context = context.ParentNode;
            }
            while (context != null);
        }

        /// <summary>
        /// Resets the current insertation mode to the
        /// rules according to the algorithm specified
        /// in 8.2.3.1 The insertion mode.
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
                    case HTMLSelectElement.Tag:
                        insert = HtmlTreeMode.InSelect;
                        break;

                    case HTMLTableCellElement.HeadTag:
                    case HTMLTableCellElement.NormalTag:
                        insert = last ? HtmlTreeMode.InBody : HtmlTreeMode.InCell;
                        break;

                    case HTMLTableRowElement.Tag:
                        insert = HtmlTreeMode.InRow;
                        break;

                    case HTMLTableSectionElement.HeadTag:
                    case HTMLTableSectionElement.FootTag:
                    case HTMLTableSectionElement.BodyTag:
                        insert = HtmlTreeMode.InTableBody;
                        break;

                    case HTMLTableCaptionElement.Tag:
                        insert = HtmlTreeMode.InCaption;
                        break;

                    case HTMLTableColElement.ColgroupTag:
                        insert = HtmlTreeMode.InColumnGroup;
                        break;

                    case HTMLTableElement.Tag:
                        insert = HtmlTreeMode.InTable;
                        break;

                    case HTMLHeadElement.Tag:
                        insert = HtmlTreeMode.InBody;
                        break;

                    case HTMLBodyElement.Tag:
                        insert = HtmlTreeMode.InBody;
                        break;

                    case HTMLFrameSetElement.Tag:
                        insert = HtmlTreeMode.InFrameset;
                        break;

                    case HTMLHtmlElement.Tag:
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
            var node = CurrentNode;

            if (token.Type == HtmlTokenType.Characters)
            {
                var chars = ((HtmlCharactersToken)token).Data;

                if (chars.Length > 0)
                {
                    var t = new HtmlCharacterToken();

                    for (int i = 0; i < chars.Length; i++)
                    {
                        t.Data = chars[i];
                        Consume(t);
                    }
                }
            }
            else if ((node == null) || node.IsInHtml || (node.IsHtmlTIP && (token.Type == HtmlTokenType.StartTag || token.Type == HtmlTokenType.Character)) ||
                (node.IsInMathML && node.NodeName == Specification.XML_ANNOTATION && token.IsStartTag(SVGElement.RootTag)) || (token.Type == HtmlTokenType.EOF) ||
                (node.IsMathMLTIP && (token.Type == HtmlTokenType.Character || (token.Type == HtmlTokenType.StartTag && (!token.IsStartTag("mglyph") && !token.IsStartTag("malignmark"))))))
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

                    case HtmlTreeMode.InTableText:
                        InTableText(token);
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
            else
                Foreign(token);
        }

        #endregion

        #region Home

        /// <summary>
        /// See 8.2.5.4.1 The "initial" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void Initial(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(doc, token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                var doctype = (HtmlDoctypeToken)token;

                if (!doctype.IsValid)
                    RaiseErrorOccurred(ErrorCode.DoctypeInvalid);

                AddElement(doctype);

                if (doctype.IsFullQuirks)
                    doc.QuirksMode = QuirksMode.On;
                else if (doctype.IsLimitedQuirks)
                    doc.QuirksMode = QuirksMode.Limited;
                
                insert = HtmlTreeMode.BeforeHtml;
            }
            else if(!token.IsIgnorable)
            {
                if (!doc.IsEmbedded)
                {
                    RaiseErrorOccurred(ErrorCode.DoctypeMissing);
                    doc.QuirksMode = QuirksMode.On;
                }

                insert = HtmlTreeMode.BeforeHtml;
                BeforeHtml(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.2 The "before html" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void BeforeHtml(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(doc, token);
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag)
            {
                var element = new HTMLHtmlElement();
                AddElementToDocument(element, token);

                //TODO
                //If the Document is being loaded as part of navigation of a browsing context, then:
                //  if the newly created element has a manifest attribute whose value is not the empty string,
                //    then resolve the value of that attribute to an absolute URL, relative to the newly created element,
                //    and if that is successful, run the application cache selection algorithm with the result of applying
                //    the URL serializer algorithm to the resulting parsed URL with the exclude fragment flag set;
                //  otherwise, if there is no such attribute, or its value is the empty string, or resolving its value fails,
                //    run the application cache selection algorithm with no manifest. The algorithm must be passed the Document object.
                insert = HtmlTreeMode.BeforeHead;
            }
            else if (token.Type == HtmlTokenType.EndTag && !(((HtmlTagToken)token).Name.IsHtmlBodyOrBreakRowElement(true)))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
            }
            else if(!token.IsIgnorable)
            {
                var element = new HTMLHtmlElement();
                AddElementToDocument(element, HtmlToken.OpenTag(HTMLHtmlElement.Tag));
                //TODO
                //If the Document is being loaded as part of navigation of a browsing context, then:
                //  run the application cache selection algorithm with no manifest, passing it the Document object.
                insert = HtmlTreeMode.BeforeHead;
                BeforeHead(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.3 The "before head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void BeforeHead(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(CurrentNode, token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag)
            {
                InBody(token);
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHeadElement.Tag)
            {
                var element = new HTMLHeadElement();
                AddElementToCurrentNode(element, token);
                insert = HtmlTreeMode.InHead;
            }
            else if (token.Type == HtmlTokenType.EndTag && !(((HtmlTagToken)token).Name.IsHtmlBodyOrBreakRowElement(true)))
            {
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
            }
            else if(!token.IsIgnorable)
            {
                BeforeHead(HtmlToken.OpenTag(HTMLHeadElement.Tag));
                InHead(token);
            }
        }
        
        /// <summary>
        /// See 8.2.5.4.4 The "in head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InHead(HtmlToken token)
        {
            if (token.IsIgnorable)
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.Comment)
                AddComment(CurrentNode, token);
            else if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag)
                InBody(token);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name.IsOneOf(HTMLBaseElement.Tag, HTMLBaseFontElement.Tag, HTMLBgsoundElement.Tag, HTMLLinkElement.Tag))
            {
                var name = ((HtmlTagToken)token).Name;
                var element = HTMLElement.Factory(name);
                AddElementToCurrentNode(element, token, true);
                CloseCurrentNode();
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLMetaElement.Tag)
            {
                var element = new HTMLMetaElement();
                AddElementToCurrentNode(element, token, true);
                CloseCurrentNode();

                var charset = element.GetAttribute(HtmlEncoding.CHARSET);

                if (charset != null && HtmlEncoding.IsSupported(charset))
                {
                    SetCharset(charset);
                    return;
                }

                charset = element.GetAttribute("http-equiv");

                if (charset != null && charset.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    charset = element.GetAttribute("content") ?? string.Empty;
                    charset = HtmlEncoding.Extract(charset);

                    if (HtmlEncoding.IsSupported(charset))
                        SetCharset(charset);
                }
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLTitleElement.Tag)
                RCDataAlgorithm((HtmlTagToken)token);
            else if (token.Type == HtmlTokenType.StartTag && (((HtmlTagToken)token).Name.IsOneOf(HTMLNoElement.NoFramesTag, HTMLStyleElement.Tag) ||
                (((HtmlTagToken)token).Name == HTMLNoElement.NoScriptTag && doc.IsScripting)))
                RawtextAlgorithm((HtmlTagToken)token);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLNoElement.NoScriptTag)
            {
                var element = new HTMLElement();
                AddElementToCurrentNode(element, token);
                insert = HtmlTreeMode.InHeadNoScript;
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLScriptElement.Tag)
            {
                var element = new HTMLScriptElement();
                //element.IsParserInserted = true;
                //element.IsAlreadyStarted = fragment;
                AddElementToCurrentNode(element, token);
                tokenizer.Switch(HtmlParseMode.Script);
                originalInsert = insert;
                insert = HtmlTreeMode.Text;
            }
            else if (token.Type == HtmlTokenType.EndTag && ((HtmlTagToken)token).Name == HTMLHeadElement.Tag)
            {
                CloseCurrentNode();
                insert = HtmlTreeMode.AfterHead;
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHeadElement.Tag)
                RaiseErrorOccurred(ErrorCode.HeadTagMisplaced);
            else if (token.Type == HtmlTokenType.EndTag && !(((HtmlTagToken)token).Name.IsHtmlBodyOrBreakRowElement()))
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
            else
            {
                CloseCurrentNode();
                insert = HtmlTreeMode.AfterHead;
                AfterHead(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.5 The "in head noscript" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InHeadNoScript(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag)
                InBody(token);
            else if (token.Type == HtmlTokenType.EndTag && ((HtmlTagToken)token).Name == HTMLNoElement.NoScriptTag)
            {
                CloseCurrentNode();
                insert = HtmlTreeMode.InHead;
            }
            else if (token.IsIgnorable)
                InHead(token);
            else if (token.Type == HtmlTokenType.Comment)
                InHead(token);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name.IsOneOf(HTMLBaseFontElement.Tag, HTMLBgsoundElement.Tag,
                        HTMLLinkElement.Tag, HTMLMetaElement.Tag, HTMLNoElement.NoFramesTag, HTMLStyleElement.Tag))
                InHead(token);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name.IsOneOf(HTMLHeadElement.Tag, HTMLNoElement.NoScriptTag))
                RaiseErrorOccurred(ErrorCode.TagInappropriate);
            else if (token.Type == HtmlTokenType.EndTag && ((HtmlTagToken)token).Name != HTMLBRElement.Tag)
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                CloseCurrentNode();
                insert = HtmlTreeMode.InHead;
                InHead(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.6 The "after head" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterHead(HtmlToken token)
        {
            if (token.IsIgnorable)
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.Comment)
                AddComment(CurrentNode, token);
            else if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag)
                InBody(token);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLBodyElement.Tag)
                AfterHeadStartTagBody((HtmlTagToken)token);
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLFrameSetElement.Tag)
            {
                var element = new HTMLFrameSetElement();
                AddElementToCurrentNode(element, token);
                insert = HtmlTreeMode.InFrameset;
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name.IsOneOf(HTMLBaseElement.Tag, HTMLBaseFontElement.Tag, HTMLBgsoundElement.Tag, 
                        HTMLLinkElement.Tag, HTMLMetaElement.Tag, HTMLNoElement.NoFramesTag, HTMLScriptElement.Tag, HTMLStyleElement.Tag, HTMLTitleElement.Tag))
            {
                RaiseErrorOccurred(ErrorCode.TagMustBeInHead);
                var index = open.Count;
                open.Add(doc.Head);
                InHead(token);
                open.RemoveAt(index);
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHeadElement.Tag)
                RaiseErrorOccurred(ErrorCode.HeadTagMisplaced);
            else if (token.Type == HtmlTokenType.EndTag && !(((HtmlTagToken)token).Name.IsHtmlBodyOrBreakRowElement()))
                RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
            else
            {
                AfterHeadStartTagBody(HtmlToken.OpenTag(HTMLBodyElement.Tag));
                frameset = true;
                Consume(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.7 The "in body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InBody(HtmlToken token)
        {
            if (token.IsNullChar)
                RaiseErrorOccurred(ErrorCode.NULL);
            else if (token.Type == HtmlTokenType.Character)
            {
                ReconstructFormatting();
                InsertCharacter(((HtmlCharacterToken)token).Data);

                if(!token.IsIgnorable)
                    frameset = false;
            }
            else if (token.Type == HtmlTokenType.Comment)
                AddComment(CurrentNode, token);
            else if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case HTMLHtmlElement.Tag:
                        RaiseErrorOccurred(ErrorCode.HtmlTagMisplaced);
                        AppendAttributesToElement(tag, open[0]);
                        break;

                    case HTMLBaseElement.Tag:
                    case HTMLBaseFontElement.Tag:
                    case HTMLBgsoundElement.Tag:
                    case HTMLLinkElement.Tag:
                    case HTMLMenuItemElement.Tag:
                    case HTMLMetaElement.Tag:
                    case HTMLNoElement.NoFramesTag:
                    case HTMLScriptElement.Tag:
                    case HTMLStyleElement.Tag:
                    case HTMLTitleElement.Tag:
                        InHead(token);
                        break;

                    case HTMLBodyElement.Tag:
                        RaiseErrorOccurred(ErrorCode.BodyTagMisplaced);

                        if (open.Count > 1 && open[1] is HTMLBodyElement)
                        {
                            frameset = false;
                            AppendAttributesToElement(tag, open[1]);
                        }

                        break;

                    case HTMLFrameSetElement.Tag:
                        RaiseErrorOccurred(ErrorCode.FramesetMisplaced);

                        if (open.Count != 1 && open[1] is HTMLBodyElement && frameset)
                        {
                            open[1].ParentNode.RemoveChild(open[1]);

                            while (open.Count > 1)
                                CloseCurrentNode();

                            var element = new HTMLFrameSetElement();
                            AddElementToCurrentNode(element, token);
                            insert = HtmlTreeMode.InFrameset;
                        }

                        break;

                    case HTMLSemanticElement.AddressTag:
                    case HTMLSemanticElement.ArticleTag:
                    case HTMLSemanticElement.AsideTag:
                    case HTMLQuoteElement.BlockTag:
                    case HTMLSemanticElement.CenterTag:
                    case HTMLDetailsElement.Tag:
                    case HTMLDialogElement.Tag:
                    case HTMLDirectoryElement.Tag:
                    case HTMLDivElement.Tag:
                    case HTMLDListElement.Tag:
                    case HTMLFieldSetElement.Tag:
                    case HTMLSemanticElement.FigcaptionTag:
                    case HTMLSemanticElement.FigureTag:
                    case HTMLSemanticElement.FooterTag:
                    case HTMLSemanticElement.HeaderTag:
                    case HTMLSemanticElement.HgroupTag:
                    case HTMLMenuElement.Tag:
                    case HTMLSemanticElement.NavTag:
                    case HTMLOListElement.Tag:
                    case HTMLParagraphElement.Tag:
                    case HTMLSemanticElement.SectionTag:
                    case HTMLSemanticElement.SummaryTag:
                    case HTMLUListElement.Tag:
                        {
                            if (IsInButtonScope(HTMLParagraphElement.Tag))
                                InBodyEndTagParagraph();

                            var element = HTMLElement.Factory(tag.Name);
                            AddElementToCurrentNode(element, token);
                        }
                        break;

                    case HTMLHeadingElement.ChapterTag:
                    case HTMLHeadingElement.SubSubSubSubSectionTag:
                    case HTMLHeadingElement.SubSubSubSectionTag:
                    case HTMLHeadingElement.SubSubSectionTag:
                    case HTMLHeadingElement.SubSectionTag:
                    case HTMLHeadingElement.SectionTag:
                        {
                            if (IsInButtonScope(HTMLParagraphElement.Tag))
                                InBodyEndTagParagraph();

                            if (CurrentNode is HTMLHeadingElement)
                            {
                                RaiseErrorOccurred(ErrorCode.HeadingNested);
                                CloseCurrentNode();
                            }

                            var element = new HTMLHeadingElement();
                            AddElementToCurrentNode(element, token);
                        }
                        break;

                    case HTMLPreElement.Tag:
                    case HTMLSemanticElement.ListingTag:
                        {
                            if (IsInButtonScope(HTMLParagraphElement.Tag))
                                InBodyEndTagParagraph();

                            var element = new HTMLPreElement();
                            AddElementToCurrentNode(element, token);
                            frameset = false;
                            var temp = tokenizer.Get();
                            if (!temp.IsNewLine) Consume(temp);
                        }
                        break;

                    case HTMLFormElement.Tag:
                        if(form == null)
                        {
                            if (IsInButtonScope(HTMLParagraphElement.Tag))
                                InBodyEndTagParagraph();

                            var element = new HTMLFormElement();
                            AddElementToCurrentNode(element, token);
                            form = element;
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.FormAlreadyOpen);

                        break;

                    case HTMLLIElement.ItemTag:
                        InBodyStartTagListItem(tag);
                        break;

                    case HTMLLIElement.DefinitionTag:
                    case HTMLLIElement.DescriptionTag:
                        InBodyStartTagDefinitionItem(tag);
                        break;

                    case HTMLSemanticElement.PlaintextTag:
                        {
                            if (IsInButtonScope(HTMLParagraphElement.Tag))
                                InBodyEndTagParagraph();

                            var plaintext = new HTMLElement();
                            AddElementToCurrentNode(plaintext, token);
                            tokenizer.Switch(HtmlParseMode.Plaintext);
                        }
                        break;

                    case HTMLButtonElement.Tag:
                        {
                            if (IsInScope(tag.Name))
                            {
                                RaiseErrorOccurred(ErrorCode.ButtonInScope);
                                InBodyEndTagBlock(tag.Name);
                                InBody(token);
                            }
                            else
                            {
                                ReconstructFormatting();
                                var element = new HTMLButtonElement();
                                AddElementToCurrentNode(element, token);
                                frameset = false;
                            }
                        }
                        break;

                    case HTMLAnchorElement.Tag:
                        {
                            for (var i = formatting.Count - 1; i >= 0; i--)
                            {
                                if (formatting[i] is ScopeMarkerNode)
                                    break;
                                else if (formatting[i].NodeName == HTMLAnchorElement.Tag)
                                {
                                    var format = formatting[i];
                                    RaiseErrorOccurred(ErrorCode.AnchorNested);
                                    HeisenbergAlgorithm(HtmlToken.CloseTag(HTMLAnchorElement.Tag));
                                    if(open.Contains(format)) open.Remove(format);
                                    if(formatting.Contains(format)) formatting.RemoveAt(i);
                                    break;
                                }
                            }

                            ReconstructFormatting();
                            var element = new HTMLAnchorElement();
                            AddElementToCurrentNode(element, token);
                            AddFormattingElement(element);
                        }
                        break;

                    case HTMLFormattingElement.BTag:
                    case HTMLFormattingElement.BigTag:
                    case HTMLFormattingElement.CodeTag:
                    case HTMLFormattingElement.EmTag:
                    case HTMLFontElement.Tag:
                    case HTMLFormattingElement.ITag:
                    case HTMLFormattingElement.STag:
                    case HTMLFormattingElement.SmallTag:
                    case HTMLFormattingElement.StrikeTag:
                    case HTMLFormattingElement.StrongTag:
                    case HTMLFormattingElement.TtTag:
                    case HTMLFormattingElement.UTag:
                        {
                            ReconstructFormatting();
                            var element = HTMLElement.Factory(tag.Name);
                            AddElementToCurrentNode(element, token);
                            AddFormattingElement(element);
                        }
                        break;

                    case HTMLFormattingElement.NobrTag:
                        {
                            ReconstructFormatting();

                            if (IsInScope(HTMLFormattingElement.NobrTag))
                            {
                                RaiseErrorOccurred(ErrorCode.NobrInScope);
                                HeisenbergAlgorithm(tag);
                                ReconstructFormatting();
                            }

                            var element = new HTMLElement();
                            AddElementToCurrentNode(element, token);
                            AddFormattingElement(element);
                        }
                        break;

                    case HTMLAppletElement.Tag:
                    case HTMLMarqueeElement.Tag:
                    case HTMLObjectElement.Tag:
                        {
                            ReconstructFormatting();
                            var element = HTMLElement.Factory(tag.Name);
                            AddElementToCurrentNode(element, token);
                            InsertScopeMarker();
                            frameset = false;
                        }
                        break;

                    case HTMLTableElement.Tag:
                        {
                            if (doc.QuirksMode == QuirksMode.Off && IsInButtonScope(HTMLParagraphElement.Tag))
                                InBodyEndTagParagraph();

                            var element = new HTMLTableElement();
                            AddElementToCurrentNode(element, token);
                            frameset = false;
                            insert = HtmlTreeMode.InTable;
                        }
                        break;

                    case HTMLAreaElement.Tag:
                    case HTMLBRElement.Tag:
                    case HTMLEmbedElement.Tag:
                    case HTMLImageElement.Tag:
                    case HTMLKeygenElement.Tag:
                    case HTMLWbrElement.Tag:
                        InBodyStartTagBreakrow(tag);
                        break;

                    case HTMLInputElement.Tag:
                        {
                            ReconstructFormatting();
                            var element = new HTMLInputElement();
                            AddElementToCurrentNode(element, token, true);
                            CloseCurrentNode();

                            if (!tag.GetAttribute("type").Equals("hidden", StringComparison.OrdinalIgnoreCase))
                                frameset = false;
                        }
                        break;

                    case HTMLParamElement.Tag:
                    case HTMLSourceElement.Tag:
                    case HTMLTrackElement.Tag:
                        {
                            var element = HTMLElement.Factory(tag.Name);
                            AddElementToCurrentNode(element, token, true);
                            CloseCurrentNode();
                        }
                        break;

                    case HTMLHRElement.Tag:
                        {
                            if (IsInButtonScope(HTMLParagraphElement.Tag))
                                InBodyEndTagParagraph();

                            var element = new HTMLHRElement();
                            AddElementToCurrentNode(element, token, true);
                            CloseCurrentNode();
                            frameset = false;
                        }
                        break;

                    case HTMLImageElement.FalseTag:
                        RaiseErrorOccurred(ErrorCode.ImageTagNamedWrong);
                        tag.Name = HTMLImageElement.Tag;
                        goto case HTMLImageElement.Tag;

                    case HTMLIsIndexElement.Tag:
                        {
                            RaiseErrorOccurred(ErrorCode.TagInappropriate);

                            if (form == null)
                            {
                                InBody(HtmlToken.CloseTag(HTMLFormElement.Tag));

                                if (tag.GetAttribute("action") != string.Empty)
                                    form.SetAttribute("action", tag.GetAttribute("action"));

                                InBody(HtmlToken.CloseTag(HTMLHRElement.Tag));
                                InBody(HtmlToken.CloseTag(HTMLLabelElement.Tag));

                                if (tag.GetAttribute("prompt") != string.Empty)
                                    InsertCharacters(tag.GetAttribute("prompt"));
                                else
                                    InsertCharacters("This is a searchable index. Enter search keywords:");

                                var input = HtmlToken.CloseTag(HTMLInputElement.Tag);
                                input.AddAttribute("name", HTMLIsIndexElement.Tag);

                                for (int i = 0; i < tag.Attributes.Count; i++)
                                {
                                    if (tag.Attributes[i].Key == "name" || tag.Attributes[i].Key == "action" || tag.Attributes[i].Key == "prompt")
                                        continue;

                                    input.AddAttribute(tag.Attributes[i].Key, tag.Attributes[i].Value);
                                }

                                InBody(input);
                                InBody(HtmlToken.OpenTag(HTMLLabelElement.Tag));
                                InBody(HtmlToken.CloseTag(HTMLHRElement.Tag));
                                InBody(HtmlToken.OpenTag(HTMLFormElement.Tag));
                            }
                        }
                        break;

                    case HTMLTextAreaElement.Tag:
                        {
                            var element = new HTMLTextAreaElement();
                            AddElementToCurrentNode(element, token);
                            tokenizer.Switch(HtmlParseMode.RCData);
                            originalInsert = insert;
                            frameset = false;
                            insert = HtmlTreeMode.Text;
                            var temp = tokenizer.Get();
                            if (!temp.IsNewLine) Consume(temp);
                        }
                        break;

                    case HTMLSemanticElement.XmpTag:
                        if (IsInButtonScope(HTMLParagraphElement.Tag))
                            InBodyEndTagParagraph();

                        ReconstructFormatting();
                        frameset = false;
                        RawtextAlgorithm(tag);
                        break;

                    case HTMLIFrameElement.Tag:
                        frameset = false;
                        RawtextAlgorithm(tag);
                        break;

                    case HTMLSelectElement.Tag:
                        {
                            ReconstructFormatting();
                            var element = new HTMLSelectElement();
                            AddElementToCurrentNode(element, token);
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
                        }

                        break;

                    case HTMLOptGroupElement.Tag:
                    case HTMLOptionElement.Tag:
                        {
                            if (CurrentNode.NodeName == HTMLOptionElement.Tag)
                                InBodyEndTagAnythingElse(HtmlToken.CloseTag(HTMLOptionElement.Tag));

                            ReconstructFormatting();
                            var element = HTMLElement.Factory(tag.Name);
                            AddElementToCurrentNode(element, token);
                        }
                        break;

                    case "rp":
                    case "rt":
                        {
                            if (IsInScope("ruby"))
                            {
                                GenerateImpliedEndTags();

                                if (CurrentNode.NodeName != "ruby")
                                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);
                            }
                            
                            var element = HTMLElement.Factory(tag.Name);
                            AddElementToCurrentNode(element, token);
                        }
                        break;

                    case HTMLNoElement.NoEmbedTag:
                        RawtextAlgorithm(tag);
                        break;

                    case HTMLNoElement.NoScriptTag:
                        if (!doc.IsScripting)
                            goto default;

                        RawtextAlgorithm(tag);
                        break;

                    case MathMLElement.RootTag:
                        {
                            var element = new MathMLElement();
                            element.NodeName = tag.Name;
                            ReconstructFormatting();

                            for (int i = 0; i < tag.Attributes.Count; i++)
                            {
                                var name = tag.Attributes[i].Key;
                                var value = tag.Attributes[i].Value;
                                element.SetAttribute(ForeignHelpers.AdjustAttributeName(MathMLHelpers.AdjustAttributeName(name)), value);
                            }

                            CurrentNode.AppendChild(element);

                            if (!tag.IsSelfClosing)
                            {
                                open.Add(element);
                                tokenizer.AcceptsCDATA = true;
                            }
                        }
                        break;

                    case SVGElement.RootTag:
                        {
                            var element = new SVGElement();
                            element.NodeName = tag.Name;
                            ReconstructFormatting();

                            for (int i = 0; i < tag.Attributes.Count; i++)
                            {
                                var name = tag.Attributes[i].Key;
                                var value = tag.Attributes[i].Value;
                                element.SetAttribute(ForeignHelpers.AdjustAttributeName(MathMLHelpers.AdjustAttributeName(name)), value);
                            }

                            CurrentNode.AppendChild(element);

                            if (!tag.IsSelfClosing)
                            {
                                open.Add(element);
                                tokenizer.AcceptsCDATA = true;
                            }
                        }
                        break;

                    case HTMLTableCaptionElement.Tag:
                    case HTMLTableColElement.ColTag:
                    case HTMLTableColElement.ColgroupTag:
                    case HTMLFrameElement.Tag:
                    case HTMLHeadElement.Tag:
                    case HTMLTableSectionElement.BodyTag:
                    case HTMLTableCellElement.NormalTag:
                    case HTMLTableSectionElement.FootTag:
                    case HTMLTableCellElement.HeadTag:
                    case HTMLTableSectionElement.HeadTag:
                    case HTMLTableRowElement.Tag:
                        RaiseErrorOccurred(ErrorCode.TagCannotStartHere);
                        break;

                    default:
                        {
                            ReconstructFormatting();
                            var element = new HTMLUnknownElement();
                            AddElementToCurrentNode(element, token);
                        }

                        break;
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case HTMLBodyElement.Tag:
                        InBodyEndTagBody();
                        break;

                    case HTMLHtmlElement.Tag:
                        if (InBodyEndTagBody())
                            AfterBody(token);

                        break;

                    case HTMLSemanticElement.AddressTag:
                    case HTMLSemanticElement.ArticleTag:
                    case HTMLSemanticElement.AsideTag:
                    case HTMLQuoteElement.BlockTag:
                    case HTMLButtonElement.Tag:
                    case HTMLSemanticElement.CenterTag:
                    case HTMLDetailsElement.Tag:
                    case HTMLDialogElement.Tag:
                    case HTMLDirectoryElement.Tag:
                    case HTMLDivElement.Tag:
                    case HTMLDListElement.Tag:
                    case HTMLFieldSetElement.Tag:
                    case HTMLSemanticElement.FigcaptionTag:
                    case HTMLSemanticElement.FigureTag:
                    case HTMLSemanticElement.FooterTag:
                    case HTMLSemanticElement.HeaderTag:
                    case HTMLSemanticElement.HgroupTag:
                    case HTMLSemanticElement.ListingTag:
                    case HTMLSemanticElement.MainTag:
                    case HTMLMenuElement.Tag:
                    case HTMLSemanticElement.NavTag:
                    case HTMLOListElement.Tag:
                    case HTMLPreElement.Tag:
                    case HTMLSemanticElement.SectionTag:
                    case HTMLSemanticElement.SummaryTag:
                    case HTMLUListElement.Tag:
                        InBodyEndTagBlock(tag.Name);
                        break;

                    case HTMLFormElement.Tag:
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
                        break;

                    case HTMLParagraphElement.Tag:
                        InBodyEndTagParagraph();
                        break;

                    case HTMLLIElement.ItemTag:
                        if (IsInListItemScope(tag.Name))
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

                    case HTMLLIElement.DefinitionTag:
                    case HTMLLIElement.DescriptionTag:
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

                    case HTMLHeadingElement.ChapterTag:
                    case HTMLHeadingElement.SubSubSubSubSectionTag:
                    case HTMLHeadingElement.SubSubSubSectionTag:
                    case HTMLHeadingElement.SubSubSectionTag:
                    case HTMLHeadingElement.SubSectionTag:
                    case HTMLHeadingElement.SectionTag:
                        if (IsHeadingInScope())
                        {
                            GenerateImpliedEndTags();

                            if (CurrentNode.NodeName != tag.Name)
                                RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                            ClearStackBackToHeading();
                            CloseCurrentNode();
                        }
                        else
                            RaiseErrorOccurred(ErrorCode.HeadingNotInScope);

                        break;

                    case HTMLAnchorElement.Tag:
                    case HTMLFormattingElement.BTag:
                    case HTMLFormattingElement.BigTag:
                    case HTMLFormattingElement.CodeTag:
                    case HTMLFormattingElement.EmTag:
                    case HTMLFontElement.Tag:
                    case HTMLFormattingElement.ITag:
                    case HTMLFormattingElement.NobrTag:
                    case HTMLFormattingElement.STag:
                    case HTMLFormattingElement.SmallTag:
                    case HTMLFormattingElement.StrikeTag:
                    case HTMLFormattingElement.StrongTag:
                    case HTMLFormattingElement.TtTag:
                    case HTMLFormattingElement.UTag:
                        HeisenbergAlgorithm(tag);
                        break;

                    case HTMLAppletElement.Tag:
                    case HTMLMarqueeElement.Tag:
                    case HTMLObjectElement.Tag:
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

                    case HTMLBRElement.Tag:
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        InBodyStartTagBreakrow(HtmlToken.OpenTag(HTMLBRElement.Tag));
                        break;

                    default:
                        InBodyEndTagAnythingElse(tag);
                        break;
                }
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                for (var i = 0; i < open.Count; i++)
                {
                    switch (open[i].NodeName)
                    {
                        case HTMLLIElement.DescriptionTag:
                        case HTMLLIElement.DefinitionTag:
                        case HTMLLIElement.ItemTag:
                        case HTMLParagraphElement.Tag:
                        case HTMLTableSectionElement.BodyTag:
                        case HTMLTableCellElement.HeadTag:
                        case HTMLTableSectionElement.FootTag:
                        case HTMLTableCellElement.NormalTag:
                        case HTMLTableSectionElement.HeadTag:
                        case HTMLTableRowElement.Tag:
                        case HTMLBodyElement.Tag:
                        case HTMLHtmlElement.Tag:
                            break;

                        default:
                            RaiseErrorOccurred(ErrorCode.BodyClosedWrong);
                            i = open.Count;
                            break;
                    }
                }

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
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                CloseCurrentNode();
                insert = originalInsert;
                Consume(token);
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name == HTMLScriptElement.Tag)
                {
                    PerformMicrotaskCheckpoint();
                    ProvideStableState();
                    var script = (HTMLScriptElement)CurrentNode;
                    CloseCurrentNode();
                    insert = originalInsert;
                    var oldInsertion = tokenizer.Stream.InsertionPoint;
                    nesting++;
                    //script.Prepare();
                    nesting--;

                    if (nesting == 0)
                        pause = false;

                    tokenizer.Stream.InsertionPoint = oldInsertion;

                    if (pendingParsingBlock != null)
                    {
                        if (nesting != 0)
                        {
                            pause = true;
                            return;
                        }

                        do
                        {
                            script = pendingParsingBlock;
                            pendingParsingBlock = null;
                            //TODO Do not call Tokenizer HERE
                            //TODO
                            //    3. If the parser's Document has a style sheet that is blocking scripts or the script's "ready to be parser-executed"
                            //       flag is not set: spin the event loop until the parser's Document has no style sheet that is blocking scripts and
                            //       the script's "ready to be parser-executed" flag is set.
                            //TODO From here on Tokenizer can be called again
                            oldInsertion = tokenizer.Stream.InsertionPoint;
                            nesting++;
                            //script.Execute();
                            nesting--;

                            if (nesting == 0)
                                pause = false;

                            tokenizer.Stream.ResetInsertionPoint();
                        }
                        while (pendingParsingBlock != null);
                    }
                }
                else
                {
                    CloseCurrentNode();
                    insert = originalInsert;
                }
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
                AddComment(CurrentNode, token);
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
                    case HTMLTableCaptionElement.Tag:
                        {
                            ClearStackBackToTable();
                            InsertScopeMarker();
                            var element = new HTMLTableCaptionElement();
                            AddElementToCurrentNode(element, token);
                            insert = HtmlTreeMode.InCaption;
                        }
                        break;

                    case HTMLTableColElement.ColgroupTag:
                        {
                            ClearStackBackToTable();
                            var element = new HTMLTableColElement();
                            AddElementToCurrentNode(element, token);
                            insert = HtmlTreeMode.InColumnGroup;
                        }
                        break;

                    case HTMLTableColElement.ColTag:
                        {
                            InTable(HtmlToken.OpenTag(HTMLTableColElement.ColgroupTag));
                            InColumnGroup(token);
                        }
                        break;

                    case HTMLTableSectionElement.BodyTag:
                    case HTMLTableSectionElement.HeadTag:
                    case HTMLTableSectionElement.FootTag:
                        {
                            ClearStackBackToTable();
                            var element = new HTMLTableSectionElement();
                            AddElementToCurrentNode(element, token);
                            insert = HtmlTreeMode.InTableBody;
                        }
                        break;

                    case HTMLTableCellElement.NormalTag:
                    case HTMLTableCellElement.HeadTag:
                    case HTMLTableRowElement.Tag:
                        {
                            InTable(HtmlToken.OpenTag(HTMLTableSectionElement.BodyTag));
                            InTableBody(token);
                        }
                        break;

                    case HTMLTableElement.Tag:
                        {
                            RaiseErrorOccurred(ErrorCode.TableNesting);

                            if (InTableEndTagTable())
                                Consume(token);
                        }
                        break;

                    case HTMLScriptElement.Tag:
                    case HTMLStyleElement.Tag:
                        InHead(token);
                        break;

                    case HTMLInputElement.Tag:
                        if (tag.GetAttribute("type").Equals("hidden", StringComparison.OrdinalIgnoreCase))
                        {
                            RaiseErrorOccurred(ErrorCode.InputUnexpected);
                            var element = new HTMLInputElement();
                            AddElementToCurrentNode(element, token, true);
                            CloseCurrentNode();
                        }
                        else
                        {
                            RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                            InBodyWithFoster(token);
                        }

                        break;

                    case HTMLFormElement.Tag:
                        {
                            RaiseErrorOccurred(ErrorCode.FormInappropriate);

                            if (form == null)
                            {
                                var element = new HTMLFormElement();
                                AddElementToCurrentNode(element, token);
                                form = element;
                                CloseCurrentNode();
                            }
                        }
                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.IllegalElementInTableDetected);
                        InBodyWithFoster(token);
                        break;
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case HTMLTableElement.Tag:
                        InTableEndTagTable();
                        break;

                    case HTMLBodyElement.Tag:
                    case HTMLTableColElement.ColgroupTag:
                    case HTMLTableColElement.ColTag:
                    case HTMLTableCaptionElement.Tag:
                    case HTMLHtmlElement.Tag:
                    case HTMLTableSectionElement.BodyTag:
                    case HTMLTableRowElement.Tag:
                    case HTMLTableSectionElement.HeadTag:
                    case HTMLTableCellElement.HeadTag:
                    case HTMLTableSectionElement.FootTag:
                    case HTMLTableCellElement.NormalTag:
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        break;

                    default:
                        {
                            RaiseErrorOccurred(ErrorCode.IllegalElementInTableDetected);
                            InBodyWithFoster(token);
                        }
                        break;
                }
            }
            else if (token.Type == HtmlTokenType.Character && CurrentNode != null && CurrentNode.IsTableElement())
            {
                tableCharacters.Clear();
                originalInsert = insert;
                insert = HtmlTreeMode.InTableText;
                InTableText(token);
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                if (CurrentNode != doc.DocumentElement)
                    RaiseErrorOccurred(ErrorCode.CurrentNodeIsNotRoot);

                End();
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
        void InTableText(HtmlToken token)
        {
            if (token.IsNullChar)
                RaiseErrorOccurred(ErrorCode.NULL);
            else if (token.Type == HtmlTokenType.Character)
                tableCharacters.Append(((HtmlCharacterToken)token).Data);
            else
            {
                var anyAreNotSpaceCharacters = false;

                for(var i = 0; i != tableCharacters.Length; i++)
                {
                    if (!Specification.IsSpaceCharacter(tableCharacters[i]))
                    {
                        RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                        anyAreNotSpaceCharacters = true;
                        break;
                    }
                }

                if (anyAreNotSpaceCharacters)
                {
                    for (var i = 0; i != tableCharacters.Length; i++)
                        InBodyWithFoster(HtmlToken.Character(tableCharacters[i]));
                }
                else
                {
                    for (var i = 0; i != tableCharacters.Length; i++)
                        InsertCharacter(tableCharacters[i]);

                }

                tableCharacters.Clear();
                insert = originalInsert;
                Consume(token);
            }
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
                    case HTMLTableCaptionElement.Tag:
                        InCaptionEndTagCaption();
                        break;

                    case HTMLBodyElement.Tag:
                    case HTMLTableCellElement.HeadTag:
                    case HTMLTableColElement.ColgroupTag:
                    case HTMLHtmlElement.Tag:
                    case HTMLTableSectionElement.BodyTag:
                    case HTMLTableColElement.ColTag:
                    case HTMLTableSectionElement.FootTag:
                    case HTMLTableCellElement.NormalTag:
                    case HTMLTableSectionElement.HeadTag:
                    case HTMLTableRowElement.Tag:
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        break;

                    case HTMLTableElement.Tag:
                        RaiseErrorOccurred(ErrorCode.TableNesting);

                        if (InCaptionEndTagCaption())
                            InTable(token);

                        break;

                    default:
                        InBody(token);
                        break;
                }
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case HTMLTableCaptionElement.Tag:
                    case HTMLTableColElement.ColTag:
                    case HTMLTableColElement.ColgroupTag:
                    case HTMLTableSectionElement.BodyTag:
                    case HTMLTableCellElement.NormalTag:
                    case HTMLTableSectionElement.FootTag:
                    case HTMLTableCellElement.HeadTag:
                    case HTMLTableSectionElement.HeadTag:
                    case HTMLTableRowElement.Tag: 
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
            if (token.IsIgnorable)
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.Comment)
                AddComment(CurrentNode, token);
            else if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.Type == HtmlTokenType.StartTag && (((HtmlTagToken)token).Name == HTMLHtmlElement.Tag))
                InBody(token);
            else if (token.Type == HtmlTokenType.StartTag && (((HtmlTagToken)token).Name == HTMLTableColElement.ColTag))
            {
                var element = new HTMLTableColElement();
                AddElementToCurrentNode(element, token, true);
                CloseCurrentNode();
            }
            else if (token.Type == HtmlTokenType.EndTag && (((HtmlTagToken)token).Name == HTMLTableColElement.ColgroupTag))
                InColumnGroupEndTagColgroup();
            else if (token.Type == HtmlTokenType.EndTag && (((HtmlTagToken)token).Name == HTMLTableColElement.ColTag))
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
            else if (token.Type == HtmlTokenType.EOF && CurrentNode == doc.DocumentElement)
                End();
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

                if (tag.Name == HTMLTableRowElement.Tag)
                {
                    ClearStackBackToTableSection();
                    var element = new HTMLTableRowElement();
                    AddElementToCurrentNode(element, token);
                    insert = HtmlTreeMode.InRow;
                }
                else if (tag.Name.IsTableCellElement())
                {
                    InTableBody(HtmlToken.CloseTag(HTMLTableRowElement.Tag));
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
                        ClearStackBackToTableSection();
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
                else if(tag.Name == HTMLTableElement.Tag)
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
                    ClearStackBackToTableRow();
                    var element = new HTMLTableCellElement();
                    AddElementToCurrentNode(element, token);
                    insert = HtmlTreeMode.InCell;
                    InsertScopeMarker();
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

                if(tag.Name == HTMLTableRowElement.Tag)   
                {
                    InRowEndTagTablerow();
                }
                else if (tag.Name == HTMLTableElement.Tag)
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
                {
                    InCellEndTagCell(((HtmlTagToken)token).Name);
                }
                else if (tag.Name.IsSpecialTableElement())
                {
                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);}
                else if (tag.Name.IsTableElement())
                {
                    if (IsInTableScope(tag.Name))
                    {
                        CloseTheCell();
                        Consume(token);
                    }
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.TableNotInScope);
                    }
                }
                else
                {
                    InBody(token);
                }
            }
            else if (token.Type == HtmlTokenType.StartTag && (((HtmlTagToken)token).Name.IsGeneralTableElement(true) || ((HtmlTagToken)token).Name.IsTableCellElement()))
            {
                var tag = (HtmlTagToken)token;

                if (IsInTableScope(HTMLTableCellElement.NormalTag) || IsInTableScope(HTMLTableCellElement.HeadTag))
                {
                    CloseTheCell();
                    Consume(token);
                }
                else
                {
                    RaiseErrorOccurred(ErrorCode.TableCellNotInScope);
                }
            }
            else
            {
                InBody(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.16 The "in select" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InSelect(HtmlToken token)
        {
            if (token.IsNullChar)
                RaiseErrorOccurred(ErrorCode.NULL);
            else if (token.Type == HtmlTokenType.Character)
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.Comment)
                AddComment(CurrentNode, token);
            else if (token.Type == HtmlTokenType.DOCTYPE)
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case HTMLHtmlElement.Tag:
                        InBody(token);
                        break;

                    case HTMLOptionElement.Tag:
                        {
                            if (CurrentNode.NodeName == HTMLOptionElement.Tag)
                                InSelectEndTagOption();

                            var element = new HTMLOptionElement();
                            AddElementToCurrentNode(element, token);
                        }
                        break;

                    case HTMLOptGroupElement.Tag:
                        {
                            if (CurrentNode.NodeName == HTMLOptionElement.Tag)
                                InSelectEndTagOption();

                            if (CurrentNode.NodeName == HTMLOptGroupElement.Tag)
                                InSelectEndTagOptgroup();

                            var element = new HTMLOptGroupElement();
                            AddElementToCurrentNode(element, token);
                        }
                        break;

                    case HTMLSelectElement.Tag:
                        {
                            RaiseErrorOccurred(ErrorCode.SelectNesting);
                            InSelectEndTagSelect();
                        }
                        break;

                    case HTMLInputElement.Tag:
                    case HTMLKeygenElement.Tag:
                    case HTMLTextAreaElement.Tag:
                        RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);

                        if (IsInSelectScope(HTMLSelectElement.Tag))
                        {
                            InSelectEndTagSelect();
                            Consume(token);
                        }

                        break;

                    case HTMLScriptElement.Tag:
                        InHead(token);
                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);
                        break;
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                switch (tag.Name)
                {
                    case HTMLOptGroupElement.Tag:
                        InSelectEndTagOptgroup();
                        break;

                    case HTMLOptionElement.Tag:
                        InSelectEndTagOption();
                        break;

                    case HTMLSelectElement.Tag:
                        InSelectEndTagSelect();
                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.TagCannotEndHere);
                        break;
                }
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                if (CurrentNode != doc.DocumentElement)
                    RaiseErrorOccurred(ErrorCode.CurrentNodeIsNotRoot);

                End();
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

            if (tag != null && (tag.Name.IsTableCellElement() || tag.Name.IsTableElement() || tag.Name == HTMLTableCaptionElement.Tag))
            {
                if (token.Type == HtmlTokenType.StartTag)
                {
                    RaiseErrorOccurred(ErrorCode.IllegalElementInSelectDetected);
                    InSelectEndTagSelect();
                    Consume(token);
                }
                else
                {
                    RaiseErrorOccurred(ErrorCode.TagCannotEndHere);

                    if (IsInTableScope(tag.Name))
                    {
                        InSelectEndTagSelect();
                        Consume(token);
                    }
                }
            }
            else
            {
                InSelect(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.18 The "after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterBody(HtmlToken token)
        {
            if (token.IsIgnorable)
            {
                InBody(token);
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(CurrentNode, token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if(token is HtmlTagToken && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag)
            {
                if (token.Type == HtmlTokenType.StartTag)
                    InBody(token);
                else if (fragment)
                    RaiseErrorOccurred(ErrorCode.TagInvalidInFragmentMode);
                else
                    insert = HtmlTreeMode.AfterAfterBody;
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                End();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                insert = HtmlTreeMode.InBody;
                InBody(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.19 The "in frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void InFrameset(HtmlToken token)
        {
            if (token.IsIgnorable)
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(CurrentNode, token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name == HTMLHtmlElement.Tag)
                    InBody(token);
                else if (tag.Name == HTMLFrameSetElement.Tag)
                {
                    var element = new HTMLFrameSetElement();
                    AddElementToCurrentNode(element, token);
                }
                else if (tag.Name == HTMLFrameElement.Tag)
                {
                    var element = new HTMLFrameElement();
                    AddElementToCurrentNode(element, token, true);
                    CloseCurrentNode();
                }
                else if (tag.Name == HTMLNoElement.NoFramesTag)
                    InHead(token);
                else
                    RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            }
            else if (token.Type == HtmlTokenType.EndTag && ((HtmlTagToken)token).Name == HTMLFrameSetElement.Tag)
            {
                if (CurrentNode != doc.DocumentElement)
                {
                    CloseCurrentNode();

                    if (fragment && CurrentNode.NodeName != HTMLFrameSetElement.Tag)
                        insert = HtmlTreeMode.AfterFrameset;
                }
                else
                    RaiseErrorOccurred(ErrorCode.CurrentNodeIsRoot);
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                if (CurrentNode != doc.DocumentElement)
                    RaiseErrorOccurred(ErrorCode.CurrentNodeIsNotRoot);

                End();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            }
        }

        /// <summary>
        /// See 8.2.5.4.20 The "after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterFrameset(HtmlToken token)
        {
            if (token.IsIgnorable)
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(CurrentNode, token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE)
            {
                RaiseErrorOccurred(ErrorCode.DoctypeTagInappropriate);
            }
            else if (token.Type == HtmlTokenType.StartTag)
            {
                var tag = (HtmlTagToken)token;

                if (tag.Name == HTMLHtmlElement.Tag)
                    InBody(token);
                else if (tag.Name == HTMLNoElement.NoFramesTag)
                    InHead(token);
                else
                    RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            }
            else if (token.Type == HtmlTokenType.EndTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag)
            {
                insert = HtmlTreeMode.AfterAfterFrameset;
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                End();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            }
        }

        /// <summary>
        /// See 8.2.5.4.21 The "after after body" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterAfterBody(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Comment)
                AddComment(doc, token);
            else if (token.Type == HtmlTokenType.DOCTYPE || token.IsIgnorable || (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag))
                InBody(token);
            else if (token.Type == HtmlTokenType.EOF)
                End();
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
                insert = HtmlTreeMode.InBody;
                InBody(token);
            }
        }

        /// <summary>
        /// See 8.2.5.4.22 The "after after frameset" insertion mode.
        /// </summary>
        /// <param name="token">The passed token.</param>
        void AfterAfterFrameset(HtmlToken token)
        {
            if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(doc, token);
            }
            else if (token.Type == HtmlTokenType.DOCTYPE || token.IsIgnorable || (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLHtmlElement.Tag))
            {
                InBody(token);
            }
            else if (token.Type == HtmlTokenType.StartTag && ((HtmlTagToken)token).Name == HTMLNoElement.NoFramesTag)
            {
                InHead(token);
            }
            else if (token.Type == HtmlTokenType.EOF)
            {
                End();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TokenNotPossible);
            }
        }

        #endregion

        #region Substates

        /// <summary>
        /// Closes the table if the section is in table scope.
        /// </summary>
        /// <param name="tag">The tag to insert which triggers the closing of the table.</param>
        void InTableBodyCloseTable(HtmlTagToken tag)
        {
            if (IsSectionInTableScope())
            {
                ClearStackBackToTableSection();
                CloseCurrentNode();
                insert = HtmlTreeMode.InTable;
                InTable(tag);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TableSectionNotInScope);
            }
        }

        /// <summary>
        /// Acts if a option end tag had been seen in the InSelect state.
        /// </summary>
        void InSelectEndTagOption()
        {
            if (CurrentNode.NodeName == HTMLOptionElement.Tag)
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
        bool InColumnGroupEndTagColgroup()
        {
            if (CurrentNode != doc.DocumentElement)
            {
                CloseCurrentNode();
                insert = HtmlTreeMode.InTable;
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.CurrentNodeIsRoot);
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
            AddElementToCurrentNode(element, token);
            frameset = false;
            insert = HtmlTreeMode.InBody;
        }

        /// <summary>
        /// Follows the generic rawtext parsing algorithm.
        /// </summary>
        /// <param name="tag">The given tag token.</param>
        void RawtextAlgorithm(HtmlTagToken tag)
        {
            var element = HTMLElement.Factory(tag.Name);
            AddElementToCurrentNode(element, tag);
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
            var element = HTMLElement.Factory(tag.Name);
            AddElementToCurrentNode(element, tag);
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
                if (node.NodeName == HTMLLIElement.ItemTag)
                {
                    InBody(HtmlToken.CloseTag(node.NodeName));
                    break;
                }

                if (node.IsSpecial && node.NodeName != HTMLSemanticElement.AddressTag && !(node is HTMLDivElement) && !(node is HTMLParagraphElement))
                    break;
                
                node = open[--index];
            }

            if (IsInButtonScope(HTMLParagraphElement.Tag))
                InBodyEndTagParagraph();

            var element = HTMLElement.Factory(tag.Name);
            AddElementToCurrentNode(element, tag);
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
                if (node.NodeName == HTMLLIElement.DefinitionTag || node.NodeName == HTMLLIElement.DescriptionTag)
                {
                    InBody(HtmlToken.CloseTag(node.NodeName));
                    break;
                }

                if (node.IsSpecial && node.NodeName != HTMLSemanticElement.AddressTag && !(node is HTMLDivElement) && !(node is HTMLParagraphElement))
                    break;

                node = open[--index];
            }

            if (IsInButtonScope(HTMLParagraphElement.Tag))
                InBodyEndTagParagraph();

            var element = HTMLElement.Factory(tag.Name);
            AddElementToCurrentNode(element, tag);
        }

        /// <summary>
        /// Acts if a button or similar end tag had been seen in the InBody state.
        /// </summary>
        /// <param name="tagName">The name of the block element.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        bool InBodyEndTagBlock(string tagName)
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

            while(outer < 8)
            {
                outer++;
                var index = 0;
                Element formattingElement = null;

                for (var j = formatting.Count - 1; j >= 0; j--)
                {
                    if (formatting[j] is ScopeMarkerNode)
                        break;
                    else if (formatting[j].NodeName == tag.Name)
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

                var openIndex = -1;

                for(var j = 0; j < open.Count; j++)
                {
                    if(open[j] == formattingElement)
                    {
                        openIndex = j;
                        break;
                    }
                }

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

                Element furthestBlock = null;
                var bookmark = index;

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

                var commonAncestor = open[openIndex - 1];
                var inner = 0;
                var node = furthestBlock;
                var lastNode = furthestBlock;

                while (inner < 3)
                {
                    inner++;
                    node = open[--index];

                    if (!formatting.Contains(node))
                    {
                        open.Remove(node);
                        continue;
                    }
                    else if (node == formattingElement)
                        break;

                    var newel = HTMLElement.Factory(node.NodeName);
                    newel.NodeName = node.NodeName;

                    for (int i = 0; i < node.Attributes.Length; i++)
                    {
                        var attr = node.Attributes[i];
                        newel.SetAttribute(attr.NodeName, attr.NodeValue);
                    }

                    open[index] = newel;
                    
                    for(var l = 0; l != formatting.Count; l++)
                    {
                        if(formatting[l] == node)
                        {
                            formatting[l] = newel;
                            break;
                        }
                    }

                    node = newel;

                    if (lastNode == furthestBlock)
                        bookmark++;
                    
                    node.AppendChild(lastNode);
                    lastNode = node;
                }

                if (commonAncestor.IsTableElement())
                    AddElementWithFoster(lastNode);
                else
                {
                    if (lastNode.ParentNode != null)
                        lastNode.ParentNode.RemoveChild(lastNode);

                    commonAncestor.AppendChild(lastNode);
                }

                var element = HTMLElement.Factory(formattingElement.NodeName);
                element.NodeName = formattingElement.NodeName;

                for (int i = 0; i < formattingElement.Attributes.Length; i++)
                {
                    var attr = formattingElement.Attributes[i];
                    element.SetAttribute(attr.NodeName, attr.NodeValue);
                }

                for (var j = furthestBlock.ChildNodes.Length - 1; j >= 0; j--)
                    element.AppendChild(furthestBlock.RemoveChild(furthestBlock.ChildNodes[j]));

                furthestBlock.AppendChild(element);
                formatting.Remove(formattingElement);
                formatting.Insert(bookmark, furthestBlock);
                open.Remove(formattingElement);
                open.Insert(index + 1, element);
            }
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
        bool InBodyEndTagBody()
        {
            if (IsInScope(HTMLBodyElement.Tag))
            {
                for (var i = 0; i < open.Count; i++)
                {
                    switch (open[i].NodeName)
                    {
                        case HTMLLIElement.DefinitionTag:
                        case HTMLLIElement.DescriptionTag:
                        case HTMLLIElement.ItemTag:
                        case HTMLOptGroupElement.Tag:
                        case HTMLOptionElement.Tag:
                        case HTMLParagraphElement.Tag:
                        case "rp":
                        case "rt":
                        case HTMLTableSectionElement.BodyTag:
                        case HTMLTableCellElement.NormalTag:
                        case HTMLTableSectionElement.FootTag:
                        case HTMLTableCellElement.HeadTag:
                        case HTMLTableSectionElement.HeadTag:
                        case HTMLTableRowElement.Tag:
                        case HTMLBodyElement.Tag:
                        case HTMLHtmlElement.Tag:
                            break;

                        default:
                            RaiseErrorOccurred(ErrorCode.BodyClosedWrong);
                            i = open.Count;
                            break;
                    }
                }

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
            var element = HTMLElement.Factory(tag.Name);
            AddElementToCurrentNode(element, tag);
            CloseCurrentNode();
            frameset = false;
        }

        /// <summary>
        /// Act as if an p end tag has been found in the InBody state.
        /// </summary>
        /// <returns>True if the token was found, otherwise false.</returns>
        bool InBodyEndTagParagraph()
        {
            if (IsInButtonScope(HTMLParagraphElement.Tag))
            {
                GenerateImpliedEndTagsExceptFor(HTMLParagraphElement.Tag);

                if (CurrentNode.NodeName != HTMLParagraphElement.Tag)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo(HTMLParagraphElement.Tag);
                CloseCurrentNode();
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.ParagraphNotInScope);
                InBody(HtmlToken.OpenTag(HTMLParagraphElement.Tag));
                InBodyEndTagParagraph();
                return false;
            }
        }

        /// <summary>
        /// Act as if an table end tag has been found in the InTable state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        bool InTableEndTagTable()
        {
            if (IsInTableScope(HTMLTableElement.Tag))
            {
                ClearStackBackTo(HTMLTableElement.Tag);
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
        bool InRowEndTagTablerow()
        {
            if (IsInTableScope(HTMLTableRowElement.Tag))
            {
                ClearStackBackToTableRow();
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
        bool InSelectEndTagSelect()
        {
            if (IsInSelectScope(HTMLSelectElement.Tag))
            {
                ClearStackBackTo(HTMLSelectElement.Tag);
                CloseCurrentNode();
                Reset();
                return true;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.SelectNotInScope);
                return false;
            }
        }

        /// <summary>
        /// Act as if an caption end tag has been found in the InCaption state.
        /// </summary>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        bool InCaptionEndTagCaption()
        {
            if (IsInTableScope(HTMLTableCaptionElement.Tag))
            {
                GenerateImpliedEndTags();

                if (CurrentNode.NodeName != HTMLTableCaptionElement.Tag)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo(HTMLTableCaptionElement.Tag);
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
        /// <param name="tagName">The tag name (td or th) that has been found.</param>
        /// <returns>True if the token was not ignored, otherwise false.</returns>
        bool InCellEndTagCell(string tagName)
        {
            if (IsInTableScope(tagName))
            {
                GenerateImpliedEndTags();

                if (CurrentNode.NodeName != tagName)
                    RaiseErrorOccurred(ErrorCode.TagDoesNotMatchCurrentNode);

                ClearStackBackTo(tagName);
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
            if (token.IsNullChar)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                InsertCharacter(Specification.REPLACEMENT);
            }
            else if (token.IsIgnorable)
                InsertCharacter(((HtmlCharacterToken)token).Data);
            else if (token.Type == HtmlTokenType.Character)
            {
                InsertCharacter(((HtmlCharacterToken)token).Data);
                frameset = false;
            }
            else if (token.Type == HtmlTokenType.Comment)
            {
                AddComment(CurrentNode, token);
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
                    case HTMLFormattingElement.BTag:
                    case HTMLFormattingElement.BigTag:
                    case HTMLQuoteElement.BlockTag:
                    case HTMLBodyElement.Tag:
                    case HTMLBRElement.Tag:
                    case HTMLSemanticElement.CenterTag:
                    case HTMLFormattingElement.CodeTag:
                    case HTMLLIElement.DefinitionTag:
                    case HTMLDivElement.Tag:
                    case HTMLDListElement.Tag:
                    case HTMLLIElement.DescriptionTag:
                    case HTMLFormattingElement.EmTag:
                    case HTMLEmbedElement.Tag:
                    case HTMLHeadingElement.ChapterTag:
                    case HTMLHeadingElement.SubSubSubSubSectionTag:
                    case HTMLHeadingElement.SubSubSubSectionTag:
                    case HTMLHeadingElement.SubSubSectionTag:
                    case HTMLHeadingElement.SubSectionTag:
                    case HTMLHeadingElement.SectionTag:
                    case HTMLHeadElement.Tag:
                    case HTMLHRElement.Tag:
                    case HTMLFormattingElement.ITag:
                    case HTMLImageElement.Tag:
                    case HTMLLIElement.ItemTag:
                    case HTMLSemanticElement.ListingTag:
                    case HTMLSemanticElement.MainTag:
                    case HTMLMenuElement.Tag:
                    case HTMLMetaElement.Tag:
                    case HTMLFormattingElement.NobrTag:
                    case HTMLOListElement.Tag:
                    case HTMLParagraphElement.Tag:
                    case HTMLPreElement.Tag:
                    case "ruby":
                    case HTMLFormattingElement.STag:
                    case HTMLFormattingElement.SmallTag:
                    case HTMLSpanElement.Tag:
                    case HTMLFormattingElement.StrongTag:
                    case HTMLFormattingElement.StrikeTag:
                    case "sub":
                    case "sup":
                    case HTMLTableElement.Tag:
                    case HTMLFormattingElement.TtTag:
                    case HTMLFormattingElement.UTag:
                    case HTMLUListElement.Tag:
                    case "var":
                        {
                            RaiseErrorOccurred(ErrorCode.TagCannotStartHere);
                            CloseCurrentNode();

                            while (!CurrentNode.IsHtmlTIP && !CurrentNode.IsMathMLTIP && !CurrentNode.IsInHtml)
                                CloseCurrentNode();

                            Consume(token);
                        }
                        break;

                    case HTMLFontElement.Tag:
                        for (var i = 0; i != tag.Attributes.Count; i++)
                        {
                            if (tag.Attributes[i].Key == "color" || tag.Attributes[i].Key == "face" || tag.Attributes[i].Key == "size")
                                goto case "var";
                        }

                        goto default;

                    default:
                        {
                            Element node;

                            if (CurrentNode.IsInMathML)
                            {
                                node = new MathMLElement();
                                node.NodeName = tag.Name;

                                for (int i = 0; i < tag.Attributes.Count; i++)
                                {
                                    var name = tag.Attributes[i].Key;
                                    var value = tag.Attributes[i].Value;
                                    node.SetAttribute(ForeignHelpers.AdjustAttributeName(MathMLHelpers.AdjustAttributeName(name)), value);
                                }
                            }
                            else
                            {
                                node = new SVGElement();
                                node.NodeName = SVGHelpers.AdjustTagName(tag.Name);

                                for (int i = 0; i < tag.Attributes.Count; i++)
                                {
                                    var name = tag.Attributes[i].Key;
                                    var value = tag.Attributes[i].Value;
                                    node.SetAttribute(ForeignHelpers.AdjustAttributeName(SVGHelpers.AdjustAttributeName(name)), value);
                                }
                            }

                            node.NamespaceURI = CurrentNode.NamespaceURI;
                            CurrentNode.AppendChild(node);
                            open.Add(node);

                            if (!tag.IsSelfClosing)
                                tokenizer.AcceptsCDATA = true;
                            else if (tag.Name == HTMLScriptElement.Tag)
                                Foreign(HtmlToken.CloseTag(HTMLScriptElement.Tag));
                        }

                        break;
                }
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                var tag = (HtmlTagToken)token;

                if (CurrentNode != null && CurrentNode is HTMLScriptElement && tag.Name == HTMLScriptElement.Tag)
                {
                    CloseCurrentNode();
                    var oldInsert = tokenizer.Stream.InsertionPoint;
                    nesting++;
                    pause = true;
                    InSvg(tag);
                    nesting--;

                    if (nesting == 0)
                        pause = false;

                    tokenizer.Stream.InsertionPoint = oldInsert;
                }
                else
                {
                    var node = CurrentNode;

                    if (node.NodeName != tag.Name)
                        RaiseErrorOccurred(ErrorCode.TagClosingMismatch);

                    while (open.Count > 0)
                    {
                        open.RemoveAt(open.Count - 1);

                        if (node.NodeName.ToLower() == tag.Name)
                            break;

                        node = CurrentNode;

                        if (node == null || node.IsInHtml)
                            break;
                    }

                    Reset();
                    Consume(token);
                }
            }
        }

        /// <summary>
        /// Processes the element according to the SVG rules.
        /// </summary>
        /// <param name="tag">The tag to process.</param>
        void InSvg(HtmlTagToken tag)
        {
            //TODO
            //Process the script element according to the SVG rules, if the user agent supports SVG. [SVG]
        }

        #endregion

        #region Scope

        /// <summary>
        /// Determines if one of the tag names (h1, h2, h3, h4, h5, h6) is in the global scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        bool IsHeadingInScope()
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node is HTMLHeadingElement)
                    return true;

                if (node.IsInHtml)
                {
                    switch (node.NodeName)
                    {
                        case HTMLMarqueeElement.Tag:
                        case HTMLObjectElement.Tag:
                        case HTMLTableCellElement.HeadTag:
                        case HTMLTableCellElement.NormalTag:
                        case HTMLHtmlElement.Tag:
                        case HTMLTableElement.Tag:
                        case HTMLTableCaptionElement.Tag:
                        case HTMLAppletElement.Tag:
                            return false;
                    }
                }
                else if (node.IsInSvg)
                {
                    if (node.IsSpecial)
                        return false;
                }
                else if (node.IsInMathML)
                {
                    if (node.IsSpecial)
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the global scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        bool IsInScope(string tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;

                if (node.IsInHtml)
                {
                    switch (node.NodeName)
                    {
                        case HTMLMarqueeElement.Tag:
                        case HTMLObjectElement.Tag:
                        case HTMLTableCellElement.HeadTag:
                        case HTMLTableCellElement.NormalTag:
                        case HTMLHtmlElement.Tag:
                        case HTMLTableElement.Tag:
                        case HTMLTableCaptionElement.Tag:
                        case HTMLAppletElement.Tag:
                            return false;
                    }
                }
                else if (node.IsInSvg)
                {
                    if (node.IsSpecial)
                        return false;
                }
                else if (node.IsInMathML)
                {
                    if(node.IsSpecial)
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the list scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        bool IsInListItemScope(string tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;

                if (node is HTMLUListElement || node is HTMLOListElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the button scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        bool IsInButtonScope(string tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;

                if (node is HTMLButtonElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if one of the tag names (tbody, tfoot, thead) is in the table scope.
        /// </summary>
        /// <returns>True if it is in scope, otherwise false.</returns>
        bool IsSectionInTableScope()
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node is HTMLTableSectionElement || node is HTMLTableSectionElement || node is HTMLTableSectionElement)
                    return true;

                if (node is HTMLHtmlElement || node is HTMLTableElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the table scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        bool IsInTableScope(string tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;
                
                if (node is HTMLHtmlElement || node is HTMLTableElement)
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if the given tag name is in the select scope.
        /// </summary>
        /// <param name="tagName">The tag name to check.</param>
        /// <returns>True if it is in scope, otherwise false.</returns>
        bool IsInSelectScope(string tagName)
        {
            for (int i = open.Count - 1; i >= 0; i--)
            {
                var node = open[i];

                if (node.NodeName == tagName)
                    return true;

                if (node is HTMLOptGroupElement || node is HTMLOptionElement)
                    return false;
            }

            return false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Resolves the encoding from the given charset and sets it.
        /// </summary>
        /// <param name="charset">The charset string.</param>
        void SetCharset(String charset)
        {
            var enc = HtmlEncoding.Resolve(charset);

            if (enc != null)
            {
                doc.InputEncoding = enc.WebName;
                tokenizer.Stream.Encoding = enc;
            }
        }

        void PerformMicrotaskCheckpoint()
        {
            //TODO
            //IF RUNNING MUTATION OBSERVERS == false
            //1. Let the running mutation observers flag be true.
            //2. Sort the tables with pending sorts.
            //3. Invoke MutationObserver objects for the unit of related similar-origin browsing contexts to which the script's browsing context belongs.
            //   ( Note: This will typically invoke scripted callbacks, which calls the jump to a code entry-point algorithm, which calls this perform a )
            //   ( microtask checkpoint algorithm again, which is why we use the running mutation observers flag to avoid reentrancy.                    )
            //4. Let the running mutation observers flag be false.
        }

        void ProvideStableState()
        {
            //When the user agent is to provide a stable state, if any asynchronously-running algorithms are awaiting a stable state, then
            //the user agent must run their synchronous section and then resume running their asynchronous algorithm (if appropriate).
        }

        /// <summary>
        /// 8.2.6 The end.
        /// </summary>
        void End()
        {
            doc.ReadyState = Readiness.Interactive;

            while (open.Count != 0)
                CloseCurrentNode();

            if (doc.ScriptsWaiting != 0)
            {
                //3.1 Spin the event loop until the first script in the list of scripts that will execute when the document has finished parsing
                //    has its "ready to be parser-executed" flag set and the parser's Document has no style sheet that is blocking scripts.
                //3.2 Execute the first script in the list of scripts that will execute when the document has finished parsing.
                //3.3 Remove the first script element from the list of scripts that will execute when the document has finished parsing (i.e. shift out the first entry in the list).
                //3.4 If the list of scripts that will execute when the document has finished parsing is still not empty, repeat these substeps again from substep 1.
            }

            doc.QueueTask(doc.RaiseDomContentLoaded);

            while (doc.ScriptsAsSoonAsPossible != 0)
                doc.SpinEventLoop();

            while (doc.IsLoadingDelayed)
                doc.SpinEventLoop();

            doc.QueueTask(() =>
            {
                doc.ReadyState = Readiness.Complete;
                //7.2 If the Document is in a browsing context, fire a simple event named load at the Document's Window object, but with its target set to the Document object
                //    (and the currentTarget set to the Window object).
            });

            if (doc.IsInBrowsingContext)
            {
                //8. If the Document is in a browsing context, then queue a task to run the following substeps:
                //8.1 If the Document's page showing flag is true, then abort this task (i.e. don't fire the event below).
                //8.2 Set the Document's page showing flag to true.
                //8.3 Fire a trusted event with the name pageshow at the Window object of the Document, but with its target set to the Document object (and the currentTarget set
                //    to the Window object), using the PageTransitionEvent interface, with the persisted attribute initialized to false. This event must not bubble, must not be
                //    cancelable, and has no default action.
            }

            //9. If the Document has any pending application cache download process tasks, then queue each such task in the order they were added to the list of pending
            //   application cache download process tasks, and then empty the list of pending application cache download process tasks. The task source for these tasks is
            //   the networking task source.

            if (doc.IsToBePrinted)
                doc.Print();

            //11. The Document is now ready for post-load tasks.
            doc.QueueTask(doc.RaiseLoadedEvent);

            if (IsAsync) tcs.SetResult(true);
        }

        #endregion

        #region Appending Nodes

        /// <summary>
        /// Appends the doctype token to the document.
        /// </summary>
        /// <param name="doctypeToken">The doctypen token.</param>
        void AddElement(HtmlDoctypeToken doctypeToken)
        {
            var node = new DocumentType();
            node.SystemId = doctypeToken.SystemIdentifier;
            node.PublicId = doctypeToken.PublicIdentifier;
            node.Name = doctypeToken.Name;
            doc.AppendChild(node);
        }

        /// <summary>
        /// Appends a comment node to the specified node.
        /// </summary>
        /// <param name="parent">The node which will contain the comment node.</param>
        /// <param name="commentToken">The comment token.</param>
        void AddComment(Node parent, HtmlToken commentToken)
        {
            var tag = (HtmlCommentToken)commentToken;
            var comment = new Comment();
            comment.Data = tag.Data;
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
                tokenizer.AcceptsCDATA = CurrentNode == null || !CurrentNode.IsInHtml;
            }
        }

        /// <summary>
        /// Appends a node to the current node and
        /// modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="elementToken">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        void AddElementToCurrentNode(Element element, HtmlToken elementToken, bool acknowledgeSelfClosing = false)
        {
            SetupElement(element, elementToken, acknowledgeSelfClosing);

            if (foster && CurrentNode.IsTableElement())
                AddElementWithFoster(element);
            else
                CurrentNode.AppendChild(element);

            open.Add(element);
            tokenizer.AcceptsCDATA = !element.IsInHtml;
        }

        /// <summary>
        /// Appends a node to the appropriate foster parent.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        void AddElementWithFoster(Element element)
        {
            var table = false;
            var index = open.Count;

            while (--index != 0)
            {
                if (open[index] is HTMLTableElement)
                {
                    table = true;
                    break;
                }
            }

            var foster = open[index].ParentNode ?? open[index + 1];

            if (table && open[index].ParentNode != null)
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
        /// Appends a node to the document and modifies the node by appending
        /// all attributes and acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="elementToken">The associated tag token.</param>
        void AddElementToDocument(Element element, HtmlToken elementToken)
        {
            doc.AppendChild(element);
            SetupElement(element, elementToken, false);
            open.Add(element);
            tokenizer.AcceptsCDATA = !element.IsInHtml;
        }

        /// <summary>
        /// Modifies the node by appending all attributes and
        /// acknowledging the self-closing flag if set.
        /// </summary>
        /// <param name="element">The node which will be added to the list.</param>
        /// <param name="elementToken">The associated tag token.</param>
        /// <param name="acknowledgeSelfClosing">Should the self-closing be acknowledged?</param>
        void SetupElement(Element element, HtmlToken elementToken, bool acknowledgeSelfClosing)
        {
            var tag = (HtmlTagToken)elementToken;
            element.NodeName = tag.Name;

            if (tag.IsSelfClosing && !acknowledgeSelfClosing)
                RaiseErrorOccurred(ErrorCode.TagCannotBeSelfClosed);

            AddAttributesToElement(tag, element);
        }

        /// <summary>
        /// Appends the attributes of the given tag token to the given node.
        /// </summary>
        /// <param name="elementToken">The tag token which carries the modifications.</param>
        /// <param name="element">The node which should be modified.</param>
        void AddAttributesToElement(HtmlTagToken elementToken, Element element)
        {
            for (var i = 0; i < elementToken.Attributes.Count; i++)
                element.SetAttribute(elementToken.Attributes[i].Key, elementToken.Attributes[i].Value);
        }

        /// <summary>
        /// Checks for each attribute on the token if the attribute is already present on the node.
        /// If it is not, the attribute and its corresponding value is added to the node.
        /// </summary>
        /// <param name="elementToken">The token with the source attributes.</param>
        /// <param name="element">The node with the target attributes.</param>
        void AppendAttributesToElement(HtmlTagToken elementToken, Element element)
        {
            foreach (var attr in elementToken.Attributes)
            {
                if (!element.HasAttribute(attr.Key))
                    element.SetAttribute(attr.Key, attr.Value);
            }
        }

        /// <summary>
        /// Inserts the given character into the current node.
        /// </summary>
        /// <param name="p">The character to insert.</param>
        void InsertCharacter(Char p)
        {
            if (foster && CurrentNode.IsTableElement())
                InsertCharacterWithFoster(p);
            else
                CurrentNode.AppendText(p);
        }

        /// <summary>
        /// Inserts the given characters into the current node.
        /// </summary>
        /// <param name="p">The characters to insert.</param>
        void InsertCharacters(String p)
        {
            if (foster && CurrentNode.IsTableElement())
            {
                for (int i = 0; i < p.Length; i++)
                    InsertCharacterWithFoster(p[i]);
            }
            else
                CurrentNode.AppendText(p);
        }

        /// <summary>
        /// Inserts the given character into the foster parent.
        /// </summary>
        /// <param name="p">The character to insert.</param>
        void InsertCharacterWithFoster(Char p)
        {
            var table = false;
            var index = open.Count;

            while (--index != 0)
            {
                if (open[index].NodeName == HTMLTableElement.Tag)
                {
                    table = true;
                    break;
                }
            }

            var foster = open[index].ParentNode ?? open[index + 1];

            if (table && open[index].ParentNode != null)
            {
                for (int i = 0; i < foster.ChildNodes.Length; i++)
                {
                    if (foster.ChildNodes[i] == open[index])
                    {
                        foster.InsertText(i, p);
                        break;
                    }
                }
            }
            else
                foster.AppendText(p);
        }

        /// <summary>
        /// Inserts a scope marker at the end of the list of active formatting elements.
        /// </summary>
        void InsertScopeMarker()
        {
            formatting.Add(ScopeMarkerNode.Element);
        }

        #endregion

        #region Closing Nodes

        /// <summary>
        /// Closes the currently open cell (td or th).
        /// </summary>
        void CloseTheCell()
        {
            if (IsInTableScope(HTMLTableCellElement.NormalTag))
                InCellEndTagCell(HTMLTableCellElement.NormalTag);
            else
                InCellEndTagCell(HTMLTableCellElement.HeadTag);
        }

        /// <summary>
        /// Clears the stack of open elements back to the given element name.
        /// </summary>
        /// <param name="tagName">The tag that will be the CurrentNode.</param>
        void ClearStackBackTo(string tagName)
        {
            while (CurrentNode.NodeName != tagName && !(CurrentNode is HTMLHtmlElement))
                CloseCurrentNode();
        }

        /// <summary>
        /// Clears the stack of open elements back to any heading element.
        /// </summary>
        void ClearStackBackToHeading()
        {
            while (!(CurrentNode is HTMLHeadingElement) && !(CurrentNode is HTMLHtmlElement))
                CloseCurrentNode();
        }

        /// <summary>
        /// Clears the stack of open elements back to any table section element.
        /// </summary>
        void ClearStackBackToTableSection()
        {
            while (!(CurrentNode is HTMLTableSectionElement) && !(CurrentNode is HTMLHtmlElement))
                CloseCurrentNode();
        }

        /// <summary>
        /// Clears the stack of open elements back to a table element.
        /// </summary>
        void ClearStackBackToTable()
        {
            while (!(CurrentNode is HTMLTableElement) && !(CurrentNode is HTMLHtmlElement))
                CloseCurrentNode();
        }

        /// <summary>
        /// Clears the stack of open elements back to a tr element.
        /// </summary>
        void ClearStackBackToTableRow()
        {
            while (!(CurrentNode is HTMLTableRowElement) && !(CurrentNode is HTMLHtmlElement))
                CloseCurrentNode();
        }

        /// <summary>
        /// Generates the implied end tags for the dd, dt, li, option, optgroup, p, rp, rt elements except for
        /// the tag given.
        /// </summary>
        /// <param name="tagName">The tag that will be excluded.</param>
        void GenerateImpliedEndTagsExceptFor(string tagName)
        {
            var list = new List<string>(new[] { HTMLLIElement.DefinitionTag, HTMLLIElement.DescriptionTag, 
                HTMLLIElement.ItemTag, HTMLOptGroupElement.Tag, HTMLOptionElement.Tag, HTMLParagraphElement.Tag, "rp", "rt" });

            if (list.Contains(tagName))
                list.Remove(tagName);

            while(list.Contains(CurrentNode.NodeName))
                CloseCurrentNode();
        }

        /// <summary>
        /// Generates the implied end tags for the dd, dt, li, option, optgroup, p, rp, rt elements.
        /// </summary>
        void GenerateImpliedEndTags()
        {
            while (CurrentNode is HTMLLIElement || CurrentNode is HTMLOptionElement || CurrentNode is HTMLOptGroupElement || 
                CurrentNode is HTMLParagraphElement || CurrentNode.NodeName == "rp" || CurrentNode.NodeName == "rt")
                CloseCurrentNode();
        }

        #endregion

        #region Formatting

        /// <summary>
        /// Adds an element to the list of active formatting elements.
        /// </summary>
        /// <param name="element">The element to add.</param>
        void AddFormattingElement(Element element)
        {
            var count = 0;

            for (var i = formatting.Count - 1; i >= 0; i--)
            {
                if (formatting[i] is ScopeMarkerNode)
                    break;

                if (formatting[i].NodeName == element.NodeName && formatting[i].Attributes.Equals(element.Attributes) && formatting[i].NamespaceURI == element.NamespaceURI)
                {
                    count++;

                    if (count == 3)
                    {
                        formatting.RemoveAt(i);
                        break;
                    }
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
                var entry = formatting[formatting.Count - 1];
                formatting.Remove(entry);

                if (entry is ScopeMarkerNode)
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

            if (entry is ScopeMarkerNode || open.Contains(entry))
                return;

            while (index > 0)
            {
                entry = formatting[--index];

                if (entry is ScopeMarkerNode || open.Contains(entry))
                {
                    index++;
                    break;
                }
            }

            for (; index < formatting.Count; index++)
            {
                entry = formatting[index];
                var element = HTMLElement.Factory(entry.NodeName);

                AddElementToCurrentNode(element, HtmlToken.OpenTag(entry.NodeName));

                for (int i = 0; i < entry.Attributes.Length; i++)
                {
                    var attr = entry.Attributes[i];
                    element.SetAttribute(attr.NodeName, attr.NodeValue);
                }

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
            if (ErrorOccurred != null)
            {
                var pck = new ParseErrorEventArgs((int)code, Errors.GetError(code));
                pck.Line = tokenizer.Stream.Line;
                pck.Column = tokenizer.Stream.Column;
                ErrorOccurred(this, pck);
            }
        }

        #endregion
    }
}
