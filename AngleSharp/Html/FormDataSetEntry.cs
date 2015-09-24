namespace AngleSharp.Html
{
    using System;
    using System.Text;

    /// <summary>
    /// Encapsulates the data contained in an entry.
    /// </summary>
    public abstract class FormDataSetEntry
    {
        #region Fields

        readonly String _name;
        readonly String _type;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new form data set entry.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="type">The type of the field.</param>
        public FormDataSetEntry(String name, String type)
        {
            _name = name;
            _type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the name has been given.
        /// </summary>
        public Boolean HasName
        {
            get { return _name != null; }
        }

        /// <summary>
        /// Gets the entry's name.
        /// </summary>
        public String Name
        {
            get { return _name ?? String.Empty; }
        }

        /// <summary>
        /// Gets the entry's type.
        /// </summary>
        public String Type
        {
            get { return _type ?? InputTypeNames.Text; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Accepts the provided visitor instance.
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        public abstract void Accept(IFormDataSetVisitor visitor);

        /// <summary>
        /// Checks if the provided boundary is already mentioned in the content.
        /// </summary>
        /// <param name="boundary">The string to check for.</param>
        /// <param name="encoding">The encoding to use for the string.</param>
        /// <returns>True if the boundary is matched, otherwise false.</returns>
        public abstract Boolean Contains(String boundary, Encoding encoding);

        #endregion
    }
}
