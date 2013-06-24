using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Samples.ViewModels
{
    public class RendererViewModel : RequestViewModel
    {
        FlowDocument root;
        Paragraph buffer;
        Uri url;

        FontStyle currentFontStyle;
        FontWeight currentFontWeight;
        Boolean currentUnderline;
        Boolean currentStrike;
        Boolean isHyperlink;

        public RendererViewModel()
        {
            root = new FlowDocument();
        }

        public FlowDocument Root
        {
            get { return root; }
        }

        protected override async Task Use(Uri url, HTMLDocument document, CancellationToken cancel)
        {
            this.url = url;
            Status = "Rendering ...";

            Reset();
            root.Blocks.Add(RenderBox(document.Body));

            await Task.Yield();
        }

        void Reset()
        {
            root.Blocks.Clear();
            currentFontStyle = FontStyles.Normal;
            currentFontWeight = FontWeights.Normal;
            currentUnderline = false;
            currentStrike = false;
            isHyperlink = false;
        }

        //
        // A good overview over the various classes for the
        // WPF flowdocument can be found here:
        //
        // http://msdn.microsoft.com/en-us/library/aa970909.aspx
        //

        Section RenderBox(Element element)
        {
            var box = new Section();
            Render(element, box);
            FlushBuffer(box.Blocks);
            return box;
        }

        void Render(Element element, Section box)
        {
            foreach (var child in element.ChildNodes)
            {
                switch (child.NodeType)
                {
                    case NodeType.Text:
                        FillBuffer((TextNode)child);
                        break;

                    case NodeType.Element:
                        var node = (Element)child;

                        switch (node.TagName)
                        {
                            case "script":
                            case "style":
                                break;

                            case "img":
                                FillBuffer(Render((HTMLImageElement)node));
                                break;

                            case "b":
                            case "strong":
                                {
                                    var previous = currentFontWeight;
                                    currentFontWeight = FontWeights.Bold;
                                    Render(node, box);
                                    currentFontWeight = previous;
                                }
                                break;

                            case "i":
                                {
                                    var previous = currentFontStyle;
                                    currentFontStyle = FontStyles.Italic;
                                    Render(node, box);
                                    currentFontStyle = previous;
                                }
                                break;

                            case "u":
                                {
                                    var previous = currentUnderline;
                                    currentUnderline = true;
                                    Render(node, box);
                                    currentUnderline = previous;
                                }
                                break;

                            case "strike":
                                {
                                    var previous = currentStrike;
                                    currentStrike = true;
                                    Render(node, box);
                                    currentStrike = previous;
                                }
                                break;

                            case "br":
                                FillBuffer(new LineBreak());
                                break;

                            case "a":
                                {
                                    var previous = isHyperlink;
                                    isHyperlink = true;
                                    Render(node, box);
                                    isHyperlink = previous;
                                }
                                break;

                            case "ul":
                                FlushBuffer(box.Blocks);
                                box.Blocks.Add(Render((HTMLUListElement)node));
                                break;

                            case "ol":
                                FlushBuffer(box.Blocks);
                                box.Blocks.Add(Render((HTMLOListElement)node));
                                break;

                            case "dl":
                                FlushBuffer(box.Blocks);
                                box.Blocks.Add(Render((HTMLDListElement)node));
                                break;

                            case "p":
                                FlushBuffer(box.Blocks);
                                Render(node, box);
                                break;

                            case "div":
                                FlushBuffer(box.Blocks);
                                box.Blocks.Add(RenderBox(node));
                                break;

                            default:
                                Render(node, box);
                                break;
                        }

                        break;
                }
            }
        }

        Inline Render(HTMLImageElement element)
        {
            var f = new Figure();
            f.FlowDirection = FlowDirection.LeftToRight;
            var container = new BlockUIContainer();
            var img = new Image();
            img.Stretch = Stretch.None;
            var src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(url, Sanitize(element.Src));
            src.EndInit();
            f.Blocks.Add(container);
            container.Child = img;
            img.Source = src;
            return f;
        }

        List Render(HTMLOListElement element)
        {
            var list = new List { MarkerStyle = TextMarkerStyle.Decimal };
            RenderList(element.Children, list);
            return list;
        }

        List Render(HTMLUListElement element)
        {
            var list = new List { MarkerStyle = TextMarkerStyle.Circle };
            RenderList(element.Children, list);
            return list;
        }

        List Render(HTMLDListElement element)
        {
            var list = new List { MarkerStyle = TextMarkerStyle.None };
            RenderList(element.Children, list);
            return list;
        }

        void RenderList(HTMLCollection children, List list)
        {
            foreach (var child in children)
            {
                if (child.TagName == "li")
                {
                    var li = new ListItem();
                    var section = new Section();
                    li.Blocks.Add(section);
                    Render(child, section);
                    list.ListItems.Add(li);
                }
            }
        }

        Table Render(HTMLTableElement element)
        {
            var table = new Table();
            return table;
        }

        void FillBuffer(Inline inline)
        {
            if (buffer == null)
                buffer = new Paragraph();

            inline.FontWeight = currentFontWeight;
            inline.FontStyle = currentFontStyle;
            if (currentUnderline) inline.TextDecorations.Add(TextDecorations.Underline);
            if (currentStrike) inline.TextDecorations.Add(TextDecorations.Strikethrough);
            buffer.Inlines.Add(inline);
        }

        void FillBuffer(TextNode element)
        {
            var s = Normalize(element.Data);

            if (!String.IsNullOrEmpty(s) && s != " ")
                FillBuffer(new Run(s));
        }

        void FlushBuffer(BlockCollection blocks)
        {
            if (buffer != null)
            {
                if (buffer.Inlines.Count > 0)
                    blocks.Add(buffer);

                buffer = null;
            }
        }

        static String Normalize(String input)
        {
            var split = input.Split(ws, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(" ", split);
        }
    }
}
