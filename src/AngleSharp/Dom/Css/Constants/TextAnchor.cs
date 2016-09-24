namespace AngleSharp.Dom.Css
{
	/// <summary>
	/// An enumeration with all possible text anchors.
	/// </summary>
	public enum TextAnchor : byte
	{
		/// <summary>
		/// The rendered characters are aligned such that the start of the 
		/// text string is at the initial current text position
		/// </summary>
		Start,
		/// <summary>
		/// The rendered characters are aligned such that the middle of the 
		/// text string is at the current text position.
		/// </summary>
		Middle,
		/// <summary>
		/// The rendered characters are aligned such that the end of the 
		/// text string is at the initial current text position.
		/// </summary>
		End
	}
}
