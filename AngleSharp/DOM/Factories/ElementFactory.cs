namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    abstract class ElementFactory<T>
        where T : Element
    {
        protected readonly Dictionary<String, Func<T>> creators;

        /// <summary>
        /// Creates a new element factory.
        /// </summary>
        public ElementFactory()
	    {
            creators = new Dictionary<String, Func<T>>(StringComparer.OrdinalIgnoreCase);
	    }

        /// <summary>
        /// Creates the default element.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="document">The associated document.</param>
        /// <returns>A new default element.</returns>
        protected abstract T CreateDefault(String name, Document document);

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="name">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        protected T CreateSpecific(String name, Document document)
        {
            Func<T> creator;

            if (creators.TryGetValue(name, out creator))
            {
                var element = creator();
                element.Owner = document;
                return element;
            }

            return CreateDefault(name, document);
        }
    }
}
