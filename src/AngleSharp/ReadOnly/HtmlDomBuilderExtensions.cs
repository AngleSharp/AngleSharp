namespace AngleSharp.Html.Parser
{
    using System;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Common;
    using ReadOnly;
    using Tokens.Struct;

    /// <summary>
    /// Extensions to be used exclusively by the DOM Builder.
    /// </summary>
    static class StructHtmlDomBuilderExtensions
    {
        public static void ClearFormatting<TElement>(this List<TElement> formatting)
            where TElement: class, IConstructableElement
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

        public static void AddFormatting<TElement>(this List<TElement> formatting, TElement element)
            where TElement: class, IConstructableElement
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddScopeMarker<TElement>(this List<TElement> formatting)
            where TElement: class, IConstructableElement
        {
            formatting.Add(null!);
        }

        public static void SetUniqueAttributes(this ReadOnlyElement element, ref StructHtmlToken token)
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

        public static HtmlTreeMode? SelectMode(this IConstructableElement element, Boolean isLast, Stack<HtmlTreeMode> templateModes)
        {
            if (element.Flags.HasFlag(NodeFlags.HtmlMember))
            {
                var tagName = element.LocalName;

                if (tagName.Is(TagNames.Select))
                {
                    return HtmlTreeMode.InSelect;
                }
                else if (TagNames._mAllTableCells.Contains(tagName))
                {
                    return isLast ? HtmlTreeMode.InBody : HtmlTreeMode.InCell;
                }
                else if (tagName.Is(TagNames.Tr))
                {
                    return HtmlTreeMode.InRow;
                }
                else if (TagNames._mAllTableSections.Contains(tagName))
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

        public static void SetUniqueAttributes<TElement>(this TElement element, ref StructHtmlToken token)
            where TElement: class, IConstructableElement
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
        public static void AddComment(this ReadOnlyElement parent, ref StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ReadOnlyProcessingInstruction.Create(parent.Owner, token.Data.String)
                : new ReadOnlyComment(parent.Owner, token.Data.String));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this ReadOnlyDocument parent, ref StructHtmlToken token)
        {
            parent.AddNode(token.IsProcessingInstruction
                ? ReadOnlyProcessingInstruction.Create(parent, token.Data.String)
                : new ReadOnlyComment(parent, token.Data.String));
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

        /// <summary>
        /// Setups a new SVG element with the attributes from the token.
        /// </summary>
        /// <param name="element">The element to setup.</param>
        /// <param name="tag">The tag token to use.</param>
        /// <returns>The finished element.</returns>
        public static ISvgElement Setup(this ISvgElement element, ref StructHtmlToken tag)
        {
            var count = tag.Attributes.Count;

            for (var i = 0; i < count; i++)
            {
                var attr = tag.Attributes[i];
                var name = attr.Name;
                var value = attr.Value;
                element.AdjustAttribute(name.AdjustToSvgAttribute(), value);
            }

            return element;
        }

        /// <summary>
        /// Setups a new math element with the attributes from the token.
        /// </summary>
        /// <param name="element">The element to setup.</param>
        /// <param name="tag">The tag token to use.</param>
        /// <returns>The finished element.</returns>
        public static IMathElement Setup(this IMathElement element, ref StructHtmlToken tag)
        {
            var count = tag.Attributes.Count;

            for (var i = 0; i < count; i++)
            {
                var attr = tag.Attributes[i];
                var name = attr.Name;
                var value = attr.Value;
                element.AdjustAttribute(name.AdjustToMathAttribute(), value);
            }

            return element;
        }

        /// <summary>
        /// Adds the attribute with the adjusted prefix, namespace and name.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public static void AdjustAttribute(this IConstructableElement element, StringOrMemory name, StringOrMemory value)
        {
            var ns = default(String);

            // TODO
            // if (IsXLinkAttribute(name))
            // {
            //     var newName = name.Substring(name.IndexOf(Symbols.Colon) + 1);
            //
            //     if (newName.IsXmlName() && newName.IsQualifiedName())
            //     {
            //         ns = NamespaceNames.XLinkUri;
            //         name = newName;
            //     }
            // }
            // else if (IsXmlAttribute(name))
            // {
            //     ns = NamespaceNames.XmlUri;
            // }
            // else if (IsXmlNamespaceAttribute(name))
            // {
            //     ns = NamespaceNames.XmlNsUri;
            // }

            if (ns is null)
            {
                element.SetOwnAttribute(name, value);
            }
            else
            {
                element.SetAttribute(ns, name, value);
            }
        }

    }
}
