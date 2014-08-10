namespace AngleSharp.DOM.Svg
{
    using AngleSharp.DOM.Factories;
    using System;

    /// <summary>
    /// Provides string to SVGElement instance creation mappings.
    /// </summary>
    internal class SvgElementFactory : ElementFactory<SVGElement>
    {
        static readonly SvgElementFactory Instance = new SvgElementFactory();

        SvgElementFactory()
        {
            creators.Add(Tags.Svg, () => new SVGSVGElement());
            creators.Add(Tags.Circle, () => new SVGCircleElement());
            creators.Add(Tags.Desc, () => new SVGDescElement());
            creators.Add(Tags.ForeignObject, () => new SVGForeignObjectElement());
            creators.Add(Tags.Title, () => new SVGTitleElement());
        }

        protected override SVGElement CreateDefault(String name, Document document)
        {
            return new SVGElement(name) { Owner = document };
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public static SVGElement Create(String tagName, Document document)
        {
            return Instance.CreateSpecific(tagName, document);
        }
    }
}
