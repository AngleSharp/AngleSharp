using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    /// <summary>
    /// Represents the element declaration.
    /// </summary>
    sealed class ElementDeclaration : Node
    {
        internal ElementDeclaration()
        {
        }

        /// <summary>
        /// Gets or sets the name of the element to define.
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the definition of the element.
        /// </summary>
        public ElementDeclarationEntry Entry
        {
            get;
            set;
        }

        /// <summary>
        /// Checks the element.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if everything is according to the definition, otherwise false.</returns>
        public Boolean Check(Element element)
        {
            return Entry.Check(element);
        }
    }
}
