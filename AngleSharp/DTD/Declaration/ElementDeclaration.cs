using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    /// <summary>
    /// Represents the element declaration.
    /// </summary>
    sealed class ElementDeclaration : Node
    {
        #region ctor

        internal ElementDeclaration()
        {
        }

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        /// <summary>
        /// Checks the element.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if everything is according to the definition, otherwise false.</returns>
        public Boolean Check(Element element)
        {
            var inspector = new NodeInspector(element);
            return Entry.Check(inspector) && inspector.IsCompleted;
        }

        #endregion
    }
}
