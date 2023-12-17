namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Tokens.Struct;

    /// <summary>
    /// Extensions to be used exclusively by the DOM Builder.
    /// </summary>
    static class HtmlDomBuilderExtensions
    {
        public static void SetAttributes(this Element element, IReadOnlyList<HtmlAttributeToken> attributes)
        {
            var container = element.Attributes;

            for (var i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var item = new Attr(attribute.Name, attribute.Value);
                item.Container = container;
                container.FastAddItem(item);
            }
        }

        public static void AddAttribute(this Element element, HtmlAttributeToken attribute)
        {
            var container = element.Attributes;
            var item = new Attr(attribute.Name, attribute.Value);
            item.Container = container;
            container.FastAddItem(item);
        }

        public static HtmlTreeMode? SelectMode(this Element element, Boolean isLast, Stack<HtmlTreeMode> templateModes)
        {
            if (element.Flags.HasFlag(NodeFlags.HtmlMember))
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
                else if (tagName.Is(TagNames.Template) && templateModes.Count > 0)
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
            }

            if (isLast)
            {
                return HtmlTreeMode.InBody;
            }
            else
            {
                return null;
            }
        }

        public static Int32 GetCode(this HtmlParseError code)
        {
            return (Int32)code;
        }

        public static void SetUniqueAttributes(this Element element, HtmlTagToken token)
        {
            for (var i = token.Attributes.Count - 1; i >= 0; i--)
            {
                if (element.HasAttribute(token.Attributes[i].Name))
                {
                    token.RemoveAttributeAt(i);
                }
            }

            element.SetAttributes(token.Attributes);
        }

        public static void SetUniqueAttributes(this Element element, StructHtmlToken token)
        {
            for (var i = token.Attributes.Count - 1; i >= 0; i--)
            {
                if (element.HasAttribute(token.Attributes[i].Name.String))
                {
                    token.RemoveAttributeAt(i);
                }
            }

            element.SetAttributes(token.Attributes);
        }

        public static void AddFormatting(this List<Element> formatting, Element element)
        {
            var count = 0;

            for (var i = formatting.Count - 1; i >= 0; i--)
            {
                var format = formatting[i];

                if (format is null)
                {
                    break;
                }

                if (format.NodeName.Is(element.NodeName) &&
                    format.NamespaceUri.Is(element.NamespaceUri) &&
                    format.Attributes.SameAs(element.Attributes) && ++count == 3)
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

                if (entry is null)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddScopeMarker(this List<Element> formatting)
        {
            formatting.Add(null!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Element parent, HtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent.Owner, token.Data)
                : new Comment(parent.Owner, token.Data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Element parent, StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent.Owner, token.Data.String)
                : new Comment(parent.Owner, token.Data.String));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Document parent, HtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent, token.Data)
                : new Comment(parent, token.Data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Document parent, StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent, token.Data.String)
                : new Comment(parent, token.Data.String));
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

    /// <summary>
    /// Extensions to be used exclusively by the DOM Builder.
    /// </summary>
    static class StructHtmlDomBuilderExtensions
    {
        public static void SetAttributes(this Element element, StructAttributes attributes)
        {
            var container = element.Attributes;

            for (var i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var item = new Attr(attribute.Name.String, attribute.Value.String);
                item.Container = container;
                container.FastAddItem(item);
            }
        }

        public static void AddAttribute(this Element element, ref MemoryHtmlAttributeToken attribute)
        {
            var container = element.Attributes;
            var item = new Attr(attribute.Name.String, attribute.Value.String);
            item.Container = container;
            container.FastAddItem(item);
        }

        public static void SetUniqueAttributes(this Element element, ref StructHtmlToken token)
        {
            for (var i = token.Attributes.Count - 1; i >= 0; i--)
            {
                if (element.HasAttribute(token.Attributes[i].Name.String))
                {
                    token.RemoveAttributeAt(i);
                }
            }

            element.SetAttributes(token.Attributes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Element parent, ref StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent.Owner, token.Data.String)
                : new Comment(parent.Owner, token.Data.String));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Document parent, ref StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent, token.Data.String)
                : new Comment(parent, token.Data.String));
        }

        public static QuirksMode GetQuirksMode(this ref StructHtmlToken doctype)
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
