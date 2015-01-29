namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The Comment interface represents textual notations within markup;
    /// although it is generally not visually shown, such comments are
    /// available to be read in the source view.
    /// </summary>
    [DomName("Comment")]
    public interface IComment : ICharacterData
    {
    }
}