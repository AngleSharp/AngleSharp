namespace AngleSharp.ReadOnly;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Dom;
using Html.Parser;
using Html.Parser.Tokens.Struct;
using Text;

static class ReadOnlyStructHtmlDomBuilderExtensions
{
    public static void SetAttributes(this ReadOnlyElement element, StructAttributes attributes)
    {
        var container = element.Attributes;

        for (var i = 0; i < attributes.Count; i++)
        {
            var attribute = attributes[i];
            var item = new ReadOnlyAttr(attribute.Name, attribute.Value);
            // item.Container = container;
            container.FastAddItem(item);
        }
    }

    public static void AddAttribute(this ReadOnlyElement element, MemoryHtmlAttributeToken attribute)
    {
        var container = element.Attributes;
        var item = new ReadOnlyAttr(attribute.Name, attribute.Value);
        // item.Container = container;
        container.FastAddItem(item);
    }

    public static HtmlTreeMode? SelectMode(this ReadOnlyElement element, Boolean isLast,
        Stack<HtmlTreeMode> templateModes)
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

    public static Int32 GetCode(this HtmlParseError code)
    {
        return (Int32)code;
    }

    public static void SetUniqueAttributes(this ReadOnlyElement element, StructHtmlToken token)
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

    public static void AddFormatting(this List<ReadOnlyElement> formatting, ReadOnlyElement element)
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

    public static void ClearFormatting(this List<ReadOnlyElement> formatting)
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
    public static void AddScopeMarker(this List<ReadOnlyElement> formatting)
    {
        formatting.Add(null!);
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

    public static void SetUniqueAttributes(this ReadOnlyElement element, ref StructHtmlToken token)
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

    public static StringOrMemory SanatizeSvgTagName(this StringOrMemory localName)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Adds the attribute with the adjusted prefix, namespace and name.
    /// </summary>
    /// <param name="element">The element to host the attribute.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    public static void AdjustAttribute(this ReadOnlyElement element, StringOrMemory name, StringOrMemory value)
    {
        var ns = default(String);

        if (IsXLinkAttribute(name))
        {
            var newName = new StringOrMemory(name.Memory.Slice(name.Memory.Span.IndexOf(Symbols.Colon) + 1));

            if (newName.IsXmlName() && newName.IsQualifiedName())
            {
                ns = NamespaceNames.XLinkUri;
                name = newName;
            }
        }
        else if (IsXmlAttribute(name))
        {
            ns = NamespaceNames.XmlUri;
        }
        else if (IsXmlNamespaceAttribute(name))
        {
            ns = NamespaceNames.XmlNsUri;
        }

        element.SetAttribute(ns, name, value);

        // if (ns is null)
        // {
        //     element.SetOwnAttribute(name, value);
        // }
        // else
        // {
        //     element.SetAttribute(ns, name, value);
        // }
    }

    #region Helpers

    private static Boolean IsXmlNamespaceAttribute(StringOrMemory name) =>
        name.Length > 4 && (name.Is(NamespaceNames.XmlNsPrefix) || name.Memory.Span.SequenceEqual("xmlns:xlink"));

    private static Boolean IsXmlAttribute(StringOrMemory name) =>
        (name.Length > 7 && "xml:".EqualsSubset(name, 0, 4)) &&
        (TagNames.Base.EqualsSubset(name, 4, 4) || AttributeNames.Lang.EqualsSubset(name, 4, 4) ||
         AttributeNames.Space.EqualsSubset(name, 4, 5));

    private static Boolean IsXLinkAttribute(StringOrMemory name) =>
        (name.Length > 9 && "xlink:".EqualsSubset(name, 0, 6)) &&
        (AttributeNames.Actuate.EqualsSubset(name, 6, 7) || AttributeNames.Arcrole.EqualsSubset(name, 6, 7) ||
         AttributeNames.Href.EqualsSubset(name, 6, 4) || AttributeNames.Role.EqualsSubset(name, 6, 4) ||
         AttributeNames.Show.EqualsSubset(name, 6, 4) || AttributeNames.Type.EqualsSubset(name, 6, 4) ||
         AttributeNames.Title.EqualsSubset(name, 6, 5));

    private static Boolean EqualsSubset(this String a, StringOrMemory b, Int32 index, Int32 length) =>
        MemoryExtensions.CompareTo(a.AsSpan().Slice(0, length), b.Memory.Span.Slice(index, length), StringComparison.Ordinal) == 0;

    #endregion
}