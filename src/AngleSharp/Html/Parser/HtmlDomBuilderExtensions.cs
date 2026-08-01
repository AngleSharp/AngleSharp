namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Common;
    using Construction;
    using Tokens.Struct;

    /// <summary>
    /// Extensions to be used exclusively by the DOM Builder.
    /// </summary>
    static class HtmlDomBuilderExtensions
    {
        public static HtmlTreeMode? SelectMode<TNode>(this TNode element, Boolean isLast, Stack<HtmlTreeMode> templateModes)
            where TNode : struct, IHtmlTreeConstructionNode<TNode>
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

        public static void SetUniqueAttributes<TNode>(this TNode element, ref StructHtmlToken token)
            where TNode : struct, IHtmlTreeConstructionNode<TNode>
        {
            ref readonly var attributes = ref StructHtmlToken.GetAttributesReference(ref token);
            for (var i = attributes.Count - 1; i >= 0; i--)
            {
                if (element.HasAttribute(attributes[i].Name))
                {
                    token.RemoveAttributeAt(i);
                }
            }

            element.SetTokenAttributes(ref token);
        }

        internal static void SetTokenAttributes<TNode>(
            this TNode element,
            ref StructHtmlToken token
        )
            where TNode : struct, IHtmlTreeConstructionNode<TNode>
        {
            ref readonly var attributes = ref StructHtmlToken.GetAttributesReference(ref token);
            element.SetAttributes(in attributes);
        }

        public static void AddFormatting<TNode>(this List<TNode> formatting, TNode element)
            where TNode : struct, IHtmlTreeConstructionNode<TNode>
        {
            var count = 0;

            for (var i = formatting.Count - 1; i >= 0; i--)
            {
                var format = formatting[i];

                if (format.IsNull)
                {
                    break;
                }

                if (format.NodeName.Is(element.NodeName) &&
                    format.NamespaceUri.Is(element.NamespaceUri) &&
                    format.AttributesSame(element) && ++count == 3)
                {
                    formatting.RemoveAt(i);
                    break;
                }
            }

            formatting.Add(element);
        }

        public static void ClearFormatting<TNode>(this List<TNode> formatting)
            where TNode : struct, IHtmlTreeConstructionNode<TNode>
        {
            while (formatting.Count != 0)
            {
                var index = formatting.Count - 1;
                var entry = formatting[index];
                formatting.RemoveAt(index);

                if (entry.IsNull)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddScopeMarker<TNode>(this List<TNode> formatting)
            where TNode : struct, IHtmlTreeConstructionNode<TNode>
        {
            formatting.Add(default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Element parent, HtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent.Owner, token.Data)
                : new Comment(parent.Owner, token.Data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Element parent, ref StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent.Owner, token.Data.ToString())
                : new Comment(parent.Owner, token.Data.ToString()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Document parent, HtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent, token.Data)
                : new Comment(parent, token.Data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Document parent, ref StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ProcessingInstruction.Create(parent, token.Data.ToString())
                : new Comment(parent, token.Data.ToString()));
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
