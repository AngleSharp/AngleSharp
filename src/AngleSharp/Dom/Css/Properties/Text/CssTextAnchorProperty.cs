namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	/// <summary>
	/// Information can be found on MDN:
	/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/text-anchor
	/// Gets the selected text-align-last.
	/// </summary>
	sealed class CssTextAnchorProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.TextAnchorConverter;

		#endregion

		#region ctor

		public CssTextAnchorProperty()
			: base(PropertyNames.TextAnchor)
		{
		}

		#endregion

		#region Properties

		internal override IValueConverter Converter
		{
			get
			{
				return StyleConverter;
			}
		}

		#endregion
	}
}
