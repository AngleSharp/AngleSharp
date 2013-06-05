using System;

namespace AngleSharp.DOM.Css
{
    struct Counter
    {
        #region Members

        string identifier;
        string listStyle;
        string separator;

        #endregion

        #region Properties

        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public string ListStyle
        {
            get { return listStyle; }
            set { listStyle = value; }
        }

        public string Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        #endregion
    }
}
