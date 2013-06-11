using System;

namespace AngleSharp.DOM.Css
{
    struct Counter
    {
        #region Members

        String identifier;
        String listStyle;
        String separator;

        #endregion

        #region Properties

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public String ListStyle
        {
            get { return listStyle; }
            set { listStyle = value; }
        }

        public String Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        #endregion
    }
}
