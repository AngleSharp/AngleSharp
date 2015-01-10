namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Provides string to SVGElement instance creation mappings.
    /// </summary>
    internal class SvgElementFactory : ElementFactory<SvgElement>
    {
        static readonly SvgElementFactory Instance = new SvgElementFactory();

        SvgElementFactory()
        {
            creators.Add(Tags.Svg, document => new SvgSvgElement(document));
            creators.Add(Tags.Circle, document => new SvgCircleElement(document));
            creators.Add(Tags.Desc, document => new SvgDescElement(document));
            creators.Add(Tags.ForeignObject, document => new SvgForeignObjectElement(document));
            creators.Add(Tags.Title, document => new SvgTitleElement(document));
        }

        protected override SvgElement CreateDefault(String name, Document document)
        {
            return new SvgElement(document, name);
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public static SvgElement Create(String tagName, Document document)
        {
            return Instance.CreateSpecific(tagName, document);
        }
    }
}
