namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// The collection of known CSS selector combinator symbols.
    /// </summary>
    static class CombinatorSymbols
    {
        /// <summary>
        /// The "=" attribute combinator.
        /// </summary>
        public static readonly String Exactly = "=";

        /// <summary>
        /// The "!=" attribute combinator.
        /// </summary>
        public static readonly String Unlike = "!=";

        /// <summary>
        /// The "~=" attribute combinator.
        /// </summary>
        public static readonly String InList = "~=";

        /// <summary>
        /// The "|=" attribute combinator.
        /// </summary>
        public static readonly String InToken = "|=";

        /// <summary>
        /// The "^=" attribute combinator.
        /// </summary>
        public static readonly String Begins = "^=";

        /// <summary>
        /// The "$=" attribute combinator.
        /// </summary>
        public static readonly String Ends = "$=";

        /// <summary>
        /// The "*=" attribute combinator.
        /// </summary>
        public static readonly String InText = "*=";

        /// <summary>
        /// The "||" combinator.
        /// </summary>
        public static readonly String Column = "||";

        /// <summary>
        /// The "|" combinator.
        /// </summary>
        public static readonly String Pipe = "|";

        /// <summary>
        /// The "+" combinator.
        /// </summary>
        public static readonly String Adjacent = "+";

        /// <summary>
        /// The " " combinator.
        /// </summary>
        public static readonly String Descendent = " ";

        /// <summary>
        /// The ">>>" combinator.
        /// </summary>
        public static readonly String Deep = ">>>";

        /// <summary>
        /// The ">" combinator.
        /// </summary>
        public static readonly String Child = ">";

        /// <summary>
        /// The "~" combinator.
        /// </summary>
        public static readonly String Sibling = "~";
    }
}
