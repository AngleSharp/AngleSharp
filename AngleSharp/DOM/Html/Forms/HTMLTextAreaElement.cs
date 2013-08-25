using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a HTML textarea element.
    /// </summary>
    [DOM("HTMLTextAreaElement")]
    public sealed class HTMLTextAreaElement : HTMLTextFormControlElement
    {
        #region Constant

        /// <summary>
        /// The textarea tag.
        /// </summary>
        internal const String Tag = "textarea";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML textarea element.
        /// </summary>
        internal HTMLTextAreaElement()
        {
            _name = Tag;
            WillValidate = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of input control (texarea).
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return Tag; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        internal Boolean IsMutable
        {
            get { return !Disabled && !Readonly; }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            //TODO
        }

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected override void Check(ValidityState state)
        {
            //TODO
        }

        #endregion
    }
}
