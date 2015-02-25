namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a simple selector (either a type selector,
    /// universal selector, attribute selector, class selector,
    /// id selector or pseudo-class).
    /// </summary>
    class SimpleSelector : ISelector
    {
        #region Fields

        static readonly SimpleSelector _all = new SimpleSelector();

        readonly Predicate<IElement> _matches;
        readonly Priority _specifity;
        readonly String _code;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a simple universal selector.
        /// </summary>
        public SimpleSelector()
        {
            _matches = _ => true;
            _code = "*";
            _specifity = Priority.Zero;
        }

        /// <summary>
        /// Creates a simple type selector.
        /// </summary>
        /// <param name="match">The type to match.</param>
        public SimpleSelector(String match)
        {
            _matches = _ => _.LocalName.Equals(match, StringComparison.OrdinalIgnoreCase);
            _specifity = Priority.OneTag;
            _code = match;
        }

        /// <summary>
        /// Creates a simple selector with the given predicate.
        /// </summary>
        /// <param name="matches">The predicate to use.</param>
        /// <param name="specifify">The specifify to use.</param>
        /// <param name="code">The CSS code of the selector.</param>
        public SimpleSelector(Predicate<IElement> matches, Priority specifify, String code)
        {
            _matches = matches;
            _specifity = specifify;
            _code = code;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a selector that matches all elements.
        /// </summary>
        public static ISelector All
        {
            get { return _all; }
        }

        /// <summary>
        /// Gets the specifity of the given selector.
        /// </summary>
        public Priority Specifity
        {
            get { return _specifity; }
        }

        /// <summary>
        /// Gets the string representation of the selector.
        /// </summary>
        public String Text
        {
            get { return _code; }
        }

        #endregion

        #region Static constructors

        /// <summary>
        /// Creates a new pseudo element :: selector.
        /// </summary>
        /// <param name="action">The action for the pseudo element selector.</param>
        /// <param name="pseudoElement">The pseudo element.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector PseudoElement(Predicate<IElement> action, String pseudoElement)
        {
            return new SimpleSelector(action, Priority.OneTag, "::" + pseudoElement);
        }

        /// <summary>
        /// Creates a new pseudo class : selector.
        /// </summary>
        /// <param name="action">The action for the pseudo class selector.</param>
        /// <param name="pseudoClass">The pseudo class.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector PseudoClass(Predicate<IElement> action, String pseudoClass)
        {
            return new SimpleSelector(action, Priority.OneClass, ":" + pseudoClass);
        }

        /// <summary>
        /// Gets a selector that matches all elements.
        /// </summary>
        /// <returns>The available universal selector.</returns>
        public static SimpleSelector Universal()
        {
            return _all;
        }

        /// <summary>
        /// Creates a new class selector.
        /// </summary>
        /// <param name="match">The class to match.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector Class(String match)
        {
            return new SimpleSelector(_ => _.ClassList.Contains(match), Priority.OneClass, "." + match);
        }

        /// <summary>
        /// Creates a new ID selector.
        /// </summary>
        /// <param name="match">The id to match.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector Id(String match)
        {
            return new SimpleSelector(_ => _.Id == match, Priority.OneId, "#" + match);
        }

        /// <summary>
        /// Creates a new attribute available selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrAvailable(String match, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            return new SimpleSelector(_ => _.HasAttribute(match), Priority.OneClass, String.Concat("[", front, "]"));
        }

        /// <summary>
        /// Creates a new attribute match selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrMatch(String match, String value, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            var code = String.Format("[{0}={1}]", front, GetValueAsString(value));
            return new SimpleSelector(_ => _.GetAttribute(match) == value, Priority.OneClass, code);
        }

        /// <summary>
        /// Creates a new attribute not-match selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="value">The value that the attribute should not have.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrNotMatch(String match, String value, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            var code = String.Format("[{0}!={1}]", front, GetValueAsString(value));
            return new SimpleSelector(_ => _.GetAttribute(match) != value, Priority.OneClass, code);
        }

        /// <summary>
        /// Creates a new attribute matches a list entry selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="value">The value (between spaces) of the attribute.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrList(String match, String value, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            var code = String.Format("[{0}~={1}]", front, GetValueAsString(value));

            if (String.IsNullOrEmpty(value))
                return new SimpleSelector(_ => false, Priority.OneClass, code);

            return new SimpleSelector(_ => (_.GetAttribute(match) ?? String.Empty).SplitSpaces().Contains(value), Priority.OneClass, code);
        }

        /// <summary>
        /// Creates a new attribute matches the begin selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="value">The begin of the value of the attribute.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrBegins(String match, String value, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            var code = String.Format("[{0}^={1}]", front, GetValueAsString(value));

            if (String.IsNullOrEmpty(value))
                return new SimpleSelector(_ => false, Priority.OneClass, code);

            return new SimpleSelector(_ => (_.GetAttribute(match) ?? String.Empty).StartsWith(value), Priority.OneClass, code);
        }

        /// <summary>
        /// Creates a new attribute matches the end selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="value">The end of the value of the attribute.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrEnds(String match, String value, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            var code = String.Format("[{0}$={1}]", front, GetValueAsString(value));

            if (String.IsNullOrEmpty(value))
                return new SimpleSelector(_ => false, Priority.OneClass, code);

            return new SimpleSelector(_ => (_.GetAttribute(match) ?? String.Empty).EndsWith(value), Priority.OneClass, code);
        }

        /// <summary>
        /// Creates a new attribute contains selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="value">The value that has to be contained in the value of the attribute.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrContains(String match, String value, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            var code = String.Format("[{0}*={1}]", front, GetValueAsString(value));

            if (String.IsNullOrEmpty(value))
                return new SimpleSelector(_ => false, Priority.OneClass, code);

            return new SimpleSelector(_ => (_.GetAttribute(match) ?? String.Empty).Contains(value), Priority.OneClass, code);
        }

        /// <summary>
        /// Creates a new attribute matches hyphen separated list selector.
        /// </summary>
        /// <param name="match">The attribute that has to be available.</param>
        /// <param name="value">The value that has to be a hyphen separated list entry of the attribute.</param>
        /// <param name="prefix">The optional namespace prefix to use.</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector AttrHyphen(String match, String value, String prefix = null)
        {
            var front = match;

            if (!String.IsNullOrEmpty(prefix))
            {
                front = String.Concat(prefix, "|", match);

                if (prefix != "*")
                    match = String.Concat(prefix, ":", match);
            }

            var code = String.Format("[{0}|={1}]", front, GetValueAsString(value));

            if (String.IsNullOrEmpty(value))
                return new SimpleSelector(_ => false, Priority.OneClass, code);

            return new SimpleSelector(_ => (_.GetAttribute(match) ?? String.Empty).HasHyphen(value), Priority.OneClass, code);
        }

        /// <summary>
        /// Creates a new type selector.
        /// </summary>
        /// <param name="match">The type to match (the tagname).</param>
        /// <returns>The new selector.</returns>
        public static SimpleSelector Type(String match)
        {
            return new SimpleSelector(match);
        }

        #endregion

        #region Helpers

        static String GetValueAsString(String value)
        {
            var containsSpace = false;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].IsSpaceCharacter())
                {
                    containsSpace = true;
                    break;
                }
            }

            if (containsSpace)
            {
                if (value.IndexOf(Symbols.SingleQuote) != -1)
                    return '"' + value + '"';

                return "'" + value + "'";
            }

            return value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given object is matched by this selector.
        /// </summary>
        /// <param name="element">The element to be matched.</param>
        /// <returns>True if the selector matches the given element, otherwise false.</returns>
        public Boolean Match(IElement element)
        {
            return _matches(element);
        }

        #endregion
    }
}
