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
            creators.Add(Tags.Svg, () => new SvgSvgElement());
            creators.Add(Tags.Circle, () => new SvgCircleElement());
            creators.Add(Tags.Desc, () => new SvgDescElement());
            creators.Add(Tags.ForeignObject, () => new SvgForeignObjectElement());
            creators.Add(Tags.Title, () => new SvgTitleElement());
        }

        protected override SvgElement CreateDefault(String name, Document document)
        {
            return new SvgElement(name) { Owner = document };
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
