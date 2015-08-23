namespace AngleSharp.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a node in the CSS parse tree.
    /// </summary>
    public abstract class CssNode
    {
        List<CssToken> _trivia;
        TextPosition _start;
        TextPosition _end;

        /// <summary>
        /// Gets or sets associated trivia, if any.
        /// </summary>
        internal List<CssToken> Trivia
        {
            get { return _trivia; }
            set { _trivia = value; }
        }

        /// <summary>
        /// Gets the start of the node.
        /// </summary>
        public TextPosition Start
        {
            get { return _start; }
            internal set { _start = value; }
        }

        /// <summary>
        /// Gets the end of the node.
        /// </summary>
        public TextPosition End
        {
            get { return _end; }
            internal set { _end = value; }
        }

        /// <summary>
        /// Gets the original source code, if any.
        /// </summary>
        /// <returns>The restored source code.</returns>
        public virtual String GetSource()
        {
            return String.Empty;
        }

        /// <summary>
        /// Gets the contained child nodes, if any.
        /// </summary>
        /// <returns>The iterator over the child nodes.</returns>
        public virtual IEnumerable<CssNode> GetChildren()
        {
            return Enumerable.Empty<CssNode>();
        }

        /// <summary>
        /// Decorates the provided string with trivia, if any.
        /// </summary>
        /// <param name="text">The text to decorate.</param>
        /// <returns>The decorated string.</returns>
        protected String Decorate(String text)
        {
            var position = Start;
            var index = 0;
            var length = _trivia != null ? _trivia.Count : 0;

            for (int i = 0; i < text.Length && index < length; i++)
            {
                if (_trivia[index].Position.Equals(position))
                    text = text.Insert(i, _trivia[index++].ToValue());

                position = position.After(text[i]);
            }

            while (index < length)
            {
                text += _trivia[index++].ToValue();
            }

            return text;
        }
    }
}
