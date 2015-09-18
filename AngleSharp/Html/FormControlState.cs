namespace AngleSharp.Html
{
    using System;

    /// <summary>
    /// Class to store the state of a form control.
    /// </summary>
    sealed class FormControlState
    {
        #region Fields

        readonly String _name;
        readonly String _type;
        readonly String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new form control state instance.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="type">The type of the field.</param>
        /// <param name="value">The value of the field.</param>
        public FormControlState(String name, String type, String value)
	    {
            _name = name;
            _type = type;
            _value = value;
	    }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the field's value.
        /// </summary>
        public String Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the type of the field.
        /// </summary>
        public String Type
        {
            get { return _type; }
        }

        #endregion
    }
}
