namespace AngleSharp.Dom.Css
{
	/// <summary>
	/// An enumeration with all possible text last alignments.
	/// </summary>
	public enum TextAlignLast : byte
	{
		/// <summary>
		/// The affected line is aligned per the value of text-align, unless
		/// text-align is justify, in which case the effect is the same as 
		/// etting text-align-last to left.
		/// </summary>
		Auto,
		/// <summary>
		/// The same as left if direction is left-to-right and right if direction 
		/// is right-to-left.
		/// </summary>
		Start,
		/// <summary>
		/// The same as right if direction is left-to-right and left if direction 
		/// is right-to-left.
		/// </summary>
		End,
		/// <summary>
		/// The inline contents are aligned to the left edge of the line box.
		/// </summary>
		Left,
		/// <summary>
		/// The inline contents are aligned to the right edge of the line box.
		/// </summary>
		Right,
		/// <summary>
		/// The inline contents are centered within the line box.
		/// </summary>
		Center,
		/// <summary>
		/// The text is justified. Text should line up their left and right edges 
		/// to the left and right content edges of the paragraph.
		/// </summary>
		Justify
	}
}
