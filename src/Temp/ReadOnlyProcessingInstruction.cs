namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyCharacterData : ReadOnlyElement
{
    public StringOrMemory Content { get; }

    internal ReadOnlyCharacterData(ReadOnlyElement? owner, StringOrMemory name, NodeType type)
        : this(owner, name, type, StringOrMemory.Empty)
    {
    }

    internal ReadOnlyCharacterData(ReadOnlyElement? owner, StringOrMemory name, NodeType type, StringOrMemory content)
        : base(owner, name, type)
    {
        Content = content;
    }
}

internal class ReadOnlyProcessingInstruction : ReadOnlyCharacterData
{
    private ReadOnlyProcessingInstruction(ReadOnlyElement? owner, StringOrMemory name)
        : base(owner, name, NodeType.ProcessingInstruction)
    {
    }

    public static ReadOnlyElement Create(ReadOnlyElement? owner, StringOrMemory tokenData)
    {
        return new ReadOnlyProcessingInstruction(owner, tokenData);
    }
}

internal class ReadOnlyTextNode : ReadOnlyCharacterData
{
    public ReadOnlyTextNode(ReadOnlyElement? owner, StringOrMemory content)
        :  base(owner, "#text", NodeType.Text, content)
    {
    }
}

internal class ReadOnlyComment : ReadOnlyCharacterData
{
    public ReadOnlyComment(ReadOnlyElement? owner, StringOrMemory tokenData)
        : base(owner, "#comment", NodeType.Comment, tokenData)
    {
    }
}