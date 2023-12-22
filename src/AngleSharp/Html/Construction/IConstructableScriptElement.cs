namespace AngleSharp.Html.Construction;

using System;
using System.Threading;
using System.Threading.Tasks;

internal interface IConstructableScriptElement: IConstructableElement
{
    internal Task RunAsync(CancellationToken cancel);
    internal Boolean Prepare(IConstructableDocument document);
}