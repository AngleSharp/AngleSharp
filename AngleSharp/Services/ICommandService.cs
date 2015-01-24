namespace AngleSharp.Services
{
    using AngleSharp.DOM;
    using System;

    interface ICommandService : IService
    {
        String CommandId { get; }

        Boolean Execute(IDocument document, Boolean showUserInterface, String value);

        Boolean IsEnabled(IDocument document);

        Boolean IsIndeterminate(IDocument document);

        Boolean IsExecuted(IDocument document);

        Boolean IsSupported(IDocument document);

        String GetValue(IDocument document);
    }
}
