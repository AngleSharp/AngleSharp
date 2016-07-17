namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
#if !NET40
    using System.Runtime.CompilerServices;
#endif

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    static class HtmlParserExtensions
    {
        public static void SetAttributes(this Element element, List<KeyValuePair<String, String>> attributes)
        {
            var container = element.Attributes;

            for (var i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var item = new Attr(attribute.Key, attribute.Value);
                container.FastAddItem(item);
            }
        }

        public static HtmlTreeMode? SelectMode(this Element element, Boolean isLast, Stack<HtmlTreeMode> templateModes)
        {
            var tagName = element.LocalName;

            if (tagName.Is(TagNames.Select))
            {
                return HtmlTreeMode.InSelect;
            }
            else if (TagNames.AllTableCells.Contains(tagName))
            {
                return isLast ? HtmlTreeMode.InBody : HtmlTreeMode.InCell;
            }
            else if (tagName.Is(TagNames.Tr))
            {
                return HtmlTreeMode.InRow;
            }
            else if (TagNames.AllTableSections.Contains(tagName))
            {
                return HtmlTreeMode.InTableBody;
            }
            else if (tagName.Is(TagNames.Body))
            {
                return HtmlTreeMode.InBody;
            }
            else if (tagName.Is(TagNames.Table))
            {
                return HtmlTreeMode.InTable;
            }
            else if (tagName.Is(TagNames.Caption))
            {
                return HtmlTreeMode.InCaption;
            }
            else if (tagName.Is(TagNames.Colgroup))
            {
                return HtmlTreeMode.InColumnGroup;
            }
            else if (tagName.Is(TagNames.Template))
            {
                return templateModes.Peek();
            }
            else if (tagName.Is(TagNames.Html))
            {
                return HtmlTreeMode.BeforeHead;
            }
            else if (tagName.Is(TagNames.Head))
            {
                return isLast ? HtmlTreeMode.InBody : HtmlTreeMode.InHead;
            }
            else if (tagName.Is(TagNames.Frameset))
            {
                return HtmlTreeMode.InFrameset;
            }
            else if (isLast)
            {
                return HtmlTreeMode.InBody;
            }

            return null;
        }

        public static Int32 GetCode(this HtmlParseError code)
        {
            return (Int32)code;
        }

        public static void SetUniqueAttributes(this Element element, List<KeyValuePair<String, String>> attributes)
        {
            for (int i = attributes.Count - 1; i >= 0; i--)
            {
                if (element.HasAttribute(attributes[i].Key))
                {
                    attributes.RemoveAt(i);
                }
            }

            element.SetAttributes(attributes);
        }

        public static void AddFormatting(this List<Element> formatting, Element element)
        {
            var count = 0;

            for (var i = formatting.Count - 1; i >= 0; i--)
            {
                var format = formatting[i];

                if (format == null)
                {
                    break;
                }

                if (format.NodeName.Is(element.NodeName) && 
                    format.NamespaceUri.Is(element.NamespaceUri) && 
                    format.Attributes.AreEqual(element.Attributes) && ++count == 3)
                {
                    formatting.RemoveAt(i);
                    break;
                }
            }

            formatting.Add(element);
        }

        public static void ClearFormatting(this List<Element> formatting)
        {
            while (formatting.Count != 0)
            {
                var index = formatting.Count - 1;
                var entry = formatting[index];
                formatting.RemoveAt(index);

                if (entry == null)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddScopeMarker(this List<Element> formatting)
        {
            formatting.Add(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Node parent, HtmlToken token)
        {
            parent.AddNode(new Comment(parent.Owner, token.Data));
        }

        public static QuirksMode GetQuirksMode(this HtmlDoctypeToken doctype)
        {
            if (doctype.IsFullQuirks)
            {
                return QuirksMode.On;
            }
            else if (doctype.IsLimitedQuirks)
            {
                return QuirksMode.Limited;
            }

            return QuirksMode.Off;
        }
    }
}
