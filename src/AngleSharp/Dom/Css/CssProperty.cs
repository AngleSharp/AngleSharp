namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    sealed class CssProperty : CssNode, ICssProperty
    {
        #region Fields
        
        readonly String _name;
        CssValue _value;

        Boolean _important;

        #endregion

        #region ctor

        internal CssProperty(String name)
        {
            _name = name;
        }

        #endregion

        #region Properties
        
        public String Value
        {
            get { return _value != null ? _value.CssText : Keywords.Initial; }
        }

        public Boolean IsInherited
        {
            get { return _value != null && _value.CssText.Is(Keywords.Inherit); }
        }

        public Boolean IsInitial
        {
            get { return _value == null || _value == CssValue.Initial; }
        }

        public String Name
        {
            get { return _name; }
        }

        public Boolean IsImportant
        {
            get { return _important; }
            set { _important = value; }
        }

        public String CssText
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Internal Properties

        internal Boolean HasValue
        {
            get { return _value != null; }
        }

        #endregion

        #region Internal Methods

        internal Boolean TrySetValue(CssValue value)
        {
            _value = value ?? CssValue.Initial;
            return true;
        }

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Declaration(Name, Value, IsImportant));
        }

        #endregion
    }
}
