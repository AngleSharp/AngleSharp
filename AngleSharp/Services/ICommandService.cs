namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a service to execute UI commands on a document.
    /// </summary>
    public interface ICommandService : IService
    {
        /// <summary>
        /// The id of the command.
        /// </summary>
        String CommandId { get; }

        /// <summary>
        /// Executes the command for the given document.
        /// </summary>
        /// <param name="document">The document to alter.</param>
        /// <param name="showUserInterface">Should the UI be shown?</param>
        /// <param name="value">The argument value.</param>
        /// <returns>A boolean if the command could be run.</returns>
        Boolean Execute(IDocument document, Boolean showUserInterface, String value);

        /// <summary>
        /// Checks if the command is currently enabled.
        /// </summary>
        /// <param name="document">The document to apply to.</param>
        /// <returns>A boolean if the command is enabled.</returns>
        Boolean IsEnabled(IDocument document);

        /// <summary>
        /// Checks if the command is currently neither enabled nor disabled.
        /// </summary>
        /// <param name="document">The document to apply to.</param>
        /// <returns>A boolean if the command is indeterminate.</returns>
        Boolean IsIndeterminate(IDocument document);

        /// <summary>
        /// Checks if the command has been run already.
        /// </summary>
        /// <param name="document">The document to apply to.</param>
        /// <returns>A boolean if the command has already been applied.</returns>
        Boolean IsExecuted(IDocument document);

        /// <summary>
        /// Checks if the command is currently supported at all.
        /// </summary>
        /// <param name="document">The document to apply to.</param>
        /// <returns>A boolean if the command is supported.</returns>
        Boolean IsSupported(IDocument document);

        /// <summary>
        /// Gets the value that would be changed at the moment.
        /// </summary>
        /// <param name="document">The document to apply to.</param>
        /// <returns>The value that would be used by the command.</returns>
        String GetValue(IDocument document);
    }
}
