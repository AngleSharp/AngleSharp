namespace AngleSharp.Html
{
    using System;

    /// <summary>
    /// Class to store the state of a form control.
    /// </summary>
    sealed class FormControlState
    {
        readonly String _name;
        readonly String _type;
        readonly String _value;

        internal FormControlState(String name, String type, String value)
	    {
            _name = name;
            _type = type;
            _value = value;
	    }

        public String Name
        {
            get { return _name; }
        }

        public String Value
        {
            get { return _value; }
        }

        public String Type
        {
            get { return _type; }
        }
    }
}
